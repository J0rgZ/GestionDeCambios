namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VerificacionesSolicitante")]
    public partial class VerificacionesSolicitante
    {
        [Key]
        public int VerificacionID { get; set; }

        public int? AsignacionCambioID { get; set; }

        public int? SolicitanteID { get; set; }

        public bool Aprobado { get; set; }

        [StringLength(500)]
        public string Comentarios { get; set; }

        public DateTime? FechaVerificacion { get; set; }

        public virtual AsignacionesCambio AsignacionesCambio { get; set; }

        public virtual Usuarios Usuarios { get; set; }
    }
}
