using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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

        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }
        // GET: Product
        public ActionResult Detail(int? id)
        {
            if (id == null)
                return RedirectToAction("Index", "Home");
            var product = _productService.GetById(id);

            var productsViewModel = Mapper.Map<Product, ProductViewModel>(product);

            var relatedProducts = _productService.GetReatedProducts(id.GetValueOrDefault(), 5);
            ViewBag.RelatedProducts = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(relatedProducts);

            var moreImages = product.MoreImages;

            var images = new List<string>();
            if (moreImages != null)
                images = new JavaScriptSerializer().Deserialize<List<string>>(moreImages);
            ViewBag.MoreImages = images;

            var tags = _productService.GetTagsByProductId(id.GetValueOrDefault()).ToList();
            ViewBag.Tags = Mapper.Map<IEnumerable<Tag>, IEnumerable<TagViewModel>>(tags);

            return View(productsViewModel);
        }

        public ActionResult Category(int id, int page = 1, string sort = "")
        {
            int totalRow;
            var pageSize = 12;
            var products = _productService.GetProductsByCategoryPaging(id, page, pageSize, sort, out totalRow);

            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);
            var totalPage = Math.Ceiling((double)totalRow / pageSize);

            var productCategory = _productCategoryService.GetById(id);
            var productCategoryViewModel = Mapper.Map<ProductCategory, ProductCategoryViewModel>(productCategory);
            ViewBag.ProductCategory = productCategoryViewModel;

            var paginationSet = new PaginationSet<ProductViewModel>
            {
                Items = productViewModel,
                TotalCount = totalRow,
                Page = page,
                TotalPages = (int)totalPage,
                MaxPage = 5,
                Sort = sort

            };
            return View(paginationSet);
        }

        public JsonResult GetListNameOfProduct(string keyword)
        {
            var productNames = _productService.GetProductsByName(keyword).Select(x => x.Name).ToList();
            return Json(new { data = productNames }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string keyword, int page = 1, string sort = "")
        {
            int totalRow;
            var pageSize = 12;
            var products = _productService.Search(keyword, sort, page, pageSize, out totalRow);

            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);
            var totalPage = Math.Ceiling((double)totalRow / pageSize);


            var paginationSet = new PaginationSet<ProductViewModel>
            {
                Items = productViewModel,
                TotalCount = totalRow,
                Page = page,
                TotalPages = (int)totalPage,
                MaxPage = 5,
                Sort = sort,
                Keyword = keyword

            };
            return View(paginationSet);
        }

        public ActionResult GetProductsByTag(string tagId, int page = 1)
        {
            int totalRow;
            var pageSize = 12;
            var products = _productService.GetProductsByTagId(tagId, page, pageSize, out totalRow);

            var productViewModel = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);
            var totalPage = Math.Ceiling((double)totalRow / pageSize);


            var paginationSet = new PaginationSet<ProductViewModel>
            {
                Items = productViewModel,
                TotalCount = totalRow,
                Page = page,
                TotalPages = (int)totalPage,
                MaxPage = 5,
                Keyword = tagId

            };
            return View(paginationSet);
        }


    }
}