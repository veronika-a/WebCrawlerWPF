namespace WebCrawlerWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class start : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        MainPage_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SPages", t => t.MainPage_Id)
                .Index(t => t.MainPage_Id);
            
            CreateTable(
                "dbo.SPages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Link = c.String(),
                        Site_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.Site_Id)
                .Index(t => t.Site_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SPages", "Site_Id", "dbo.Sites");
            DropForeignKey("dbo.Sites", "MainPage_Id", "dbo.SPages");
            DropIndex("dbo.SPages", new[] { "Site_Id" });
            DropIndex("dbo.Sites", new[] { "MainPage_Id" });
            DropTable("dbo.SPages");
            DropTable("dbo.Sites");
        }
    }
}
