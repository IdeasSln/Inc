using Inc.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Inc.Controllers
{
    public class MainController : Controller
    {
        //[Authorize]
        public ActionResult Index()
        {
            dynamic dropdowndata = new ExpandoObject();
            dropdowndata.Gender = SQLFUNC.GetTableValues("INCIDENT_Gender").Select(x => new ViewModel_DropDownGrid() { TableId = x.TableId, Description = x.Description, Active = x.Active }).ToList();
            dropdowndata.Disposition = SQLFUNC.GetTableValues("INCIDENT_Disposition").Select(x => new ViewModel_DropDownGrid() { TableId = x.TableId, Description = x.Description, Active = x.Active }).ToList();
            dropdowndata.IncidentType = SQLFUNC.GetTableValues("INCIDENT_TYPE").Select(x => new ViewModel_DropDownGrid() { TableId = x.TableId, Description = x.Description, Active = x.Active }).ToList();
            dropdowndata.Location = SQLFUNC.GetTableValues("INCIDENT_Location").Select(x => new ViewModel_DropDownGrid() { TableId = x.TableId, Description = x.Description, Active = x.Active }).ToList();
            dropdowndata.POI_Type = SQLFUNC.GetTableValues("INCIDENT_POI_TYPE").Select(x => new ViewModel_DropDownGrid() { TableId = x.TableId, Description = x.Description, Active = x.Active }).ToList();
            dropdowndata.Report_Written_By = SQLFUNC.GetTableValues("INCIDENT_Report_Written_By").Select(x => new ViewModel_DropDownGrid() { TableId = x.TableId, Description = x.Description, Active = x.Active }).ToList();
            dropdowndata.Report_Reviewed_By = SQLFUNC.GetTableValues("INCIDENT_Report_Reviewed_By").Select(x => new ViewModel_DropDownGrid() { TableId = x.TableId, Description = x.Description, Active = x.Active }).ToList();
            dropdowndata.EquipmentType = SQLFUNC.GetTableValues("INCIDENT_Equipment_Type").Select(x => new ViewModel_DropDownGrid() { TableId = x.TableId, Description = x.Description, Active = x.Active }).ToList();
            dropdowndata.EquipmentStatus = SQLFUNC.GetTableValues("INCIDENT_Equipment_Status").Select(x => new ViewModel_DropDownGrid() { TableId = x.TableId, Description = x.Description, Active = x.Active }).ToList();
            dropdowndata.POIType = SQLFUNC.GetTableValues("INCIDENT_POI_Type").Select(x => new ViewModel_DropDownGrid() { TableId = x.TableId, Description = x.Description, Active = x.Active }).ToList();
            return View(dropdowndata);
        }

        [HttpPost]
        public JsonResult NewComplaint(Complaint cmpt)
        {
            if (cmpt.Id == 0)
            {
                SQLFUNC.InsertNewComplaint(cmpt);
            }
            else
            {
                SQLFUNC.UpdateComplaint(cmpt);
            }
            return Json(true);
        }
        [HttpPost]
        public JsonResult GetMaingridIncident()
        {
            return Json(SQLFUNC.GetMaingridComplaints());
        }

        [HttpPost]
        public JsonResult GetIncident(int ComplaintId)
        {
            return new JsonResult() { Data = SQLFUNC.GetComplaints(ComplaintId), MaxJsonLength = 2147483647 };
        }

        [HttpPost]
        public JsonResult ConvertToB64(IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                var imageFile = files.First();
                byte[] image = new byte[imageFile.ContentLength];
                imageFile.InputStream.Read(image, 0, image.Length);
                string strBase64 = Convert.ToBase64String(image);
                string strExtension = imageFile.ContentType.Split('/')[1];
                string base64Path = string.Format("data:image/{0};base64,{1}", strExtension, strBase64);
                return Json(base64Path);
            }

            return Json("");
        }
        [HttpPost]
        public ActionResult RemoveImage(string[] fileNames)
        {
            return Content("");
        }
    }
}