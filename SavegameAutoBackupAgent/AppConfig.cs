using System;
using System.Configuration;
using System.Linq;
using System.Reflection;

namespace SaveScumAgent
{
    public abstract class AppConfig : IDisposable
    {
        public abstract void Dispose();

        public static AppConfig Change(string path)
        {
            return new ChangeAppConfig(path);
        }

        private class ChangeAppConfig : AppConfig
        {
            private readonly string _oldConfig =
                AppDomain.CurrentDomain.GetData("APP_CONFIG_FILE").ToString();

            private bool _disposedValue;

            public ChangeAppConfig(string path)
            {
                AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", path);
                ResetConfigMechanism();
            }

            public override void Dispose()
            {
                if (!_disposedValue)
                {
                    AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", _oldConfig);
                    ResetConfigMechanism();


                    _disposedValue = true;
                }
                GC.SuppressFinalize(this);
            }

            private static void ResetConfigMechanism()
            {
                var fieldInfo = typeof (ConfigurationManager)
                    .GetField("s_initState", BindingFlags.NonPublic |
                                             BindingFlags.Static);
                fieldInfo?.SetValue(null, 0);

                var field = typeof (ConfigurationManager)
                    .GetField("s_configSystem", BindingFlags.NonPublic |
                                                BindingFlags.Static);
                field?.SetValue(null, null);

                var info = typeof (ConfigurationManager)
                    .Assembly.GetTypes().First(x => x.FullName ==
                                                    "System.Configuration.ClientConfigPaths")
                    .GetField("s_current", BindingFlags.NonPublic |
                                           BindingFlags.Static);
                info?.SetValue(null, null);
            }
        }
    }
}