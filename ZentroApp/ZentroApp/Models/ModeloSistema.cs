using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace ZentroApp.Models
{
    public partial class ModeloSistema : DbContext
    {
        public ModeloSistema()
            : base("name=ModeloSistema")
        {
        }

        public virtual DbSet<AprobacionesCambio> AprobacionesCambio { get; set; }
        public virtual DbSet<AreasTrabajo> AreasTrabajo { get; set; }
        public virtual DbSet<AsignacionesCambio> AsignacionesCambio { get; set; }
        public virtual DbSet<CambiosGitHub> CambiosGitHub { get; set; }
        public virtual DbSet<Cronograma> Cronograma { get; set; }
        public virtual DbSet<ElementosConfiguracion> ElementosConfiguracion { get; set; }
        public virtual DbSet<Entregables> Entregables { get; set; }
        public virtual DbSet<EstadosCambio> EstadosCambio { get; set; }
        public virtual DbSet<EstadosFlujo> EstadosFlujo { get; set; }
        public virtual DbSet<EstadosProyecto> EstadosProyecto { get; set; }
        public virtual DbSet<Fases> Fases { get; set; }
        public virtual DbSet<HistorialECS> HistorialECS { get; set; }
        public virtual DbSet<InformesCambio> InformesCambio { get; set; }
        public virtual DbSet<IntegracionGitHub> IntegracionGitHub { get; set; }
        public virtual DbSet<Metodologias> Metodologias { get; set; }
        public virtual DbSet<Notificaciones> Notificaciones { get; set; }
        public virtual DbSet<OrdenesCambio> OrdenesCambio { get; set; }
        public virtual DbSet<Proyectos> Proyectos { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<RolesProyecto> RolesProyecto { get; set; }
        public virtual DbSet<SolicitudesCambio> SolicitudesCambio { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<UsuariosAreas> UsuariosAreas { get; set; }
        public virtual DbSet<VerificacionesSolicitante> VerificacionesSolicitante { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AprobacionesCambio>()
                .Property(e => e.Comentarios)
                .IsUnicode(false);

            modelBuilder.Entity<AreasTrabajo>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<AreasTrabajo>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<AsignacionesCambio>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<AsignacionesCambio>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<AsignacionesCambio>()
                .Property(e => e.Comentarios)
                .IsUnicode(false);

            modelBuilder.Entity<CambiosGitHub>()
                .Property(e => e.CommitHash)
                .IsUnicode(false);

            modelBuilder.Entity<CambiosGitHub>()
                .Property(e => e.MensajeCommit)
                .IsUnicode(false);

            modelBuilder.Entity<CambiosGitHub>()
                .Property(e => e.AutorCommit)
                .IsUnicode(false);

            modelBuilder.Entity<CambiosGitHub>()
                .Property(e => e.EstadoSincronizacion)
                .IsUnicode(false);

            modelBuilder.Entity<Cronograma>()
                .Property(e => e.Comentarios)
                .IsUnicode(false);

            modelBuilder.Entity<Cronograma>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<ElementosConfiguracion>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<ElementosConfiguracion>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<ElementosConfiguracion>()
                .Property(e => e.Tipo)
                .IsUnicode(false);

            modelBuilder.Entity<ElementosConfiguracion>()
                .Property(e => e.Version)
                .IsUnicode(false);

            modelBuilder.Entity<ElementosConfiguracion>()
                .Property(e => e.RutaArchivo)
                .IsUnicode(false);

            modelBuilder.Entity<Entregables>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Entregables>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Entregables>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<EstadosCambio>()
                .Property(e => e.PorcentajeAvance)
                .HasPrecision(5, 2);

            modelBuilder.Entity<EstadosCambio>()
                .Property(e => e.EstadoActual)
                .IsUnicode(false);

            modelBuilder.Entity<EstadosCambio>()
                .Property(e => e.Comentarios)
                .IsUnicode(false);

            modelBuilder.Entity<EstadosFlujo>()
                .Property(e => e.EstadoAnterior)
                .IsUnicode(false);

            modelBuilder.Entity<EstadosFlujo>()
                .Property(e => e.EstadoNuevo)
                .IsUnicode(false);

            modelBuilder.Entity<EstadosFlujo>()
                .Property(e => e.Comentario)
                .IsUnicode(false);

            modelBuilder.Entity<EstadosProyecto>()
                .Property(e => e.PorcentajeAvance)
                .HasPrecision(5, 2);

            modelBuilder.Entity<EstadosProyecto>()
                .Property(e => e.EstadoActual)
                .IsUnicode(false);

            modelBuilder.Entity<EstadosProyecto>()
                .Property(e => e.Comentarios)
                .IsUnicode(false);

            modelBuilder.Entity<Fases>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Fases>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<HistorialECS>()
                .Property(e => e.VersionAnterior)
                .IsUnicode(false);

            modelBuilder.Entity<HistorialECS>()
                .Property(e => e.VersionNueva)
                .IsUnicode(false);

            modelBuilder.Entity<HistorialECS>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<InformesCambio>()
                .Property(e => e.ImpactoTecnico)
                .IsUnicode(false);

            modelBuilder.Entity<InformesCambio>()
                .Property(e => e.ImpactoCosto)
                .HasPrecision(10, 2);

            modelBuilder.Entity<InformesCambio>()
                .Property(e => e.Recomendacion)
                .IsUnicode(false);

            modelBuilder.Entity<InformesCambio>()
                .Property(e => e.Justificacion)
                .IsUnicode(false);

            modelBuilder.Entity<InformesCambio>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<IntegracionGitHub>()
                .Property(e => e.RepositorioURL)
                .IsUnicode(false);

            modelBuilder.Entity<IntegracionGitHub>()
                .Property(e => e.RamaPrincipal)
                .IsUnicode(false);

            modelBuilder.Entity<IntegracionGitHub>()
                .Property(e => e.UltimoCommitHash)
                .IsUnicode(false);

            modelBuilder.Entity<Metodologias>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Metodologias>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Notificaciones>()
                .Property(e => e.Titulo)
                .IsUnicode(false);

            modelBuilder.Entity<Notificaciones>()
                .Property(e => e.Mensaje)
                .IsUnicode(false);

            modelBuilder.Entity<Notificaciones>()
                .Property(e => e.TipoNotificacion)
                .IsUnicode(false);

            modelBuilder.Entity<OrdenesCambio>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<OrdenesCambio>()
                .Property(e => e.Comentarios)
                .IsUnicode(false);

            modelBuilder.Entity<Proyectos>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Proyectos>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<Proyectos>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<Roles>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Roles>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<SolicitudesCambio>()
                .Property(e => e.Titulo)
                .IsUnicode(false);

            modelBuilder.Entity<SolicitudesCambio>()
                .Property(e => e.Descripcion)
                .IsUnicode(false);

            modelBuilder.Entity<SolicitudesCambio>()
                .Property(e => e.Justificacion)
                .IsUnicode(false);

            modelBuilder.Entity<SolicitudesCambio>()
                .Property(e => e.TipoCambio)
                .IsUnicode(false);

            modelBuilder.Entity<SolicitudesCambio>()
                .Property(e => e.Prioridad)
                .IsUnicode(false);

            modelBuilder.Entity<SolicitudesCambio>()
                .Property(e => e.Estado)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.NombreUsuario)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Contrasena)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Nombre)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Apellido)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .Property(e => e.Telefono)
                .IsUnicode(false);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.AprobacionesCambio)
                .WithOptional(e => e.Usuarios)
                .HasForeignKey(e => e.ComiteAprobadorID);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.AreasTrabajo)
                .WithOptional(e => e.Usuarios)
                .HasForeignKey(e => e.ResponsableID);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.AsignacionesCambio)
                .WithOptional(e => e.Usuarios)
                .HasForeignKey(e => e.ResponsableID);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Cronograma)
                .WithOptional(e => e.Usuarios)
                .HasForeignKey(e => e.CreadoPor);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Cronograma1)
                .WithOptional(e => e.Usuarios1)
                .HasForeignKey(e => e.ResponsableID);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.ElementosConfiguracion)
                .WithOptional(e => e.Usuarios)
                .HasForeignKey(e => e.CreadoPor);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.ElementosConfiguracion1)
                .WithOptional(e => e.Usuarios1)
                .HasForeignKey(e => e.ModificadoPor);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Entregables)
                .WithOptional(e => e.Usuarios)
                .HasForeignKey(e => e.CreadoPor);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Entregables1)
                .WithOptional(e => e.Usuarios1)
                .HasForeignKey(e => e.ResponsableID);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.EstadosCambio)
                .WithOptional(e => e.Usuarios)
                .HasForeignKey(e => e.ReportadoPor);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.EstadosFlujo)
                .WithOptional(e => e.Usuarios)
                .HasForeignKey(e => e.CambiadoPor);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.EstadosProyecto)
                .WithOptional(e => e.Usuarios)
                .HasForeignKey(e => e.ReportadoPor);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.HistorialECS)
                .WithOptional(e => e.Usuarios)
                .HasForeignKey(e => e.CambiadoPor);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.InformesCambio)
                .WithOptional(e => e.Usuarios)
                .HasForeignKey(e => e.EvaluadorID);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Metodologias)
                .WithOptional(e => e.Usuarios)
                .HasForeignKey(e => e.CreadoPor);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.OrdenesCambio)
                .WithOptional(e => e.Usuarios)
                .HasForeignKey(e => e.JefeProyectoID);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Proyectos)
                .WithOptional(e => e.Usuarios)
                .HasForeignKey(e => e.ClienteID);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Proyectos1)
                .WithOptional(e => e.Usuarios1)
                .HasForeignKey(e => e.CreadoPor);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.Proyectos2)
                .WithOptional(e => e.Usuarios2)
                .HasForeignKey(e => e.JefeProyectoID);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.RolesProyecto)
                .WithOptional(e => e.Usuarios)
                .HasForeignKey(e => e.AsignadoPor);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.RolesProyecto1)
                .WithOptional(e => e.Usuarios1)
                .HasForeignKey(e => e.UsuarioID);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.SolicitudesCambio)
                .WithOptional(e => e.Usuarios)
                .HasForeignKey(e => e.SolicitanteID);

            modelBuilder.Entity<Usuarios>()
                .HasMany(e => e.VerificacionesSolicitante)
                .WithOptional(e => e.Usuarios)
                .HasForeignKey(e => e.SolicitanteID);

            modelBuilder.Entity<VerificacionesSolicitante>()
                .Property(e => e.Comentarios)
                .IsUnicode(false);
        }
    }
}
