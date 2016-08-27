namespace BuySell.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotationsAdded : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.VehicleAds", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.VehicleAds", "VehicleType", c => c.String(nullable: false));
            AlterColumn("dbo.VehicleAds", "Color", c => c.String(nullable: false));
            AlterColumn("dbo.VehicleAds", "Condition", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VehicleAds", "Condition", c => c.String());
            AlterColumn("dbo.VehicleAds", "Color", c => c.String());
            AlterColumn("dbo.VehicleAds", "VehicleType", c => c.String());
            AlterColumn("dbo.VehicleAds", "Title", c => c.String());
        }
    }
}
