namespace OnlineShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCreateDateTableFeedback : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Feedbacks", "CreatdDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Feedbacks", "CreatdDate", c => c.DateTime(nullable: false));
        }
    }
}
