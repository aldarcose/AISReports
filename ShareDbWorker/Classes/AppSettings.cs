using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SharedDbWorker
{
    public static class AppSettings
    {
        public static object Get(string key)
        {
            object result = System.Configuration.ConfigurationManager.AppSettings[key];
            if (result == null)
            {
                var appConfig = ConfigurationManager.OpenExeConfiguration(Assembly.GetExecutingAssembly().Location);
                if (appConfig.AppSettings.Settings.Count > 0)
                {
                    result = appConfig.AppSettings.Settings[key].Value;
                }
            }
            return result;
        }

        public static void Set(string key)
        {
            throw new NotImplementedException();
        }
    }
}
