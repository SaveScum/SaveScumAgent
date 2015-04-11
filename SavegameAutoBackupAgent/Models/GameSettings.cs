using SaveScumAgent.Archiver.Formats;
using SaveScumAgent.UtilityClasses;

namespace SaveScumAgent.Models
{
    public class GameSettings
    {
        public ArchiveFormat Format { get; set; }

        public virtual Game Game { get; set; }

        public PathString  SavePathString { get; set; }

        public bool Default { get; set; } = false;
    }
}
