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
        private ICommonService _commonService;
        private IProductService _productService;

        public HomeController(IProductCategoryService productCategoryService, ICommonService commonService, IProductService productService)
        {
            _productCategoryService = productCategoryService;
            _commonService = commonService;
            _productService = productService;

        }
        public ActionResult Index()
        {
            var slides = _commonService.GetSlides();
            var lastProducts = _productService.GetLastest();
            var topSaleProducts = _productService.GetHotProduct();



            var slideViewModels = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slides);
            var lastProductViewModels = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(lastProducts);
            var topSaleProductViewModels = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(topSaleProducts);

            var homeViewModel = new HomeViewModel
            {
                SlideViewModels = slideViewModels,
                LastProductViewModels = lastProductViewModels,
                TopSaleProductViewModels = topSaleProductViewModels
            };
            return View(homeViewModel);
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