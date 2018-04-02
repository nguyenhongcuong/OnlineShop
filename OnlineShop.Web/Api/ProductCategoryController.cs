using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using OnlineShop.Model.Models;
using OnlineShop.Service;
using OnlineShop.Web.Infrastructure.Core;
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

        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage requestMessage)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                var productCategories = _productCategoryService.GetAll();
                var productCategoryViewModel = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<
                ProductCategoryViewModel>>(productCategories);
                var responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, productCategoryViewModel);
                return responseMessage;
            });
        }
    }
}
