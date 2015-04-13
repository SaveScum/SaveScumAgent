using System.Data.Entity;
using SaveScumAgent.Archiver.Formats;

namespace Data.Models
{
    public class SaveScumDbInitializer : DropCreateDatabaseAlways<SaveScumContext>
    {
        //MigrateDatabaseToLatestVersion<SaveScumContext, Configuration>();
    }
}
