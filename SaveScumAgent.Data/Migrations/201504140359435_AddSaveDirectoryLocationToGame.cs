namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSaveDirectoryLocationToGame : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "SaveDirectoryLocation", c => c.String(maxLength: 260));
            CreateIndex("dbo.Games", "GameSettingsId");
            AddForeignKey("dbo.Games", "GameSettingsId", "dbo.GameSettings", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "GameSettingsId", "dbo.GameSettings");
            DropIndex("dbo.Games", new[] { "GameSettingsId" });
            DropColumn("dbo.Games", "SaveDirectoryLocation");
        }
    }
}
