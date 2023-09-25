using CRMAPP.DataAccess.Contracts.Language;
using CRMAPP.Utility;
using Microsoft.AspNetCore.Mvc;

namespace CRMAPP.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class HomeController : Controller
    {
        public readonly ILanguage _languageSrv;
        public HomeController(ILanguage languageSrv)
        {   
            _languageSrv = languageSrv;
        }
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SetLanguage(string language)
        {
            StaticData.selectedLanguage = language;
            return Json(new { data = true });
        }

    }
}
