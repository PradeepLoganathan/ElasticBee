using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LogDashWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            IReadOnlyCollection<LogEventTypes.Metrics> m;
            string time = DateTime.Now.ToString();

            ViewBag.Greeting = "hello , the time is " + time;
            LogDashWeb.App_Start.LogDashboard dash = new App_Start.LogDashboard();
            m = dash.GetAllLogEntries();
            return View(m);
        }
    }
}