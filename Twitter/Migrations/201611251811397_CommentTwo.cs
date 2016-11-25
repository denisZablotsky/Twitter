namespace Twitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CommentTwo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        CreatingDate = c.DateTime(nullable: false),
                        TweetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tweets", t => t.TweetId, cascadeDelete: true)
                .Index(t => t.TweetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "TweetId", "dbo.Tweets");
            DropIndex("dbo.Comments", new[] { "TweetId" });
            DropTable("dbo.Comments");
        }
    }
}
