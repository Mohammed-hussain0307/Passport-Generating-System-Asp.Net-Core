using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PassportGeneratingSystem.DAL;
using PassportGeneratingSystem.Models;

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
                return View();
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
                var user = userDetail_DAL.GetUserById(id);

                if(user != null)
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

        [HttpPost]
        public IActionResult Edit(UserDetail userDetail)
        {
            try
            {
                bool isUpdate = userDetail_DAL.UpdateUser(userDetail);

                if (isUpdate)
                {
                    TempData["Success"] = "Your detail updated";
                }
                else
                {
                    TempData["Error"] = "Cant update your detail";
                }
            }
            catch(Exception e)
            {
                TempData["Error"] = e.Message;
                return View();
            }
            return View("~/Views/Home/UserMain.cshtml");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                bool isDelete = userDetail_DAL.DeleteUser(id);
                if (isDelete)
                {
                    TempData["Success"] = "Succesfully removed";
                }
            }
            catch (Exception e)
            {
                TempData["Error"] = e.Message;
            }
            return View();
        }
    }
}
