
using Inc.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Inc.Controllers
{
    public class AdminController : Controller
    {
        //[Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetGridData(int tableId)
        {
            List<ViewModel_DropDownGrid> dropDownGrid = null;
            string tableName = GetGridName(tableId);

            dropDownGrid = SQLFUNC.GetTableValues(tableName).Select(x => new ViewModel_DropDownGrid() { TableId = x.TableId, Description = x.Description, Active = x.Active }).ToList();
            return Json(dropDownGrid);
        }
        [HttpPost]
        public JsonResult UpdateGridData(int tableId, List<ViewModel_DropDownGrid> gridData)
        {
            string tableName = GetGridName(tableId);
            foreach (ViewModel_DropDownGrid viewModel in gridData)
            {
                if (viewModel.TableId == 0)
                {
                    SQLFUNC.InsertTableValues(tableName, viewModel);
                }
                else
                {
                    SQLFUNC.UpdateTableValues(tableName, viewModel);
                }
            }
            return Json(true);
        }
        private string GetGridName(int tableId)
        {
            switch (tableId)
            {
                case 1:
                    return "INCIDENT_Gender";
                case 2:
                    return "INCIDENT_Type";
                case 3:
                    return "INCIDENT_Location";
                case 4:
                    return "INCIDENT_Disposition";
                case 5:
                    return "INCIDENT_Report_Written_By";
                case 6:
                    return "INCIDENT_Report_Reviewed_By";
                case 7:
                    return "INCIDENT_Equipment_Type";
                case 8:
                    return "INCIDENT_Equipment_Status";
                case 9:
                    return "INCIDENT_POI_Type";
                default:
                    return null;
            }
        }
    }
}