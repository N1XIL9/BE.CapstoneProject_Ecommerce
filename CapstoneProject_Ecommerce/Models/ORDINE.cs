namespace CapstoneProject_Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ORDINE")]
    public partial class ORDINE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ORDINE()
        {
            DETTAGLIO = new HashSet<DETTAGLIO>();
        }

        [Key]
        public int IdOrdine { get; set; }

        public int IdUser { get; set; }

        [Required]
        [StringLength(50)]
        public string Confermato { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Data Ordine")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public DateTime DataOrdine { get; set; } = DateTime.UtcNow;

        [Column(TypeName = "money")]
        [Display(Name = "Totale")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal ImportoTotale { get; set; }

        [Required]
        [StringLength(50)]
        public string Evaso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETTAGLIO> DETTAGLIO { get; set; }

        public virtual USER USER { get; set; }
    }
}
