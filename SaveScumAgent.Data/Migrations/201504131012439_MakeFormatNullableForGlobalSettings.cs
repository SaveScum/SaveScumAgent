namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeFormatNullableForGlobalSettings : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GameSettings", "Format", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GameSettings", "Format", c => c.Int(nullable: false));
        }
    }
}
