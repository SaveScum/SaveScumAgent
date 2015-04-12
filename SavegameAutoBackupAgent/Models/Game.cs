using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaveScumAgent.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }


        public int? GameSettingsId { get; set; }

        //[ForeignKey("GameSettingsId")]
        //public virtual GameSettings Settings { get; set; }

        public virtual ICollection<ArchiveEntry> ArchiveEntries { get; set; }

        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        //TODO: Add a virtual member that will create a game-specific dictionary for path formatting

    }
}
