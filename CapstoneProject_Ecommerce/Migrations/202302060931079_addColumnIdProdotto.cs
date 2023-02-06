namespace CapstoneProject_Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColumnIdProdotto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TAGLIE", "IdProdotto", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TAGLIE", "IdProdotto");
        }
    }
}
