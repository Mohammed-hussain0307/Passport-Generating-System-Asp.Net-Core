using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PassportGeneratingSystem.DAL;
using PassportGeneratingSystem.Models;

namespace PassportGeneratingSystem.Controllers;

public class HomeController : Controller
{
    User_DAL user = new User_DAL();

    public int userId;
    public IActionResult Index()
    {
        return View();
    }

    //Get Create Page
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(NewUser newUser)
    {
        bool isAdded = false;

        try
        {
            if (ModelState.IsValid)
            {
                if (newUser.Password == newUser.ConfirmPassword)
                {
                    isAdded = user.CreateUser(newUser);

                    if (isAdded)
                    {
                        TempData["Success"] = "Your detail successfully registered";
                    }
                }
                else
                {
                    TempData["Error"] = "Entered password not matched";
                    return View();
                }
            }
        }
        catch(Exception e)
        {
            TempData["Error"] = e.Message;
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(Login login)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var userList = user.UserLogin(login);

                if (userList.Any(u => u.LoginID == login.LoginID && u.Password == login.Password))
                {
                    return RedirectToAction("UserMain");
                }
                else
                {
                    TempData["Error"] = "Invalid user id or password";
                    return RedirectToAction("Login");
                }
            }
        }
        catch(Exception e)
        {
            TempData["Error"] = e.Message;            
        }
        return View();
    }

    [HttpGet]
    public IActionResult UserMain()
    {
        return View();
    }
    
    public IActionResult Contact()
    {
        return View();
    }
}
