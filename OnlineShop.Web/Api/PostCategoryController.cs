using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using OnlineShop.Model.Models;
using OnlineShop.Service;
using OnlineShop.Web.Infrastructure.Core;
using OnlineShop.Web.Infrastructure.Extensions;
using OnlineShop.Web.Models;

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
                HttpResponseMessage responseMessage = null;
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
                var postCategories = _postCategoryService.GetAll();
                var postCategoryViewModels = Mapper.Map<List<PostCategoryViewModel>>(postCategories);
                //_postCategoryService.Save();
                responseMessage = requestMessage.CreateResponse(HttpStatusCode.OK, postCategoryViewModels);

                return responseMessage;
            });
        }

        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage requestMessage, PostCategoryViewModel postCategoryViewModel)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                if (ModelState.IsValid)
                {
                    PostCategory postCategory = new PostCategory();
                    postCategory.UpdatePostCategory(postCategoryViewModel);
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

        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage requestMessage, PostCategoryViewModel postCategoryViewModel)
        {
            return CreateHttpResponse(requestMessage, () =>
            {
                HttpResponseMessage responseMessage = null;
                if (ModelState.IsValid)
                {
                    PostCategory postCategory = _postCategoryService.GetById(postCategoryViewModel.Id);
                    postCategory.UpdatePostCategory(postCategoryViewModel);
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
