﻿using DotNetCore.CAP;
using EasyCaching.Core;
using IoTSharp.Data;
using IoTSharp.Dtos;
using IoTSharp.Extensions;
using IoTSharp.FlowRuleEngine;
using IoTSharp.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTSharp.Handlers
{
    public interface IEventBusHandler
    {
        public void StoreAttributeData(PlayloadData msg);

        public void StoreTelemetryData(PlayloadData msg);
    }

    public class EventBusHandler : IEventBusHandler, ICapSubscribe
    {
        private readonly AppSettings _appSettings;
        private readonly ILogger _logger;
        private readonly IServiceScopeFactory _scopeFactor;
        private readonly IStorage _storage;
        private readonly FlowRuleProcessor _flowRuleProcessor;
        private readonly IEasyCachingProvider _caching;

        public EventBusHandler(ILogger<EventBusHandler> logger, IServiceScopeFactory scopeFactor
           , IOptions<AppSettings> options, IStorage storage, FlowRuleProcessor flowRuleProcessor, IEasyCachingProviderFactory factory
            )
        {
            string _hc_Caching = $"{nameof(CachingUseIn)}-{Enum.GetName(options.Value.CachingUseIn)}";
            _appSettings = options.Value;
            _logger = logger;
            _scopeFactor = scopeFactor;
            _storage = storage;
            _flowRuleProcessor = flowRuleProcessor;
            _caching = factory.GetCachingProvider(_hc_Caching);
        }

        [CapSubscribe("iotsharp.services.datastream.attributedata")]
        public async void StoreAttributeData(PlayloadData msg)
        {
            try
            {
                using (var _scope = _scopeFactor.CreateScope())
                {
                    using (var _dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        var device = _dbContext.Device.FirstOrDefault(d => d.Id == msg.DeviceId);
                        if (device != null)
                        {
                            var mb = msg.MsgBody;
                            Dictionary<string, object> dc = new Dictionary<string, object>();
                            mb.ToList().ForEach(kp =>
                            {
                                if (kp.Value?.GetType() == typeof(System.Text.Json.JsonElement))
                                {
                                    var je = (System.Text.Json.JsonElement)kp.Value;
                                    switch (je.ValueKind)
                                    {
                                        case System.Text.Json.JsonValueKind.Undefined:
                                        case System.Text.Json.JsonValueKind.Object:
                                        case System.Text.Json.JsonValueKind.Array:
                                            dc.Add(kp.Key, je.GetRawText());
                                            break;

                                        case System.Text.Json.JsonValueKind.String:
                                            dc.Add(kp.Key, je.GetString());
                                            break;

                                        case System.Text.Json.JsonValueKind.Number:
                                            dc.Add(kp.Key, je.GetDouble());
                                            break;

                                        case System.Text.Json.JsonValueKind.True:
                                        case System.Text.Json.JsonValueKind.False:
                                            dc.Add(kp.Key, je.GetBoolean());
                                            break;

                                        case System.Text.Json.JsonValueKind.Null:
                                            break;

                                        default:
                                            break;
                                    }
                                }
                                else if (kp.Value != null)
                                {
                                    dc.Add(kp.Key, kp.Value);
                                }

                            });
                            var result2 = await _dbContext.SaveAsync<AttributeLatest>(dc, device.Id, msg.DataSide);
                            result2.exceptions?.ToList().ForEach(ex =>
                            {
                                _logger.LogError($"{ex.Key} {ex.Value} {Newtonsoft.Json.JsonConvert.SerializeObject(msg.MsgBody[ex.Key])}");
                            });
                            _logger.LogInformation($"更新{device.Name}({device.Id})属性数据结果{result2.ret}");
                            ExpandoObject obj = new ExpandoObject();
                            dc.ToList().ForEach(kv =>
                            {
                                obj.TryAdd(kv.Key, kv.Value);
                            });
                            await RunRules(msg.DeviceId, obj, MountType.Attribute);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "StoreAttributeData:"+ex.Message);
            }

      
        }

        [CapSubscribe("iotsharp.services.datastream.alarm")]
        public async void OccurredAlarm(CreateAlarmDto alarmDto)
        {
            try
            {
                using (var _scope = _scopeFactor.CreateScope())
                {
                    using (var _dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        var alm = await _dbContext.OccurredAlarm(alarmDto);
                        if (alm.Code ==(int) ApiCode.Success)
                        {
                            alarmDto.warnDataId = alm.Data.Id;
                            alarmDto.CreateDateTime = alm.Data.AckDateTime;
                            if (alm.Data.Propagate)
                            {
                                await RunRules(alm.Data.OriginatorId, alarmDto, MountType.Alarm);
                            }
                        }
                        else
                        {
                            //如果设备通过网关创建， 当警告先来， 设备创建再后会出现此问题。
                            _logger.LogWarning( $"处理{alarmDto.OriginatorName} 的告警{alarmDto.AlarmType} 错误:{alm.Code}-{alm.Msg}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"处理{alarmDto.OriginatorName} 的告警{alarmDto.AlarmType} 时遇到异常:{ex.Message}");

            }
        }


        [CapSubscribe("iotsharp.services.datastream.devicestatus")]
        public void DeviceStatus( PlayloadData status)
        {
            try
            {
                using (var _scope = _scopeFactor.CreateScope())
                {
                    using (var _dbContext = _scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        var dev = _dbContext.Device.FirstOrDefault(d=>d.Id==status.DeviceId);
                        if (dev != null)
                        {
                            if (dev.Online == true && status.DeviceStatus != Data.DeviceStatus.Good)
                            {
                                dev.Online = false;
                                dev.LastActive = DateTime.Now;
                                Task.Run(() => RunRules(dev.Id, status, MountType.Offline));
                                //真正离线
                            }
                            else if (dev.Online == false && status.DeviceStatus== Data.DeviceStatus.Good)
                            {
                                dev.Online = true;
                                dev.LastActive = DateTime.Now;
                                Task.Run(() => RunRules(dev.Id, status, MountType.Online));
                                //真正掉线
                       
                            }
                            _dbContext.SaveChanges();
                        }
                        else
                        {
                            _logger.LogWarning( $"未找到设备{status.DeviceId} ，因此无法处理设备状态");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"处理{status.DeviceId} 的状态{status.DeviceStatus} 时遇到异常:{ex.Message}");
            
            }
        }




        [CapSubscribe("iotsharp.services.datastream.telemetrydata")]
        public async void StoreTelemetryData(PlayloadData msg)
        {
            var result = await _storage.StoreTelemetryAsync(msg);
            var data = from t in result.telemetries
                     select new TelemetryDataDto() { DateTime = t.DateTime, DataType=t.Type, KeyName = t.KeyName, Value = t.ToObject() };
            var array = data.ToList();
            ExpandoObject exps = new();
            array.ForEach(td =>
            {
                exps.TryAdd(td.KeyName, td.Value);
            });
            await RunRules(msg.DeviceId, (dynamic)exps, MountType.Telemetry);
            await RunRules(msg.DeviceId, array, MountType.TelemetryArray);
        }

        private async Task RunRules(Guid devid, object obj, MountType mountType)
        {
            try
            {
                var rules = await _caching.GetAsync($"ruleid_{devid}_{Enum.GetName(mountType)}", async () =>
                {
                    using (var scope = _scopeFactor.CreateScope())
                    using (var _dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                    {
                        var guids = await _dbContext.GerDeviceRulesIdList(devid, mountType);
                        return guids;
                    }
                }, TimeSpan.FromSeconds(_appSettings.RuleCachingExpiration));
                if (rules.HasValue)
                {
                    rules.Value.ToList().ForEach(async g =>
                    {
                        try
                        {
                            await _flowRuleProcessor.RunFlowRules(g, obj, devid, EventType.Normal, null);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex,$"为设备{devid}执行规则链{g}时遇到错误{ex.Message}");
                        }
                    });
                }
                else
                {
                    _logger.LogDebug($"{devid}的数据无相关规则链处理。");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError ( ex,$"{devid}处理规则链时遇到异常:{ex.Message}");

            }
        }
    }
}