namespace Twitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hashtag : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hashtags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tag = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Hash",
                c => new
                    {
                        TweetId = c.Int(nullable: false),
                        HashtagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TweetId, t.HashtagId })
                .ForeignKey("dbo.Tweets", t => t.TweetId, cascadeDelete: true)
                .ForeignKey("dbo.Hashtags", t => t.HashtagId, cascadeDelete: true)
                .Index(t => t.TweetId)
                .Index(t => t.HashtagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Hash", "HashtagId", "dbo.Hashtags");
            DropForeignKey("dbo.Hash", "TweetId", "dbo.Tweets");
            DropIndex("dbo.Hash", new[] { "HashtagId" });
            DropIndex("dbo.Hash", new[] { "TweetId" });
            DropTable("dbo.Hash");
            DropTable("dbo.Hashtags");
        }
    }
}
