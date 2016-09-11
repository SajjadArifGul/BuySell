namespace BuySell.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDBDALCompleted : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessoryBrands",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
                "dbo.Cities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
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
                        Name = c.String(nullable: false),
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
                        Name = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
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
                "dbo.Currencies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CountryID = c.Int(nullable: false),
                        Symbol = c.String(),
                        ISOCode = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Countries", t => t.CountryID, cascadeDelete: false)
                .Index(t => t.CountryID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Path = c.String(nullable: false, maxLength: 255),
                        AdID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Ads", t => t.AdID, cascadeDelete: true)
                .Index(t => t.AdID);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false),
                        PostingTime = c.DateTime(nullable: false),
                        ReviewStars = c.Int(nullable: false),
                        AdID = c.Int(nullable: false),
                        SellerID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Ads", t => t.AdID, cascadeDelete: true)
                .ForeignKey("dbo.Sellers", t => t.SellerID, cascadeDelete: false)
                .Index(t => t.AdID)
                .Index(t => t.SellerID);
            
            CreateTable(
                "dbo.Sellers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Username = c.String(nullable: false),
                        Name = c.String(nullable: false),
                        CountryID = c.Int(nullable: false),
                        StateID = c.Int(nullable: false),
                        CityID = c.Int(nullable: false),
                        MobileNumber = c.String(nullable: false),
                        JoinDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cities", t => t.CityID, cascadeDelete: false)
                .ForeignKey("dbo.Countries", t => t.CountryID, cascadeDelete: false)
                .ForeignKey("dbo.States", t => t.StateID, cascadeDelete: false)
                .Index(t => t.CountryID)
                .Index(t => t.StateID)
                .Index(t => t.CityID);
            
            CreateTable(
                "dbo.Bikes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VehicleBrandID = c.Int(nullable: false),
                        YearID = c.Int(nullable: false),
                        DrivenKilometers = c.Int(nullable: false),
                        ColorID = c.Int(nullable: false),
                        Insurance = c.Boolean(),
                        AdID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Ads", t => t.AdID, cascadeDelete: true)
                .ForeignKey("dbo.Colors", t => t.ColorID, cascadeDelete: true)
                .ForeignKey("dbo.VehicleBrands", t => t.VehicleBrandID, cascadeDelete: true)
                .ForeignKey("dbo.Years", t => t.YearID, cascadeDelete: true)
                .Index(t => t.VehicleBrandID)
                .Index(t => t.YearID)
                .Index(t => t.ColorID)
                .Index(t => t.AdID);
            
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
                "dbo.VehicleBrands",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Image = c.Binary(),
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
            
            CreateTable(
                "dbo.CellPhones",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        AccessoryBrandID = c.Int(nullable: false),
                        OperatingSystem = c.String(nullable: false),
                        AdID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AccessoryBrands", t => t.AccessoryBrandID, cascadeDelete: true)
                .ForeignKey("dbo.Ads", t => t.AdID, cascadeDelete: true)
                .Index(t => t.AccessoryBrandID)
                .Index(t => t.AdID);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Laptops", "AdID", "dbo.Ads");
            DropForeignKey("dbo.Laptops", "AccessoryBrandID", "dbo.AccessoryBrands");
            DropForeignKey("dbo.CellPhones", "AdID", "dbo.Ads");
            DropForeignKey("dbo.CellPhones", "AccessoryBrandID", "dbo.AccessoryBrands");
            DropForeignKey("dbo.Bikes", "YearID", "dbo.Years");
            DropForeignKey("dbo.Bikes", "VehicleBrandID", "dbo.VehicleBrands");
            DropForeignKey("dbo.Bikes", "ColorID", "dbo.Colors");
            DropForeignKey("dbo.Bikes", "AdID", "dbo.Ads");
            DropForeignKey("dbo.Ads", "StateID", "dbo.States");
            DropForeignKey("dbo.Sellers", "StateID", "dbo.States");
            DropForeignKey("dbo.Reviews", "SellerID", "dbo.Sellers");
            DropForeignKey("dbo.Sellers", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.Sellers", "CityID", "dbo.Cities");
            DropForeignKey("dbo.Ads", "SellerID", "dbo.Sellers");
            DropForeignKey("dbo.Reviews", "AdID", "dbo.Ads");
            DropForeignKey("dbo.Images", "AdID", "dbo.Ads");
            DropForeignKey("dbo.Ads", "CurrencyID", "dbo.Currencies");
            DropForeignKey("dbo.Currencies", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.Ads", "CountryID", "dbo.Countries");
            DropForeignKey("dbo.Ads", "ConditionID", "dbo.Conditions");
            DropForeignKey("dbo.Ads", "CityID", "dbo.Cities");
            DropForeignKey("dbo.Cities", "StateID", "dbo.States");
            DropForeignKey("dbo.States", "CountryID", "dbo.Countries");
            DropIndex("dbo.Laptops", new[] { "AdID" });
            DropIndex("dbo.Laptops", new[] { "AccessoryBrandID" });
            DropIndex("dbo.CellPhones", new[] { "AdID" });
            DropIndex("dbo.CellPhones", new[] { "AccessoryBrandID" });
            DropIndex("dbo.Bikes", new[] { "AdID" });
            DropIndex("dbo.Bikes", new[] { "ColorID" });
            DropIndex("dbo.Bikes", new[] { "YearID" });
            DropIndex("dbo.Bikes", new[] { "VehicleBrandID" });
            DropIndex("dbo.Sellers", new[] { "CityID" });
            DropIndex("dbo.Sellers", new[] { "StateID" });
            DropIndex("dbo.Sellers", new[] { "CountryID" });
            DropIndex("dbo.Reviews", new[] { "SellerID" });
            DropIndex("dbo.Reviews", new[] { "AdID" });
            DropIndex("dbo.Images", new[] { "AdID" });
            DropIndex("dbo.Currencies", new[] { "CountryID" });
            DropIndex("dbo.States", new[] { "CountryID" });
            DropIndex("dbo.Cities", new[] { "StateID" });
            DropIndex("dbo.Ads", new[] { "SellerID" });
            DropIndex("dbo.Ads", new[] { "CityID" });
            DropIndex("dbo.Ads", new[] { "StateID" });
            DropIndex("dbo.Ads", new[] { "CountryID" });
            DropIndex("dbo.Ads", new[] { "CurrencyID" });
            DropIndex("dbo.Ads", new[] { "ConditionID" });
            DropTable("dbo.VehicleTypes");
            DropTable("dbo.Laptops");
            DropTable("dbo.CellPhones");
            DropTable("dbo.Years");
            DropTable("dbo.VehicleBrands");
            DropTable("dbo.Colors");
            DropTable("dbo.Bikes");
            DropTable("dbo.Sellers");
            DropTable("dbo.Reviews");
            DropTable("dbo.Images");
            DropTable("dbo.Currencies");
            DropTable("dbo.Conditions");
            DropTable("dbo.Countries");
            DropTable("dbo.States");
            DropTable("dbo.Cities");
            DropTable("dbo.Ads");
            DropTable("dbo.AccessoryBrands");
        }
    }
}
