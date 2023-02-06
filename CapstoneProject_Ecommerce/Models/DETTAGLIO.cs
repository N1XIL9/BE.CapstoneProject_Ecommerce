namespace CapstoneProject_Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DETTAGLIO")]
    public partial class DETTAGLIO
    {
        [Key]
        public int IdDettaglio { get; set; }

        public int IdUser { get; set; }

        public int IdTaglia { get; set; }

        public int? IdOrdine { get; set; }

        public int IdProdotto { get; set; }

        [Display(Name ="Quantità")]
        public int Quantita { get; set; }

        [Display(Name = "Totale")]
        [DisplayFormat(DataFormatString = "{0:C2}")]

        public decimal PrezzoTotale { get; set; }

        public virtual ORDINE ORDINE { get; set; }

        public virtual PRODOTTO PRODOTTO { get; set; }

        public virtual TAGLIE TAGLIE { get; set; }

        public virtual USER USER { get; set; }
    }
}
