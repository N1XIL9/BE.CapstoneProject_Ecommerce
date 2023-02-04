namespace CapstoneProject_Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TAGLIE")]
    public partial class TAGLIE
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TAGLIE()
        {
            DETTAGLIO = new HashSet<DETTAGLIO>();
        }

        [Key]
        public int IdTaglie { get; set; }

        [Required]
        [StringLength(1)]
        public string TagliaProdotto { get; set; }

        public int QuantitaTaglia { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETTAGLIO> DETTAGLIO { get; set; }
    }
}
