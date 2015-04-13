namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGlobalSettings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GameSettings", "IsGlobal", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GameSettings", "IsGlobal");
        }
    }
}
