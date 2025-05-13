namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IntegracionGitHub")]
    public partial class IntegracionGitHub
    {
        [Key]
        public int IntegracionID { get; set; }

        public int? ECSID { get; set; }

        [Required]
        [StringLength(255)]
        public string RepositorioURL { get; set; }

        [StringLength(100)]
        public string RamaPrincipal { get; set; }

        [StringLength(100)]
        public string UltimoCommitHash { get; set; }

        public DateTime? UltimaSincronizacion { get; set; }

        public bool? Estado { get; set; }

        public virtual ElementosConfiguracion ElementosConfiguracion { get; set; }
    }
}
