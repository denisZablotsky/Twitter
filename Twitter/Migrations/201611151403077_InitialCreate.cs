namespace Twitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tweets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        CreatingDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Email = c.String(),
                        SignupData = c.DateTime(nullable: false),
                        Age = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tweets", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserProfiles", "Id", "dbo.Users");
            DropIndex("dbo.UserProfiles", new[] { "Id" });
            DropIndex("dbo.Tweets", new[] { "UserId" });
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Users");
            DropTable("dbo.Tweets");
        }
    }
}
