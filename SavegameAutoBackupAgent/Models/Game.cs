using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SaveScumAgent.Models
{
    public class Game
    {
        public virtual GameSettings Settings { get; set; }
        public virtual ICollection<ArchiveEntry> ArchiveEntries { get; set; }

        [Required]
        [MinLength(3)]
        public string Title { get; set; }

    }
}
