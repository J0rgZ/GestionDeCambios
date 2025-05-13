namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Entregables
    {
        [Key]
        public int EntregableID { get; set; }

        public int? CronogramaID { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(500)]
        public string Descripcion { get; set; }

        public int? ECSID { get; set; }

        [Column(TypeName = "date")]
        public DateTime FechaEntregaEstimada { get; set; }

        [Column(TypeName = "date")]
        public DateTime? FechaEntregaReal { get; set; }

        public int? ResponsableID { get; set; }

        [StringLength(20)]
        public string Estado { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public int? CreadoPor { get; set; }

        public virtual Cronograma Cronograma { get; set; }

        public virtual ElementosConfiguracion ElementosConfiguracion { get; set; }

        public virtual Usuarios Usuarios { get; set; }

        public virtual Usuarios Usuarios1 { get; set; }
    }
}
