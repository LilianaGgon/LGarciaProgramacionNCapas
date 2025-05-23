USE [master]
GO
/****** Object:  Database [LGarciaProgramacionNCapas]    Script Date: 4/22/2025 2:28:04 PM ******/
CREATE DATABASE [LGarciaProgramacionNCapas]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LGarciaProgramacionNCapas', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\LGarciaProgramacionNCapas.mdf' , SIZE = 3136KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'LGarciaProgramacionNCapas_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\LGarciaProgramacionNCapas_log.ldf' , SIZE = 1088KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LGarciaProgramacionNCapas].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET ARITHABORT OFF 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET  ENABLE_BROKER 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET RECOVERY FULL 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET  MULTI_USER 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'LGarciaProgramacionNCapas', N'ON'
GO
USE [LGarciaProgramacionNCapas]
GO
/****** Object:  StoredProcedure [dbo].[CambiarEstatus]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CambiarEstatus] 
@IdUsuario INT,
@Estatus BIT
AS
BEGIN
	UPDATE Usuario
	SET Estatus = @Estatus
	WHERE IdUsuario = @IdUsuario
END
GO
/****** Object:  StoredProcedure [dbo].[CandidatoAdd]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CandidatoAdd]
@Nombre VARCHAR(50), 
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50),
@Edad VARCHAR(10),
@Correo VARCHAR(128), 
@Telefono VARCHAR(20),
@Direccion VARCHAR(500),
@Foto VARBINARY(MAX),
@Curriculum VARBINARY(MAX),
@IdUniversidad INT,
@IdCarrera INT,
@IdBolsaTrabajo INT,
@IdVacante INT
AS
BEGIN
	INSERT INTO Candidato (Nombre, ApellidoPaterno, ApellidoMaterno, Edad,
			Correo, Telefono, Direccion, Foto, Curriculum,
			IdUniversidad, IdCarrera, IdBolsaTrabajo, IdVacante)
		VALUES (@Nombre, @ApellidoPaterno, @ApellidoMaterno, @Edad,
			@Correo, @Telefono, @Direccion, @Foto, @Curriculum, 
			@IdUniversidad, @IdCarrera, @IdBolsaTrabajo, @IdVacante)
END
GO
/****** Object:  StoredProcedure [dbo].[CandidatoDelete]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CandidatoDelete]
@IdCandidato INT
AS
BEGIN
	DELETE FROM Candidato WHERE IdCandidato = @IdCandidato
END
GO
/****** Object:  StoredProcedure [dbo].[CandidatoGetAll]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CandidatoGetAll]
AS
BEGIN
	SELECT IdCandidato, Candidato.Nombre, ApellidoPaterno,
	ApellidoMaterno, Edad, Correo, Telefono, Direccion,
	Foto, Curriculum, Candidato.IdUniversidad, Universidad.Nombre AS NombreUniversidad,
	Candidato.IdCarrera, Carrera.Nombre AS NombreCarrera, 
	Candidato.IdBolsaTrabajo, BolsaTrabajo.Nombre AS NombreBolsaTrabajo,
	Candidato.IdVacante, Vacante.Nombre AS NombreVacante
	FROM Candidato
	LEFT JOIN Universidad ON Candidato.IdUniversidad = Universidad.IdUniversidad
	LEFT JOIN Carrera ON Candidato.IdCarrera = Carrera.IdCarrera
	LEFT JOIN BolsaTrabajo ON Candidato.IdBolsaTrabajo = BolsaTrabajo.IdBolsaTrabajo
	LEFT JOIN Vacante ON Candidato.IdVacante = Vacante.IdVacante
END
GO
/****** Object:  StoredProcedure [dbo].[CandidatoGetById]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CandidatoGetById]
@IdCandidato INT
AS
BEGIN
	SELECT IdCandidato, Candidato.Nombre, ApellidoPaterno,
	ApellidoMaterno, Edad, Correo, Telefono, Direccion,
	Foto, Curriculum, Candidato.IdUniversidad, Universidad.Nombre AS NombreUniversidad,
	Candidato.IdCarrera, Carrera.Nombre AS NombreCarrera, 
	Candidato.IdBolsaTrabajo, BolsaTrabajo.Nombre AS NombreBolsaTrabajo,
	Candidato.IdVacante, Vacante.Nombre AS NombreVacante
	FROM Candidato
	LEFT JOIN Universidad ON Candidato.IdUniversidad = Universidad.IdUniversidad
	LEFT JOIN Carrera ON Candidato.IdCarrera = Carrera.IdCarrera
	LEFT JOIN BolsaTrabajo ON Candidato.IdBolsaTrabajo = BolsaTrabajo.IdBolsaTrabajo
	LEFT JOIN Vacante ON Candidato.IdVacante = Vacante.IdVacante
	WHERE IdCandidato = @IdCandidato
END
GO
/****** Object:  StoredProcedure [dbo].[CandidatoUpdate]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CandidatoUpdate]
@IdCandidato INT,
@Nombre VARCHAR(50), 
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50),
@Edad VARCHAR(10),
@Correo VARCHAR(128), 
@Telefono VARCHAR(20),
@Direccion VARCHAR(500),
@Foto VARBINARY(MAX),
@Curriculum VARBINARY(MAX),
@IdUniversidad INT,
@IdCarrera INT,
@IdBolsaTrabajo INT,
@IdVacante INT
AS
BEGIN
	UPDATE Candidato 
	SET Nombre = @Nombre, ApellidoPaterno = @ApellidoPaterno, ApellidoMaterno = @ApellidoMaterno,
		Edad = @Edad, Correo = @Correo, Telefono = @Telefono, Direccion = @Direccion, Foto = @Foto,
		Curriculum = @Curriculum, IdUniversidad = @IdUniversidad, IdCarrera = @IdCarrera, 
		IdBolsaTrabajo = @IdBolsaTrabajo, IdVacante = @IdVacante
	WHERE IdCandidato = @IdCandidato
END
GO
/****** Object:  StoredProcedure [dbo].[ColoniaGetByIdMunicipio]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[ColoniaGetByIdMunicipio]
@IdMunicipio INT
AS
BEGIN 
	SELECT
	IdColonia, 
	Nombre
	FROM Colonia
	WHERE IdMunicipio = @IdMunicipio
END
GO
/****** Object:  StoredProcedure [dbo].[DireccionGetByIdUsuario]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DireccionGetByIdUsuario]
--Parametros de entrada
@IdUsuario INT
AS
BEGIN --{
	SELECT
	Usuario.IdUsuario,
	Direccion.IdDireccion,
	Colonia.Nombre AS NombreColonia,
	Colonia.IdColonia,
	Municipio.Nombre AS NombreMunicipio,
	Municipio.IdMunicipio,
	Estado.Nombre AS NombreEstado,
	Estado.IdEstado
	FROM Usuario 
	LEFT JOIN Direccion ON Usuario.IdUsuario = Direccion.IdUsuario
	LEFT JOIN Colonia ON Direccion.IdColonia = Colonia.IdColonia
	LEFT JOIN Municipio ON Colonia.IdMunicipio = Municipio.IdMunicipio
	LEFT JOIN Estado ON Municipio.IdMunicipio = Estado.IdEstado
	WHERE Usuario.IdUsuario = @IdUsuario
END--}
GO
/****** Object:  StoredProcedure [dbo].[EstadoGetAll]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[EstadoGetAll]
AS
BEGIN
	SELECT
	IdEstado, 
	Nombre
	FROM Estado
END
GO
/****** Object:  StoredProcedure [dbo].[GBIUDPrueba]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[GBIUDPrueba]
--Parametros de entrada
@IdUsuario INT
AS 
BEGIN --{
	SELECT Usuario.IdUsuario,
	Usuario.Nombre AS NombreUsuario,
	ApellidoPaterno, 
	ApellidoMaterno, 
	Celular,
	UserName,
	Email,
	Password,
	CONVERT(VARCHAR, FechaNacimiento, 103) AS FechaNacimiento, 
	Sexo, 
	Telefono,
	Estatus, 
	CURP, 
	IdRol,
	Direccion.Calle,
	Direccion.NumeroInterior,
	Direccion.NumeroExterior,
	Direccion.IdColonia AS DireccionIdColonia,
	Colonia.IdColonia,
	Colonia.IdMunicipio AS ColoniaIdMunicipio,
	Colonia.Nombre AS NombreColonia,
	Municipio.IdMunicipio,
	Municipio.IdEstado AS MunicipioIdEstado,
	Municipio.Nombre AS NombreMunicipio,
	Estado.IdEstado,
	Estado.Nombre AS NombreEstado
	FROM Usuario
	LEFT JOIN Direccion ON Usuario.IdUsuario = Direccion.IdUsuario
	LEFT JOIN Colonia ON Direccion.IdColonia = Colonia.IdColonia
	LEFT JOIN Municipio ON Colonia.IdMunicipio = Municipio.IdMunicipio
	LEFT JOIN Estado ON Municipio.IdEstado  = Estado.IdEstado
	WHERE Usuario.IdUsuario = @IdUsuario
END --}
GO
/****** Object:  StoredProcedure [dbo].[GetAllPrueba]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetAllPrueba]
--Parametros de entrada
AS 
BEGIN --{
	SELECT Usuario.IdUsuario,
	Usuario.Nombre AS NombreUsuario,
	ApellidoPaterno, 
	ApellidoMaterno, 
	Celular,
	UserName,
	Email,
	Password,
	CONVERT(VARCHAR, FechaNacimiento, 103) AS FechaNacimiento, 
	Sexo, 
	Telefono,
	Estatus, 
	CURP,
	Imagen, 
	IdRol,
	Direccion.Calle,
	Direccion.NumeroInterior,
	Direccion.NumeroExterior,
	Direccion.IdColonia AS DireccionIdColonia,
	Colonia.IdColonia,
	Colonia.IdMunicipio AS ColoniaIdMunicipio,
	Municipio.IdMunicipio,
	Municipio.IdEstado AS MunicipioIdEstado,
	Estado.IdEstado
	FROM Usuario
	LEFT JOIN Direccion ON Usuario.IdUsuario = Direccion.IdUsuario
	LEFT JOIN Colonia ON Direccion.IdColonia = Colonia.IdColonia
	LEFT JOIN Municipio ON Colonia.IdMunicipio = Municipio.IdMunicipio
	LEFT JOIN Estado ON Municipio.IdMunicipio = Estado.IdEstado
END --}
GO
/****** Object:  StoredProcedure [dbo].[MunicipioGetByIdEstado]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[MunicipioGetByIdEstado]
@IdEstado INT
AS
BEGIN
	SELECT
	IdMunicipio,
	Nombre
	FROM Municipio
	WHERE IdEstado = @IdEstado
END
GO
/****** Object:  StoredProcedure [dbo].[UniversidadGetAll]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UniversidadGetAll]
AS
BEGIN
	SELECT IdUniversidad, Nombre
	FROM Universidad
END
GO
/****** Object:  StoredProcedure [dbo].[UsuarioAdd]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioAdd]
--Parametros de entrada
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50),
@Celular VARCHAR(20),
@UserName VARCHAR(50),
@Email VARCHAR(254),
@Password VARCHAR(50),
@FechaNacimiento DATE, 
@Sexo CHAR(2),
@Telefono VARCHAR(20),
@Estatus BIT,
@CURP VARCHAR(50),
@Imagen VARBINARY(MAX),
@IdRol INT
AS 
BEGIN --{
	INSERT INTO Usuario(Nombre, ApellidoPaterno, ApellidoMaterno, Celular, UserName, Email, Password,
		FechaNacimiento, Sexo, Telefono, Estatus, CURP, Imagen, IdRol) 
		VALUES (@Nombre, @ApellidoPaterno, @ApellidoMaterno, @Celular, @UserName, @Email, @Password,
		@FechaNacimiento, @Sexo, @Telefono, @Estatus, @CURP, @Imagen, @IdRol)
END --}
GO
/****** Object:  StoredProcedure [dbo].[UsuarioDelete]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioDelete]
--Parametros de entrada
@IdUsuario INT
AS 
BEGIN --{
	DELETE FROM Usuario WHERE IdUsuario = @IdUsuario
END --}
GO
/****** Object:  StoredProcedure [dbo].[UsuarioDireccioDelete]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioDireccioDelete]
@IdUsuario INT
AS 
BEGIN 
	BEGIN TRANSACTION 
		BEGIN TRY 
			DELETE FROM Direccion WHERE IdUsuario = @IdUsuario
			DELETE FROM Usuario WHERE IdUsuario = @IdUsuario

			COMMIT
		END TRY

		BEGIN CATCH 
			ROLLBACK
		END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[UsuarioDireccionAdd]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioDireccionAdd]
@Nombre VARCHAR(50), 
@ApellidoPaterno VARCHAR(50), 
@ApellidoMaterno VARCHAR(50),
@Celular VARCHAR(20), 
@UserName VARCHAR(50), 
@Email VARCHAR(254),
@Password VARCHAR(50), 
@FechaNacimiento DATE, 
@Sexo CHAR(2), 
@Telefono VARCHAR(20), 
@CURP VARCHAR(50), 
--@Imagen VARBINARY(max) SOLO SE MANDA SI NO SE USAN WEB SERVICES,  
@IdRol INT,
@Calle VARCHAR(50), 
@NumeroInterior VARCHAR(20), 
@NumeroExterior VARCHAR(20), 
@IdColonia INT
AS 
BEGIN 
	DECLARE @IdUsuario INT
	BEGIN TRANSACTION 
		BEGIN TRY 
			INSERT INTO Usuario (
				Nombre, ApellidoPaterno, 
				ApellidoMaterno, Celular, 
				UserName, Email,
				Password, FechaNacimiento, 
				Sexo, Telefono, 
				Estatus, CURP, 
				Imagen, 
				IdRol)
				VALUES (
				@Nombre, @ApellidoPaterno, 
				@ApellidoMaterno, @Celular, 
				@UserName, @Email,
				@Password, @FechaNacimiento, 
				@Sexo, @Telefono, 
				'False', @CURP, 
				-- @Imagen SOLO SE MANDA SI NO SE USAN WEB SERVICES, 
				NULL, --SOLO PARA WEB SERVICES
				@IdRol)

			SET @IdUsuario = @@IDENTITY

			INSERT INTO Direccion (
				Calle, NumeroInterior, 
				NumeroExterior, IdColonia, 
				IdUsuario)
			VALUES (
				@Calle, @NumeroInterior, 
				@NumeroExterior, @IdColonia, 
				@IdUsuario)

			COMMIT TRANSACTION
		END TRY

		BEGIN CATCH 
			ROLLBACK TRANSACTION
		END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[UsuarioDireccioUpdate]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioDireccioUpdate]
@IdUsuario INT,
@Nombre VARCHAR(50), 
@ApellidoPaterno VARCHAR(50), 
@ApellidoMaterno VARCHAR(50),
@Celular VARCHAR(20), 
@UserName VARCHAR(50), 
@Email VARCHAR(254),
@Password VARCHAR(50), 
@FechaNacimiento DATE, 
@Sexo CHAR(2), 
@Telefono VARCHAR(20), 
@Estatus BIT, 
@CURP VARCHAR(50), 
--@Imagen VARBINARY(max) SOLO SE MANDA SI NO SE USAN WEB SERVICES, 
@IdRol INT,
@Calle VARCHAR(50), 
@NumeroInterior VARCHAR(20), 
@NumeroExterior VARCHAR(20), 
@IdColonia INT
AS 
BEGIN 
	BEGIN TRANSACTION 
		BEGIN TRY 
			UPDATE Usuario 
			SET
				Nombre = @Nombre, 
				ApellidoPaterno = @ApellidoPaterno, 
				ApellidoMaterno = @ApellidoMaterno, 
				Celular = @Celular, 
				UserName = @UserName, 
				Email = @Email,
				Password = @Password, 
				FechaNacimiento = @FechaNacimiento, 
				Sexo = @Sexo, 
				Telefono = @Telefono, 
				Estatus = @Estatus, 
				CURP = @CURP, 
				--Imagen = @Imagen SOLO SE MANDA SI NO SE USAN WEB SERVICES, 
				Imagen = NULL, --SOLO PARA WEB SERVICES
				IdRol = @IdRol
			WHERE IdUsuario = @IdUsuario
			
			UPDATE Direccion 
			SET 
				Calle = @Calle, 
				NumeroInterior = @NumeroInterior, 
				NumeroExterior = @NumeroExterior, 
				IdColonia = @IdColonia
			WHERE IdUsuario = @IdUsuario

			COMMIT TRANSACTION
		END TRY

		BEGIN CATCH 
			ROLLBACK TRANSACTION
		END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[UsuarioGetAll]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioGetAll]
--Parametros de entrada
AS 
BEGIN --{
	SELECT 
	Usuario.IdUsuario,
	Usuario.Nombre AS NombreUsuario,
	Usuario.ApellidoPaterno, 
	Usuario.ApellidoMaterno, 
	Usuario.Celular,
	Usuario.UserName,
	Usuario.Email,
	Usuario.Password,
	CONVERT(VARCHAR, Usuario.FechaNacimiento) AS FechaNacimiento, 
	Usuario.Sexo, 
	Usuario.Telefono,
	Usuario.Estatus, 
	Usuario.CURP, 
	Usuario.Imagen,
	Rol.Nombre AS NombreRol,
	Rol.IdRol,
	Direccion.Calle,
	Direccion.NumeroInterior,
	Direccion.NumeroExterior,
	Colonia.IdColonia,
	Colonia.Nombre AS NombreColonia,
	Municipio.IdMunicipio,
	Municipio.Nombre AS NombreMunicipio,
	Estado.IdEstado,
	Estado.Nombre AS NombreEstado
	FROM Usuario

	left join Rol ON Usuario.IdRol = Rol.IdRol
	LEFT JOIN Direccion ON Usuario.IdUsuario = Direccion.IdUsuario
	LEFT JOIN Colonia ON Direccion.IdColonia = Colonia.IdColonia
	LEFT JOIN Municipio ON Colonia.IdMunicipio = Municipio.IdMunicipio
	LEFT JOIN Estado ON Municipio.IdEstado = Estado.IdEstado
END --}
GO
/****** Object:  StoredProcedure [dbo].[UsuarioGetAllBusquedaAbierta]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioGetAllBusquedaAbierta]
--Parametros de entrada
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50),
@IdRol INT
AS 
BEGIN --{
/*
IF (@Nombre='' AND @ApellidoPaterno='' AND @ApellidoMaterno='' AND @IdRol=0)
	--Si se cumple se muestran todos
	BEGIN
		SELECT
		Usuario.IdUsuario, Usuario.Nombre AS NombreUsuario, Usuario.ApellidoPaterno, Usuario.ApellidoMaterno, Usuario.Celular,
		Usuario.UserName, Usuario.Email, Usuario.Password, CONVERT(VARCHAR, Usuario.FechaNacimiento) AS FechaNacimiento, 
		Usuario.Sexo, Usuario.Telefono, Usuario.Estatus, Usuario.CURP, Usuario.Imagen, Rol.Nombre AS NombreRol, Rol.IdRol,
		Direccion.Calle, Direccion.NumeroInterior, Direccion.NumeroExterior, Direccion.IdColonia AS DireccionIdColonia,
		Colonia.IdColonia, Colonia.Nombre AS NombreColonia, Colonia.IdMunicipio AS ColoniaIdMunicipio, Municipio.IdMunicipio, 
		Municipio.Nombre AS NombreMunicipio, Municipio.IdEstado AS MunicipioIdEstado, Estado.IdEstado, Estado.Nombre AS NombreEstado
		FROM Usuario
		LEFT JOIN Rol ON Usuario.IdRol = Rol.IdRol
		LEFT JOIN Direccion ON Usuario.IdUsuario = Direccion.IdUsuario
		LEFT JOIN Colonia ON Direccion.IdColonia = Colonia.IdColonia
		LEFT JOIN Municipio ON Colonia.IdMunicipio = Municipio.IdMunicipio
		LEFT JOIN Estado ON Municipio.IdEstado = Estado.IdEstado
	END
ELSE 
	--Si no se cumple se filtran
	BEGIN
		SELECT
		Usuario.IdUsuario, Usuario.Nombre AS NombreUsuario, Usuario.ApellidoPaterno, Usuario.ApellidoMaterno, Usuario.Celular,
		Usuario.UserName, Usuario.Email, Usuario.Password, CONVERT(VARCHAR, Usuario.FechaNacimiento) AS FechaNacimiento, 
		Usuario.Sexo, Usuario.Telefono, Usuario.Estatus, Usuario.CURP, Usuario.Imagen, Rol.Nombre AS NombreRol, Rol.IdRol,
		Direccion.Calle, Direccion.NumeroInterior, Direccion.NumeroExterior, Direccion.IdColonia AS DireccionIdColonia,
		Colonia.IdColonia, Colonia.Nombre AS NombreColonia, Colonia.IdMunicipio AS ColoniaIdMunicipio, Municipio.IdMunicipio, 
		Municipio.Nombre AS NombreMunicipio, Municipio.IdEstado AS MunicipioIdEstado, Estado.IdEstado, Estado.Nombre AS NombreEstado
		FROM Usuario
		LEFT JOIN Rol ON Usuario.IdRol = Rol.IdRol
		LEFT JOIN Direccion ON Usuario.IdUsuario = Direccion.IdUsuario
		LEFT JOIN Colonia ON Direccion.IdColonia = Colonia.IdColonia
		LEFT JOIN Municipio ON Colonia.IdMunicipio = Municipio.IdMunicipio
		LEFT JOIN Estado ON Municipio.IdEstado = Estado.IdEstado
		WHERE 
		(@Nombre LIKE '' OR Usuario.Nombre LIKE '%'+@Nombre+'%') 
		AND (@ApellidoPaterno LIKE '' OR Usuario.ApellidoPaterno LIKE '%'+@ApellidoPaterno+'%')
		AND (@ApellidoMaterno LIKE '' OR Usuario.ApellidoMaterno LIKE '%'+@ApellidoMaterno+'%')
		AND (@IdRol = 0 or Usuario.IdRol = @IdRol)
	END 
	*/


	DECLARE @SELECT NVARCHAR(MAX);
	SET @SELECT = 'SELECT
		Usuario.IdUsuario, Usuario.Nombre AS NombreUsuario, Usuario.ApellidoPaterno, Usuario.ApellidoMaterno, Usuario.Celular,
		Usuario.UserName, Usuario.Email, Usuario.Password, CONVERT(VARCHAR, Usuario.FechaNacimiento) AS FechaNacimiento, 
		Usuario.Sexo, Usuario.Telefono, Usuario.Estatus, Usuario.CURP, Usuario.Imagen, Rol.Nombre AS NombreRol, Rol.IdRol,
		Direccion.Calle, Direccion.NumeroInterior, Direccion.NumeroExterior, Colonia.IdColonia, Colonia.Nombre AS NombreColonia, Municipio.IdMunicipio, 
		Municipio.Nombre AS NombreMunicipio, Estado.IdEstado, Estado.Nombre AS NombreEstado
		FROM Usuario
		LEFT JOIN Rol ON Usuario.IdRol = Rol.IdRol
		LEFT JOIN Direccion ON Usuario.IdUsuario = Direccion.IdUsuario
		LEFT JOIN Colonia ON Direccion.IdColonia = Colonia.IdColonia
		LEFT JOIN Municipio ON Colonia.IdMunicipio = Municipio.IdMunicipio
		LEFT JOIN Estado ON Municipio.IdEstado = Estado.IdEstado ';
		print(@select)

	IF (@Nombre<>'' OR @ApellidoPaterno<>'' OR @ApellidoMaterno<>'' OR @IdRol<>0)
	BEGIN
		DECLARE @CONDICIONES NVARCHAR(MAX);
		SET @CONDICIONES = 'WHERE('''+@Nombre+''' = '''' OR Usuario.Nombre LIKE ''%' +@Nombre+'%'') 
			AND ('''+@ApellidoPaterno+''' = '''' OR Usuario.ApellidoPaterno LIKE ''%' +@ApellidoPaterno+'%'')
			AND ('''+@ApellidoMaterno+''' = '''' OR Usuario.ApellidoMaterno LIKE ''%' +@ApellidoMaterno+'%'')
			AND ('''+CONVERT(VARCHAR, @IdRol)+'''= 0 OR Usuario.IdRol = '+ CONVERT(VARCHAR, @IdRol)+')'
		SET @SELECT = @SELECT + @CONDICIONES;

		print(@CONDICIONES)
	END 

	EXEC sp_executesql @SELECT;
END --}


GO
/****** Object:  StoredProcedure [dbo].[UsuarioGetAllViewBusquedaAbierta]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioGetAllViewBusquedaAbierta]
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50),
@IdRol INT
AS 
BEGIN --{

IF (@Nombre='' AND @ApellidoPaterno='' AND @ApellidoMaterno='' AND @IdRol=0)
	--Si se cumple se muestran todos
	BEGIN
		SELECT
		IdUsuario, NombreUsuario, ApellidoPaterno, ApellidoMaterno, Celular, UserName, Email, Password, FechaNacimiento, 
		Sexo, Telefono, Estatus, CURP, Imagen, NombreRol, IdRol, Calle, NumeroInterior, NumeroExterior,
		IdColonia, NombreColonia, IdMunicipio, NombreMunicipio, IdEstado, NombreEstado
		FROM UsuarioGetAllView
	END
ELSE 
	--Si no se cumple se filtran
	BEGIN
		SELECT
		IdUsuario, NombreUsuario, ApellidoPaterno, ApellidoMaterno, Celular, UserName, Email, Password, FechaNacimiento, 
		Sexo, Telefono, Estatus, CURP, Imagen, NombreRol, IdRol, Calle, NumeroInterior, NumeroExterior,
		IdColonia, NombreColonia, IdMunicipio, NombreMunicipio, IdEstado, NombreEstado
		FROM UsuarioGetAllView
		WHERE 
		(@Nombre LIKE '' OR NombreUsuario LIKE '%'+@Nombre+'%') 
		AND (@ApellidoPaterno LIKE '' OR ApellidoPaterno LIKE '%'+@ApellidoPaterno+'%')
		AND (@ApellidoMaterno LIKE '' OR ApellidoMaterno LIKE '%'+@ApellidoMaterno+'%')
		AND (@IdRol = 0 or IdRol = @IdRol)
	END 
END --}
GO
/****** Object:  StoredProcedure [dbo].[UsuarioGetById]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioGetById]
--Parametros de entrada
@IdUsuario INT
AS 
BEGIN --{
	SELECT Usuario.IdUsuario,
	Usuario.Nombre AS NombreUsuario,
	ApellidoPaterno, 
	ApellidoMaterno, 
	Celular,
	UserName,
	Email,
	Password,
	CONVERT(VARCHAR, FechaNacimiento) AS FechaNacimiento, 
	Sexo, 
	Telefono,
	Estatus, 
	CURP,
	Imagen, 
	IdRol,
	Direccion.Calle,
	Direccion.NumeroInterior,
	Direccion.NumeroExterior,
	Direccion.IdColonia AS DireccionIdColonia,
	Colonia.IdColonia,
	Colonia.IdMunicipio AS ColoniaIdMunicipio,
	Colonia.Nombre AS NombreColonia,
	Municipio.IdMunicipio,
	Municipio.IdEstado AS MunicipioIdEstado,
	Municipio.Nombre AS NombreMunicipio,
	Estado.IdEstado,
	Estado.Nombre AS NombreEstado
	FROM Usuario
	LEFT JOIN Direccion ON Usuario.IdUsuario = Direccion.IdUsuario
	LEFT JOIN Colonia ON Direccion.IdColonia = Colonia.IdColonia
	LEFT JOIN Municipio ON Colonia.IdMunicipio = Municipio.IdMunicipio
	LEFT JOIN Estado ON Municipio.IdEstado  = Estado.IdEstado
	WHERE Usuario.IdUsuario = @IdUsuario
END --}
GO
/****** Object:  StoredProcedure [dbo].[UsuarioUpdate]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UsuarioUpdate]
--Parametros de entrada
@IdUsuario INT,
@Nombre VARCHAR(50),
@ApellidoPaterno VARCHAR(50),
@ApellidoMaterno VARCHAR(50),
@Celular VARCHAR(20),
@UserName VARCHAR(50),
@Email VARCHAR(254),
@Password VARCHAR(50),
@FechaNacimiento DATE, 
@Sexo CHAR(2),
@Telefono VARCHAR(20),
@Estatus BIT,
@CURP VARCHAR(50),
@IdRol INT
AS 
BEGIN --{
	UPDATE Usuario 
	SET Nombre = @Nombre, 
	ApellidoPaterno = @ApellidoPaterno, 
	ApellidoMaterno = @ApellidoMaterno, 
	Celular = @Celular, 
	UserName = @UserName, 
	Email = @Email, 
	Password = @Password, 
	FechaNacimiento = @FechaNacimiento,
	Sexo = @Sexo, 
	Telefono = @Telefono, 
	Estatus = @Estatus, 
	CURP = @CURP,
	IdRol = @IdRol
	WHERE IdUsuario = @IdUsuario
END --}
GO
/****** Object:  Table [dbo].[BolsaTrabajo]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BolsaTrabajo](
	[IdBolsaTrabajo] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdBolsaTrabajo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Candidato]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Candidato](
	[IdCandidato] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[ApellidoPaterno] [varchar](50) NOT NULL,
	[ApellidoMaterno] [varchar](50) NULL,
	[Edad] [varchar](10) NOT NULL,
	[Correo] [varchar](128) NOT NULL,
	[Telefono] [varchar](20) NOT NULL,
	[Direccion] [varchar](500) NOT NULL,
	[Foto] [varbinary](max) NULL,
	[Curriculum] [varbinary](max) NULL,
	[IdUniversidad] [int] NULL,
	[IdCarrera] [int] NULL,
	[IdBolsaTrabajo] [int] NULL,
	[IdVacante] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCandidato] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Correo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Carrera]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Carrera](
	[IdCarrera] [int] NOT NULL,
	[Nombre] [varchar](500) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCarrera] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Colonia]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Colonia](
	[IdColonia] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[CodigoPostal] [varchar](10) NOT NULL,
	[IdMunicipio] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdColonia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Direccion]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Direccion](
	[IdDireccion] [int] IDENTITY(1,1) NOT NULL,
	[Calle] [varchar](50) NOT NULL,
	[NumeroInterior] [varchar](20) NULL,
	[NumeroExterior] [varchar](20) NOT NULL,
	[IdColonia] [int] NULL,
	[IdUsuario] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdDireccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Estado]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Estado](
	[IdEstado] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEstado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EstatusVacante]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EstatusVacante](
	[IdEstatusVacante] [tinyint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEstatusVacante] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Municipio]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Municipio](
	[IdMunicipio] [int] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[IdEstado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdMunicipio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Rol]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Rol](
	[IdRol] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Universidad]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Universidad](
	[IdUniversidad] [int] NOT NULL,
	[Nombre] [varchar](500) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdUniversidad] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Nombre] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Usuario](
	[IdUsuario] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[ApellidoPaterno] [varchar](50) NOT NULL,
	[ApellidoMaterno] [varchar](50) NULL,
	[Celular] [varchar](20) NULL,
	[UserName] [varchar](50) NOT NULL,
	[Email] [varchar](254) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[FechaNacimiento] [date] NOT NULL,
	[Sexo] [char](2) NOT NULL,
	[Telefono] [varchar](20) NOT NULL,
	[Estatus] [bit] NOT NULL,
	[CURP] [varchar](50) NULL,
	[Imagen] [varbinary](max) NULL,
	[IdRol] [int] NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[IdUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Vacante]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Vacante](
	[IdVacante] [int] NOT NULL,
	[Nombre] [varchar](500) NOT NULL,
	[FechaPublicacion] [datetime] NOT NULL,
	[FechaLimite] [datetime] NOT NULL,
	[UrlVacante] [varchar](500) NOT NULL,
	[IdEstatusVacante] [tinyint] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdVacante] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[UsuarioGetAllView]    Script Date: 4/22/2025 2:28:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[UsuarioGetAllView]
AS
	SELECT 
	Usuario.IdUsuario,
	Usuario.Nombre AS NombreUsuario,
	Usuario.ApellidoPaterno, 
	Usuario.ApellidoMaterno, 
	Usuario.Celular,
	Usuario.UserName,
	Usuario.Email,
	Usuario.Password,
	CONVERT(VARCHAR, Usuario.FechaNacimiento) AS FechaNacimiento, 
	Usuario.Sexo, 
	Usuario.Telefono,
	Usuario.Estatus, 
	Usuario.CURP, 
	Usuario.Imagen,
	Rol.Nombre AS NombreRol,
	Rol.IdRol,
	Direccion.Calle,
	Direccion.NumeroInterior,
	Direccion.NumeroExterior,
	Colonia.IdColonia,
	Colonia.Nombre AS NombreColonia,
	Municipio.IdMunicipio,
	Municipio.Nombre AS NombreMunicipio,
	Estado.IdEstado,
	Estado.Nombre AS NombreEstado
	FROM Usuario

	left join Rol ON Usuario.IdRol = Rol.IdRol
	LEFT JOIN Direccion ON Usuario.IdUsuario = Direccion.IdUsuario
	LEFT JOIN Colonia ON Direccion.IdColonia = Colonia.IdColonia
	LEFT JOIN Municipio ON Colonia.IdMunicipio = Municipio.IdMunicipio
	LEFT JOIN Estado ON Municipio.IdEstado = Estado.IdEstado


GO
ALTER TABLE [dbo].[Candidato]  WITH CHECK ADD FOREIGN KEY([IdBolsaTrabajo])
REFERENCES [dbo].[BolsaTrabajo] ([IdBolsaTrabajo])
GO
ALTER TABLE [dbo].[Candidato]  WITH CHECK ADD FOREIGN KEY([IdCarrera])
REFERENCES [dbo].[Carrera] ([IdCarrera])
GO
ALTER TABLE [dbo].[Candidato]  WITH CHECK ADD FOREIGN KEY([IdUniversidad])
REFERENCES [dbo].[Universidad] ([IdUniversidad])
GO
ALTER TABLE [dbo].[Candidato]  WITH CHECK ADD FOREIGN KEY([IdVacante])
REFERENCES [dbo].[Vacante] ([IdVacante])
GO
ALTER TABLE [dbo].[Colonia]  WITH CHECK ADD FOREIGN KEY([IdMunicipio])
REFERENCES [dbo].[Municipio] ([IdMunicipio])
GO
ALTER TABLE [dbo].[Direccion]  WITH CHECK ADD FOREIGN KEY([IdColonia])
REFERENCES [dbo].[Colonia] ([IdColonia])
GO
ALTER TABLE [dbo].[Direccion]  WITH CHECK ADD FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuario] ([IdUsuario])
GO
ALTER TABLE [dbo].[Municipio]  WITH CHECK ADD FOREIGN KEY([IdEstado])
REFERENCES [dbo].[Estado] ([IdEstado])
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD FOREIGN KEY([IdRol])
REFERENCES [dbo].[Rol] ([IdRol])
GO
ALTER TABLE [dbo].[Vacante]  WITH CHECK ADD FOREIGN KEY([IdEstatusVacante])
REFERENCES [dbo].[EstatusVacante] ([IdEstatusVacante])
GO
USE [master]
GO
ALTER DATABASE [LGarciaProgramacionNCapas] SET  READ_WRITE 
GO
