using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PassportGeneratingSystem.DAL;
using PassportGeneratingSystem.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace PassportGeneratingSystem.Controllers
{
    public class UserController : Controller
    {
        public static int userId;
        UserDetail_DAL userDetail_DAL = new UserDetail_DAL();

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserDetail userDetail)
        {
            try { 
            bool isAdded = false;

                if (ModelState.IsValid)
                {                    
                    isAdded = userDetail_DAL.Register(userDetail,userId);
                    if (isAdded)
                    {
                        TempData["Success"] = "Your deatils successfully registered";
                    }
                    else
                    {
                        TempData["Error"] = "Cant register your details";
                    }
                }
            }
            catch(Exception e)
            {
                TempData["Error"] = e.Message;
                return View("~/Views/Home/UserMain.cshtml");
            }
            return View("~/Views/Home/UserMain.cshtml");
        }

        //view all user
        [HttpGet]
        public IActionResult AllUser()
        {
            try
            {
                var userList = userDetail_DAL.GetAllUser(userId);
                return View(userList);
            }
            catch(Exception e)
            {
                TempData["Error"] = e.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {                
              var user = userDetail_DAL.GetUserById(id).FirstOrDefault();

              if (user != null)
              {
                 return View(user);
              }
            }
            catch(Exception e)
            {
                TempData["Error"] = e.Message;
            }
            return View();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                bool isDelete = userDetail_DAL.DeleteUser(id);
                if (isDelete)
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
            }
            return View("~/Views/Home/UserMain.cshtml");
        }

        [HttpGet]
        public IActionResult Booking()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(UserDetail user, string actionType,IFormFile file)
        {
            try
            {
                if (actionType == "update")
                {
                    if (ModelState.IsValid)
                    {
                        if (file != null && file.Length > 0)
                        {
                            using (var memoryStream = new MemoryStream())
                            {
                                file.CopyTo(memoryStream);
                                user.Document = memoryStream.ToArray();
                            }
                        }

                        bool isUpdate = userDetail_DAL.UpdateUser(user);

                        if (isUpdate)
                        {
                            TempData["Success"] = "Your detail updated";
                            return View();
                        }
                        else
                        {
                            TempData["Error"] = "Cant update your detail";
                        }
                    }
                }
                else if (actionType == "apply")
                {
                    user.status = "submit";
                    bool isBooked = userDetail_DAL.Booking(user);

                    if (isBooked)
                    {
                        TempData["Success"] = "Successfully apply";
                    }
                    else
                    {
                        TempData["Error"] = "Cant apply your form";
                    }
                }
                else
                {
                    TempData["Error"] = "Some error in this user detail";
                }
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
            }
            return View("~/Views/Home/UserMain.cshtml");
        }
    }
}
