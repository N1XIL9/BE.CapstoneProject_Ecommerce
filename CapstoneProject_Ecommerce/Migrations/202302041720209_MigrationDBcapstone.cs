namespace CapstoneProject_Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrationDBcapstone : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DETTAGLIO",
                c => new
                    {
                        IdDettaglio = c.Int(nullable: false, identity: true),
                        IdUser = c.Int(nullable: false),
                        IdTaglia = c.Int(nullable: false),
                        IdOrdine = c.Int(),
                        IdProdotto = c.Int(nullable: false),
                        Quantita = c.Int(nullable: false),
                        PrezzoTotale = c.Decimal(nullable: false, precision: 19, scale: 4),
                    })
                .PrimaryKey(t => t.IdDettaglio)
                .ForeignKey("dbo.ORDINE", t => t.IdOrdine)
                .ForeignKey("dbo.USER", t => t.IdUser)
                .ForeignKey("dbo.PRODOTTO", t => t.IdProdotto)
                .ForeignKey("dbo.TAGLIE", t => t.IdTaglia)
                .Index(t => t.IdUser)
                .Index(t => t.IdTaglia)
                .Index(t => t.IdOrdine)
                .Index(t => t.IdProdotto);
            
            CreateTable(
                "dbo.ORDINE",
                c => new
                    {
                        IdOrdine = c.Int(nullable: false, identity: true),
                        IdUser = c.Int(nullable: false),
                        Confermato = c.String(nullable: false, maxLength: 50),
                        DataOrdine = c.DateTime(nullable: false, storeType: "date"),
                        ImportoTotale = c.Decimal(nullable: false, storeType: "money"),
                        Evaso = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.IdOrdine)
                .ForeignKey("dbo.USER", t => t.IdUser)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.USER",
                c => new
                    {
                        IdUser = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Nome = c.String(maxLength: 50),
                        Cognome = c.String(maxLength: 50),
                        Email = c.String(maxLength: 50),
                        Pass = c.String(nullable: false, maxLength: 50),
                        Ruolo = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.IdUser);
            
            CreateTable(
                "dbo.PRODOTTO",
                c => new
                    {
                        IdProdotto = c.Int(nullable: false, identity: true),
                        NomeProdotto = c.String(nullable: false, maxLength: 50),
                        Descrizione = c.String(nullable: false, maxLength: 50),
                        Prezzo = c.Decimal(nullable: false, storeType: "money"),
                        Foto = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.IdProdotto);
            
            CreateTable(
                "dbo.TAGLIE",
                c => new
                    {
                        IdTaglie = c.Int(nullable: false, identity: true),
                        TagliaProdotto = c.String(nullable: false, maxLength: 1, fixedLength: true, unicode: false),
                        QuantitaTaglia = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdTaglie);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DETTAGLIO", "IdTaglia", "dbo.TAGLIE");
            DropForeignKey("dbo.DETTAGLIO", "IdProdotto", "dbo.PRODOTTO");
            DropForeignKey("dbo.ORDINE", "IdUser", "dbo.USER");
            DropForeignKey("dbo.DETTAGLIO", "IdUser", "dbo.USER");
            DropForeignKey("dbo.DETTAGLIO", "IdOrdine", "dbo.ORDINE");
            DropIndex("dbo.ORDINE", new[] { "IdUser" });
            DropIndex("dbo.DETTAGLIO", new[] { "IdProdotto" });
            DropIndex("dbo.DETTAGLIO", new[] { "IdOrdine" });
            DropIndex("dbo.DETTAGLIO", new[] { "IdTaglia" });
            DropIndex("dbo.DETTAGLIO", new[] { "IdUser" });
            DropTable("dbo.TAGLIE");
            DropTable("dbo.PRODOTTO");
            DropTable("dbo.USER");
            DropTable("dbo.ORDINE");
            DropTable("dbo.DETTAGLIO");
        }
    }
}
