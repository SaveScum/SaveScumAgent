using System.Data.Entity.Migrations;
using System.Linq;
using Data.Models;

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
            var g = context.GlobalSettings.Any() ? context.GlobalSettings.First() : new GlobalSettings();
            context.GlobalSettings.AddOrUpdate(x => x.IsGlobal, g);
        }
    }
}