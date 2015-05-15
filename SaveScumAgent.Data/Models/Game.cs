using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}