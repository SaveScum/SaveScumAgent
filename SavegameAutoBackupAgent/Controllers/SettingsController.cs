using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using SaveScumAgent.Archiver.Formats;

namespace SaveScumAgent.Controllers
{
    internal class SettingsController
    {
        private static GameSettings _fallbackSettings;

        private static GameSettings FallbackSettings
        {
            get
            {
                using (var db = new SaveScumContext())
                {
                    _fallbackSettings = db.GlobalSettings.First();
                }
                return _fallbackSettings;
            }
        }

        public ArchiveFormat? Format
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string ArchivesLocation
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string SaveDirectoryLocation
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public int? ArchiveTriggerDelay
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}