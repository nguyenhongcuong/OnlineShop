using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using OnlineShop.Model.Models;
using OnlineShop.Service;
using OnlineShop.Web.Models;

namespace OnlineShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private IProductCategoryService _productCategoryService;

        public HomeController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;

        }
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
            var productCategories = _productCategoryService.GetAll();
            var productCategoryViewModels = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(productCategories);
            return PartialView(productCategoryViewModels);
        }
    }
}