using DemoOne.DAL;
using DemoOne.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoOne.Controllers
{
    public class StudentController : Controller
    {
        StudentDetail_DAL studentDetail = new StudentDetail_DAL();
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentDetail student)
        {
            bool isCreate = false;

            try
            {
                if (ModelState.IsValid)
                {
                    isCreate = studentDetail.CreateStudent(student);

                    if (isCreate)
                    {
                        TempData["Success"] = "Created Successfully";
                    }
                }
            }
            catch(Exception e)
            {
                TempData["Error"] = e.Message;
            }
            return View();
        }
    }
}
