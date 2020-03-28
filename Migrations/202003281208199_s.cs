namespace WebCrawlerWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class s : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sites", "MainPage_Id", "dbo.SPages");
            DropIndex("dbo.Sites", new[] { "MainPage_Id" });
            AddColumn("dbo.Sites", "MainPage", c => c.String());
            DropColumn("dbo.Sites", "MainPage_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sites", "MainPage_Id", c => c.Int());
            DropColumn("dbo.Sites", "MainPage");
            CreateIndex("dbo.Sites", "MainPage_Id");
            AddForeignKey("dbo.Sites", "MainPage_Id", "dbo.SPages", "Id");
        }
    }
}
