using System.Data.Entity;
using Microsoft.SqlServer.Server;
using SaveScumAgent.Archiver.Formats;

namespace SaveScumAgent.Models
{
    public class SaveScumDbInitializer : DropCreateDatabaseAlways<SaveScumContext>
    {
        protected override void Seed(SaveScumContext context)
        {
            var defaultSettings = new DefaultSettings()
            {
                //Format = ArchiveFormat.Zip,
                SaveLocation = Properties.Settings.Default.SaveScumAppArchives
            };

            context.GameSettings.Add(defaultSettings);
            //context.SaveChanges();
            base.Seed(context);
        }
    }
}
