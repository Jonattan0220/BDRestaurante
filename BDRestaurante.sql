USE [master]
GO
/****** Object:  Database [BDRestaurante]    Script Date: 10/06/2022 0:13:43 ******/
CREATE DATABASE [BDRestaurante]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BDRestaurante', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\BDRestaurante.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BDRestaurante_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\BDRestaurante_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BDRestaurante] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BDRestaurante].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BDRestaurante] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BDRestaurante] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BDRestaurante] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BDRestaurante] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BDRestaurante] SET ARITHABORT OFF 
GO
ALTER DATABASE [BDRestaurante] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [BDRestaurante] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BDRestaurante] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BDRestaurante] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BDRestaurante] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BDRestaurante] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BDRestaurante] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BDRestaurante] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BDRestaurante] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BDRestaurante] SET  DISABLE_BROKER 
GO
ALTER DATABASE [BDRestaurante] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BDRestaurante] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BDRestaurante] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BDRestaurante] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BDRestaurante] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BDRestaurante] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [BDRestaurante] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BDRestaurante] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BDRestaurante] SET  MULTI_USER 
GO
ALTER DATABASE [BDRestaurante] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BDRestaurante] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BDRestaurante] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BDRestaurante] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BDRestaurante] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BDRestaurante] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BDRestaurante] SET QUERY_STORE = OFF
GO
USE [BDRestaurante]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 10/06/2022 0:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[ClienteId] [int] IDENTITY(1,1) NOT NULL,
	[Identificacion] [varchar](50) NOT NULL,
	[Nombres] [varchar](50) NOT NULL,
	[Apellidos] [varchar](50) NOT NULL,
	[Direccion] [varchar](50) NOT NULL,
	[Telefono] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Cliente_1] PRIMARY KEY CLUSTERED 
(
	[ClienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleXFactura]    Script Date: 10/06/2022 0:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleXFactura](
	[IdDetalleXFactura] [int] IDENTITY(1,1) NOT NULL,
	[FacturaId] [int] NOT NULL,
	[SupervisorId] [int] NOT NULL,
	[Plato] [varchar](50) NOT NULL,
	[Valor] [int] NOT NULL,
 CONSTRAINT [PK_DetalleXFactura] PRIMARY KEY CLUSTERED 
(
	[IdDetalleXFactura] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Factura]    Script Date: 10/06/2022 0:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Factura](
	[FacturaId] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NOT NULL,
	[MesaId] [int] NOT NULL,
	[MeseroId] [int] NOT NULL,
	[FechaHora] [datetime] NOT NULL,
 CONSTRAINT [PK_Factura] PRIMARY KEY CLUSTERED 
(
	[FacturaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mesa]    Script Date: 10/06/2022 0:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mesa](
	[MesaId] [int] IDENTITY(1,1) NOT NULL,
	[NroMesa] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Reservada] [bit] NOT NULL,
	[Puestos] [int] NOT NULL,
 CONSTRAINT [PK_Mesa] PRIMARY KEY CLUSTERED 
(
	[MesaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mesero]    Script Date: 10/06/2022 0:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mesero](
	[MeseroId] [int] IDENTITY(1,1) NOT NULL,
	[Nombres] [varchar](50) NOT NULL,
	[Apellidos] [varchar](50) NOT NULL,
	[Edad] [int] NOT NULL,
	[Antiguedad] [int] NOT NULL,
 CONSTRAINT [PK_Mesero] PRIMARY KEY CLUSTERED 
(
	[MeseroId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supervisor]    Script Date: 10/06/2022 0:13:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supervisor](
	[SupervirsorId] [int] IDENTITY(1,1) NOT NULL,
	[Nombres] [varchar](50) NOT NULL,
	[Apellidos] [varchar](50) NOT NULL,
	[Edad] [int] NOT NULL,
	[Antiguedad] [int] NOT NULL,
 CONSTRAINT [PK_Supervisor] PRIMARY KEY CLUSTERED 
(
	[SupervirsorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cliente] ON 

INSERT [dbo].[Cliente] ([ClienteId], [Identificacion], [Nombres], [Apellidos], [Direccion], [Telefono]) VALUES (2, N'3423452343', N'Juan', N'Velez', N'Calle 3 4-25', N'3020304050')
INSERT [dbo].[Cliente] ([ClienteId], [Identificacion], [Nombres], [Apellidos], [Direccion], [Telefono]) VALUES (3, N'4545345345', N'Alejandra', N'Herrera', N'Calle 5 67-98', N'3032432423')
SET IDENTITY_INSERT [dbo].[Cliente] OFF
GO
SET IDENTITY_INSERT [dbo].[DetalleXFactura] ON 

INSERT [dbo].[DetalleXFactura] ([IdDetalleXFactura], [FacturaId], [SupervisorId], [Plato], [Valor]) VALUES (1, 4, 1, N'Pasta', 10000)
INSERT [dbo].[DetalleXFactura] ([IdDetalleXFactura], [FacturaId], [SupervisorId], [Plato], [Valor]) VALUES (2, 4, 1, N'Sopa', 20000)
INSERT [dbo].[DetalleXFactura] ([IdDetalleXFactura], [FacturaId], [SupervisorId], [Plato], [Valor]) VALUES (3, 5, 1, N'Pasta', 10000)
INSERT [dbo].[DetalleXFactura] ([IdDetalleXFactura], [FacturaId], [SupervisorId], [Plato], [Valor]) VALUES (4, 4, 1, N'Arroz', 5000)
INSERT [dbo].[DetalleXFactura] ([IdDetalleXFactura], [FacturaId], [SupervisorId], [Plato], [Valor]) VALUES (5, 5, 1, N'Helado', 2000)
INSERT [dbo].[DetalleXFactura] ([IdDetalleXFactura], [FacturaId], [SupervisorId], [Plato], [Valor]) VALUES (6, 6, 1, N'Lentejas', 3000)
INSERT [dbo].[DetalleXFactura] ([IdDetalleXFactura], [FacturaId], [SupervisorId], [Plato], [Valor]) VALUES (7, 7, 1, N'Pasta', 20000)
INSERT [dbo].[DetalleXFactura] ([IdDetalleXFactura], [FacturaId], [SupervisorId], [Plato], [Valor]) VALUES (8, 8, 1, N'Pasta', 15000)
SET IDENTITY_INSERT [dbo].[DetalleXFactura] OFF
GO
SET IDENTITY_INSERT [dbo].[Factura] ON 

INSERT [dbo].[Factura] ([FacturaId], [ClienteId], [MesaId], [MeseroId], [FechaHora]) VALUES (4, 2, 1, 1, CAST(N'2022-06-09T10:13:00.000' AS DateTime))
INSERT [dbo].[Factura] ([FacturaId], [ClienteId], [MesaId], [MeseroId], [FechaHora]) VALUES (5, 2, 1, 2, CAST(N'2022-06-03T13:18:00.000' AS DateTime))
INSERT [dbo].[Factura] ([FacturaId], [ClienteId], [MesaId], [MeseroId], [FechaHora]) VALUES (6, 2, 1, 1, CAST(N'2022-05-08T12:06:00.000' AS DateTime))
INSERT [dbo].[Factura] ([FacturaId], [ClienteId], [MesaId], [MeseroId], [FechaHora]) VALUES (7, 3, 1, 1, CAST(N'2022-06-09T22:28:00.000' AS DateTime))
INSERT [dbo].[Factura] ([FacturaId], [ClienteId], [MesaId], [MeseroId], [FechaHora]) VALUES (8, 3, 1, 1, CAST(N'2022-05-09T23:25:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[Factura] OFF
GO
SET IDENTITY_INSERT [dbo].[Mesa] ON 

INSERT [dbo].[Mesa] ([MesaId], [NroMesa], [Nombre], [Reservada], [Puestos]) VALUES (1, 1, N'Mesa Grande', 1, 4)
SET IDENTITY_INSERT [dbo].[Mesa] OFF
GO
SET IDENTITY_INSERT [dbo].[Mesero] ON 

INSERT [dbo].[Mesero] ([MeseroId], [Nombres], [Apellidos], [Edad], [Antiguedad]) VALUES (1, N'Natalia', N'Ramirez', 25, 2)
INSERT [dbo].[Mesero] ([MeseroId], [Nombres], [Apellidos], [Edad], [Antiguedad]) VALUES (2, N'Paola', N'Vanegas', 24, 2)
SET IDENTITY_INSERT [dbo].[Mesero] OFF
GO
SET IDENTITY_INSERT [dbo].[Supervisor] ON 

INSERT [dbo].[Supervisor] ([SupervirsorId], [Nombres], [Apellidos], [Edad], [Antiguedad]) VALUES (1, N'Carlos', N'Villamizar', 30, 4)
SET IDENTITY_INSERT [dbo].[Supervisor] OFF
GO
ALTER TABLE [dbo].[DetalleXFactura]  WITH CHECK ADD  CONSTRAINT [FK_DetalleXFactura_Factura] FOREIGN KEY([FacturaId])
REFERENCES [dbo].[Factura] ([FacturaId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DetalleXFactura] CHECK CONSTRAINT [FK_DetalleXFactura_Factura]
GO
ALTER TABLE [dbo].[DetalleXFactura]  WITH CHECK ADD  CONSTRAINT [FK_DetalleXFactura_Supervisor] FOREIGN KEY([SupervisorId])
REFERENCES [dbo].[Supervisor] ([SupervirsorId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DetalleXFactura] CHECK CONSTRAINT [FK_DetalleXFactura_Supervisor]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Cliente] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Cliente] ([ClienteId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Cliente]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Mesa] FOREIGN KEY([MesaId])
REFERENCES [dbo].[Mesa] ([MesaId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Mesa]
GO
ALTER TABLE [dbo].[Factura]  WITH CHECK ADD  CONSTRAINT [FK_Factura_Mesero1] FOREIGN KEY([MeseroId])
REFERENCES [dbo].[Mesero] ([MeseroId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Factura] CHECK CONSTRAINT [FK_Factura_Mesero1]
GO
USE [master]
GO
ALTER DATABASE [BDRestaurante] SET  READ_WRITE 
GO
