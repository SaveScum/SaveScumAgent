using System;
using System.IO;
using System.Reflection;

namespace SaveScumAgent
{
    internal class SaveScumInitializer
    {
        private const string AppConfigFileTemplate = "{0}\\config.xml";
        private static string _appConfigFilename;

        public static void SetupDataDirectory()
        {
            var dataDir = EnsureDataDirectoryExists();
            EnsureConfigFileExists(dataDir);
            AppConfig.Change(_appConfigFilename);
        }

        private static string EnsureDataDirectoryExists()
        {
            var dataDir = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            if (!Directory.Exists(dataDir))
                Directory.CreateDirectory(dataDir);

            return dataDir;
        }

        private static void EnsureConfigFileExists(string dataDir)
        {
            if (_appConfigFilename == null)
                _appConfigFilename = string.Format(AppConfigFileTemplate, dataDir);

            if (!File.Exists(_appConfigFilename))
                CreateAppConfig(_appConfigFilename);
        }

        private static void CreateAppConfig(string configFile)
        {
            var assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "SaveScumAgent.App.config";
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var sr = new StreamReader(stream))
            {
                var query = sr.ReadToEnd();
                File.WriteAllText(configFile, query);
            }
        }
    }
}