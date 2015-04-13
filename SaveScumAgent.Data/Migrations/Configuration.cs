using Data.Models;
using SaveScumAgent.Archiver.Formats;

namespace Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.Models.SaveScumContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Data.Models.SaveScumContext context)
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
