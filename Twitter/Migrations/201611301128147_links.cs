namespace Twitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class links : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tweets", "Links", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tweets", "Links");
        }
    }
}
