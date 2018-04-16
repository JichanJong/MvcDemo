using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Test.Web.Models;

namespace Test.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }

        public JsonResult GetPersons()
        {
            List<Person> persons = new List<Person>();
            for (int i = 0; i < 10; i++)
            {
                persons.Add(new Person() 
                { 
                    Age = 18 + i,
                    Name = "AA" + i.ToString()
                });
            }
            return Json(persons, JsonRequestBehavior.AllowGet);
        }
    }
}