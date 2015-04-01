using System;
using SaveScumAgent.Archiver.Formats;

namespace SaveScumAgent.Archiver
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class ArchiveFormatAttribute : Attribute
    {
        public ArchiveFormatAttribute(Type formatType)
        {
            var archiver = formatType as IArchiver;
            if (archiver != null)
            {
                FormatType = formatType;
            }
            else
            {
                throw new ArgumentException("Must implement IArchiver", "formatType");
            }
        }

        public Type FormatType { get; set; }
    }
}