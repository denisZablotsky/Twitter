namespace Twitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentWithAuthor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "AuthourId", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "AuthorName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "AuthorName");
            DropColumn("dbo.Comments", "AuthourId");
        }
    }
}
