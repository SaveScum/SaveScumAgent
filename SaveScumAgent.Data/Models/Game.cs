using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Data.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }


        public int? GameSettingsId { get; set; }

        public virtual ICollection<ArchiveEntry> ArchiveEntries { get; set; }

        [Required]
        [MinLength(3)]
        [StringLength(50)]
        public string Title { get; set; }

        [MinLength(3)]
        [StringLength(50)]
        public string FilesafeTitle { get; set; }

        //TODO: Add a virtual member that will create a game-specific dictionary for path formatting

    }
}
