using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZentroApp.Models;

namespace ZentroApp.Controllers
{
    public class HomeController : Controller
    {
        private ModeloSistema db = new ModeloSistema();

        // GET: Home
        public ActionResult Index()
        {
            try
            {
                // Recuperar información del usuario actual
                int userId = (int)Session["UserID"];
                string rolNombre = (string)Session["RolNombre"];

                // Modelo para pasar a la vista
                ViewBag.Rol = rolNombre;
                ViewBag.NombreCompleto = Session["FullName"];

                // Obtener mensajes o notificaciones pendientes
                var notificaciones = ObtenerNotificacionesUsuario(userId);

                // Pasar datos a la vista
                ViewBag.Notificaciones = notificaciones;
                ViewBag.CantidadNotificaciones = notificaciones.Count;

                // Obtener estadísticas según el rol
                switch (rolNombre)
                {
                    case "Administrador":
                        ViewBag.TotalUsuarios = db.Usuarios.Count(u => u.Estado == true);
                        ViewBag.TotalProyectos = db.Proyectos.Count();
                        ViewBag.TotalCambiosPendientes = db.SolicitudesCambio.Count(s => s.Estado == "Pendiente");
                        break;

                    case "Jefe de Proyecto":
                        ViewBag.MisProyectos = db.Proyectos.Count(p => p.JefeProyectoID == userId);
                        ViewBag.TotalCambiosPendientes = db.SolicitudesCambio.Count(s => s.Estado == "Pendiente" &&
                            db.Proyectos.Any(p => p.ProyectoID == s.ProyectoID && p.JefeProyectoID == userId));
                        break;

                    case "Desarrollador":
                        ViewBag.TareasAsignadas = db.AsignacionesCambio.Count(a => a.ResponsableID == userId && a.Estado != "Completada");
                        break;

                    case "Cliente":
                        ViewBag.MisSolicitudes = db.SolicitudesCambio.Count(s => s.SolicitanteID == userId);
                        ViewBag.SolicitudesAprobadas = db.SolicitudesCambio.Count(s => s.SolicitanteID == userId && s.Estado == "Aprobada");
                        ViewBag.SolicitudesRechazadas = db.SolicitudesCambio.Count(s => s.SolicitanteID == userId && s.Estado == "Rechazada");
                        break;

                    default:
                        // Estadísticas generales para otros roles
                        ViewBag.TotalProyectosActivos = db.Proyectos.Count(p => p.Estado == "Activo");
                        break;
                }

                return View();
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al cargar el dashboard: " + ex.Message;
                return View();
            }
        }

        // Método para obtener notificaciones del usuario
        private List<NotificacionViewModel> ObtenerNotificacionesUsuario(int usuarioId)
        {
            try
            {
                // Obtener notificaciones de la base de datos
                var notificaciones = db.Notificaciones
                    .Where(n => n.UsuarioID == usuarioId && n.Leido == false)
                    .OrderByDescending(n => n.FechaEnvio)
                    .Take(5)
                    .Select(n => new NotificacionViewModel
                    {
                        NotificacionID = n.NotificacionID,
                        Mensaje = n.Mensaje,
                        Tipo = n.TipoNotificacion,
                        Fecha = n.FechaEnvio.Value
                    })
                    .ToList();

                // Si no hay tabla de notificaciones o para pruebas, generar algunas notificaciones de ejemplo
                if (notificaciones.Count == 0)
                {
                    notificaciones = new List<NotificacionViewModel>
                    {
                        new NotificacionViewModel
                        {
                            NotificacionID = 1,
                            Mensaje = "Bienvenido al sistema ZentroApp",
                            Tipo = "info",
                            Fecha = DateTime.Now,
                            URL = "#"
                        }
                    };

                    // Agregar más notificaciones según el rol
                    string rolNombre = (string)Session["RolNombre"];

                    if (rolNombre == "Administrador")
                    {
                        notificaciones.Add(new NotificacionViewModel
                        {
                            NotificacionID = 2,
                            Mensaje = "Hay 3 usuarios nuevos pendientes de activación",
                            Tipo = "warning",
                            Fecha = DateTime.Now.AddHours(-2),
                            URL = "/Usuarios/Index"
                        });
                    }
                    else if (rolNombre == "Jefe de Proyecto")
                    {
                        notificaciones.Add(new NotificacionViewModel
                        {
                            NotificacionID = 2,
                            Mensaje = "Tienes 2 solicitudes de cambio pendientes de revisión",
                            Tipo = "warning",
                            Fecha = DateTime.Now.AddHours(-1),
                            URL = "/SolicitudesCambio/Index"
                        });
                    }
                    else if (rolNombre == "Desarrollador")
                    {
                        notificaciones.Add(new NotificacionViewModel
                        {
                            NotificacionID = 2,
                            Mensaje = "Se te ha asignado una nueva tarea",
                            Tipo = "success",
                            Fecha = DateTime.Now.AddMinutes(-30),
                            URL = "/AsignacionesCambio/Index"
                        });
                    }
                    else if (rolNombre == "Cliente")
                    {
                        notificaciones.Add(new NotificacionViewModel
                        {
                            NotificacionID = 2,
                            Mensaje = "Tu solicitud de cambio ha sido aprobada",
                            Tipo = "success",
                            Fecha = DateTime.Now.AddHours(-3),
                            URL = "/SolicitudesCambio/Details/1"
                        });
                    }
                }

                return notificaciones;
            }
            catch
            {
                // En caso de error, devolver una lista vacía
                return new List<NotificacionViewModel>();
            }
        }
    }

    // Clase auxiliar para manejar notificaciones
    public class NotificacionViewModel
    {
        public int NotificacionID { get; set; }
        public string Mensaje { get; set; }
        public string Tipo { get; set; } // success, info, warning, danger
        public DateTime Fecha { get; set; }
        public string URL { get; set; }
    }
}
