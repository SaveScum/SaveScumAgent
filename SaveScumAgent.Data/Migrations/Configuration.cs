using Data.Models;
using SaveScumAgent.Archiver.Formats;

namespace Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

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
                    ArchivesLocation = "{ApplicationData}\\SaveScum\\Archives\\{GameTitle}"
                });

            var g = context.GlobalSettings.Any() ? context.GlobalSettings.First() : new GlobalSettings();

            context.GlobalSettings.AddOrUpdate(x => x.IsGlobal, g);
        }
    }
}
