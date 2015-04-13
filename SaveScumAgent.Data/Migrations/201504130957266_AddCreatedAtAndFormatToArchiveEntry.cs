namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCreatedAtAndFormatToArchiveEntry : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ArchiveEntries", "CreatedAt", c => c.DateTime(nullable: false));
            AddColumn("dbo.ArchiveEntries", "Identifier", c => c.String(nullable: false, maxLength: 4000));
            AddColumn("dbo.ArchiveEntries", "Format", c => c.Int(nullable: false));
            AddColumn("dbo.GameSettings", "IsDefault", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameSettings", "IsDefault");
            DropColumn("dbo.ArchiveEntries", "Format");
            DropColumn("dbo.ArchiveEntries", "Identifier");
            DropColumn("dbo.ArchiveEntries", "CreatedAt");
        }
    }
}
