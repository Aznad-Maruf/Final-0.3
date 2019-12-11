namespace CompileError.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeColumnameForSalesdetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SalesDetails", "ProductId", c => c.Int(nullable: false));
            DropColumn("dbo.SalesDetails", "ProductCode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SalesDetails", "ProductCode", c => c.String());
            DropColumn("dbo.SalesDetails", "ProductId");
        }
    }
}
