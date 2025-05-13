namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("InformesCambio")]
    public partial class InformesCambio
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InformesCambio()
        {
            AprobacionesCambio = new HashSet<AprobacionesCambio>();
        }

        [Key]
        public int InformeCambioID { get; set; }

        public int? SolicitudCambioID { get; set; }

        public int? EvaluadorID { get; set; }

        [StringLength(500)]
        public string ImpactoTecnico { get; set; }

        public decimal? ImpactoCosto { get; set; }

        public int? ImpactoTiempo { get; set; }

        [Required]
        [StringLength(50)]
        public string Recomendacion { get; set; }

        [StringLength(500)]
        public string Justificacion { get; set; }

        public DateTime? FechaEvaluacion { get; set; }

        [StringLength(20)]
        public string Estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AprobacionesCambio> AprobacionesCambio { get; set; }

        public virtual Usuarios Usuarios { get; set; }

        public virtual SolicitudesCambio SolicitudesCambio { get; set; }
    }
}
