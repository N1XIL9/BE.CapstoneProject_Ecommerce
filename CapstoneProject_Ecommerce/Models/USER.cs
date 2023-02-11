namespace CapstoneProject_Ecommerce.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("USER")]
    public partial class USER
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public USER()
        {
            DETTAGLIO = new HashSet<DETTAGLIO>();
            ORDINE = new HashSet<ORDINE>();
        }

        [Key]
        public int IdUser { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [StringLength(50)]
        public string Nome { get; set; }

        [StringLength(50)]
        public string Cognome { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Password")]
        public string Pass { get; set; }

        [Required]
        [StringLength(50)]
        public string Ruolo { get; set; }

        //AUTENTICAZIONE
        public static bool Autenticato(string username, string password)
        {

            ModelDBcontext db = new ModelDBcontext();
            var us = db.USER.Where(x => x.Username == username && x.Pass == password).FirstOrDefault();

            if (us != null)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DETTAGLIO> DETTAGLIO { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ORDINE> ORDINE { get; set; }
    }
}
