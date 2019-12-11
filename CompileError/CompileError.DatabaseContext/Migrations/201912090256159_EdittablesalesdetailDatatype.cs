namespace CompileError.DatabaseContext.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EdittablesalesdetailDatatype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SalesDetails", "Quantity", c => c.Int(nullable: false));
            AlterColumn("dbo.SalesDetails", "MRP", c => c.Double(nullable: false));
            AlterColumn("dbo.SalesDetails", "TotalMRP", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SalesDetails", "TotalMRP", c => c.Single(nullable: false));
            AlterColumn("dbo.SalesDetails", "MRP", c => c.Single(nullable: false));
            AlterColumn("dbo.SalesDetails", "Quantity", c => c.Single(nullable: false));
        }
    }
}
