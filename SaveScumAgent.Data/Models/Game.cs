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

        [ForeignKey("Id")]
        [Required]
        public virtual GameSettings Settings { get; set; }
        
        public virtual ICollection<ArchiveEntry> ArchiveEntries { get; set; }

        [Required]
        [MinLength(3)]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string FilesafeTitle { get; set; }

        [MaxLength(260)]
        public string SaveDirectoryLocation { get; set; }

        //TODO: Add a virtual member that will create a game-specific dictionary for path formatting

    }
}
