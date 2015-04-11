using System.IO;
using SaveScumAgent.Properties;
using SaveScumAgent.UtilityClasses;

namespace SaveScumAgent
{
    internal class SaveScumInitializer
    {
        public static void SetupDataDirectory()
        {
            EnsureDataDirectoryExists();
            CreateAppConfig();
        }

        private static void EnsureDataDirectoryExists()
        {
            var dataDir = Settings.Default.SaveScumAppDataDirectory.FormatWith(SpecialFolderHelper.PathsDictionary);
            if (!Directory.Exists(dataDir))
                Directory.CreateDirectory(dataDir);
        }

        private static void CreateAppConfig()
        {
        }
    }
}