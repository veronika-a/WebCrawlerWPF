namespace WebCrawlerWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pladd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SPages", "PageLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SPages", "PageLink");
        }
    }
}
