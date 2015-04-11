using System;
using System.Data.Entity;
using System.Environment;
using System.Linq;
using SaveScumAgent.UtilityClasses;
using System.Configuration;

namespace SaveScumAgent.Models
{
    public class SaveScumContext : DbContext
    {

        public SaveScumContext() : this(Properties.Settings.Default.SaveScumContext.Replace("{APPDATA}", GetFolderPath(SpecialFolder.ApplicationData)))
        {
            
        }

        public SaveScumContext(string sConnectionString) : base(sConnectionString)
        {
            Database.SetInitializer(new SaveScumDbInitializer());
        }

        public DbSet<Game>  Games { get; set; }
        public DbSet<ArchiveEntry> ArchiveEntries { get; set; }
        public DbSet<GameSettings> GameSettings { get; set; }

        public GameSettings DefaultGameSettings {
            get { return GameSettings.First(x => x.Default); }
    }


        
    }

    //public class MyEntity
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //}
}