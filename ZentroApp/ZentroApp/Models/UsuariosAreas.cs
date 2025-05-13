namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UsuariosAreas
    {
        [Key]
        public int UsuarioAreaID { get; set; }

        public int? UsuarioID { get; set; }

        public int? AreaID { get; set; }

        public bool? EsPrincipal { get; set; }

        public DateTime? FechaAsignacion { get; set; }

        public bool? Estado { get; set; }

        public virtual AreasTrabajo AreasTrabajo { get; set; }

        public virtual Usuarios Usuarios { get; set; }
    }
}
