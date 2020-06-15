namespace WebCrawlerWPF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pldel : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.SPages", "Link");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SPages", "Link", c => c.String());
        }
    }
}
