namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Proyectos
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Proyectos()
        {
            Cronograma = new HashSet<Cronograma>();
            EstadosProyecto = new HashSet<EstadosProyecto>();
            RolesProyecto = new HashSet<RolesProyecto>();
            SolicitudesCambio = new HashSet<SolicitudesCambio>();
        }

        [Key]
        public int ProyectoID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        public int? MetodologiaID { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaInicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaFinEstimada { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaFinReal { get; set; }

        public int? JefeProyectoID { get; set; }

        public int? ClienteID { get; set; }

        [StringLength(20)]
        public string Estado { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public int? CreadoPor { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cronograma> Cronograma { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstadosProyecto> EstadosProyecto { get; set; }

        public virtual Metodologias Metodologias { get; set; }

        public virtual Usuarios Usuarios { get; set; }

        public virtual Usuarios Usuarios1 { get; set; }

        public virtual Usuarios Usuarios2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RolesProyecto> RolesProyecto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SolicitudesCambio> SolicitudesCambio { get; set; }
    }
}
