using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineShop.Model.Models;
using OnlineShop.Service;
using OnlineShop.Web.Infrastructure.Core;

namespace OnlineShop.Web.Api
{
    [RoutePrefix("api/postcategory")]
    public class PostCategoryController : BaseApiController
    {
        private IPostCategoryService _postCategoryService;
        // GET: api/PostCategory
        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) : base(errorService)
        {
            _postCategoryService = postCategoryService;
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage requestMessage)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage  = null;
                //if (ModelState.IsValid)
                //{
                //    var result = _postCategoryService.GetAll();
                //    _postCategoryService.Save();
                //    responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, result);
                //}
                //else
                //{
                //    requestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                //}
                var result = _postCategoryService.GetAll();
                //_postCategoryService.Save();
                responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, result);

                return responseMessage;
            });
        }


        public HttpResponseMessage Post(HttpRequestMessage requestMessage, PostCategory postCategory)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                if (ModelState.IsValid)
                {
                    var result = _postCategoryService.Add(postCategory);
                    _postCategoryService.Save();
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.Created, result);
                }
                else
                {
                    requestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return responseMessage;
            });
        }


        public HttpResponseMessage Put(HttpRequestMessage requestMessage, PostCategory postCategory)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                if (ModelState.IsValid)
                {
                   _postCategoryService.Update(postCategory);
                    _postCategoryService.Save();
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    requestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return responseMessage;
            });
        }

        public HttpResponseMessage Delete(HttpRequestMessage requestMessage, int? postCategoryId)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                if (ModelState.IsValid)
                {
                    _postCategoryService.Delete(postCategoryId);
                    _postCategoryService.Save();
                    responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    requestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return responseMessage;
            });
        }


    }
}
