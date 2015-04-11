using System.Data.Entity;
using SaveScumAgent.Archiver.Formats;

namespace SaveScumAgent.Models
{
    class SaveScumDbInitializer : DropCreateDatabaseAlways<SaveScumContext>
    {
        protected override void Seed(SaveScumContext context)
        {
            var defaultSettings = new GameSettings
            {
                Default = true,
                Format = ArchiveFormat.Zip,
                SavePathString = "{APPDATA}\\SaveScum\\Archives"
            };

            context.GameSettings.Add(defaultSettings);
            base.Seed(context);
        }
    }
}
