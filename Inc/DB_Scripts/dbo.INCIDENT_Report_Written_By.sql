USE [ComplaintsContext-20180325194636]
GO

/****** Object: Table [dbo].[INCIDENT_Report_Written_By] Script Date: 5/6/2018 12:04:31 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[INCIDENT_Report_Written_By] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [Description]        NVARCHAR (MAX) NULL,
    [Active]             BIT            NULL,
    [Audit_createdby]    NVARCHAR (MAX) NULL,
    [Audit_modifiedby]   NVARCHAR (MAX) NULL,
    [Audit_createddate]  DATETIME       NULL,
    [Audit_modifieddate] DATETIME       NULL
);


