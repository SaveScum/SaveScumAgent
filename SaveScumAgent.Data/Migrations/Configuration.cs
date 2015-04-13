using System.Data.Entity.Migrations;
using System.Linq;
using Data.Models;
using SaveScumAgent.Archiver.Formats;

namespace Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<SaveScumContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SaveScumContext context)
        {
            context.DefaultSettings.AddOrUpdate(x => x.IsDefault,
                new DefaultSettings
                {
                    ArchiveTriggerDelay = 5000,
                    Format = ArchiveFormat.Zip,
                    SaveLocation = "{APPDATA}\\SaveScum\\{GAMETITLE}"
                });

            var g = context.GlobalSettings.Any() ? context.GlobalSettings.First() : new GlobalSettings();

            context.GlobalSettings.AddOrUpdate(x => x.IsGlobal, new GlobalSettings
            {
                ArchiveTriggerDelay = g.ArchiveTriggerDelay,
                Format = g.Format,
                SaveLocation = g.SaveLocation
            });
        }
    }
}