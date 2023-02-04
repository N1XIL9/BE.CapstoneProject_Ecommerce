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
        public DateTime DataOrdine { get; set; }

        [Column(TypeName = "money")]
        public decimal ImportoTotale { get; set; }

        [Required]
        [StringLength(50)]
        public string Evaso { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETTAGLIO> DETTAGLIO { get; set; }

        public virtual USER USER { get; set; }
    }
}
