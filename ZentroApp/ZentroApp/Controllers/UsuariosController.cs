using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ZentroApp.Models;

namespace ZentroApp.Controllers
{
    public class UsuariosController : Controller
    {
        private ModeloSistema db = new ModeloSistema();

        // GET: Usuarios/Index - Solo visible para administradores
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            // Verificar si el usuario está autenticado y es administrador
            if (Session["UserID"] == null || Session["RolNombre"].ToString() != "Administrador")
            {
                return RedirectToAction("Login");
            }

            var usuarios = Usuarios.ListarUsuarios(db);
            return View(usuarios);
        }

        // GET: Usuarios/Login - Accesible para todos
        public ActionResult Login()
        {
            // Si ya está autenticado, redirigir a la página de inicio
            if (Session["UserID"] != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        // POST: Usuarios/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string nombreUsuario, string contrasena, int? rolID)
        {
            try
            {
                // Intentar iniciar sesión
                var usuario = Usuarios.IniciarSesion(db, nombreUsuario, contrasena, rolID);

                if (usuario != null)
                {
                    // Variables para el rol
                    int? userRoleID = usuario.RolID;
                    string rolNombre = "Usuario";

                    // Determinar qué rol usar para la sesión
                    if (rolID.HasValue && rolID > 0)
                    {
                        // Si se seleccionó un rol específico, verificar si el usuario tiene permiso para ese rol
                        var selectedRole = db.Roles.Find(rolID.Value);
                        if (selectedRole == null)
                        {
                            ModelState.AddModelError("", "El rol seleccionado no es válido.");
                            return View();
                        }

                        // Verificar si el usuario tiene permiso para ese rol (implementar lógica según necesidad)
                        bool tienePermiso = VerificarPermisoRol(usuario.UsuarioID, rolID.Value);
                        if (!tienePermiso)
                        {
                            ModelState.AddModelError("", "No tienes permiso para acceder con ese rol.");
                            return View();
                        }

                        userRoleID = rolID;
                        rolNombre = selectedRole.Nombre;
                    }
                    else if (usuario.RolID.HasValue)
                    {
                        // Si no se seleccionó un rol, usar el rol predeterminado del usuario
                        var userRole = db.Roles.Find(usuario.RolID.Value);
                        if (userRole != null)
                        {
                            rolNombre = userRole.Nombre;
                        }
                    }

                    // Guardar información en la sesión
                    Session["UserID"] = usuario.UsuarioID;
                    Session["UserName"] = usuario.NombreUsuario;
                    Session["FullName"] = usuario.Nombre + " " + usuario.Apellido;
                    Session["RolID"] = userRoleID;
                    Session["RolNombre"] = rolNombre;
                    Session["Avatar"] = ObtenerRutaAvatar(rolNombre); // Nueva función para avatar

                    // Establecer cookies de autenticación
                    FormsAuthentication.SetAuthCookie(usuario.NombreUsuario, false);

                    // Mensaje de bienvenida
                    TempData["Mensaje"] = $"Bienvenido/a {usuario.Nombre}. Has ingresado como {rolNombre}.";
                    TempData["RolActual"] = rolNombre;

                    // Redireccionar a la página de inicio
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Nombre de usuario o contraseña incorrectos.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                // Registrar el error
                ModelState.AddModelError("", "Error al iniciar sesión: " + ex.Message);
                return View();
            }
        }

        // GET: Usuarios/Logout
        public ActionResult Logout()
        {
            // Limpiar la sesión
            Session.Clear();
            Session.Abandon();

            // Eliminar cookie de autenticación
            FormsAuthentication.SignOut();

            // Mensaje de confirmación
            TempData["Mensaje"] = "Has cerrado sesión correctamente.";

            return RedirectToAction("Login");
        }

        // GET: Usuarios/MiPerfil
        [Authorize]
        public ActionResult MiPerfil()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login");
            }

            int usuarioID = Convert.ToInt32(Session["UserID"]);
            Usuarios usuario = db.Usuarios.Find(usuarioID);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            // Agregar información adicional para la vista
            ViewBag.RolActual = Session["RolNombre"].ToString();
            ViewBag.Roles = db.Roles.ToList();

            return View(usuario);
        }

        // GET: Usuarios/CambiarContrasena
        [Authorize]
        public ActionResult CambiarContrasena()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        // POST: Usuarios/CambiarContrasena
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CambiarContrasena(string contrasenaActual, string nuevaContrasena, string confirmarContrasena)
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login");
            }

            int usuarioID = Convert.ToInt32(Session["UserID"]);

            if (string.IsNullOrEmpty(contrasenaActual) || string.IsNullOrEmpty(nuevaContrasena) || string.IsNullOrEmpty(confirmarContrasena))
            {
                ModelState.AddModelError("", "Todos los campos son obligatorios.");
                return View();
            }

            if (nuevaContrasena != confirmarContrasena)
            {
                ModelState.AddModelError("", "La nueva contraseña y la confirmación no coinciden.");
                return View();
            }

            try
            {
                bool resultado = CambiarContrasenaUsuario(usuarioID, contrasenaActual, nuevaContrasena);
                if (resultado)
                {
                    TempData["Mensaje"] = "Contraseña cambiada exitosamente.";
                    return RedirectToAction("MiPerfil");
                }
                else
                {
                    ModelState.AddModelError("", "La contraseña actual es incorrecta.");
                    return View();
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al cambiar la contraseña: " + ex.Message);
                return View();
            }
        }

        // Métodos auxiliares
        private bool VerificarPermisoRol(int usuarioID, int rolID)
        {
            // Implementar lógica para verificar si el usuario tiene permiso para el rol seleccionado
            // Por ahora, devolvemos true para simplificar
            return true;
        }

        private bool CambiarContrasenaUsuario(int usuarioID, string contrasenaActual, string nuevaContrasena)
        {
            // Implementar lógica para cambiar la contraseña
            // Este es un método de ejemplo y debe ser adaptado a tu modelo de datos
            var usuario = db.Usuarios.Find(usuarioID);
            if (usuario == null)
            {
                return false;
            }

            // Verificar contraseña actual (suponiendo que tienes un método para verificar)
            // Esto es un ejemplo y debe adaptarse a tu sistema de autenticación
            if (usuario.Contrasena != contrasenaActual) // En un sistema real, usar hashing
            {
                return false;
            }

            // Cambiar contraseña
            usuario.Contrasena = nuevaContrasena; // En un sistema real, aplicar hash a la contraseña
            db.SaveChanges();

            return true;
        }

        private string ObtenerRutaAvatar(string rolNombre)
        {
            // Devolver la ruta del avatar según el rol
            switch (rolNombre)
            {
                case "Administrador":
                    return "/Content/Images/avatar-admin.png";
                case "Jefe de Proyecto":
                    return "/Content/Images/avatar-jefe.png";
                case "Analista":
                    return "/Content/Images/avatar-analista.png";
                case "Desarrollador":
                    return "/Content/Images/avatar-dev.png";
                case "QA":
                    return "/Content/Images/avatar-qa.png";
                case "Cliente":
                    return "/Content/Images/avatar-cliente.png";
                case "Comité de Cambios":
                    return "/Content/Images/avatar-comite.png";
                default:
                    return "/Content/Images/avatar-default.png";
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}