using System.Data.Entity;

namespace Data.Models
{
    public class SaveScumContext : DbContext
    {
        public SaveScumContext() : this("name=SaveScumContext")
        {
        }

        public SaveScumContext(string sConnectionString) : base(sConnectionString)
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<SaveScumContext, Configuration>());
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<ArchiveEntry> ArchiveEntries { get; set; }
        public DbSet<GameSettings> GameSettings { get; set; }
        public DbSet<DefaultSettings> DefaultSettings { get; set; }
        public DbSet<GlobalSettings> GlobalSettings { get; set; }

    }
}