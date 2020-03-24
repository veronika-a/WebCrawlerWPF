namespace WebCrawlerWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Statistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumberOfVisits = c.Int(nullable: false),
                        Site_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.Site_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Site_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Statistics", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Statistics", "Site_Id", "dbo.Sites");
            DropIndex("dbo.Statistics", new[] { "User_Id" });
            DropIndex("dbo.Statistics", new[] { "Site_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Statistics");
        }
    }
}
