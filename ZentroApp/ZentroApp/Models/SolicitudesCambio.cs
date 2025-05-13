namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SolicitudesCambio")]
    public partial class SolicitudesCambio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SolicitudesCambio()
        {
            EstadosCambio = new HashSet<EstadosCambio>();
            EstadosFlujo = new HashSet<EstadosFlujo>();
            InformesCambio = new HashSet<InformesCambio>();
            OrdenesCambio = new HashSet<OrdenesCambio>();
        }

        [Key]
        public int SolicitudCambioID { get; set; }

        public int? ProyectoID { get; set; }

        public int? ECSID { get; set; }

        public int? SolicitanteID { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(1000)]
        public string Descripcion { get; set; }

        [Required]
        [StringLength(500)]
        public string Justificacion { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoCambio { get; set; }

        [Required]
        [StringLength(20)]
        public string Prioridad { get; set; }

        [StringLength(20)]
        public string Estado { get; set; }

        public DateTime? FechaSolicitud { get; set; }

        public virtual ElementosConfiguracion ElementosConfiguracion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstadosCambio> EstadosCambio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstadosFlujo> EstadosFlujo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InformesCambio> InformesCambio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdenesCambio> OrdenesCambio { get; set; }

        public virtual Proyectos Proyectos { get; set; }

        public virtual Usuarios Usuarios { get; set; }
    }
}
