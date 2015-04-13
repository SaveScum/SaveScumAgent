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
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "dbo.GameSettings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FormatId = c.Int(nullable: false),
                        Format = c.Int(nullable: false),
                        SaveLocation = c.String(maxLength: 4000),
                        ArchiveTriggerDelay = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameSettingsId = c.Int(),
                        Title = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ArchiveEntries", "Game_Id", "dbo.Games");
            DropIndex("dbo.ArchiveEntries", new[] { "Game_Id" });
            DropTable("dbo.Games");
            DropTable("dbo.GameSettings");
            DropTable("dbo.ArchiveEntries");
        }
    }
}
