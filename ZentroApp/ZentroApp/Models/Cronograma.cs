namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cronograma")]
    public partial class Cronograma
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Cronograma()
        {
            Entregables = new HashSet<Entregables>();
        }

        public int CronogramaID { get; set; }

        public int? ProyectoID { get; set; }

        public int? FaseID { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaInicio { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaFinEstimada { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaFinReal { get; set; }

        public int? ResponsableID { get; set; }

        [StringLength(500)]
        public string Comentarios { get; set; }

        [StringLength(20)]
        public string Estado { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public int? CreadoPor { get; set; }

        public virtual Usuarios Usuarios { get; set; }

        public virtual Fases Fases { get; set; }

        public virtual Proyectos Proyectos { get; set; }

        public virtual Usuarios Usuarios1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Entregables> Entregables { get; set; }
    }
}
