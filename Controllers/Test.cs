using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Prototype1.Models;
using MySql.Data.MySqlClient;
using UserProfile.Models;

namespace Prototype1.Controllers;

public class Test : Controller
{
    public IActionResult Test1()
    {
        return View();
    }

    [HttpPost]
    public IActionResult GetDetails()
    {
        LoginModel umodel = new LoginModel();
        umodel.Name = HttpContext.Request.Form["txtName"].ToString();
        umodel.Password = HttpContext.Request.Form["txtPass"].ToString();
        // umodel.ID = Convert.ToInt32(HttpContext.Request.Form["txtID"]);
        // umodel.Address = HttpContext.Request.Form["txtAddress"].ToString();
        // umodel.Email = HttpContext.Request.Form["txtEmail"].ToString();
        int result = umodel.SaveDetails();
        Console.WriteLine(result);
        if (result > 0)
        {
            ViewBag.Result = "Logged In Successfully";
        }
        else
        {
            ViewBag.Result = "Something Went Wrong";
        }
        return View("Test1");
    }
}