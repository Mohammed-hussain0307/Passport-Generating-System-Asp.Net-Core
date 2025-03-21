using Microsoft.AspNetCore.Mvc;
using PassportGeneratingSystem.DAL;
using PassportGeneratingSystem.Models;

namespace PassportGeneratingSystem.Controllers
{
    public class AdminController : Controller
    {
        Admin_DAL admin = new Admin_DAL();

        [HttpGet]
        public IActionResult Admin()
        {
            return View();
        }
        public IActionResult Admin(Login login)
        {
            try
            {
                var adminn = admin.AdminLogin(login);

                if (adminn.Any(a => a.LoginID == login.LoginID && a.Password == login.Password))
                {
                    return RedirectToAction("AdminMain");
                }
                else
                {
                    TempData["Error"] = "Login Failed";
                    return View();
                }
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                return View();
            }
        }

        public IActionResult AdminMain()
        {
            return View();
        }

        public IActionResult AllApplication()
        {
            try
            {
                var adminn = admin.Applications();
                    return View(adminn);
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                return View();
            }
        }
    }
}
