namespace CapstoneProject_Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Drawing.Design;
    using System.Web.Mvc;
    using System.Web.UI.WebControls;

    [Table("TAGLIE")]
    public partial class TAGLIE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TAGLIE()
        {
            DETTAGLIO = new HashSet<DETTAGLIO>();
        }

        [Key]
        [Display(Name = "Taglia")]
        public int IdTaglie { get; set; }
        [Display(Name = "Prodotto")]


        public int IdProdotto { get; set; }

        [Required]
        [StringLength(1)]
        [Display(Name = "Taglia")]
        public string TagliaProdotto { get; set; }

        [Display(Name = "Quantità")]
        public int QuantitaTaglia { get; set; }
         

    

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETTAGLIO> DETTAGLIO { get; set; }
    }
}
