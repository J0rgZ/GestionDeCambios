namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RolesProyecto")]
    public partial class RolesProyecto
    {
        [Key]
        public int RolProyectoID { get; set; }

        public int? ProyectoID { get; set; }

        public int? UsuarioID { get; set; }

        public int? RolID { get; set; }

        public DateTime? FechaAsignacion { get; set; }

        public int? AsignadoPor { get; set; }

        public bool? Estado { get; set; }

        public virtual Proyectos Proyectos { get; set; }

        public virtual Roles Roles { get; set; }

        public virtual Usuarios Usuarios { get; set; }

        public virtual Usuarios Usuarios1 { get; set; }
    }
}
