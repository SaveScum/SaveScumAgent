using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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

        /// <summary>
        /// Popularity score is an exponential decay value.
        /// x = age in days
        /// e = Euler's number
        /// score = e ^ (-x/7)
        /// </summary>
        [NotMapped]
        public double PopularityScore
        {
            get
            {
                var age = DateTime.UtcNow.Subtract(CreatedAt);
                return Math.Pow(Math.E, ((-1 * age.TotalDays) / 7));                
            }
        }        
    }
}