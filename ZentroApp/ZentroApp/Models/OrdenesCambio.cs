namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OrdenesCambio")]
    public partial class OrdenesCambio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public OrdenesCambio()
        {
            AsignacionesCambio = new HashSet<AsignacionesCambio>();
            HistorialECS = new HashSet<HistorialECS>();
        }

        [Key]
        public int OrdenCambioID { get; set; }

        public int? SolicitudCambioID { get; set; }

        public int? AprobacionCambioID { get; set; }

        public int? JefeProyectoID { get; set; }

        public DateTime? FechaEmision { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaImplementacionEstimada { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaImplementacionReal { get; set; }

        [StringLength(20)]
        public string Estado { get; set; }

        [StringLength(500)]
        public string Comentarios { get; set; }

        public virtual AprobacionesCambio AprobacionesCambio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AsignacionesCambio> AsignacionesCambio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistorialECS> HistorialECS { get; set; }

        public virtual Usuarios Usuarios { get; set; }

        public virtual SolicitudesCambio SolicitudesCambio { get; set; }
    }
}
