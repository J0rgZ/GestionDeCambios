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
    [Authorize]
    public class UsuariosController : Controller
    {
        private ModeloSistema db = new ModeloSistema();

        // GET: Usuarios
        [Authorize(Roles = "Administrador")]
        public ActionResult Index()
        {
            var usuarios = Usuarios.ListarUsuarios(db);
            return View(usuarios);
        }

        // GET: Usuarios/Details/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Usuarios usuario = Usuarios.ObtenerUsuarioPorID(db, id.Value);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            ViewBag.RolID = new SelectList(db.Roles, "RolID", "NombreRol");
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create([Bind(Include = "UsuarioID,NombreUsuario,Contrasena,Nombre,Apellido,Email,Telefono,RolID")] Usuarios usuario)
        {
            if (ModelState.IsValid)
            {
                if (Usuarios.AgregarUsuario(db, usuario))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo agregar el usuario. Verifique que el nombre de usuario y correo no existan en el sistema.");
                }
            }

            ViewBag.RolID = new SelectList(db.Roles, "RolID", "NombreRol", usuario.RolID);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Usuarios usuario = Usuarios.ObtenerUsuarioPorID(db, id.Value);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            // No enviar la contraseña a la vista
            usuario.Contrasena = string.Empty;

            ViewBag.RolID = new SelectList(db.Roles, "RolID", "NombreRol", usuario.RolID);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit([Bind(Include = "UsuarioID,NombreUsuario,Contrasena,Nombre,Apellido,Email,Telefono,RolID,Estado")] Usuarios usuario)
        {
            if (ModelState.IsValid)
            {
                if (Usuarios.EditarUsuario(db, usuario))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "No se pudo editar el usuario. Verifique que el nombre de usuario y correo no existan en el sistema.");
                }
            }

            ViewBag.RolID = new SelectList(db.Roles, "RolID", "NombreRol", usuario.RolID);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Usuarios usuario = Usuarios.ObtenerUsuarioPorID(db, id.Value);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Usuarios.EliminarUsuario(db, id))
            {
                return RedirectToAction("Index");
            }
            else
            {
                // Si no se pudo eliminar, volver a la vista de detalle con un mensaje de error
                ModelState.AddModelError("", "No se pudo eliminar el usuario.");
                return View(Usuarios.ObtenerUsuarioPorID(db, id));
            }
        }

        // GET: Usuarios/MiPerfil
        [Authorize]
        public ActionResult MiPerfil()
        {
            string nombreUsuario = User.Identity.Name;
            Usuarios usuario = db.Usuarios.FirstOrDefault(u => u.NombreUsuario == nombreUsuario);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/CambiarContrasena
        [Authorize]
        public ActionResult CambiarContrasena()
        {
            return View();
        }

        // POST: Usuarios/CambiarContrasena
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult CambiarContrasena(string contrasenaActual, string nuevaContrasena, string confirmarContrasena)
        {
            // Validar que la nueva contraseña coincida con la confirmación
            if (nuevaContrasena != confirmarContrasena)
            {
                ModelState.AddModelError("", "La nueva contraseña y la confirmación no coinciden.");
                return View();
            }

            // Obtener el usuario actual
            string nombreUsuario = User.Identity.Name;
            Usuarios usuario = db.Usuarios.FirstOrDefault(u => u.NombreUsuario == nombreUsuario);

            if (usuario == null)
            {
                return HttpNotFound();
            }

            // Cambiar la contraseña
            if (Usuarios.CambiarContrasena(db, usuario.UsuarioID, contrasenaActual, nuevaContrasena))
            {
                TempData["Mensaje"] = "La contraseña ha sido cambiada exitosamente.";
                return RedirectToAction("MiPerfil");
            }
            else
            {
                ModelState.AddModelError("", "La contraseña actual es incorrecta o no se pudo cambiar la contraseña.");
                return View();
            }
        }

        // GET: Usuarios/Login (Sin autorización para permitir el acceso a todos)
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            // Si ya está autenticado, redirigir a la página de inicio
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: Usuarios/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(string nombreUsuario, string contrasena, string rolSeleccionado, string returnUrl)
        {
            try
            {
                // Intentar iniciar sesión
                var usuario = Usuarios.IniciarSesion(db, nombreUsuario, contrasena);

                if (usuario != null)
                {
                    // Verificar si se seleccionó un rol específico
                    if (!string.IsNullOrEmpty(rolSeleccionado))
                    {
                        int rolID = int.Parse(rolSeleccionado);

                        // Verificar si el usuario tiene el rol seleccionado
                        if (usuario.RolID != rolID)
                        {
                            // Verificar si el usuario tiene acceso a múltiples roles
                            // En una aplicación real, podría haber una tabla de roles por usuario
                            // Aquí simplemente permitimos el acceso para pruebas

                            // Si se requiere restricción estricta, descomentar:
                            // ModelState.AddModelError("", "No tienes permisos para acceder con el rol seleccionado.");
                            // return View();

                            // Obtener el rol seleccionado para la sesión
                            var rol = db.Roles.Find(rolID);
                            if (rol == null)
                            {
                                ModelState.AddModelError("", "El rol seleccionado no es válido.");
                                return View();
                            }

                            // Crear datos de usuario con el rol seleccionado
                            string userData = rol.Nombre;

                            // Establecer la cookie con el rol seleccionado
                            FormsAuthentication.SetAuthCookie(usuario.NombreUsuario, false);

                            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                                1,                           // Versión
                                usuario.NombreUsuario,       // Nombre de usuario
                                DateTime.Now,                // Fecha de emisión
                                DateTime.Now.AddHours(8),    // Fecha de expiración (8 horas)
                                false,                       // No persistente
                                userData                     // Datos de usuario (rol seleccionado)
                            );

                            // Cifrar el ticket
                            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                            // Crear la cookie
                            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                            Response.Cookies.Add(authCookie);

                            // Guardar información en la sesión
                            Session["UserID"] = usuario.UsuarioID;
                            Session["UserName"] = usuario.NombreUsuario;
                            Session["FullName"] = usuario.Nombre + " " + usuario.Apellido;
                            Session["RolID"] = rolID;
                            Session["RolNombre"] = rol.Nombre;

                            // Registrar acceso con rol seleccionado
                            TempData["Mensaje"] = $"Bienvenido/a {usuario.Nombre}. Has ingresado como {rol.Nombre}.";

                            // Redireccionar
                            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            {
                                return Redirect(returnUrl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                    }

                    // Si no se seleccionó un rol específico o el usuario tiene ese rol,
                    // proceder con el inicio de sesión normal

                    // Crear ticket de autenticación
                    FormsAuthentication.SetAuthCookie(usuario.NombreUsuario, false);

                    // Agregar rol del usuario a la cookie
                    string userData = usuario.RolID.HasValue ? db.Roles.Find(usuario.RolID.Value).Nombre : "";

                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                        1,                           // Versión
                        usuario.NombreUsuario,       // Nombre de usuario
                        DateTime.Now,                // Fecha de emisión
                        DateTime.Now.AddHours(8),    // Fecha de expiración (8 horas)
                        false,                       // No persistente
                        userData                     // Datos de usuario (rol)
                    );

                    // Cifrar el ticket
                    string encryptedTicket = FormsAuthentication.Encrypt(ticket);

                    // Crear la cookie
                    HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                    Response.Cookies.Add(authCookie);

                    // Guardar información en la sesión
                    Session["UserID"] = usuario.UsuarioID;
                    Session["UserName"] = usuario.NombreUsuario;
                    Session["FullName"] = usuario.Nombre + " " + usuario.Apellido;
                    Session["RolID"] = usuario.RolID;
                    Session["RolNombre"] = userData;

                    // Mensaje de bienvenida
                    var rolNombre = usuario.RolID.HasValue ? db.Roles.Find(usuario.RolID.Value).Nombre : "Usuario";
                    TempData["Mensaje"] = $"Bienvenido/a {usuario.Nombre}. Has ingresado como {rolNombre}.";

                    // Redireccionar a la URL de retorno o a la página de inicio
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
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
        [AllowAnonymous]
        public ActionResult Logout()
        {
            // Cerrar sesión
            FormsAuthentication.SignOut();

            // Limpiar la cookie
            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);

            // Limpiar la sesión
            Session.Clear();
            Session.Abandon();

            // Mensaje de confirmación
            TempData["Mensaje"] = "Has cerrado sesión correctamente.";

            return RedirectToAction("Login");
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