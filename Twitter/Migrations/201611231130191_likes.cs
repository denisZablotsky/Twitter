namespace Twitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class likes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tweets", "Likes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tweets", "Likes");
        }
    }
}
