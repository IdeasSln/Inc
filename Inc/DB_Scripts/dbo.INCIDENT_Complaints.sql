USE [ComplaintsContext-20180325194636]
GO

/****** Object: Table [dbo].[INCIDENT_Complaints] Script Date: 5/6/2018 12:02:24 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[INCIDENT_Complaints] (
    [Id]                      INT            IDENTITY (1, 1) NOT NULL,
    [Report_Date]             DATETIME       NULL,
    [Incident_Occurance_Date] DATETIME       NULL,
    [Narrative]               NVARCHAR (MAX) NULL,
    [Complainant_Id]          INT            NULL,
    [Disposition_Id]          INT            NULL,
    [Incident_Location_Id]    INT            NULL,
    [Incident_Type_Id]        INT            NULL,
    [Report_Reviewed_By_Id]   INT            NULL,
    [Report_Written_By_Id]    INT            NULL,
    [Audit_createdby]         NVARCHAR (MAX) NULL,
    [Audit_modifiedby]        NVARCHAR (MAX) NULL,
    [Audit_createddate]       DATETIME       NULL,
    [Audit_modifieddate]      DATETIME       NULL,
    [Be_Id]                   NVARCHAR (50)  NULL
);


