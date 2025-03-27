using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PracticeDemo.DAL;

namespace PracticeDemo.Controllers
{
    public class ServiceController : Controller
    {
        // GET: ServiceController
        public ActionResult Index()
        {
            return View();
        }

        public IActionResult ServiceType()
        {
            Service_DAL service_DAL = new Service_DAL();
            try
            {
                var serviceType = service_DAL.Service();

                if(serviceType != null)
                {
                    return View(serviceType);
                }
                else
                {
                    TempData["Error"] = "Not found";
                    return RedirectToAction("Index");
                }
            }
            catch(Exception e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction("Index");
            }
        }
    }
}
