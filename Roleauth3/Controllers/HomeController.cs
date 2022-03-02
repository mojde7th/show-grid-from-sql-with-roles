
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Roleauth3.Data;
using Roleauth3.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Roleauth3.Controllers
{
    

    public class HomeController : Controller
        
    {

       
        private readonly ILogger<HomeController> _logger;

        string mainconn = @"Data Source=PERSONALSRV-KAR\SQL2016;Initial Catalog=PersonDetailDBb;User ID=sa;Password=12341234;";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
          
        }
        

        public IActionResult Index()
        {

           
            DataTable dt = new DataTable();
            using (SqlConnection sqlconn = new SqlConnection(mainconn))
            {
                sqlconn.Open();
                SqlCommand sqlcomm = new SqlCommand();
                string sqlquery = "select  [PNDCode],[PNDCat] from [PersonDetailDBb].[dbo].[JOINT2] ";
                //sqlcomm.CommandText = sqlquery;
                //sqlcomm.Connection = sqlconn;
                SqlDataAdapter sda = new SqlDataAdapter(sqlquery,sqlconn);
                sda.Fill(dt);
            }
            //GridView1.DataSource = dt;
            //GridView1.DataBind();
           
            return View(dt);
        }
        [Authorize(Roles = "admin")]
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
}
