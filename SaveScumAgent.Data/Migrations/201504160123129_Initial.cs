namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArchiveEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedAt = c.DateTime(nullable: false),
                        Identifier = c.String(nullable: false, maxLength: 4000),
                        Format = c.Int(nullable: false),
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50),
                        FilesafeTitle = c.String(nullable: false, maxLength: 50),
                        SaveDirectoryLocation = c.String(maxLength: 260),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GameSettings", t => t.Id)
                .Index(t => t.Id)
                .Index(t => t.Title, unique: true)
                .Index(t => t.FilesafeTitle, unique: true);
            
            CreateTable(
                "dbo.GameSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Format = c.Int(),
                        ArchivesLocation = c.String(maxLength: 260),
                        SaveDirectoryLocation = c.String(maxLength: 260),
                        ArchiveTriggerDelay = c.Int(),
                        IsGlobal = c.Boolean(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "Id", "dbo.GameSettings");
            DropForeignKey("dbo.ArchiveEntries", "Game_Id", "dbo.Games");
            DropIndex("dbo.Games", new[] { "FilesafeTitle" });
            DropIndex("dbo.Games", new[] { "Title" });
            DropIndex("dbo.Games", new[] { "Id" });
            DropIndex("dbo.ArchiveEntries", new[] { "Game_Id" });
            DropTable("dbo.GameSettings");
            DropTable("dbo.Games");
            DropTable("dbo.ArchiveEntries");
        }
    }
}
