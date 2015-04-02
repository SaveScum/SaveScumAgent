using System;
using SaveScumAgent.Archiver.Formats;

namespace SaveScumAgent.Archiver
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ArchiveFormatAttribute : Attribute
    {
        public ArchiveFormatAttribute(Type formatType)
        {
            using (var archiver = Activator.CreateInstance(formatType) as ArchiverBase)
                if (archiver != null)
                {
                    FormatType = formatType;
                }
                else
                {
                    throw new ArgumentException("Must implement Archiver", "formatType");
                }
        }

        public Type FormatType { get; set; }
    }
}