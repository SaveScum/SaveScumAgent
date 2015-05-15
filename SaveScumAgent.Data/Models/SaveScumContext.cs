using System.Data.Entity;
using Data.Migrations;

namespace Data.Models
{
    public class SaveScumContext : DbContext
    {
        public SaveScumContext() : this("SaveScumContext")
        {
        }

        public SaveScumContext(string sConnectionString) : base(sConnectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SaveScumContext, Configuration>());
        }

        //public SaveScumContext(int something) : base()

        public DbSet<Game> Games { get; set; }
        public DbSet<ArchiveEntry> ArchiveEntries { get; set; }
        public DbSet<GameSettings> GameSettings { get; set; }
        public DbSet<GlobalSettings> GlobalSettings { get; set; }
    }
}