using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using OnlineShop.Model.Models;
using OnlineShop.Service;
using OnlineShop.Web.Infrastructure.Core;
using OnlineShop.Web.Models;
using OnlineShop.Web.Infrastructure.Extensions;

namespace OnlineShop.Web.Api
{
    [RoutePrefix("api/product")]
    public class ProductController : BaseApiController
    {
        private IProductService _productService;


        public ProductController(IErrorService errorService, IProductService productService) : base(errorService)
        {
            _productService = productService;
        }

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage requestMessage)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                var products = _productService.GetAll();
                var productViewModels = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);
                var responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, productViewModels);
                return responseMessage;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage requestMessage, string keyword, int page = 0,
            int pageSize = 20)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                int totalRow = 0;
                var products = _productService.GetAll(keyword);
                totalRow = products.Count();


                var query = products.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var productViewModels = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(query);

                var paginationSet = new PaginationSet<ProductViewModel>
                {
                    Items = productViewModels,
                    Page = page,
                    TotalPages = (int) Math.Ceiling((decimal) totalRow / pageSize),
                    TotalCount = totalRow
                };
                responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, paginationSet);

                return responseMessage;
            });
        }


        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage requestMessage, int? id)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                var product = _productService.GetById(id);
                var productViewModel = Mapper.Map<Product, ProductViewModel>(product);
                responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, productViewModel);
                return responseMessage;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage requestMessage, ProductViewModel productViewModel)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                if (ModelState.IsValid)
                {
                    var productCreate = new Product();
                    productCreate.UpdateProduct(productViewModel);
                    productCreate.CreatedDate = DateTime.Now;

                    _productService.Add(productCreate);
                    _productService.Save();

                    var productViewModelResult = Mapper.Map<Product, ProductViewModel>(productCreate);

                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.Created, productViewModelResult);
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
        public HttpResponseMessage Update(HttpRequestMessage requestMessage, ProductViewModel productViewModel)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;

                if (ModelState.IsValid)
                {
                    var productUpdate = _productService.GetById(productViewModel.Id);

                    productUpdate.UpdateProduct(productViewModel);
                    productUpdate.UpdatedDate = DateTime.Now;

                    _productService.Update(productUpdate);
                    _productService.Save();

                    var productViewModelResult = Mapper.Map<Product, ProductViewModel>(productUpdate);

                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.Created, productViewModelResult);
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
                    var productDelete = _productService.Delete(id);
                    _productService.Save();

                    var produtViewModel = Mapper.Map<Product, ProductViewModel>(productDelete);

                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.Created, produtViewModel);
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
