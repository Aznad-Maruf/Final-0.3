namespace CompileError.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPurchaseAndDetails : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchasedProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PurchaseId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        ManufactureDate = c.String(),
                        ExpireDate = c.String(),
                        Quantity = c.Double(nullable: false),
                        UnitPrice = c.Double(nullable: false),
                        Mrp = c.Double(nullable: false),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Purchases", t => t.PurchaseId, cascadeDelete: true)
                .Index(t => t.PurchaseId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Purchases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BillNo = c.String(),
                        SupplierId = c.Int(nullable: false),
                        Date = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.PurchaseDetails");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PurchaseDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Int(nullable: false),
                        TotalPrice = c.Int(nullable: false),
                        MRP = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.PurchasedProducts", "PurchaseId", "dbo.Purchases");
            DropForeignKey("dbo.PurchasedProducts", "ProductId", "dbo.Products");
            DropIndex("dbo.PurchasedProducts", new[] { "ProductId" });
            DropIndex("dbo.PurchasedProducts", new[] { "PurchaseId" });
            DropTable("dbo.Purchases");
            DropTable("dbo.PurchasedProducts");
        }
    }
}
