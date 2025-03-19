using Microsoft.AspNetCore.Mvc;
using PassportGeneratingSystem.DAL;
using PassportGeneratingSystem.Models;

namespace PassportGeneratingSystem.Controllers
{
    public class OfficerController : Controller
    {
        Police_DAL officer = new Police_DAL();

        [HttpGet]
        public IActionResult OfficerLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OfficerLogin(Login login)
        {
            try
            {
                var office = officer.Login(login);

                if (office.Any(p => p.LoginID == login.LoginID && p.Password == login.Password))
                {
                    return RedirectToAction("OfficerMain");
                }
                else
                {
                    TempData["Error"] = "invalid login id or password";
                    return View();
                }
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                return View();
            }
        }

        public IActionResult OfficerMain()
        {
            return View();
        }
    }
}
