namespace BuySell.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotationsAddedToAllModelClasses : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AccessoryAds", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.AccessoryAds", "OperatingSystem", c => c.String(nullable: false));
            AlterColumn("dbo.AccessoryAds", "Ram", c => c.String(nullable: false));
            AlterColumn("dbo.AccessoryAds", "Processor", c => c.String(nullable: false));
            AlterColumn("dbo.AccessoryAds", "HardDisk", c => c.String(nullable: false));
            AlterColumn("dbo.AccessoryAds", "Condition", c => c.String(nullable: false));
            AlterColumn("dbo.AccessoryBrands", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Cities", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.States", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Countries", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Currencies", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Sellers", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Sellers", "Username", c => c.String(nullable: false));
            AlterColumn("dbo.Sellers", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Sellers", "MobileNumber", c => c.String(nullable: false));
            AlterColumn("dbo.VehicleBrands", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.VehicleBrands", "Name", c => c.String());
            AlterColumn("dbo.Sellers", "MobileNumber", c => c.String());
            AlterColumn("dbo.Sellers", "Name", c => c.String());
            AlterColumn("dbo.Sellers", "Username", c => c.String());
            AlterColumn("dbo.Sellers", "Email", c => c.String());
            AlterColumn("dbo.Currencies", "Name", c => c.String());
            AlterColumn("dbo.Countries", "Name", c => c.String());
            AlterColumn("dbo.States", "Name", c => c.String());
            AlterColumn("dbo.Cities", "Name", c => c.String());
            AlterColumn("dbo.AccessoryBrands", "Name", c => c.String());
            AlterColumn("dbo.AccessoryAds", "Condition", c => c.String());
            AlterColumn("dbo.AccessoryAds", "HardDisk", c => c.String());
            AlterColumn("dbo.AccessoryAds", "Processor", c => c.String());
            AlterColumn("dbo.AccessoryAds", "Ram", c => c.String());
            AlterColumn("dbo.AccessoryAds", "OperatingSystem", c => c.String());
            AlterColumn("dbo.AccessoryAds", "Title", c => c.String());
        }
    }
}
