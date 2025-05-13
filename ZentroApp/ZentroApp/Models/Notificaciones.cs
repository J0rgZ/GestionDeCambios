namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Notificaciones
    {
        [Key]
        public int NotificacionID { get; set; }

        public int? UsuarioID { get; set; }

        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Required]
        [StringLength(500)]
        public string Mensaje { get; set; }

        public DateTime? FechaEnvio { get; set; }

        public bool? Leido { get; set; }

        public DateTime? FechaLectura { get; set; }

        [Required]
        [StringLength(50)]
        public string TipoNotificacion { get; set; }

        public int? ReferenciaID { get; set; }

        public bool? Estado { get; set; }

        public virtual Usuarios Usuarios { get; set; }
    }
}
