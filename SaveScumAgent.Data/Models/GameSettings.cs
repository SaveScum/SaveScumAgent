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

        [ForeignKey("Id")]
        public virtual Game Game { get; set; }

        [MaxLength(260)]
        public string ArchivesLocation { get; set; }


        [MaxLength(260)]
        public string SaveDirectoryLocation { get; set; }

        public int? ArchiveTriggerDelay { get; set; }

    }
}
