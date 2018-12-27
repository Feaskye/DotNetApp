using Aspose.Cells;
using SkyTeleFault.App_Start;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SkyTeleFault.Controllers
{
    public class HomeController : ControllerBase
    {
        DBAccessHelper _dBAccessHelper = new DBAccessHelper();

        public ActionResult Index()
        {
            var data = _dBAccessHelper.GetDataTable();
            return View(data);
        }




        [HttpGet]
        public ActionResult AddEleFault()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ActionResult AddEleFault(NameValueCollection collection)
        {
            collection = Request.Form;
            DateTime dateTime = DateTime.Now;
            if (!DateTime.TryParse(collection["AddTime"].ToString(),out dateTime))
            {
                return JsonResult("时间格式有误");
            }
            _dBAccessHelper.InsertData(collection);
            return JsonResult(true);
        }


        [HttpGet]
        public ActionResult ImportFile(int id)
        {
            var dtdata = _dBAccessHelper.GetDataTable(id);
            if (dtdata == null || dtdata.Rows.Count <= 0)
            {
                return Content("该项无数据");
            }
            var template = Server.MapPath("/template.xls");
            if (System.IO.File.Exists(template))
            {
                WorkbookDesigner wb = new WorkbookDesigner();
                wb.Open(template);
                Workbook workbook = wb.Workbook;
                Worksheet sheet = workbook.Worksheets[0];
                Aspose.Cells.Cells cells = sheet.Cells;

                DataRow dr = dtdata.Rows[0];
                //序号
                cells["B2"].PutValue(dr["InputMan"].ToString());
                cells["E2"].PutValue(dr["AddTime"].ToString());
                cells["A5"].PutValue(dr["LineName"].ToString());
                cells["B5"].PutValue(dr["UpDownOpen"].ToString());
                cells["C5"].PutValue(dr["LineNumber"].ToString());
                cells["D5"].PutValue(dr["LineCompany"].ToString());
                cells["E5"].PutValue(dr["CompanyTel"].ToString());
                cells["F5"].PutValue(dr["OutputTime"].ToString());
                cells["G5"].PutValue(dr["CommissionTime"].ToString());
                cells["C6"].PutValue(dr["IsAnatomy"].ToString() == "1" ? "是" : "否");
                cells["G6"].PutValue(dr["IsSendTele"].ToString() == "1" ? "是" : "否");
                cells["A9"].PutValue(dr["ConstructCompany"].ToString());
                cells["B9"].PutValue(dr["ConstructMan"].ToString());
                cells["C9"].PutValue(dr["ConstructPhone"].ToString());
                cells["D9"].PutValue(dr["FaultDate"].ToString());
                cells["E9"].PutValue(dr["FaultType"].ToString());
                cells["F9"].PutValue(dr["FaultReason"].ToString());
                cells["A12"].PutValue(dr["导体"].ToString());
                cells["B12"].PutValue(dr["半导体"].ToString());
                cells["C12"].PutValue(dr["绝缘层"].ToString());
                cells["D12"].PutValue(dr["屏蔽层"].ToString());
                cells["E12"].PutValue(dr["铠装"].ToString());
                cells["F11"].PutValue(dr["XinNumber"].ToString());
                cells["A15"].PutValue(dr["RepairCompany"].ToString());
                cells["B15"].PutValue(dr["RepairMan"].ToString());
                cells["C15"].PutValue(dr["RepairPhone"].ToString());
                cells["D15"].PutValue(dr["FinishTime"].ToString());
                cells["E15"].PutValue(dr["UseTime"].ToString());
                cells["F15"].PutValue(dr["Description"].ToString());

                sheet.AutoFitRows();

                //byte[] bytes = new byte[1024];
                //using (var stream = new FileStream(Server.MapPath("/"+DateTime.Now.ToString("yyyyMMddHHmmss")+".xls"), FileMode.OpenOrCreate, FileAccess.ReadWrite))
                //{
                //    workbook.Save(stream, FileFormatType.Excel97To2003);//ContentDisposition.Attachment, 
                //    stream.Read(bytes, 0, bytes.Length);
                //    return File(bytes, "application/vnd.ms-excel", dr["InputMan"].ToString() + "电缆（故障）信息一览表.xls");
                //}
                MemoryStream stream = new MemoryStream();
                workbook.Save(stream, FileFormatType.Excel97To2003);
                WriteStream(stream, dr["InputMan"].ToString() + "电缆（故障）信息一览表.xls");
            }
            return Content("导出失败");
        }


        private void WriteStream(MemoryStream memoryStream, string excelFileName)
        {
            HttpContext context = System.Web.HttpContext.Current;
            context.Response.Clear();
            context.Response.AddHeader("content-disposition", String.Format("attachment;filename ={0}", excelFileName));
            memoryStream.WriteTo(context.Response.OutputStream);
            memoryStream.Close();
            context.Response.End();
        }


    }
}
/*
 { ".xla", "application/vnd.ms-excel" },
{ ".xlc", "application/vnd.ms-excel" },
{ ".xll", "application/x-excel" },
{ ".xlm", "application/vnd.ms-excel" },
{ ".xls", "application/vnd.ms-excel" },
{ ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
{ ".xlt", "application/vnd.ms-excel" },
{ ".xlw", "application/vnd.ms-excel" },
     */
