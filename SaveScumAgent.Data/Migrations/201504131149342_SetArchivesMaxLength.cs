namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetArchivesMaxLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.GameSettings", "ArchivesLocation", c => c.String(maxLength: 260));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.GameSettings", "ArchivesLocation", c => c.String(maxLength: 4000));
        }
    }
}
