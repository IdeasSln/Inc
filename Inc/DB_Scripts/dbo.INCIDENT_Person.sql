USE [ComplaintsContext-20180325194636]
GO

/****** Object: Table [dbo].[INCIDENT_Person] Script Date: 5/6/2018 12:03:49 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[INCIDENT_Person] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [Last_Name]          NVARCHAR (MAX) NULL,
    [First_Name]         NVARCHAR (MAX) NULL,
    [Middle_Name]        NVARCHAR (MAX) NULL,
    [DOB]                DATETIME       NULL,
    [Street]             NVARCHAR (MAX) NULL,
    [City]               NVARCHAR (MAX) NULL,
    [State]              NVARCHAR (MAX) NULL,
    [Zip]                NVARCHAR (MAX) NULL,
    [Home_Number]        NVARCHAR (MAX) NULL,
    [Mobile_Number]      NVARCHAR (MAX) NULL,
    [Other_Number]       NVARCHAR (MAX) NULL,
    [AKA]                NVARCHAR (MAX) NULL,
    [Gender_Id]          INT            NULL,
    [Audit_createdby]    NVARCHAR (MAX) NULL,
    [Audit_modifiedby]   NVARCHAR (MAX) NULL,
    [Audit_createddate]  DATETIME       NULL,
    [Audit_modifieddate] DATETIME       NULL
);


