using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SaveScumAgent.Archiver.Formats;

namespace SaveScumAgent.Data.Models
{
    public class GameSettings
    {
        public ArchiveFormat Format { get; set; }

        public virtual Game Game { get; set; }
    }
}
