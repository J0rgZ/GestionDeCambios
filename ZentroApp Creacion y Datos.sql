-- Script de Creación de Base de Datos para Sistema de Gestión de Cambios
-- Creación de la Base de Datos
CREATE DATABASE ZentroApp;
GO

USE ZentroApp;
GO

-- MÓDULO DE SEGURIDAD

-- Tabla de Roles
CREATE TABLE Roles (
    RolID INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(50) NOT NULL,
    Descripcion VARCHAR(255),
    FechaCreacion DATETIME DEFAULT GETDATE(),
    Estado BIT DEFAULT 1
);
GO

-- Tabla de Usuarios
CREATE TABLE Usuarios (
    UsuarioID INT PRIMARY KEY IDENTITY(1,1),
    NombreUsuario VARCHAR(50) NOT NULL UNIQUE,
    Contrasena VARCHAR(255) NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Telefono VARCHAR(20),
    RolID INT REFERENCES Roles(RolID),
    FechaCreacion DATETIME DEFAULT GETDATE(),
    UltimoAcceso DATETIME,
    Estado BIT DEFAULT 1
);
GO

-- MÓDULO DE MANTENIMIENTO

-- Tabla de Metodologías
CREATE TABLE Metodologias (
    MetodologiaID INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(500),
    FechaCreacion DATETIME DEFAULT GETDATE(),
    CreadoPor INT REFERENCES Usuarios(UsuarioID),
    Estado BIT DEFAULT 1
);
GO

-- Tabla de Fases
CREATE TABLE Fases (
    FaseID INT PRIMARY KEY IDENTITY(1,1),
    MetodologiaID INT REFERENCES Metodologias(MetodologiaID),
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(500),
    OrdenSecuencia INT NOT NULL,
    DuracionEstimada INT, -- En días
    FechaCreacion DATETIME DEFAULT GETDATE(),
    Estado BIT DEFAULT 1
);
GO

-- Tabla de Elementos de Configuración de Software (ECS)
CREATE TABLE ElementosConfiguracion (
    ECSID INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(500),
    Tipo VARCHAR(50) NOT NULL, -- Documento, Código, Interfaz, etc.
    Version VARCHAR(20) DEFAULT '1.0',
    RutaArchivo VARCHAR(255),
    FechaCreacion DATETIME DEFAULT GETDATE(),
    CreadoPor INT REFERENCES Usuarios(UsuarioID),
    UltimaModificacion DATETIME,
    ModificadoPor INT REFERENCES Usuarios(UsuarioID),
    Estado BIT DEFAULT 1
);
GO

-- Tabla para la integración con GitHub
CREATE TABLE IntegracionGitHub (
    IntegracionID INT PRIMARY KEY IDENTITY(1,1),
    ECSID INT REFERENCES ElementosConfiguracion(ECSID),
    RepositorioURL VARCHAR(255) NOT NULL,
    RamaPrincipal VARCHAR(100) DEFAULT 'main',
    UltimoCommitHash VARCHAR(100),
    UltimaSincronizacion DATETIME,
    Estado BIT DEFAULT 1
);
GO

-- Tabla de Áreas de Trabajo
CREATE TABLE AreasTrabajo (
    AreaID INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(500),
    ResponsableID INT REFERENCES Usuarios(UsuarioID),
    FechaCreacion DATETIME DEFAULT GETDATE(),
    Estado BIT DEFAULT 1
);
GO

-- Relacionar usuarios con áreas
CREATE TABLE UsuariosAreas (
    UsuarioAreaID INT PRIMARY KEY IDENTITY(1,1),
    UsuarioID INT REFERENCES Usuarios(UsuarioID),
    AreaID INT REFERENCES AreasTrabajo(AreaID),
    EsPrincipal BIT DEFAULT 0,
    FechaAsignacion DATETIME DEFAULT GETDATE(),
    Estado BIT DEFAULT 1
);
GO

-- MÓDULO PROYECTO

-- Tabla de Proyectos
CREATE TABLE Proyectos (
    ProyectoID INT PRIMARY KEY IDENTITY(1,1),
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(500),
    MetodologiaID INT REFERENCES Metodologias(MetodologiaID),
    FechaInicio DATE NOT NULL,
    FechaFinEstimada DATE NOT NULL,
    FechaFinReal DATE,
    JefeProyectoID INT REFERENCES Usuarios(UsuarioID),
    ClienteID INT REFERENCES Usuarios(UsuarioID),
    Estado VARCHAR(20) DEFAULT 'Iniciado', -- Iniciado, En Progreso, Finalizado, Cancelado
    FechaCreacion DATETIME DEFAULT GETDATE(),
    CreadoPor INT REFERENCES Usuarios(UsuarioID)
);
GO

-- Tabla de Roles de Proyecto (Asignación de usuarios a proyectos con roles específicos)
CREATE TABLE RolesProyecto (
    RolProyectoID INT PRIMARY KEY IDENTITY(1,1),
    ProyectoID INT REFERENCES Proyectos(ProyectoID),
    UsuarioID INT REFERENCES Usuarios(UsuarioID),
    RolID INT REFERENCES Roles(RolID),
    FechaAsignacion DATETIME DEFAULT GETDATE(),
    AsignadoPor INT REFERENCES Usuarios(UsuarioID),
    Estado BIT DEFAULT 1
);
GO

-- Tabla de Cronograma
CREATE TABLE Cronograma (
    CronogramaID INT PRIMARY KEY IDENTITY(1,1),
    ProyectoID INT REFERENCES Proyectos(ProyectoID),
    FaseID INT REFERENCES Fases(FaseID),
    FechaInicio DATE NOT NULL,
    FechaFinEstimada DATE NOT NULL,
    FechaFinReal DATE,
    ResponsableID INT REFERENCES Usuarios(UsuarioID),
    Comentarios VARCHAR(500),
    Estado VARCHAR(20) DEFAULT 'Pendiente', -- Pendiente, En Progreso, Finalizado
    FechaCreacion DATETIME DEFAULT GETDATE(),
    CreadoPor INT REFERENCES Usuarios(UsuarioID)
);
GO

-- Tabla de Entregables del Cronograma
CREATE TABLE Entregables (
    EntregableID INT PRIMARY KEY IDENTITY(1,1),
    CronogramaID INT REFERENCES Cronograma(CronogramaID),
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(500),
    ECSID INT REFERENCES ElementosConfiguracion(ECSID),
    FechaEntregaEstimada DATE NOT NULL,
    FechaEntregaReal DATE,
    ResponsableID INT REFERENCES Usuarios(UsuarioID),
    Estado VARCHAR(20) DEFAULT 'Pendiente', -- Pendiente, En Progreso, Finalizado, Rechazado
    FechaCreacion DATETIME DEFAULT GETDATE(),
    CreadoPor INT REFERENCES Usuarios(UsuarioID)
);
GO

-- MÓDULO DE CONTROL DE CAMBIOS

-- Tabla de Solicitudes de Cambio
CREATE TABLE SolicitudesCambio (
    SolicitudCambioID INT PRIMARY KEY IDENTITY(1,1),
    ProyectoID INT REFERENCES Proyectos(ProyectoID),
    ECSID INT REFERENCES ElementosConfiguracion(ECSID),
    SolicitanteID INT REFERENCES Usuarios(UsuarioID),
    Titulo VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(1000) NOT NULL,
    Justificacion VARCHAR(500) NOT NULL,
    TipoCambio VARCHAR(50) NOT NULL, -- Correctivo, Adaptativo, Perfectivo, Preventivo
    Prioridad VARCHAR(20) NOT NULL, -- Alta, Media, Baja
    Estado VARCHAR(20) DEFAULT 'Pendiente', -- Pendiente, En Evaluación, Aprobado, Rechazado, En Implementación, Implementado
    FechaSolicitud DATETIME DEFAULT GETDATE()
);
GO

-- Tabla de Informes de Cambio
CREATE TABLE InformesCambio (
    InformeCambioID INT PRIMARY KEY IDENTITY(1,1),
    SolicitudCambioID INT REFERENCES SolicitudesCambio(SolicitudCambioID),
    EvaluadorID INT REFERENCES Usuarios(UsuarioID),
    ImpactoTecnico VARCHAR(500),
    ImpactoCosto DECIMAL(10,2),
    ImpactoTiempo INT, -- En días
    Recomendacion VARCHAR(50) NOT NULL, -- Aprobar, Rechazar, Posponer
    Justificacion VARCHAR(500),
    FechaEvaluacion DATETIME DEFAULT GETDATE(),
    Estado VARCHAR(20) DEFAULT 'Pendiente' -- Pendiente, Enviado, Aprobado, Rechazado
);
GO

-- Tabla de Aprobaciones de Cambio
CREATE TABLE AprobacionesCambio (
    AprobacionCambioID INT PRIMARY KEY IDENTITY(1,1),
    InformeCambioID INT REFERENCES InformesCambio(InformeCambioID),
    ComiteAprobadorID INT REFERENCES Usuarios(UsuarioID),
    Aprobado BIT NOT NULL,
    Comentarios VARCHAR(500),
    FechaAprobacion DATETIME DEFAULT GETDATE()
);
GO

-- Tabla de Órdenes de Cambio
CREATE TABLE OrdenesCambio (
    OrdenCambioID INT PRIMARY KEY IDENTITY(1,1),
    SolicitudCambioID INT REFERENCES SolicitudesCambio(SolicitudCambioID),
    AprobacionCambioID INT REFERENCES AprobacionesCambio(AprobacionCambioID),
    JefeProyectoID INT REFERENCES Usuarios(UsuarioID),
    FechaEmision DATETIME DEFAULT GETDATE(),
    FechaImplementacionEstimada DATE NOT NULL,
    FechaImplementacionReal DATE,
    Estado VARCHAR(20) DEFAULT 'Emitida', -- Emitida, En Progreso, Implementada, Verificada
    Comentarios VARCHAR(500)
);
GO

-- Tabla de Asignaciones de Cambio (Detalle de la orden de cambio)
CREATE TABLE AsignacionesCambio (
    AsignacionCambioID INT PRIMARY KEY IDENTITY(1,1),
    OrdenCambioID INT REFERENCES OrdenesCambio(OrdenCambioID),
    ECSID INT REFERENCES ElementosConfiguracion(ECSID),
    ResponsableID INT REFERENCES Usuarios(UsuarioID),
    AreaID INT REFERENCES AreasTrabajo(AreaID),
    Descripcion VARCHAR(500) NOT NULL,
    FechaAsignacion DATETIME DEFAULT GETDATE(),
    FechaFinalizacionEstimada DATE NOT NULL,
    FechaFinalizacionReal DATE,
    Estado VARCHAR(20) DEFAULT 'Asignada', -- Asignada, En Progreso, Finalizada, Verificada
    Comentarios VARCHAR(500)
);
GO

-- Tabla de mapeo entre cambios y commits
CREATE TABLE CambiosGitHub (
    CambioGitHubID INT PRIMARY KEY IDENTITY(1,1),
    AsignacionCambioID INT REFERENCES AsignacionesCambio(AsignacionCambioID),
    CommitHash VARCHAR(100) NOT NULL,
    MensajeCommit VARCHAR(500),
    FechaCommit DATETIME NOT NULL,
    AutorCommit VARCHAR(100),
    EstadoSincronizacion VARCHAR(20) DEFAULT 'Pendiente' -- Pendiente, Sincronizado, Conflicto
);
GO

-- Tabla de Verificaciones de Cambio por el Solicitante
CREATE TABLE VerificacionesSolicitante (
    VerificacionID INT PRIMARY KEY IDENTITY(1,1),
    AsignacionCambioID INT REFERENCES AsignacionesCambio(AsignacionCambioID),
    SolicitanteID INT REFERENCES Usuarios(UsuarioID),
    Aprobado BIT NOT NULL,
    Comentarios VARCHAR(500),
    FechaVerificacion DATETIME DEFAULT GETDATE()
);
GO

-- MÓDULO DE INFORME DE ESTADO

-- Tabla de Estados de Proyecto
CREATE TABLE EstadosProyecto (
    EstadoProyectoID INT PRIMARY KEY IDENTITY(1,1),
    ProyectoID INT REFERENCES Proyectos(ProyectoID),
    FechaReporte DATETIME DEFAULT GETDATE(),
    PorcentajeAvance DECIMAL(5,2) NOT NULL,
    EstadoActual VARCHAR(20) NOT NULL, -- En Planificación, En Desarrollo, En Pruebas, Finalizado, etc.
    Comentarios VARCHAR(1000),
    ReportadoPor INT REFERENCES Usuarios(UsuarioID)
);
GO

-- Tabla de Estados de Cambio
CREATE TABLE EstadosCambio (
    EstadoCambioID INT PRIMARY KEY IDENTITY(1,1),
    SolicitudCambioID INT REFERENCES SolicitudesCambio(SolicitudCambioID),
    FechaReporte DATETIME DEFAULT GETDATE(),
    PorcentajeAvance DECIMAL(5,2) NOT NULL,
    EstadoActual VARCHAR(20) NOT NULL, -- En Análisis, En Desarrollo, En Pruebas, Implementado, etc.
    Comentarios VARCHAR(1000),
    ReportadoPor INT REFERENCES Usuarios(UsuarioID)
);
GO

-- Tabla de Estados de Flujo para seguimiento más detallado
CREATE TABLE EstadosFlujo (
    EstadoFlujoID INT PRIMARY KEY IDENTITY(1,1),
    SolicitudCambioID INT REFERENCES SolicitudesCambio(SolicitudCambioID),
    EstadoAnterior VARCHAR(50),
    EstadoNuevo VARCHAR(50) NOT NULL,
    Comentario VARCHAR(500),
    CambiadoPor INT REFERENCES Usuarios(UsuarioID),
    FechaCambio DATETIME DEFAULT GETDATE()
);
GO

-- Tabla de Historial de Cambios en ECS
CREATE TABLE HistorialECS (
    HistorialECSID INT PRIMARY KEY IDENTITY(1,1),
    ECSID INT REFERENCES ElementosConfiguracion(ECSID),
    OrdenCambioID INT REFERENCES OrdenesCambio(OrdenCambioID),
    VersionAnterior VARCHAR(20),
    VersionNueva VARCHAR(20) NOT NULL,
    FechaCambio DATETIME DEFAULT GETDATE(),
    CambiadoPor INT REFERENCES Usuarios(UsuarioID),
    Descripcion VARCHAR(500) NOT NULL
);
GO

-- Tabla de Notificaciones
CREATE TABLE Notificaciones (
    NotificacionID INT PRIMARY KEY IDENTITY(1,1),
    UsuarioID INT REFERENCES Usuarios(UsuarioID),
    Titulo VARCHAR(100) NOT NULL,
    Mensaje VARCHAR(500) NOT NULL,
    FechaEnvio DATETIME DEFAULT GETDATE(),
    Leido BIT DEFAULT 0,
    FechaLectura DATETIME,
    TipoNotificacion VARCHAR(50) NOT NULL, -- SolicitudCambio, AprobacionCambio, OrdenCambio, etc.
    ReferenciaID INT, -- ID de la entidad relacionada (SolicitudCambioID, OrdenCambioID, etc.)
    Estado BIT DEFAULT 1
);
GO

-- Inserción de datos iniciales

-- Insertar roles básicos
INSERT INTO Roles (Nombre, Descripcion)
VALUES 
('Administrador', 'Control total del sistema'),
('Jefe de Proyecto', 'Gestiona proyectos y aprueba cambios'),
('Analista', 'Responsable del análisis de requerimientos'),
('Desarrollador', 'Responsable de la implementación del código'),
('QA', 'Responsable de pruebas y aseguramiento de calidad'),
('Cliente', 'Solicitante de cambios'),
('Comité de Cambios', 'Evalúa y aprueba solicitudes de cambio');
GO

-- Insertar usuario administrador por defecto
INSERT INTO Usuarios (NombreUsuario, Contrasena, Nombre, Apellido, Email, RolID)
VALUES ('admin', HASHBYTES('SHA2_256', 'admin123'), 'Administrador', 'Sistema', 'admin@zentroapp.com', 1);
GO

-- Insertar usuarios de prueba
INSERT INTO Usuarios (NombreUsuario, Contrasena, Nombre, Apellido, Email, RolID)
VALUES 
('jefe1', HASHBYTES('SHA2_256', 'zentro123'), 'Juan', 'Pérez', 'jperez@zentroapp.com', 2),
('analista1', HASHBYTES('SHA2_256', 'zentro123'), 'Ana', 'García', 'agarcia@zentroapp.com', 3),
('dev1', HASHBYTES('SHA2_256', 'zentro123'), 'Carlos', 'Martínez', 'cmartinez@zentroapp.com', 4),
('qa1', HASHBYTES('SHA2_256', 'zentro123'), 'María', 'López', 'mlopez@zentroapp.com', 5),
('cliente1', HASHBYTES('SHA2_256', 'zentro123'), 'Pedro', 'Sánchez', 'psanchez@cliente.com', 6),
('comite1', HASHBYTES('SHA2_256', 'zentro123'), 'Laura', 'Rodríguez', 'lrodriguez@zentroapp.com', 7),
('jefe2', HASHBYTES('SHA2_256', 'zentro123'), 'Roberto', 'Gómez', 'rgomez@zentroapp.com', 2),
('analista2', HASHBYTES('SHA2_256', 'zentro123'), 'Sofía', 'Hernández', 'shernandez@zentroapp.com', 3),
('dev2', HASHBYTES('SHA2_256', 'zentro123'), 'Miguel', 'Torres', 'mtorres@zentroapp.com', 4),
('cliente2', HASHBYTES('SHA2_256', 'zentro123'), 'Elena', 'Díaz', 'ediaz@cliente.com', 6);
GO

-- Insertar metodologías comunes
INSERT INTO Metodologias (Nombre, Descripcion, CreadoPor)  
VALUES 
('Scrum', 'Metodología ágil para desarrollo de software', 1),
('Cascada', 'Metodología secuencial para desarrollo de software', 1),
('RUP', 'Rational Unified Process - Metodología iterativa', 1),
('Kanban', 'Metodología visual para gestión de flujo de trabajo', 1),
('XP', 'Extreme Programming - Metodología ágil', 1);
GO

-- Insertar fases para metodología Cascada
INSERT INTO Fases (MetodologiaID, Nombre, Descripcion, OrdenSecuencia, DuracionEstimada)
VALUES 
(2, 'Requerimientos', 'Definición y documentación de requisitos', 1, 15),
(2, 'Análisis', 'Análisis de los requerimientos', 2, 20),
(2, 'Diseño', 'Diseño de la solución', 3, 30),
(2, 'Implementación', 'Desarrollo de la solución', 4, 45),
(2, 'Pruebas', 'Verificación y validación', 5, 20),
(2, 'Despliegue', 'Puesta en producción', 6, 10),
(2, 'Mantenimiento', 'Soporte post-implementación', 7, 30);
GO

-- Insertar fases para metodología Scrum
INSERT INTO Fases (MetodologiaID, Nombre, Descripcion, OrdenSecuencia, DuracionEstimada)
VALUES 
(1, 'Product Backlog', 'Definición de requisitos pendientes', 1, 5),
(1, 'Sprint Planning', 'Planificación del sprint', 2, 2),
(1, 'Sprint', 'Desarrollo del incremento', 3, 15),
(1, 'Daily Scrum', 'Reuniones diarias', 4, 15),
(1, 'Sprint Review', 'Revisión del incremento', 5, 1),
(1, 'Sprint Retrospective', 'Análisis del proceso', 6, 1),
(1, 'Release', 'Entrega del incremento', 7, 2);
GO

-- Insertar fases para metodología RUP
INSERT INTO Fases (MetodologiaID, Nombre, Descripcion, OrdenSecuencia, DuracionEstimada)
VALUES 
(3, 'Inicio', 'Definición de alcance y visión', 1, 20),
(3, 'Elaboración', 'Diseño de la arquitectura', 2, 30),
(3, 'Construcción', 'Implementación de la solución', 3, 60),
(3, 'Transición', 'Despliegue al usuario final', 4, 20);
GO

-- Insertar áreas de trabajo
INSERT INTO AreasTrabajo (Nombre, Descripcion, ResponsableID)
VALUES 
('Análisis', 'Área encargada del análisis de requerimientos', 3),
('Desarrollo', 'Área encargada de la implementación de código', 4),
('QA', 'Área encargada de pruebas y control de calidad', 5),
('Soporte', 'Área encargada del soporte post-implementación', 8);
GO

-- Asignar usuarios a áreas
INSERT INTO UsuariosAreas (UsuarioID, AreaID, EsPrincipal)
VALUES 
(3, 1, 1), -- Ana García a Análisis como principal
(8, 1, 0), -- Sofía Hernández a Análisis como secundario
(4, 2, 1), -- Carlos Martínez a Desarrollo como principal
(9, 2, 1), -- Miguel Torres a Desarrollo como principal
(5, 3, 1); -- María López a QA como principal
GO

-- Insertar proyectos de prueba
INSERT INTO Proyectos (Nombre, Descripcion, MetodologiaID, FechaInicio, FechaFinEstimada, JefeProyectoID, ClienteID, Estado, CreadoPor)
VALUES 
('Sistema de Facturación', 'Sistema de facturación electrónica', 1, '2025-01-01', '2025-06-30', 2, 6, 'En Progreso', 1),
('Portal Web Corporativo', 'Desarrollo de portal web informativo', 2, '2025-02-15', '2025-08-15', 7, 10, 'Iniciado', 1),
('App Móvil Bancaria', 'Aplicación móvil para servicios bancarios', 3, '2025-03-01', '2025-12-31', 2, 6, 'Iniciado', 1);
GO

-- Asignar roles en proyectos
INSERT INTO RolesProyecto (ProyectoID, UsuarioID, RolID, AsignadoPor)
VALUES 
(1, 2, 2, 1),  -- Juan Pérez como Jefe del Proyecto 1
(1, 3, 3, 2),  -- Ana García como Analista del Proyecto 1
(1, 4, 4, 2),  -- Carlos Martínez como Desarrollador del Proyecto 1
(1, 5, 5, 2),  -- María López como QA del Proyecto 1
(1, 6, 6, 1),  -- Pedro Sánchez como Cliente del Proyecto 1
(2, 7, 2, 1),  -- Roberto Gómez como Jefe del Proyecto 2
(2, 8, 3, 7),  -- Sofía Hernández como Analista del Proyecto 2
(2, 9, 4, 7),  -- Miguel Torres como Desarrollador del Proyecto 2
(2, 10, 6, 1); -- Elena Díaz como Cliente del Proyecto 2
GO

-- Insertar Elementos de Configuración
INSERT INTO ElementosConfiguracion (Nombre, Descripcion, Tipo, Version, RutaArchivo, CreadoPor)
VALUES 
('Documento de Requerimientos', 'Especificación de requisitos del sistema', 'Documento', '1.0', '/docs/req_sistema_facturacion.docx', 3),
('Modelo de Base de Datos', 'Estructura de la base de datos del sistema', 'Documento', '1.0', '/docs/modelo_bd_facturacion.pdf', 3),
('Módulo de Facturación', 'Código fuente del módulo de facturación', 'Código', '1.0', '/src/facturacion/', 4),
('Interfaz de Usuario', 'Diseño de la interfaz de usuario', 'Interfaz', '1.0', '/design/ui_facturacion.figma', 8),
('API REST', 'Documentación y código de API REST', 'Código', '1.0', '/src/api/', 9);
GO

-- Integración con GitHub
INSERT INTO IntegracionGitHub (ECSID, RepositorioURL, RamaPrincipal)
VALUES 
(3, 'https://github.com/zentroapp/facturacion.git', 'main'),
(5, 'https://github.com/zentroapp/api.git', 'develop');
GO

-- Crear cronogramas para el proyecto
INSERT INTO Cronograma (ProyectoID, FaseID, FechaInicio, FechaFinEstimada, ResponsableID, Estado, CreadoPor)
VALUES 
(1, 8, '2025-01-01', '2025-01-15', 2, 'Finalizado', 2),
(1, 9, '2025-01-16', '2025-01-17', 2, 'Finalizado', 2),
(1, 10, '2025-01-18', '2025-02-01', 4, 'En Progreso', 2),
(1, 11, '2025-01-18', '2025-02-01', 2, 'En Progreso', 2),
(2, 1, '2025-02-15', '2025-03-01', 7, 'En Progreso', 7),
(2, 2, '2025-03-02', '2025-03-21', 8, 'Pendiente', 7);
GO

-- Crear entregables
INSERT INTO Entregables (CronogramaID, Nombre, Descripcion, ECSID, FechaEntregaEstimada, ResponsableID, Estado, CreadoPor)
VALUES 
(1, 'Product Backlog Inicial', 'Lista inicial de requerimientos priorizados', 1, '2025-01-15', 3, 'Finalizado', 2),
(3, 'Primer Sprint', 'Módulo básico de facturación', 3, '2025-02-01', 4, 'En Progreso', 2),
(5, 'Requerimientos Portal Web', 'Documento de requerimientos del portal', 4, '2025-03-01', 8, 'En Progreso', 7);
GO

-- Insertar solicitudes de cambio
INSERT INTO SolicitudesCambio (ProyectoID, ECSID, SolicitanteID, Titulo, Descripcion, Justificacion, TipoCambio, Prioridad, Estado)
VALUES 
(1, 3, 6, 'Modificar formato de factura', 'Se requiere incluir el logo del cliente en el formato de factura', 'Requerimiento legal para facturas electrónicas', 'Adaptativo', 'Alta', 'Pendiente'),
(1, 1, 6, 'Agregar campo de descuento', 'Incluir un campo para descuentos especiales en la factura', 'Necesidad de campañas promocionales', 'Perfectivo', 'Media', 'Pendiente'),
(2, 4, 10, 'Cambiar paleta de colores', 'Modificar los colores de la interfaz según nueva guía de marca', 'Actualización de imagen corporativa', 'Adaptativo', 'Baja', 'Pendiente');
GO

-- Insertar informes de cambio
INSERT INTO InformesCambio (SolicitudCambioID, EvaluadorID, ImpactoTecnico, ImpactoCosto, ImpactoTiempo, Recomendacion, Justificacion)
VALUES 
(1, 3, 'Impacto bajo en estructura de datos y código', 50.00, 2, 'Aprobar', 'Cambio sencillo con alto valor para el cliente'),
(2, 3, 'Impacto medio en estructura de datos y cálculos', 250.00, 5, 'Aprobar', 'Funcionalidad importante para el negocio');
GO

-- Insertar aprobaciones de cambio
INSERT INTO AprobacionesCambio (InformeCambioID, ComiteAprobadorID, Aprobado, Comentarios)
VALUES 
(1, 7, 1, 'Aprobado por impacto bajo y alta prioridad');
GO

-- Insertar órdenes de cambio
INSERT INTO OrdenesCambio (SolicitudCambioID, AprobacionCambioID, JefeProyectoID, FechaImplementacionEstimada, Estado)
VALUES 
(1, 1, 2, '2025-02-10', 'Emitida');
GO

-- Insertar asignaciones de cambio
INSERT INTO AsignacionesCambio (OrdenCambioID, ECSID, ResponsableID, AreaID, Descripcion, FechaFinalizacionEstimada, Estado)
VALUES 
(1, 3, 4, 2, 'Modificar la plantilla de factura para incluir el logo del cliente', '2025-02-08', 'Asignada');
GO

-- Insertar cambios GitHub (simulación)
INSERT INTO CambiosGitHub (AsignacionCambioID, CommitHash, MensajeCommit, FechaCommit, AutorCommit)
VALUES 
(1, 'a1b2c3d4e5f6g7h8i9j0', 'Implementado logo en plantilla de factura', '2025-02-05 14:30:00', 'cmartinez');
GO

-- Insertar estado de proyecto
INSERT INTO EstadosProyecto (ProyectoID, PorcentajeAvance, EstadoActual, Comentarios, ReportadoPor)
VALUES 
(1, 25.00, 'En Desarrollo', 'Avanzando según cronograma. Sprint 1 en progreso.', 2),
(2, 10.00, 'En Planificación', 'Fase de requerimientos en desarrollo', 7);
GO

-- Insertar estado de cambios
INSERT INTO EstadosCambio (SolicitudCambioID, PorcentajeAvance, EstadoActual, Comentarios, ReportadoPor)
VALUES 
(1, 0.00, 'En Análisis', 'Evaluando impacto técnico del cambio', 3);
GO

-- Insertar estado de flujo
INSERT INTO EstadosFlujo (SolicitudCambioID, EstadoAnterior, EstadoNuevo, Comentario, CambiadoPor)
VALUES 
(1, 'Pendiente', 'En Evaluación', 'Solicitud recibida y en proceso de evaluación', 3),
(1, 'En Evaluación', 'Aprobado', 'Cambio aprobado por el comité', 7);
GO

-- Insertar historial de ECS
INSERT INTO HistorialECS (ECSID, OrdenCambioID, VersionAnterior, VersionNueva, CambiadoPor, Descripcion)
VALUES 
(3, 1, '1.0', '1.1', 4, 'Actualización para incluir logo en plantilla de factura');
GO

-- Insertar notificaciones
INSERT INTO Notificaciones (UsuarioID, Titulo, Mensaje, TipoNotificacion, ReferenciaID)
VALUES 
(4, 'Nueva asignación de cambio', 'Se te ha asignado una nueva tarea de implementación de cambio', 'OrdenCambio', 1),
(6, 'Solicitud de cambio aprobada', 'Tu solicitud de cambio para incluir logo en facturas ha sido aprobada', 'AprobacionCambio', 1),
(2, 'Nuevo cambio aprobado', 'Se ha aprobado un cambio para el proyecto Sistema de Facturación', 'InformeCambio', 1);
GO