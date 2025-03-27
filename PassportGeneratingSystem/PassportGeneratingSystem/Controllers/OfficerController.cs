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

        public IActionResult AllApplication()
        {
            try
            {
                var officerr = officer.Applications();
                return View(officerr);
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
                var user = officer.GetApplication(id).FirstOrDefault();
                if (user != null)
                {
                    return View(user);
                }
                else
                {
                    TempData["Error"] = "User not found";
                    return RedirectToAction("OfficerMain");
                }
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction("OfficerMain");
            }
        }

        [HttpPost]
        public IActionResult Approved(UserDetail user, string actionType)
        {
            try
            {
                if (actionType == "verify")
                {
                    bool isApprove = officer.Verified(user);
                    if (isApprove)
                    {
                        TempData["Success"] = "Application Verified";
                        return RedirectToAction("OfficerMain");
                    }
                    else
                    {
                        TempData["Error"] = "Cant verified the application";
                        return RedirectToAction("OfficerMain");
                    }
                }
                else if (actionType == "reject")
                {
                    bool isApprove = officer.Rejected(user);
                    if (isApprove)
                    {
                        TempData["Success"] = "Application rejected";
                        return RedirectToAction("OfficerMain");
                    }
                    else
                    {
                        TempData["Error"] = "Cant reject the application";
                        return RedirectToAction("OfficerMain");
                    }
                }
                else
                {
                    return RedirectToAction("OfficerMain");
                }
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction("OfficerMain");
            }
        }

        public IActionResult ViewPdf(int id)
        {
            try
            {
                var viewPdf = officer.GetApplication(id).FirstOrDefault();

                if (viewPdf?.Document != null || viewPdf?.Document?.Length > 0)
                {
                    return File(viewPdf.Document, "application/pdf");
                }
                else
                {
                    TempData["Error"] = "File not found";
                    return RedirectToAction("AdminMain");
                }
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction("AdminMain");
            }
        }

        public IActionResult AllOfficer()
        {
            try
            {
                var allofficer = officer.ViewOfficer();
                return View(allofficer);
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction("OfficerMain");
            }
        }

        public IActionResult AddOfficer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddOfficer(NewUser addOfficer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (addOfficer.Password == addOfficer.ConfirmPassword)
                    {
                        bool isAdded = officer.CreateOfficer(addOfficer);
                        if (isAdded)
                        {
                            TempData["Success"] = "Admin detail added successfully";
                        }
                        else
                        {
                            TempData["Error"] = "Cant add admin detail";
                        }
                    }
                    else
                    {
                        TempData["Error"] = "Password doesnot match";
                        return View();
                    }
                }
                return RedirectToAction("OfficerMain");
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
                return RedirectToAction("OfficerMain");
            }
        }
    }
}
