using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Edit index";

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

        [ChildActionOnly]
        public ActionResult _FooterPartial()
        {
            return PartialView();
        }


        [ChildActionOnly]
        public ActionResult _HeaderPartial()
        {
            return PartialView();
        }

        [ChildActionOnly]
        public ActionResult _CategoriesPartial()
        {
            return PartialView();
        }
    }
}