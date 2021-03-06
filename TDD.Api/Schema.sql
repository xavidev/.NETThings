﻿IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = 'AUTHENTICATION')
DROP DATABASE [AUTHENTICATION]

CREATE DATABASE [AUTHENTICATION]
GO

USE [AUTHENTICATION]
GO

CREATE TABLE [dbo].[USERS](
    [UserName] [nvarchar](30) NOT NULL PRIMARY KEY,
    [Password] [nvarchar](100) NOT NULL
)
GO
