namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIndexToGameTitle : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Games", "Title", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Games", new[] { "Title" });
        }
    }
}
