using System.Data.Entity;

namespace SaveScumAgent
{
    public class SaveScumContext : DbContext
    {
        public SaveScumContext() : base("name=SaveScumContext")
        {
            Database.SetInitializer();
        }

        public DbSet<Game>  Games { get; set; }
        public DbSet<ArchiveEntry> ArchiveEntries { get; set; }
        public DbSet<GameSettings> GameSettings { get; set; }
        
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}