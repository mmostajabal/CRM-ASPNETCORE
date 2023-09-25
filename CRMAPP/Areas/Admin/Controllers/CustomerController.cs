using CRMAPP.DataAccess.Contracts.Customers;
using CRMAPP.DataAccess.Contracts.Language;
using CRMAPP.DataAccess.Data;
using CRMAPP.DataAccess.Migrations;
using CRMAPP.DataAccess.Services.Customers;
using CRMAPP.Models;
using CRMAPP.Utility;
using CRMAPP.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CRMAPP.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticData.ROLE_ADMIN)]
   
    public class CustomerController : Controller
    {
        private readonly ApplicationDBContext _db;
        private readonly ICustomer _customerSrv;
        private readonly ILanguage _languageSrv;
        /// <summary>
        /// CustomerController
        /// </summary>
        /// <param name="db"></param>
        /// <param name="customerSrv"></param>
        /// <param name="languageSrv"></param>        
        public CustomerController(ApplicationDBContext db, ICustomer customerSrv, ILanguage languageSrv)
        {
            _db = db;
            _customerSrv = customerSrv;
            _languageSrv = languageSrv;
        }

        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            
            return View();
        }

        /// <summary>
        /// Create
        /// 
        /// </summary>
        /// <param name="customerNo"></param>
        /// <returns></returns>
        public IActionResult Create(string? customerNo)
        {
                CustomerVM customerVM;
                if (!string.IsNullOrEmpty(customerNo))
                {
                    customerVM = _customerSrv.GetByCustomerNo(customerNo).GetAwaiter().GetResult();
                }
                else
                {
                    customerVM = new CustomerVM();
                }

                return PartialView(customerVM);
        }
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="customerVM"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomerVM customerVM)
        {
            if (ModelState.IsValid)
            {
                customerVM.UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
                if (string.IsNullOrEmpty(customerVM.CustomerNo))
                {
                    _customerSrv.Add(customerVM).GetAwaiter().GetResult();
                    TempData["success"] = _languageSrv.TranslateSrv("customerCreated");
                }
                else
                {
                    _customerSrv.Update(customerVM).GetAwaiter().GetResult();
                    TempData["success"] = _languageSrv.TranslateSrv("customerUpdated");
                }
                return RedirectToAction("Index");
            }
            return View(customerVM);
        }

        #region API Section
        /// <summary>
        /// Get All Customer
        /// </summary>
        /// <returns></returns>
        [HttpGet]        
        public IActionResult GetAll()
        {
            IList<CustomerVM> customers = _customerSrv.GetAll().GetAwaiter().GetResult();
            return Json(new { data = customers });

        }
        /// <summary>
        /// Get
        /// </summary>
        /// <param name="custno"></param>
        /// <returns></returns>
        [HttpGet]        
        public IActionResult Get(string custno)
        {
            IList<CustomerVM> customers = _customerSrv.GetAll().GetAwaiter().GetResult();
            return Json(new { data = customers });
        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="customerNo"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(string customerNo)
        {
            CustomerVM customerVM = _customerSrv.GetByCustomerNo(customerNo).GetAwaiter().GetResult();
            if (customerVM == null) return Json(false);

            customerVM.UserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            customerVM.Status = 2;
            _customerSrv.Update(customerVM).GetAwaiter().GetResult();

            return Json( true );
        }
        #endregion
    }
}
