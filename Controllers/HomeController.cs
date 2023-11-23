using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Prototype1.Models;
using MySql.Data.MySqlClient;
using UserProfile.Models;

namespace Prototype1.Controllers;
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {

        return View("Index", "~/Views/Shared/_MyLayout.cshtml");
    }

    [HttpPost]
    public IActionResult PostDetails()
    {
        LoginModel umodel = new LoginModel();

        umodel.Name = HttpContext.Request.Form["txtName"].ToString();
        umodel.Password = HttpContext.Request.Form["txtPass"].ToString();
        umodel.Type = HttpContext.Request.Form["txtType"].ToString();

        int result = umodel.SaveDetails();

        if (result == 4)
        {
            ViewBag.Result = "Logged In Successfully";
            return View("LandingPage", "~/Views/Shared/_Layout.cshtml");
        }
        else if (result == 5)
        {
            ViewBag.Result = "Logged in User Succesfully";
            return View("Employee", "~/Views/Shared/_MyLayout.cshtml");
        }
        else
        {
            ViewBag.Result = "Username Or Password Is Incorrect";
        }

        return View("Index", "~/Views/Shared/_MyLayout.cshtml");
    }

        public IActionResult Home()
    {
        return View("LandingPage", "~/Views/Shared/_Layout.cshtml");
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Employee()
    {
        return View("Employee", "~/Views/Shared/_MyLayout.cshtml");//This function is responsible to get the view page to the UI
    }
    public IActionResult EmployeeList()
    {
        return View();
    }
    public IActionResult Create()
    {
        return View();
    }
    public IActionResult Profile()
    {
        return View();
    }
    [HttpPost]
    public IActionResult GetDetails()
    {
        UserDataModel umodel = new UserDataModel();
        umodel.Name = HttpContext.Request.Form["txtName"].ToString();
        umodel.ID = HttpContext.Request.Form["txtID"].ToString();
        // umodel.ID = Convert.ToInt32(HttpContext.Request.Form["txtID"]);
        umodel.Address = HttpContext.Request.Form["txtAddress"].ToString();
        umodel.Email = HttpContext.Request.Form["txtEmail"].ToString();
        int result = umodel.SaveDetails();
        if (result > 0)
        {
            ViewBag.Result = "Data Saved Successfully";
        }
        else
        {
            ViewBag.Result = "Something Went Wrong";
        }
        return View("Profile");
    }

    public IActionResult Salary()
    {
        return View("Salary");
    }
    [HttpPost]
    public IActionResult PostSalary()
    {
        SalaryModel umodel = new SalaryModel();
        umodel.ID = HttpContext.Request.Form["txtID"].ToString();
        umodel.Amount = HttpContext.Request.Form["txtAmount"].ToString();

        int result = umodel.SaveDetails();
        if (result > 0)
        {
            ViewBag.Result = "Data Saved Successfully";
        }
        else
        {
            ViewBag.Result = "Something Went Wrong";
        }
        return View("Salary");
    }

    public IActionResult SalaryList()
    {
        return View("SalaryList");
    }

    public IActionResult MemberShip()
    {
        return View("MemberShip");
    }
    public IActionResult Test()
    {
        return View("Test");
    }
    [HttpPost]
    public IActionResult PostMember()
    {
        MemberModel umodel = new MemberModel();
        umodel.Name = HttpContext.Request.Form["txtName"].ToString();
        umodel.ID = HttpContext.Request.Form["txtID"].ToString();
        umodel.Date = HttpContext.Request.Form["txtStart"].ToString();
        umodel.Contribution = HttpContext.Request.Form["txtContribution"].ToString();

        int result = umodel.SaveDetails();
        if (result > 0)
        {
            ViewBag.Result = "Data Saved Successfully";
        }
        else
        {
            ViewBag.Result = "Something Went Wrong";
        }
        return View("MemberShip");
    }

    public IActionResult MemberView()
    {
        return View("MemberView");
    }

    public IActionResult ProvidentFund()
    {
        return View("ProvidentFund");
    }
    [HttpPost]
    public IActionResult PostPF()
    {
        PFModel umodel = new PFModel();
        umodel.PeriodFrom = HttpContext.Request.Form["txtFrom"].ToString();
        umodel.PeriodTo = HttpContext.Request.Form["txtTo"].ToString();
        umodel.LastMonth = HttpContext.Request.Form["txtLast"].ToString();
        // umodel.Invest = Convert.ToDouble(HttpContext.Request.Form["txtTK"]);
        // umodel.Profit = Convert.ToDouble(HttpContext.Request.Form["txtProfit"]);



        int result = umodel.checkFund();
        if (result > 0)
        {
            ViewBag.Result = "Data Saved Successfully";
        }
        else
        {
            ViewBag.Result = "Something Went Wrong";
        }
        return View("ProvidentFund");
    }

    public IActionResult InitializePF()
    {
        return View("InitializePF");
    }
    [HttpPost]
    public IActionResult StartPF()
    {
        StartFundModel umodel = new StartFundModel();
        umodel.Period_from = HttpContext.Request.Form["txtFrom"].ToString();

        umodel.Period_to = HttpContext.Request.Form["txtTo"].ToString();

        umodel.Employee_ID = HttpContext.Request.Form["txtId"].ToString();

        umodel.Employee_balance_start = Convert.ToDouble(HttpContext.Request.Form["txtObec"]);

        umodel.Company_balance_start = Convert.ToDouble(HttpContext.Request.Form["txtObcc"]);

        umodel.Employee_amount = Convert.ToDouble(HttpContext.Request.Form["txtAec"]);

        umodel.Company_amount = Convert.ToDouble(HttpContext.Request.Form["txtAcc"]);

        umodel.Employee_profit = Convert.ToDouble(HttpContext.Request.Form["txtEprofit"]);

        umodel.Company_profit = Convert.ToDouble(HttpContext.Request.Form["txtCprofit"]);

        umodel.Employee_balance_close = Convert.ToDouble(HttpContext.Request.Form["txtCbe"]);

        umodel.Company_balance_close = Convert.ToDouble(HttpContext.Request.Form["txtCce"]);


        int result = umodel.SaveDetails();
        if (result > 0)
        {
            ViewBag.Result = "Data Saved Successfully";
        }
        else
        {
            ViewBag.Result = "Something Went Wrong";
        }
        return View("InitializePF");
    }

    public IActionResult PFList()
    {
        return View("PFList");
    }

    public IActionResult Profit()
    {
        return View("Profit");
    }

    [HttpPost]
    public IActionResult GetProfit()
    {
        ProfitModel umodel = new ProfitModel();
        umodel.Invest = Convert.ToDouble(HttpContext.Request.Form["txtTK"]);
        umodel.PeriodFrom = HttpContext.Request.Form["txtFrom"].ToString();
        umodel.PeriodTo = HttpContext.Request.Form["txtTo"].ToString();
        umodel.LastMonth = HttpContext.Request.Form["txtLM"].ToString();
        umodel.Profit = Convert.ToDouble(HttpContext.Request.Form["txtProfit"]);



        int result = umodel.SaveDetails();
        if (result > 0)
        {
            ViewBag.Result = "Data Saved Successfully";
        }
        else
        {
            ViewBag.Result = "Something Went Wrong";
        }
        return View("Profit");
    }


    public IActionResult ProfitShow()
    {
        return View("ProfitShow");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
