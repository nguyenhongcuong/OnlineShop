using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using System.Web.Script.Serialization;
using AutoMapper;
using OnlineShop.Model.Models;
using OnlineShop.Service;
using OnlineShop.Web.Infrastructure.Core;
using OnlineShop.Web.Infrastructure.Extensions;
using OnlineShop.Web.Models;

namespace OnlineShop.Web.Api
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : BaseApiController
    {
        private IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService)
            : base(errorService)
        {
            _productCategoryService = productCategoryService;
        }

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage requestMessage)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                var productCategories = _productCategoryService.GetAll();
                var productCategoryViewModels = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<
                ProductCategoryViewModel>>(productCategories);
                var responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, productCategoryViewModels);
                return responseMessage;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage requestMessage, string keyword, int page = 1, int pageSize = 20)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                int totalRow = 0;
                var productCategories = _productCategoryService.GetAll(keyword);
                totalRow = productCategories.Count();
                var query = productCategories.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);
                var productCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<
                ProductCategoryViewModel>>(query);

                var paginationSet = new PaginationSet<ProductCategoryViewModel>
                {
                    Items = productCategoryViewModel,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };


                var responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, paginationSet);
                return responseMessage;
            });
        }

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage requestMessage,
            int? id)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                var productCategory = _productCategoryService.GetById(id);
                var productCategoryViewModel = Mapper.Map<ProductCategory, ProductCategoryViewModel>(productCategory);
                var responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, productCategoryViewModel);
                return responseMessage;
            });
        }


        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage requestMessage,
            ProductCategoryViewModel productCategoryViewModel)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                if (ModelState.IsValid)
                {
                    var productCategory = new ProductCategory();
                    productCategory.UpdateProductCategory(productCategoryViewModel);
                    _productCategoryService.Add(productCategory);
                    _productCategoryService.Save();

                    var productCategoryViewModelResult = Mapper.Map<ProductCategory, ProductCategoryViewModel>(productCategory);
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.Created,
                        productCategoryViewModelResult);
                }
                else
                {
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }

                return responseMessage;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage requestMessage,
            ProductCategoryViewModel productCategoryViewModel)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                if (ModelState.IsValid)
                {
                    var productCategory = _productCategoryService.GetById(productCategoryViewModel.Id);
                    productCategory.UpdateProductCategory(productCategoryViewModel);
                    productCategory.UpdatedDate = DateTime.Now;


                    _productCategoryService.Update(productCategory);
                    _productCategoryService.Save();

                    var productCategoryViewModelResult =
                        Mapper.Map<ProductCategory, ProductCategoryViewModel>(productCategory);
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.Created,
                        productCategoryViewModelResult);

                }
                else
                {
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return responseMessage;
            });
        }


        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage requestMessage, int? id)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                if (ModelState.IsValid)
                {
                    var productCategoryDelete = _productCategoryService.Delete(id.GetValueOrDefault());
                    _productCategoryService.Save();

                    var productCategoryViewModel =
                        Mapper.Map<ProductCategory, ProductCategoryViewModel>(productCategoryDelete);
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, productCategoryViewModel);
                }
                else
                {
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }

                return responseMessage;
            });
        }


        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage requestMessage, string data)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;

                if (ModelState.IsValid)
                {
                    var productCategoryIdDeletes = new JavaScriptSerializer().Deserialize<List<int>>(data);
                    foreach (var item in productCategoryIdDeletes)
                    {
                        _productCategoryService.Delete(item);
                    }

                    _productCategoryService.Save();
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, true);
                }
                else
                {
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }

                return responseMessage;
            });
        }







    }
}
