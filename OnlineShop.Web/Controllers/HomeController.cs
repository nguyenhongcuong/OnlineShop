using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
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

        public HomeController(IProductCategoryService productCategoryService , ICommonService commonService , IProductService productService)
        {
            _productCategoryService = productCategoryService;
            _commonService = commonService;
            _productService = productService;

        }

        [OutputCache(Duration = 120 , Location = OutputCacheLocation.Server)]
        public ActionResult Index()
        {
            var slides = _commonService.GetSlides();
            var lastProducts = _productService.GetLastest();
            var topSaleProducts = _productService.GetHotProduct();



            var slideViewModels = Mapper.Map<IEnumerable<Slide> , IEnumerable<SlideViewModel>>(slides);
            var lastProductViewModels = Mapper.Map<IEnumerable<Product> , IEnumerable<ProductViewModel>>(lastProducts);
            var topSaleProductViewModels = Mapper.Map<IEnumerable<Product> , IEnumerable<ProductViewModel>>(topSaleProducts);

            var homeViewModel = new HomeViewModel
            {
                SlideViewModels = slideViewModels ,
                LastProductViewModels = lastProductViewModels ,
                TopSaleProductViewModels = topSaleProductViewModels
            };
            return View(homeViewModel);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult _FooterPartial()
        {
            return PartialView();
        }


        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult _HeaderPartial()
        {
            return PartialView();
        }

        [ChildActionOnly]
        [OutputCache(Duration = 3600)]
        public ActionResult _CategoriesPartial()
        {
            var productCategories = _productCategoryService.GetAll();
            var productCategoryViewModels = Mapper.Map<IEnumerable<ProductCategory> , IEnumerable<ProductCategoryViewModel>>(productCategories);
            return PartialView(productCategoryViewModels);
        }
    }
}