namespace BuySell.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CellPhoneModelAdded : DbMigration
    {
        public override void Up()
        {
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CellPhones", "AdID", "dbo.Ads");
            DropForeignKey("dbo.CellPhones", "AccessoryBrandID", "dbo.AccessoryBrands");
            DropIndex("dbo.CellPhones", new[] { "AdID" });
            DropIndex("dbo.CellPhones", new[] { "AccessoryBrandID" });
            DropTable("dbo.CellPhones");
        }
    }
}
