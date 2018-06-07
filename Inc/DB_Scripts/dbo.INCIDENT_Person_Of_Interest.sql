USE [ComplaintsContext-20180325194636]
GO

/****** Object: Table [dbo].[INCIDENT_Person_Of_Interest] Script Date: 5/6/2018 12:03:58 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[INCIDENT_Person_Of_Interest] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [Comments]           NVARCHAR (MAX) NULL,
    [Photo]              NVARCHAR (MAX) NULL,
    [Person_Id]          INT            NULL,
    [Type_Id]            INT            NULL,
    [Complaint_Id]       INT            NULL,
    [Audit_createdby]    NVARCHAR (MAX) NULL,
    [Audit_modifiedby]   NVARCHAR (MAX) NULL,
    [Audit_createddate]  DATETIME       NULL,
    [Audit_modifieddate] DATETIME       NULL
);


