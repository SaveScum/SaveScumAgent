using System;
using System.ComponentModel.DataAnnotations;
using SaveScumAgent.Archiver.Formats;

namespace Data.Models
{
    public class ArchiveEntry
    {
        public ArchiveEntry() : this(DateTime.UtcNow)
        {
        }

        public ArchiveEntry(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public string Identifier { get; set; }

        [Required]
        public ArchiveFormat Format { get; set; }

        public virtual Game Game { get; set; }
    }
}