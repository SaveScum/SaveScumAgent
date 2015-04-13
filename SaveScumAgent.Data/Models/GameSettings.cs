using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SaveScumAgent.Archiver.Formats;
using SaveScumAgent.UtilityClasses;
//using SaveScumAgent.Archiver.Formats;

namespace Data.Models
{
    public class GameSettings
    {
        [Key]
        public int Id { get; set; }

        public ArchiveFormat? Format { get; set; }

        public string  SaveLocation { get; set; } 

        public int? ArchiveTriggerDelay { get; set; }

        [NotMapped]
        public string SaveLocationPathString => new PathString(SaveLocation);

        [NotMapped]
        public string FormattedSaveLocationPathString => new PathString(SaveLocation.FormatWith(SpecialFolderHelper.PathsDictionary));

    }
}
