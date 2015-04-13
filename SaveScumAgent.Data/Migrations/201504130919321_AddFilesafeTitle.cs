namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddFilesafeTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "FilesafeTitle", c => c.String(maxLength: 50));
            CreateIndex("dbo.Games", "FilesafeTitle", unique: true);
        }

        public override void Down()
        {
            DropIndex("dbo.Games", new[] {"FilesafeTitle"});
            DropColumn("dbo.Games", "FilesafeTitle");
        }
    }
}