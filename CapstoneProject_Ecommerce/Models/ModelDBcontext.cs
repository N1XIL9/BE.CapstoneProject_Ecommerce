using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CapstoneProject_Ecommerce.Models
{
    public partial class ModelDBcontext : DbContext
    {
        public ModelDBcontext()
            : base("name=ModelDBcontext1")
        {
        }

        public virtual DbSet<DETTAGLIO> DETTAGLIO { get; set; }
        public virtual DbSet<ORDINE> ORDINE { get; set; }
        public virtual DbSet<PRODOTTO> PRODOTTO { get; set; }
        public virtual DbSet<TAGLIE> TAGLIE { get; set; }
        public virtual DbSet<USER> USER { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DETTAGLIO>()
                .Property(e => e.PrezzoTotale)
                .HasPrecision(19, 4);

            modelBuilder.Entity<ORDINE>()
                .Property(e => e.ImportoTotale)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRODOTTO>()
                .Property(e => e.Prezzo)
                .HasPrecision(19, 4);

            modelBuilder.Entity<PRODOTTO>()
                .HasMany(e => e.DETTAGLIO)
                .WithRequired(e => e.PRODOTTO)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAGLIE>()
                .Property(e => e.TagliaProdotto)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<TAGLIE>()
                .HasMany(e => e.DETTAGLIO)
                .WithRequired(e => e.TAGLIE)
                .HasForeignKey(e => e.IdTaglia)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.DETTAGLIO)
                .WithRequired(e => e.USER)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.ORDINE)
                .WithRequired(e => e.USER)
                .WillCascadeOnDelete(false);
        }
    }
}
