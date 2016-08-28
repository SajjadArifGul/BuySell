namespace BuySell.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewModelClasses : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccessoryAds", "AccessoryBrandID", "dbo.AccessoryBrands");
            DropForeignKey("dbo.AccessoryAds", "CityID", "dbo.Cities");
            DropForeignKey("dbo.AccessoryAds", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.AccessoryAds", "CurrencyID", "dbo.Currencies");
            DropForeignKey("dbo.Images", "AccessoryAd_ID", "dbo.AccessoryAds");
            DropForeignKey("dbo.Sellers", "ImageID", "dbo.Images");
            DropForeignKey("dbo.AccessoryAds", "SellerID", "dbo.Sellers");
            DropForeignKey("dbo.AccessoryAds", "StateID", "dbo.States");
            DropForeignKey("dbo.VehicleAds", "CityID", "dbo.Cities");
            DropForeignKey("dbo.VehicleAds", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.VehicleAds", "CurrencyID", "dbo.Currencies");
            DropForeignKey("dbo.Images", "VehicleAd_ID", "dbo.VehicleAds");
            DropForeignKey("dbo.VehicleAds", "SellerID", "dbo.Sellers");
            DropForeignKey("dbo.VehicleAds", "StateID", "dbo.States");
            DropForeignKey("dbo.VehicleAds", "VehicleBrandID", "dbo.VehicleBrands");
            DropIndex("dbo.AccessoryAds", new[] { "AccessoryBrandID" });
            DropIndex("dbo.AccessoryAds", new[] { "CurrencyID" });
            DropIndex("dbo.AccessoryAds", new[] { "CountryID" });
            DropIndex("dbo.AccessoryAds", new[] { "StateID" });
            DropIndex("dbo.AccessoryAds", new[] { "CityID" });
            DropIndex("dbo.AccessoryAds", new[] { "SellerID" });
            DropIndex("dbo.Images", new[] { "AccessoryAd_ID" });
            DropIndex("dbo.Images", new[] { "VehicleAd_ID" });
            DropIndex("dbo.Sellers", new[] { "ImageID" });
            DropIndex("dbo.VehicleAds", new[] { "VehicleBrandID" });
            DropIndex("dbo.VehicleAds", new[] { "CurrencyID" });
            DropIndex("dbo.VehicleAds", new[] { "CountryID" });
            DropIndex("dbo.VehicleAds", new[] { "StateID" });
            DropIndex("dbo.VehicleAds", new[] { "CityID" });
            DropIndex("dbo.VehicleAds", new[] { "SellerID" });
            CreateTable(
                "dbo.Ads",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        ConditionID = c.Int(nullable: false),
                        Description = c.String(maxLength: 500),
                        CurrencyID = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CountryID = c.Int(nullable: false),
                        StateID = c.Int(nullable: false),
                        CityID = c.Int(nullable: false),
                        SellerID = c.Int(nullable: false),
                        Slug = c.String(nullable: false),
                        PostingTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cities", t => t.CityID, cascadeDelete: true)
                .ForeignKey("dbo.Conditions", t => t.ConditionID, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.CountryID, cascadeDelete: true)
                .ForeignKey("dbo.Currencies", t => t.CurrencyID, cascadeDelete: true)
                .ForeignKey("dbo.Sellers", t => t.SellerID, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.StateID, cascadeDelete: true)
                .Index(t => t.ConditionID)
                .Index(t => t.CurrencyID)
                .Index(t => t.CountryID)
                .Index(t => t.StateID)
                .Index(t => t.CityID)
                .Index(t => t.SellerID);
            
            CreateTable(
                "dbo.Conditions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ConditionType = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Colors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        HexCode = c.String(nullable: false),
                        RGBRed = c.String(nullable: false),
                        RGBGreen = c.String(nullable: false),
                        RGBBlue = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Laptops",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccessoryBrandID = c.Int(nullable: false),
                        OperatingSystem = c.String(nullable: false),
                        Ram = c.String(nullable: false),
                        Processor = c.String(nullable: false),
                        HardDisk = c.String(nullable: false),
                        AdID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccessoryBrands", t => t.AccessoryBrandID, cascadeDelete: true)
                .ForeignKey("dbo.Ads", t => t.AdID, cascadeDelete: true)
                .Index(t => t.AccessoryBrandID)
                .Index(t => t.AdID);
            
            CreateTable(
                "dbo.VehicleTypes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Years",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        YearNo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Images", "Path", c => c.String(nullable: false, maxLength: 255));
            AddColumn("dbo.Images", "AdID", c => c.Int(nullable: false));
            CreateIndex("dbo.Images", "AdID");
            AddForeignKey("dbo.Images", "AdID", "dbo.Ads", "ID", cascadeDelete: true);
            DropColumn("dbo.AccessoryBrands", "Image");
            DropColumn("dbo.Images", "Name");
            DropColumn("dbo.Images", "Content");
            DropColumn("dbo.Images", "AccessoryAd_ID");
            DropColumn("dbo.Images", "VehicleAd_ID");
            DropColumn("dbo.Sellers", "ImageID");
            DropTable("dbo.AccessoryAds");
            DropTable("dbo.VehicleAds");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VehicleAds",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        VehicleType = c.String(nullable: false),
                        VehicleBrandID = c.Int(nullable: false),
                        RegistrationYear = c.Int(nullable: false),
                        DrivenKilometers = c.Int(nullable: false),
                        Color = c.String(nullable: false),
                        Insurance = c.Boolean(),
                        Condition = c.String(nullable: false),
                        Description = c.String(),
                        CurrencyID = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CountryID = c.Int(nullable: false),
                        StateID = c.Int(nullable: false),
                        CityID = c.Int(nullable: false),
                        SellerID = c.Int(nullable: false),
                        PostingTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AccessoryAds",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        AccessoryBrandID = c.Int(nullable: false),
                        OperatingSystem = c.String(nullable: false),
                        Ram = c.String(nullable: false),
                        Processor = c.String(nullable: false),
                        HardDisk = c.String(nullable: false),
                        Condition = c.String(nullable: false),
                        Description = c.String(),
                        CurrencyID = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CountryID = c.Int(nullable: false),
                        StateID = c.Int(nullable: false),
                        CityID = c.Int(nullable: false),
                        SellerID = c.Int(nullable: false),
                        PostingTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Sellers", "ImageID", c => c.Int(nullable: false));
            AddColumn("dbo.Images", "VehicleAd_ID", c => c.Int());
            AddColumn("dbo.Images", "AccessoryAd_ID", c => c.Int());
            AddColumn("dbo.Images", "Content", c => c.Binary());
            AddColumn("dbo.Images", "Name", c => c.String());
            AddColumn("dbo.AccessoryBrands", "Image", c => c.Binary());
            DropForeignKey("dbo.Laptops", "AdID", "dbo.Ads");
            DropForeignKey("dbo.Laptops", "AccessoryBrandID", "dbo.AccessoryBrands");
            DropForeignKey("dbo.Ads", "StateID", "dbo.States");
            DropForeignKey("dbo.Ads", "SellerID", "dbo.Sellers");
            DropForeignKey("dbo.Images", "AdID", "dbo.Ads");
            DropForeignKey("dbo.Ads", "CurrencyID", "dbo.Currencies");
            DropForeignKey("dbo.Ads", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.Ads", "ConditionID", "dbo.Conditions");
            DropForeignKey("dbo.Ads", "CityID", "dbo.Cities");
            DropIndex("dbo.Laptops", new[] { "AdID" });
            DropIndex("dbo.Laptops", new[] { "AccessoryBrandID" });
            DropIndex("dbo.Images", new[] { "AdID" });
            DropIndex("dbo.Ads", new[] { "SellerID" });
            DropIndex("dbo.Ads", new[] { "CityID" });
            DropIndex("dbo.Ads", new[] { "StateID" });
            DropIndex("dbo.Ads", new[] { "CountryID" });
            DropIndex("dbo.Ads", new[] { "CurrencyID" });
            DropIndex("dbo.Ads", new[] { "ConditionID" });
            DropColumn("dbo.Images", "AdID");
            DropColumn("dbo.Images", "Path");
            DropTable("dbo.Years");
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.Laptops");
            DropTable("dbo.Colors");
            DropTable("dbo.Conditions");
            DropTable("dbo.Ads");
            CreateIndex("dbo.VehicleAds", "SellerID");
            CreateIndex("dbo.VehicleAds", "CityID");
            CreateIndex("dbo.VehicleAds", "StateID");
            CreateIndex("dbo.VehicleAds", "CountryID");
            CreateIndex("dbo.VehicleAds", "CurrencyID");
            CreateIndex("dbo.VehicleAds", "VehicleBrandID");
            CreateIndex("dbo.Sellers", "ImageID");
            CreateIndex("dbo.Images", "VehicleAd_ID");
            CreateIndex("dbo.Images", "AccessoryAd_ID");
            CreateIndex("dbo.AccessoryAds", "SellerID");
            CreateIndex("dbo.AccessoryAds", "CityID");
            CreateIndex("dbo.AccessoryAds", "StateID");
            CreateIndex("dbo.AccessoryAds", "CountryID");
            CreateIndex("dbo.AccessoryAds", "CurrencyID");
            CreateIndex("dbo.AccessoryAds", "AccessoryBrandID");
            AddForeignKey("dbo.VehicleAds", "VehicleBrandID", "dbo.VehicleBrands", "ID", cascadeDelete: true);
            AddForeignKey("dbo.VehicleAds", "StateID", "dbo.States", "ID", cascadeDelete: true);
            AddForeignKey("dbo.VehicleAds", "SellerID", "dbo.Sellers", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Images", "VehicleAd_ID", "dbo.VehicleAds", "ID");
            AddForeignKey("dbo.VehicleAds", "CurrencyID", "dbo.Currencies", "ID", cascadeDelete: true);
            AddForeignKey("dbo.VehicleAds", "CountryID", "dbo.Countries", "ID", cascadeDelete: true);
            AddForeignKey("dbo.VehicleAds", "CityID", "dbo.Cities", "ID", cascadeDelete: true);
            AddForeignKey("dbo.AccessoryAds", "StateID", "dbo.States", "ID", cascadeDelete: true);
            AddForeignKey("dbo.AccessoryAds", "SellerID", "dbo.Sellers", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Sellers", "ImageID", "dbo.Images", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Images", "AccessoryAd_ID", "dbo.AccessoryAds", "ID");
            AddForeignKey("dbo.AccessoryAds", "CurrencyID", "dbo.Currencies", "ID", cascadeDelete: true);
            AddForeignKey("dbo.AccessoryAds", "CountryID", "dbo.Countries", "ID", cascadeDelete: true);
            AddForeignKey("dbo.AccessoryAds", "CityID", "dbo.Cities", "ID", cascadeDelete: true);
            AddForeignKey("dbo.AccessoryAds", "AccessoryBrandID", "dbo.AccessoryBrands", "ID", cascadeDelete: true);
        }
    }
}
