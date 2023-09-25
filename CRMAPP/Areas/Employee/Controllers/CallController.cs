using CRMAPP.DataAccess.Contracts.Customers;
using CRMAPP.DataAccess.Contracts.Calls;
using CRMAPP.DataAccess.Data;
using CRMAPP.DataAccess.Services.Customers;
using CRMAPP.Utility;
using CRMAPP.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using CRMAPP.DataAccess.Migrations;
using CRMAPP.DataAccess.Contracts.Language;

using FastReport;
using FastReport.Data;
using FastReport.Web;

namespace CRMAPP.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize(Roles=$"{StaticData.ROLE_EMPLOYEE}, {StaticData.ROLE_ADMIN}")]
    
    public class CallController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly ICustomer _customerSrv;
        private readonly ICalls _callSrv;
        private readonly ILanguage _languageSrv;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// CallController
        /// </summary>
        /// <param name="db"></param>
        /// <param name="customerSrv"></param>
        /// <param name="callSrv"></param>
        /// <param name="languageSrv"></param>
        /// <param name="hostingEnvironment"></param>
        /// <param name="configuration"></param>
        public CallController(ApplicationDBContext db, ICustomer customerSrv, ICalls callSrv, ILanguage languageSrv, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _db = db;
            _customerSrv = customerSrv;
            _callSrv = callSrv;
            _languageSrv = languageSrv;
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration; 
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <param name="customerNo"></param>
        /// <returns></returns>
        public IActionResult Index(string? customerNo)
        {
            IList<SelectListItem> customersList = _customerSrv.GetAll().GetAwaiter().GetResult().Select(item=>new SelectListItem
            {
                Value = item.CustomerNo,
                Text = item.CustomerSurName + ' ' + item.CustomerName,
                Selected = (item.CustomerNo == customerNo)
            }).ToList();

            return View(customersList);
        }

        /// <summary>
        /// Create ( GET)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Create(int id)
        {
            CallVM callVM;
            if (id != 0)
            {
                callVM= _callSrv.GetById(id).GetAwaiter().GetResult();
            }
            else
            {
                callVM = new CallVM();
            }

            return PartialView(callVM);
        }

        /// <summary>
        /// Create ( POST)
        /// </summary>
        /// <param name="callVM"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(CallVM callVM)
        {
            if (ModelState.IsValid)
            {
                callVM.UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
                if (callVM.Id == 0)
                {
                    _callSrv.Add(callVM).GetAwaiter().GetResult();
                    TempData["success"] =  _languageSrv.TranslateSrv("callRegisterd");
                }
                else
                {
                    _callSrv.Update(callVM).GetAwaiter().GetResult();
                    TempData["success"] = _languageSrv.TranslateSrv("callUpdated");
                }
                return RedirectToAction("Index", new { customerNo = callVM.CustomerNo });
            }

            return View(callVM);
        }

        /// <summary>
        /// CRMRep
        /// </summary>
        /// <returns></returns>
        public IActionResult CRMRep()
        {
            return View();
        }

        /// <summary>
        /// CallsPDFRep
        /// </summary>
        /// <returns></returns>
        public IActionResult CallsPDFRep()
        {
            FastReport.Utils.Config.WebMode = true;
            Report rep = new Report();
            string path = _hostingEnvironment.ContentRootPath + "\\FastReport\\CustomerCall.frx";
            var msSqlDataConnnection = new MsSqlDataConnection();
            msSqlDataConnnection.ConnectionString = _configuration.GetConnectionString("CRMConnection");
        
            rep.Load(path);
            
           List<CustomerCallVM> callsList = _callSrv.GetCustomerCalls().GetAwaiter().GetResult().ToList();

            rep.RegisterData(callsList, "CustomerCallRef");
            if (rep.Report.Prepare())
            {
                FastReport.Export.PdfSimple.PDFSimpleExport pdfExport = new FastReport.Export.PdfSimple.PDFSimpleExport();
                pdfExport.ShowProgress = false;
                pdfExport.Subject = "Customer Calls";
                pdfExport.Title  = "CRM";
                System.IO.MemoryStream ms = new MemoryStream();
                rep.Report.Export(pdfExport, ms);
                rep.Dispose();
                ms.Position = 0;
                return File(ms, "application/pdf", "customerCall.pf");
            }

            return null;
        }

        /// <summary>
        /// CallsHTMLRep
        /// </summary>
        /// <returns></returns>
        public IActionResult CallsHTMLRep()
        {
            FastReport.Utils.Config.WebMode = true;
            WebReport web = new WebReport();
            string path = _hostingEnvironment.ContentRootPath + "\\FastReport\\CustomerCall.frx";
            var msSqlDataConnnection = new MsSqlDataConnection();
            msSqlDataConnnection.ConnectionString = _configuration.GetConnectionString("CRMConnection");
            //rep.
            web.Report.Load(path);

            List<CustomerCallVM> callsList = _callSrv.GetCustomerCalls().GetAwaiter().GetResult().ToList();

            web.Report.RegisterData(callsList, "CustomerCallRef");

            return View(web);
        }

        #region API Section
        /// <summary>
        /// GetAll
        /// </summary>
        /// <param name="custno"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll(string custno, int status)
        {
            IList<CallVM> callsList = _callSrv.GetByCustomerNo(custno, status).GetAwaiter().GetResult().ToList();

            return Json(new { data = callsList });
        }
        /// <summary>
        /// GetCall
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
        [HttpGet]
        public IActionResult GetCall(int id)
        {
            CallVM? call= _callSrv.GetById(id).GetAwaiter().GetResult();
           
           return Json(call);
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            CallVM? callVM = _callSrv.GetById(id).GetAwaiter().GetResult();
            if (callVM == null) return Json(false);

            callVM.UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            callVM.Status = 2;
            _callSrv.Update(callVM).GetAwaiter().GetResult();

            return Json(true);
        }
        #endregion
    }
}
