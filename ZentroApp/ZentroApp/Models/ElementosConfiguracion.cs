namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ElementosConfiguracion")]
    public partial class ElementosConfiguracion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ElementosConfiguracion()
        {
            AsignacionesCambio = new HashSet<AsignacionesCambio>();
            Entregables = new HashSet<Entregables>();
            HistorialECS = new HashSet<HistorialECS>();
            IntegracionGitHub = new HashSet<IntegracionGitHub>();
            SolicitudesCambio = new HashSet<SolicitudesCambio>();
        }

        [Key]
        public int ECSID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required]
        [StringLength(50)]
        public string Tipo { get; set; }

        [StringLength(20)]
        public string Version { get; set; }

        [StringLength(255)]
        public string RutaArchivo { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public int? CreadoPor { get; set; }

        public DateTime? UltimaModificacion { get; set; }

        public int? ModificadoPor { get; set; }

        public bool? Estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AsignacionesCambio> AsignacionesCambio { get; set; }

        public virtual Usuarios Usuarios { get; set; }

        public virtual Usuarios Usuarios1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Entregables> Entregables { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistorialECS> HistorialECS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IntegracionGitHub> IntegracionGitHub { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SolicitudesCambio> SolicitudesCambio { get; set; }
    }
}
