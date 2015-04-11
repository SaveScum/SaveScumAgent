using System.Data.Entity;
using SaveScumAgent.Archiver.Formats;

namespace SaveScumAgent.Models
{
    public class SaveScumDbInitializer : DropCreateDatabaseAlways<SaveScumContext>
    {
        protected override void Seed(SaveScumContext context)
        {
            var defaultSettings = new GameSettings
            {
                Default = true,
                Format = ArchiveFormat.Zip,
                SaveLocation = "{APPDATA}\\SaveScum\\Archives"
            };

            context.GameSettings.Add(defaultSettings);
            //context.SaveChanges();
            base.Seed(context);
        }
    }
}
