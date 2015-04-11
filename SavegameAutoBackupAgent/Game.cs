using System.Collections.Generic;

namespace SaveScumAgent
{
    public class Game
    {
        public int ID { get; set; }
        public virtual GameSettings Settings { get; set; }
        public virtual ICollection<ArchiveEntry> ArchiveEntries { get; set; }
        public string Title { get; set; }

    }
}
