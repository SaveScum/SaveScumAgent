﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace AutoBackup.DatabaseTools
{
    public partial class ModelsContainer : DbContext
    {
        public ModelsContainer()
            : base("name=ModelsContainer")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<Archive> Archives { get; set; }
        public virtual DbSet<ChangedFile> ChangedFiles { get; set; }
    }
}
