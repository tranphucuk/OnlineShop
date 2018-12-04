namespace OnlineShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFeedbackTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Feedbacks", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Feedbacks", "CreatedDate");
        }
    }
}
