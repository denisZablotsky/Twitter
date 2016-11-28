namespace Twitter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Avatar : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "AvatarLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "AvatarLink");
        }
    }
}
