namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArchivesLocationToGameSettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameSettings", "SaveDirectoryLocation", c => c.String(maxLength: 260));
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameSettings", "SaveDirectoryLocation");
        }
    }
}
