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

        public IActionResult GetByID(int id)
        {
            try
            {
                var user = admin.GetApplication(id).FirstOrDefault();
                if(user != null)
                {
                    return View(user);
                }
                else
                {
                    TempData["Error"] = "User not found";
                    return RedirectToAction("AdminMain");
                }
            }
            catch(Exception e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction("AdminMain");
            }            
        }

        [HttpPost]
        public IActionResult Approved(UserDetail user,string actionType)
        {
            try
            {
                if (actionType == "approve")
                {
                    bool isApprove = admin.Approved(user);
                    if (isApprove)
                    {
                        TempData["Success"] = "Application approved";
                        return RedirectToAction("AdminMain");
                    }
                    else
                    {
                        TempData["Error"] = "Cant approve the application";
                        return RedirectToAction("AdminMain");
                    }
                }
                else if(actionType == "reject")
                {
                    bool isApprove = admin.Rejected(user);
                    if (isApprove)
                    {
                        TempData["Success"] = "Application rejected";
                        return RedirectToAction("AdminMain");
                    }
                    else
                    {
                        TempData["Error"] = "Cant reject the application";
                        return RedirectToAction("AdminMain");
                    }
                }
                else
                {
                    return RedirectToAction("AdminMain");
                }
            }
            catch(Exception e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction("AdminMain");
            }
        }
    }
}
