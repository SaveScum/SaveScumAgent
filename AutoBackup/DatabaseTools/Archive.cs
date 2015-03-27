//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace AutoBackup.DatabaseTools
{
    public partial class Archive
    {
        public Archive()
        {
            this.ChangedFiles = new HashSet<ChangedFile>();
        }
    
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Filename { get; set; }
        public int Size { get; set; }
        public string Comment { get; set; }
    
        public virtual Game Game { get; set; }
        public virtual ICollection<ChangedFile> ChangedFiles { get; set; }
    }
}
