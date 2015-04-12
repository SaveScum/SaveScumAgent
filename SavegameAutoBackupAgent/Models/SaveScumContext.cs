using System.Configuration;
using System.Data.Entity;

namespace SaveScumAgent.Models
{
    public class SaveScumContext : DbContext
    {
        public SaveScumContext() : this("name=SaveScumContext")
            //this(Properties.Settings.Default.SaveScumContext.Replace("{APPDATA}", GetFolderPath(SpecialFolder.ApplicationData)))
        {
        }

        public SaveScumContext(string sConnectionString) : base(sConnectionString)
        {
            var connection = ConfigurationManager.ConnectionStrings;
            Database.SetInitializer(new SaveScumDbInitializer());
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<ArchiveEntry> ArchiveEntries { get; set; }
        public DbSet<GameSettings> GameSettings { get; set; }
        public DbSet<DefaultSettings> DefaultGameSettings { get; set; }
    }
}