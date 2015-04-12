using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using SaveScumAgent.Archiver.Formats;
using SaveScumAgent.UtilityClasses;

namespace SaveScumAgent.Models
{
    public class GameSettings
    {
        [Key]
        public int Id { get; set; }
        public int FormatId { get; set; }

        //[NotMapped]
        //public ArchiveFormat Format
        //{
        //    get { return (ArchiveFormat) FormatId; }
        //    set { FormatId = (int) value; }
        //}

        //public int? GameId { get; set; }

        //[ForeignKey("GameId")]
        //public virtual Game Game { get; set; }

        public string  SaveLocation { get; set; }

        [NotMapped]
        public string SaveLocationPathString => new PathString(SaveLocation);

        [NotMapped]
        public string FormattedSaveLocationPathString => SaveLocation.FormatWith(SpecialFolderHelper.PathsDictionary);

    }
}
