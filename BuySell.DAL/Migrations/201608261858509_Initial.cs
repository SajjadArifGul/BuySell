namespace BuySell.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessoryAds",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        AccessoryBrandID = c.Int(nullable: false),
                        OperatingSystem = c.String(),
                        Ram = c.String(),
                        Processor = c.String(),
                        HardDisk = c.String(),
                        Condition = c.String(),
                        Description = c.String(),
                        CurrencyID = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CountryID = c.Int(nullable: false),
                        StateID = c.Int(nullable: false),
                        CityID = c.Int(nullable: false),
                        SellerID = c.Int(nullable: false),
                        PostingTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccessoryBrands", t => t.AccessoryBrandID, cascadeDelete: true)
                .ForeignKey("dbo.Cities", t => t.CityID, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.CountryID, cascadeDelete: true)
                .ForeignKey("dbo.Currencies", t => t.CurrencyID, cascadeDelete: true)
                .ForeignKey("dbo.Sellers", t => t.SellerID, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.StateID, cascadeDelete: true)
                .Index(t => t.AccessoryBrandID)
                .Index(t => t.CurrencyID)
                .Index(t => t.CountryID)
                .Index(t => t.StateID)
                .Index(t => t.CityID)
                .Index(t => t.SellerID);
            
            CreateTable(
                "dbo.AccessoryBrands",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImageID = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Images", t => t.ImageID, cascadeDelete: true)
                .Index(t => t.ImageID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Content = c.Binary(),
                        AccessoryAd_ID = c.Int(),
                        VehicleAd_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccessoryAds", t => t.AccessoryAd_ID)
                .ForeignKey("dbo.VehicleAds", t => t.VehicleAd_ID)
                .Index(t => t.AccessoryAd_ID)
                .Index(t => t.VehicleAd_ID);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        StateID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.States", t => t.StateID, cascadeDelete: false)
                .Index(t => t.StateID);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        CountryID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Countries", t => t.CountryID, cascadeDelete: false)
                .Index(t => t.CountryID);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CountryID = c.Int(nullable: false),
                        Symbol = c.String(),
                        ISOCode = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Countries", t => t.CountryID, cascadeDelete: false)
                .Index(t => t.CountryID);
            
            CreateTable(
                "dbo.Sellers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Username = c.String(),
                        Name = c.String(),
                        CountryID = c.Int(nullable: false),
                        StateID = c.Int(nullable: false),
                        CityID = c.Int(nullable: false),
                        MobileNumber = c.String(),
                        JoinDate = c.DateTime(nullable: false),
                        ImageID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cities", t => t.CityID, cascadeDelete: false)
                .ForeignKey("dbo.Countries", t => t.CountryID, cascadeDelete: false)
                .ForeignKey("dbo.Images", t => t.ImageID, cascadeDelete: false)
                .ForeignKey("dbo.States", t => t.StateID, cascadeDelete: false)
                .Index(t => t.CountryID)
                .Index(t => t.StateID)
                .Index(t => t.CityID)
                .Index(t => t.ImageID);
            
            CreateTable(
                "dbo.VehicleAds",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        VehicleType = c.String(),
                        VehicleBrandID = c.Int(nullable: false),
                        RegistrationYear = c.Int(nullable: false),
                        DrivenKilometers = c.Int(nullable: false),
                        Color = c.String(),
                        Insurance = c.Boolean(),
                        Condition = c.String(),
                        Description = c.String(),
                        CurrencyID = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CountryID = c.Int(nullable: false),
                        StateID = c.Int(nullable: false),
                        CityID = c.Int(nullable: false),
                        SellerID = c.Int(nullable: false),
                        PostingTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cities", t => t.CityID, cascadeDelete: true)
                .ForeignKey("dbo.Countries", t => t.CountryID, cascadeDelete: true)
                .ForeignKey("dbo.Currencies", t => t.CurrencyID, cascadeDelete: true)
                .ForeignKey("dbo.Sellers", t => t.SellerID, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.StateID, cascadeDelete: true)
                .ForeignKey("dbo.VehicleBrands", t => t.VehicleBrandID, cascadeDelete: true)
                .Index(t => t.VehicleBrandID)
                .Index(t => t.CurrencyID)
                .Index(t => t.CountryID)
                .Index(t => t.StateID)
                .Index(t => t.CityID)
                .Index(t => t.SellerID);
            
            CreateTable(
                "dbo.VehicleBrands",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ImageID = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Images", t => t.ImageID, cascadeDelete: true)
                .Index(t => t.ImageID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VehicleAds", "VehicleBrandID", "dbo.VehicleBrands");
            DropForeignKey("dbo.VehicleBrands", "ImageID", "dbo.Images");
            DropForeignKey("dbo.VehicleAds", "StateID", "dbo.States");
            DropForeignKey("dbo.VehicleAds", "SellerID", "dbo.Sellers");
            DropForeignKey("dbo.Images", "VehicleAd_ID", "dbo.VehicleAds");
            DropForeignKey("dbo.VehicleAds", "CurrencyID", "dbo.Currencies");
            DropForeignKey("dbo.VehicleAds", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.VehicleAds", "CityID", "dbo.Cities");
            DropForeignKey("dbo.AccessoryAds", "StateID", "dbo.States");
            DropForeignKey("dbo.AccessoryAds", "SellerID", "dbo.Sellers");
            DropForeignKey("dbo.Sellers", "StateID", "dbo.States");
            DropForeignKey("dbo.Sellers", "ImageID", "dbo.Images");
            DropForeignKey("dbo.Sellers", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.Sellers", "CityID", "dbo.Cities");
            DropForeignKey("dbo.Images", "AccessoryAd_ID", "dbo.AccessoryAds");
            DropForeignKey("dbo.AccessoryAds", "CurrencyID", "dbo.Currencies");
            DropForeignKey("dbo.Currencies", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.AccessoryAds", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.AccessoryAds", "CityID", "dbo.Cities");
            DropForeignKey("dbo.Cities", "StateID", "dbo.States");
            DropForeignKey("dbo.States", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.AccessoryAds", "AccessoryBrandID", "dbo.AccessoryBrands");
            DropForeignKey("dbo.AccessoryBrands", "ImageID", "dbo.Images");
            DropIndex("dbo.VehicleBrands", new[] { "ImageID" });
            DropIndex("dbo.VehicleAds", new[] { "SellerID" });
            DropIndex("dbo.VehicleAds", new[] { "CityID" });
            DropIndex("dbo.VehicleAds", new[] { "StateID" });
            DropIndex("dbo.VehicleAds", new[] { "CountryID" });
            DropIndex("dbo.VehicleAds", new[] { "CurrencyID" });
            DropIndex("dbo.VehicleAds", new[] { "VehicleBrandID" });
            DropIndex("dbo.Sellers", new[] { "ImageID" });
            DropIndex("dbo.Sellers", new[] { "CityID" });
            DropIndex("dbo.Sellers", new[] { "StateID" });
            DropIndex("dbo.Sellers", new[] { "CountryID" });
            DropIndex("dbo.Currencies", new[] { "CountryID" });
            DropIndex("dbo.States", new[] { "CountryID" });
            DropIndex("dbo.Cities", new[] { "StateID" });
            DropIndex("dbo.Images", new[] { "VehicleAd_ID" });
            DropIndex("dbo.Images", new[] { "AccessoryAd_ID" });
            DropIndex("dbo.AccessoryBrands", new[] { "ImageID" });
            DropIndex("dbo.AccessoryAds", new[] { "SellerID" });
            DropIndex("dbo.AccessoryAds", new[] { "CityID" });
            DropIndex("dbo.AccessoryAds", new[] { "StateID" });
            DropIndex("dbo.AccessoryAds", new[] { "CountryID" });
            DropIndex("dbo.AccessoryAds", new[] { "CurrencyID" });
            DropIndex("dbo.AccessoryAds", new[] { "AccessoryBrandID" });
            DropTable("dbo.VehicleBrands");
            DropTable("dbo.VehicleAds");
            DropTable("dbo.Sellers");
            DropTable("dbo.Currencies");
            DropTable("dbo.Countries");
            DropTable("dbo.States");
            DropTable("dbo.Cities");
            DropTable("dbo.Images");
            DropTable("dbo.AccessoryBrands");
            DropTable("dbo.AccessoryAds");
        }
    }
}
