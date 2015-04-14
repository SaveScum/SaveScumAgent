namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUniqueConstraintToFilesafeTitle : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Games", "FilesafeTitle", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Games", "Title", unique: true);
            CreateIndex("dbo.Games", "FilesafeTitle", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Games", new[] { "FilesafeTitle" });
            DropIndex("dbo.Games", new[] { "Title" });
            AlterColumn("dbo.Games", "FilesafeTitle", c => c.String(maxLength: 50));
        }
    }
}
