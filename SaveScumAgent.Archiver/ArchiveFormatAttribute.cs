using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveScumAgent.Archiver
{
    [AttributeUsage(AttributeTargets.Field)]
    class ArchiveFormatAttribute : Attribute
    {
        public Type FormatType { get; set; }

        public ArchiveFormatAttribute(Type formatType)
        {
            if (formatType is IArchiver)
            {
                FormatType = formatType;
            }
            else
            {
                throw new ArgumentException("Must implement IArchiver", "formatType");
            }

        }
    }
}
