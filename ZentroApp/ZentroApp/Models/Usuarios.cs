namespace ZentroApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public partial class Usuarios
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Usuarios()
        {
            AprobacionesCambio = new HashSet<AprobacionesCambio>();
            AreasTrabajo = new HashSet<AreasTrabajo>();
            AsignacionesCambio = new HashSet<AsignacionesCambio>();
            Cronograma = new HashSet<Cronograma>();
            Cronograma1 = new HashSet<Cronograma>();
            ElementosConfiguracion = new HashSet<ElementosConfiguracion>();
            ElementosConfiguracion1 = new HashSet<ElementosConfiguracion>();
            Entregables = new HashSet<Entregables>();
            Entregables1 = new HashSet<Entregables>();
            EstadosCambio = new HashSet<EstadosCambio>();
            EstadosFlujo = new HashSet<EstadosFlujo>();
            EstadosProyecto = new HashSet<EstadosProyecto>();
            HistorialECS = new HashSet<HistorialECS>();
            InformesCambio = new HashSet<InformesCambio>();
            Metodologias = new HashSet<Metodologias>();
            Notificaciones = new HashSet<Notificaciones>();
            OrdenesCambio = new HashSet<OrdenesCambio>();
            Proyectos = new HashSet<Proyectos>();
            Proyectos1 = new HashSet<Proyectos>();
            Proyectos2 = new HashSet<Proyectos>();
            RolesProyecto = new HashSet<RolesProyecto>();
            RolesProyecto1 = new HashSet<RolesProyecto>();
            SolicitudesCambio = new HashSet<SolicitudesCambio>();
            UsuariosAreas = new HashSet<UsuariosAreas>();
            VerificacionesSolicitante = new HashSet<VerificacionesSolicitante>();
        }

        [Key]
        public int UsuarioID { get; set; }

        [Required]
        [StringLength(50)]
        public string NombreUsuario { get; set; }

        [Required]
        [StringLength(255)]
        public string Contrasena { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Telefono { get; set; }

        public int? RolID { get; set; }

        public DateTime? FechaCreacion { get; set; }

        public DateTime? UltimoAcceso { get; set; }

        public bool? Estado { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AprobacionesCambio> AprobacionesCambio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AreasTrabajo> AreasTrabajo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AsignacionesCambio> AsignacionesCambio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cronograma> Cronograma { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cronograma> Cronograma1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ElementosConfiguracion> ElementosConfiguracion { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ElementosConfiguracion> ElementosConfiguracion1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Entregables> Entregables { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Entregables> Entregables1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstadosCambio> EstadosCambio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstadosFlujo> EstadosFlujo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EstadosProyecto> EstadosProyecto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistorialECS> HistorialECS { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InformesCambio> InformesCambio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Metodologias> Metodologias { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notificaciones> Notificaciones { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrdenesCambio> OrdenesCambio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Proyectos> Proyectos { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Proyectos> Proyectos1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Proyectos> Proyectos2 { get; set; }

        public virtual Roles Roles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RolesProyecto> RolesProyecto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RolesProyecto> RolesProyecto1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SolicitudesCambio> SolicitudesCambio { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UsuariosAreas> UsuariosAreas { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VerificacionesSolicitante> VerificacionesSolicitante { get; set; }

        // Métodos agregados para gestión de usuarios

        /// <summary>
        /// Método para agregar un nuevo usuario al sistema
        /// </summary>
        /// <param name="db">Contexto de la base de datos</param>
        /// <param name="usuario">Objeto usuario a agregar</param>
        /// <returns>True si el usuario fue agregado correctamente, False en caso contrario</returns>
        public static bool AgregarUsuario(ModeloSistema db, Usuarios usuario)
        {
            try
            {
                // Validar que el nombre de usuario no exista
                if (db.Usuarios.Any(u => u.NombreUsuario == usuario.NombreUsuario))
                {
                    return false;
                }

                // Validar que el email no exista
                if (db.Usuarios.Any(u => u.Email == usuario.Email))
                {
                    return false;
                }

                // Cifrar la contraseña
                usuario.Contrasena = EncriptarContrasena(usuario.Contrasena);

                // Establecer valores por defecto
                usuario.FechaCreacion = DateTime.Now;
                usuario.UltimoAcceso = null;
                usuario.Estado = true;

                // Agregar el usuario a la base de datos
                db.Usuarios.Add(usuario);
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Método para editar un usuario existente
        /// </summary>
        /// <param name="db">Contexto de la base de datos</param>
        /// <param name="usuario">Objeto usuario con los datos actualizados</param>
        /// <returns>True si el usuario fue editado correctamente, False en caso contrario</returns>
        public static bool EditarUsuario(ModeloSistema db, Usuarios usuario)
        {
            try
            {
                // Obtener el usuario original
                var usuarioOriginal = db.Usuarios.Find(usuario.UsuarioID);

                if (usuarioOriginal == null)
                {
                    return false;
                }

                // Validar que el nombre de usuario no exista (si se cambió)
                if (usuario.NombreUsuario != usuarioOriginal.NombreUsuario &&
                    db.Usuarios.Any(u => u.NombreUsuario == usuario.NombreUsuario))
                {
                    return false;
                }

                // Validar que el email no exista (si se cambió)
                if (usuario.Email != usuarioOriginal.Email &&
                    db.Usuarios.Any(u => u.Email == usuario.Email))
                {
                    return false;
                }

                // Actualizar los datos del usuario
                usuarioOriginal.NombreUsuario = usuario.NombreUsuario;
                usuarioOriginal.Nombre = usuario.Nombre;
                usuarioOriginal.Apellido = usuario.Apellido;
                usuarioOriginal.Email = usuario.Email;
                usuarioOriginal.Telefono = usuario.Telefono;
                usuarioOriginal.RolID = usuario.RolID;
                usuarioOriginal.Estado = usuario.Estado;

                // Si la contraseña es diferente (y no está vacía), actualizarla
                if (!string.IsNullOrEmpty(usuario.Contrasena) &&
                    usuario.Contrasena != usuarioOriginal.Contrasena)
                {
                    usuarioOriginal.Contrasena = EncriptarContrasena(usuario.Contrasena);
                }

                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Método para eliminar un usuario (desactivación lógica)
        /// </summary>
        /// <param name="db">Contexto de la base de datos</param>
        /// <param name="usuarioID">ID del usuario a eliminar</param>
        /// <returns>True si el usuario fue eliminado correctamente, False en caso contrario</returns>
        public static bool EliminarUsuario(ModeloSistema db, int usuarioID)
        {
            try
            {
                var usuario = db.Usuarios.Find(usuarioID);

                if (usuario == null)
                {
                    return false;
                }

                // Eliminación lógica (desactivar)
                usuario.Estado = false;
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Método para obtener la lista de todos los usuarios activos
        /// </summary>
        /// <param name="db">Contexto de la base de datos</param>
        /// <returns>Lista de usuarios activos</returns>
        public static List<Usuarios> ListarUsuarios(ModeloSistema db)
        {
            return db.Usuarios.Where(u => u.Estado == true).ToList();
        }

        /// <summary>
        /// Método para buscar un usuario por su ID
        /// </summary>
        /// <param name="db">Contexto de la base de datos</param>
        /// <param name="usuarioID">ID del usuario a buscar</param>
        /// <returns>Objeto usuario si se encuentra, null en caso contrario</returns>
        public static Usuarios ObtenerUsuarioPorID(ModeloSistema db, int usuarioID)
        {
            return db.Usuarios.Find(usuarioID);
        }

        /// <summary>
        /// Método para iniciar sesión
        /// </summary>
        /// <param name="db">Contexto de la base de datos</param>
        /// <param name="nombreUsuario">Nombre de usuario</param>
        /// <param name="contrasena">Contraseña sin encriptar</param>
        /// <returns>Objeto usuario si las credenciales son correctas, null en caso contrario</returns>
        public static Usuarios IniciarSesion(ModeloSistema db, string nombreUsuario, string contrasena)
        {
            // Encriptar la contraseña para compararla
            string contrasenaEncriptada = EncriptarContrasena(contrasena);

            // Buscar usuario por nombre y contraseña
            var usuario = db.Usuarios
                .FirstOrDefault(u => u.NombreUsuario == nombreUsuario &&
                                   u.Contrasena == contrasenaEncriptada &&
                                   u.Estado == true);

            if (usuario != null)
            {
                // Actualizar último acceso
                usuario.UltimoAcceso = DateTime.Now;
                db.SaveChanges();
            }

            return usuario;
        }

        /// <summary>
        /// Método para validar el cambio de contraseña
        /// </summary>
        /// <param name="db">Contexto de la base de datos</param>
        /// <param name="usuarioID">ID del usuario</param>
        /// <param name="contrasenaActual">Contraseña actual sin encriptar</param>
        /// <param name="nuevaContrasena">Nueva contraseña sin encriptar</param>
        /// <returns>True si el cambio fue exitoso, False en caso contrario</returns>
        public static bool CambiarContrasena(ModeloSistema db, int usuarioID, string contrasenaActual, string nuevaContrasena)
        {
            try
            {
                var usuario = db.Usuarios.Find(usuarioID);

                if (usuario == null)
                {
                    return false;
                }

                // Verificar la contraseña actual
                string contrasenaEncriptada = EncriptarContrasena(contrasenaActual);

                if (usuario.Contrasena != contrasenaEncriptada)
                {
                    return false;
                }

                // Actualizar a la nueva contraseña
                usuario.Contrasena = EncriptarContrasena(nuevaContrasena);
                db.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Método privado para encriptar contraseñas usando SHA256
        /// </summary>
        /// <param name="contrasena">Contraseña en texto plano</param>
        /// <returns>Contraseña encriptada en formato hexadecimal</returns>
        private static string EncriptarContrasena(string contrasena)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(contrasena));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
