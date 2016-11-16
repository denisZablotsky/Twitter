namespace Twitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TwitterDbWhisFollowers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Follow",
                c => new
                    {
                        FollowerId = c.Int(nullable: false),
                        FollowingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FollowerId, t.FollowingId })
                .ForeignKey("dbo.Users", t => t.FollowerId)
                .ForeignKey("dbo.Users", t => t.FollowingId)
                .Index(t => t.FollowerId)
                .Index(t => t.FollowingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Follow", "FollowingId", "dbo.Users");
            DropForeignKey("dbo.Follow", "FollowerId", "dbo.Users");
            DropIndex("dbo.Follow", new[] { "FollowingId" });
            DropIndex("dbo.Follow", new[] { "FollowerId" });
            DropTable("dbo.Follow");
        }
    }
}
