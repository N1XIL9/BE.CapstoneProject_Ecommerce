namespace CapstoneProject_Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_oggetto_messaggio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.USER", "Oggetto", c => c.String());
            AddColumn("dbo.USER", "Messaggio", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.USER", "Messaggio");
            DropColumn("dbo.USER", "Oggetto");
        }
    }
}
