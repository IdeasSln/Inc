
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.IO;
using System.Web.Mvc;
using System;

namespace Inc.Controllers
{
    public class ReportsController : Controller
    {
      
      [HttpGet]
        public FileStreamResult PrintReport(int RptId)
        {


            DataSet dtReport = SQLFUNC.GetIncidentReport(RptId);
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/ProjectReports"), "EquipmentReport.rpt"));
            rd.SetDataSource(dtReport);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "EquipmentReport.pdf");

            //PrintEqReport(RptId);
            //DataSet dtReport = SQLFUNC.GetIncidentReport(RptId);
            //ReportDocument rd = new ReportDocument();
            //rd.Load(Path.Combine(Server.MapPath("~/ProjectReports"), "IncidentReport.rpt"));
            //rd.SetDataSource(dtReport.Tables["Complaints"]);
            //Response.Buffer = false;
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            //stream.Seek(0, SeekOrigin.Begin);
            //return File(stream, "application/pdf", "IncidentReport.pdf");
        }

        private FileStreamResult PrintEqReport(int rptId)
        {
            DataSet dtReport = SQLFUNC.GetIncidentReport(rptId);
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/ProjectReports"), "EquipmentReport.rpt"));
            rd.SetDataSource(dtReport);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            stream.Seek(0, SeekOrigin.Begin);
            return File(stream, "application/pdf", "EquipmentReport.pdf");
        }
    }
}