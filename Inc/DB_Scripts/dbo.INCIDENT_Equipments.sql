USE [ComplaintsContext-20180325194636]
GO

/****** Object: Table [dbo].[INCIDENT_Equipments] Script Date: 5/6/2018 12:03:25 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[INCIDENT_Equipments] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [Photo]              NVARCHAR (MAX) NULL,
    [Value]              REAL           NOT NULL,
    [Occurance_Date]     DATETIME       NOT NULL,
    [Description]        NVARCHAR (MAX) NULL,
    [Incident_Id]        INT            NULL,
    [Status_Id]          INT            NULL,
    [Type_Id]            INT            NULL,
    [Complaint_Id]       INT            NULL,
    [Audit_createdby]    NVARCHAR (MAX) NULL,
    [Audit_modifiedby]   NVARCHAR (MAX) NULL,
    [Audit_createddate]  DATETIME       NULL,
    [Audit_modifieddate] DATETIME       NULL
);


