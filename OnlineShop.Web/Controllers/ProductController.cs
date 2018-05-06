using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using OnlineShop.Model.Models;
using OnlineShop.Service;
using OnlineShop.Web.Infrastructure.Core;
using OnlineShop.Web.Models;

namespace OnlineShop.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private IProductCategoryService _productCategoryService;

        public ProductController(IProductService productService , IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }
        // GET: Product
        public ActionResult Detail(int id)
        {
            return View();
        }

        public ActionResult Category(int id , int page = 1)
        {
            int totalRow;
            var pageSize = 12;
            var products = _productService.GetProductsByCategoryPaging(id , page , pageSize , out totalRow);

            var productViewModel = Mapper.Map<IEnumerable<Product> , IEnumerable<ProductViewModel>>(products);
            var totalPage = Math.Ceiling((double)totalRow / pageSize);

            var productCategory = _productCategoryService.GetById(id);
            var productCategoryViewModel = Mapper.Map<ProductCategory , ProductCategoryViewModel>(productCategory);
            ViewBag.ProductCategory = productCategoryViewModel;

            var paginationSet = new PaginationSet<ProductViewModel>
            {
                Items = productViewModel ,
                TotalCount = totalRow ,
                Page = page ,
                TotalPages = (int)totalPage ,
                MaxPage = 5

            };
            return View(paginationSet);
        }
    }
}