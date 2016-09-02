namespace BuySell.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedBikeModel : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bikes", "YearID", "dbo.Years");
            DropForeignKey("dbo.Bikes", "VehicleBrandID", "dbo.VehicleBrands");
            DropForeignKey("dbo.Bikes", "ColorID", "dbo.Colors");
            DropForeignKey("dbo.Bikes", "AdID", "dbo.Ads");
            DropIndex("dbo.Bikes", new[] { "AdID" });
            DropIndex("dbo.Bikes", new[] { "ColorID" });
            DropIndex("dbo.Bikes", new[] { "YearID" });
            DropIndex("dbo.Bikes", new[] { "VehicleBrandID" });
            DropTable("dbo.Bikes");
        }
    }
}
