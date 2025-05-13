namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HistorialECS
    {
        public int HistorialECSID { get; set; }

        public int? ECSID { get; set; }

        public int? OrdenCambioID { get; set; }

        [StringLength(20)]
        public string VersionAnterior { get; set; }

        [Required]
        [StringLength(20)]
        public string VersionNueva { get; set; }

        public DateTime? FechaCambio { get; set; }

        public int? CambiadoPor { get; set; }

        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; }

        public virtual ElementosConfiguracion ElementosConfiguracion { get; set; }

        public virtual Usuarios Usuarios { get; set; }

        public virtual OrdenesCambio OrdenesCambio { get; set; }
    }
}
