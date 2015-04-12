using System;
using System.IO;
using System.Reflection;
using SaveScumAgent.Properties;
using SaveScumAgent.UtilityClasses;

namespace SaveScumAgent
{
    internal class SaveScumInitializer
    {

        private const string AppConfigFileTemplate = "{0}\\config.xml";
        private static string _appConfigFilename;

        public static void SetupDataDirectory()
        {
            var dataDir = EnsureDataDirectoryExists();

            if (_appConfigFilename == null)
                _appConfigFilename = String.Format(AppConfigFileTemplate, dataDir);
            EnsureConfigFileExists(_appConfigFilename);
            AppConfig.Change(_appConfigFilename);
        }

        private static string EnsureDataDirectoryExists()
        {
            var dataDir = Settings.Default.SaveScumAppDataDirectory.FormatWith(SpecialFolderHelper.PathsDictionary);
            if (!Directory.Exists(dataDir))
                Directory.CreateDirectory(dataDir);

            return dataDir;
        }


        private static void EnsureConfigFileExists(string configFile)
        {
            if (!File.Exists(configFile))
                CreateAppConfig(configFile);

        }

        private static void CreateAppConfig(string configFile)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "SaveScumAgent.App.config";
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var sr = new StreamReader(stream))
            {
                var query = sr.ReadToEnd();
                File.WriteAllText(configFile, query);
            }
        }
    }
}