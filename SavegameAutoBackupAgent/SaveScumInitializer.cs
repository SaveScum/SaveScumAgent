using System;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Diagnostics;
using System.Environment;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveScumAgent.UtilityClasses;

namespace SaveScumAgent
{
    class SaveScumInitializer
    {
        public static void SetupDataDirectory()
        {
            EnsureDataDirectoryExists();
            CreateAppConfig();

        }

        private static void EnsureDataDirectoryExists()
        {
            var dataDir = Properties.Settings.Default.SaveScumAppDataDirectory.FormatWith(SpecialFolderHelper.PathsDictionary);
            if (!Directory.Exists(dataDir))
                Directory.CreateDirectory(dataDir);
        }

        private static void CreateAppConfig()
        {
            
        }
    }
}
