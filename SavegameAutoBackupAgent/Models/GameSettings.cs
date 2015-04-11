using System.ComponentModel.DataAnnotations.Schema;
using SaveScumAgent.Archiver.Formats;
using SaveScumAgent.UtilityClasses;

namespace SaveScumAgent.Models
{
    public class GameSettings
    {
        public ArchiveFormat Format { get; set; }

        public virtual Game Game { get; set; }

        public string  SaveLocation { get; set; }

        [NotMapped]
        public string SaveLocationPathString => new PathString(SaveLocation);

        public bool Default { get; set; } = false;
    }
}
