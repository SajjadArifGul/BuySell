namespace BuySell.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewModelDetailsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "PostingTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Reviews", "ReviewStars", c => c.Int(nullable: false));
            AddColumn("dbo.Reviews", "SellerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Reviews", "SellerID");
            AddForeignKey("dbo.Reviews", "SellerID", "dbo.Sellers", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "SellerID", "dbo.Sellers");
            DropIndex("dbo.Reviews", new[] { "SellerID" });
            DropColumn("dbo.Reviews", "SellerID");
            DropColumn("dbo.Reviews", "ReviewStars");
            DropColumn("dbo.Reviews", "PostingTime");
        }
    }
}
