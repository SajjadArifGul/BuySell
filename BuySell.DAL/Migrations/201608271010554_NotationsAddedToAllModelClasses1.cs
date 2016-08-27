namespace BuySell.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotationsAddedToAllModelClasses1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccessoryBrands", "ImageID", "dbo.Images");
            DropForeignKey("dbo.VehicleBrands", "ImageID", "dbo.Images");
            DropIndex("dbo.AccessoryBrands", new[] { "ImageID" });
            DropIndex("dbo.VehicleBrands", new[] { "ImageID" });
            AddColumn("dbo.AccessoryBrands", "Image", c => c.Binary());
            AddColumn("dbo.VehicleBrands", "Image", c => c.Binary());
            DropColumn("dbo.AccessoryBrands", "ImageID");
            DropColumn("dbo.VehicleBrands", "ImageID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.VehicleBrands", "ImageID", c => c.Int(nullable: false));
            AddColumn("dbo.AccessoryBrands", "ImageID", c => c.Int(nullable: false));
            DropColumn("dbo.VehicleBrands", "Image");
            DropColumn("dbo.AccessoryBrands", "Image");
            CreateIndex("dbo.VehicleBrands", "ImageID");
            CreateIndex("dbo.AccessoryBrands", "ImageID");
            AddForeignKey("dbo.VehicleBrands", "ImageID", "dbo.Images", "ID", cascadeDelete: true);
            AddForeignKey("dbo.AccessoryBrands", "ImageID", "dbo.Images", "ID", cascadeDelete: true);
        }
    }
}
