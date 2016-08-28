using System;
using System.Collections.Generic;
using System.Configuration;

namespace Scheduler.API.IntegrationTests
{
    public abstract class StaticCacheBase
    {
        public static TSettingType GetApplicationSetting<TSettingType>(string settingKey)
        {
            object value = ConfigurationManager.AppSettings[settingKey];
            if (value == null)
            {
                throw new KeyNotFoundException(string.Format("Key {0} was not found in the configuration file.", settingKey));
            }

            TSettingType castedValue = (TSettingType)value;

            if (castedValue == null)
            {
                throw new InvalidOperationException(string.Format("Application setting with key {0} is not of type {1}", settingKey, typeof(TSettingType)));
            }

            return castedValue;
        }
    }
}
