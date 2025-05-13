namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AprobacionesCambio")]
    public partial class AprobacionesCambio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AprobacionesCambio()
        {
            OrdenesCambio = new HashSet<OrdenesCambio>();
        }

        [Key]
        public int AprobacionCambioID { get; set; }

        public int? InformeCambioID { get; set; }

        public int? ComiteAprobadorID { get; set; }

        public bool Aprobado { get; set; }

        [StringLength(500)]
        public string Comentarios { get; set; }

        public DateTime? FechaAprobacion { get; set; }

        public virtual Usuarios Usuarios { get; set; }

        public virtual InformesCambio InformesCambio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdenesCambio> OrdenesCambio { get; set; }
    }
}
