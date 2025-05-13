namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EstadosCambio")]
    public partial class EstadosCambio
    {
        [Key]
        public int EstadoCambioID { get; set; }

        public int? SolicitudCambioID { get; set; }

        public DateTime? FechaReporte { get; set; }

        public decimal PorcentajeAvance { get; set; }

        [Required]
        [StringLength(20)]
        public string EstadoActual { get; set; }

        [StringLength(1000)]
        public string Comentarios { get; set; }

        public int? ReportadoPor { get; set; }

        public virtual Usuarios Usuarios { get; set; }

        public virtual SolicitudesCambio SolicitudesCambio { get; set; }
    }
}
