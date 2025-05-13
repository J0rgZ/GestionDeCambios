namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EstadosFlujo")]
    public partial class EstadosFlujo
    {
        [Key]
        public int EstadoFlujoID { get; set; }

        public int? SolicitudCambioID { get; set; }

        [StringLength(50)]
        public string EstadoAnterior { get; set; }

        [Required]
        [StringLength(50)]
        public string EstadoNuevo { get; set; }

        [StringLength(500)]
        public string Comentario { get; set; }

        public int? CambiadoPor { get; set; }

        public DateTime? FechaCambio { get; set; }

        public virtual Usuarios Usuarios { get; set; }

        public virtual SolicitudesCambio SolicitudesCambio { get; set; }
    }
}
