namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CambiosGitHub")]
    public partial class CambiosGitHub
    {
        [Key]
        public int CambioGitHubID { get; set; }

        public int? AsignacionCambioID { get; set; }

        [Required]
        [StringLength(100)]
        public string CommitHash { get; set; }

        [StringLength(500)]
        public string MensajeCommit { get; set; }

        public DateTime FechaCommit { get; set; }

        [StringLength(100)]
        public string AutorCommit { get; set; }

        [StringLength(20)]
        public string EstadoSincronizacion { get; set; }

        public virtual AsignacionesCambio AsignacionesCambio { get; set; }
    }
}
