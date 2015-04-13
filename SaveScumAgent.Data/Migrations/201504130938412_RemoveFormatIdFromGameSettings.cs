namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveFormatIdFromGameSettings : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.GameSettings", "FormatId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GameSettings", "FormatId", c => c.Int(nullable: false));
        }
    }
}
