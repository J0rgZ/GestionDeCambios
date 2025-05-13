namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AsignacionesCambio")]
    public partial class AsignacionesCambio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AsignacionesCambio()
        {
            CambiosGitHub = new HashSet<CambiosGitHub>();
            VerificacionesSolicitante = new HashSet<VerificacionesSolicitante>();
        }

        [Key]
        public int AsignacionCambioID { get; set; }

        public int? OrdenCambioID { get; set; }

        public int? ECSID { get; set; }

        public int? ResponsableID { get; set; }

        public int? AreaID { get; set; }

        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; }

        public DateTime? FechaAsignacion { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaFinalizacionEstimada { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaFinalizacionReal { get; set; }

        [StringLength(20)]
        public string Estado { get; set; }

        [StringLength(500)]
        public string Comentarios { get; set; }

        public virtual AreasTrabajo AreasTrabajo { get; set; }

        public virtual ElementosConfiguracion ElementosConfiguracion { get; set; }

        public virtual OrdenesCambio OrdenesCambio { get; set; }

        public virtual Usuarios Usuarios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CambiosGitHub> CambiosGitHub { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VerificacionesSolicitante> VerificacionesSolicitante { get; set; }
    }
}
