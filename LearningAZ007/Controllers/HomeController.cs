using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LearningAZ007.Models;
using Microsoft.Data.SqlClient;

namespace LearningAZ007.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        CourseVM courseVM = new CourseVM();
        //string mySetting = _configuration.GetConnectionString("DataConnection");
        string mySetting = "Server=tcp:appserver4005.database.windows.net,1433;Initial Catalog=appdb;Persist Security Info=False;User ID=sqladmin;Password=admin@123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        var sqlConnection = new SqlConnection(mySetting);
        sqlConnection.Open();
        var sqlCommand = new SqlCommand("SELECT COURSE_ID,COURSE_NAME,RATING FROM COURSE", sqlConnection);
        courseVM.CourseList = new List<Course>();
        using (var reader = sqlCommand.ExecuteReader())
        {
            while (reader.Read())
            {
                courseVM.CourseList.Add(new Course
                {
                    CourseId = Convert.ToInt32(reader["COURSE_ID"]),
                    CourseName = reader["COURSE_NAME"].ToString(),
                    Rating = Convert.ToInt32(reader["RATING"])
                });
            }
        }
        return View(courseVM);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
