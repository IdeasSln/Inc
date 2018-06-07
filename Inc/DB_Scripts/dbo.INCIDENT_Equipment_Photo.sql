USE [ComplaintsContext-20180325194636]
GO

/****** Object: Table [dbo].[INCIDENT_Equipment_Photo] Script Date: 5/6/2018 12:02:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[INCIDENT_Equipment_Photo] (
    [Equipment_Id]       INT            NULL,
    [Photo]              VARCHAR (MAX)  NULL,
    [Description]        VARCHAR (MAX)  NULL,
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [Audit_createdby]    NVARCHAR (MAX) NULL,
    [Audit_modifiedby]   NVARCHAR (MAX) NULL,
    [Audit_createddate]  DATETIME       NULL,
    [Audit_modifieddate] DATETIME       NULL
);


