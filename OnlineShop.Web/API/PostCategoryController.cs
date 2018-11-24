using AutoMapper;
using OnlineShop.Model.Model;
using OnlineShop.Service;
using OnlineShop.Web.Infrastructure.Core;
using OnlineShop.Web.Infrastructure.Extensions;
using OnlineShop.Web.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OnlineShop.Web.API
{
    [RoutePrefix("api/postcategory")]
    [Authorize]
    public class PostCategoryController : ApiControllerBase
    {
        IPostCategoryService _postCategoryService;

        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) : base(errorService)
        {
            this._postCategoryService = postCategoryService;
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage requestMessage)
        {
            return this.CreateHttpReponse(requestMessage, () =>
            {
                var listPostCategory = _postCategoryService.GetAll();
                var listPostCategoryVm = Mapper.Map<List<PostCategoryViewModel>>(listPostCategory);
                var res = requestMessage.CreateResponse(HttpStatusCode.OK, listPostCategoryVm);
                return res;
            });
        }

        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage requestMessage, PostCategoryViewModel postCategoryVm)
        {
            return this.CreateHttpReponse(requestMessage, () =>
            {
                HttpResponseMessage res = null;
                if (!ModelState.IsValid)
                {
                    res = requestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var newPostCategory = new PostCategory();
                    newPostCategory.UpdatePostCategory(postCategoryVm);

                    var category = _postCategoryService.Add(newPostCategory);
                    _postCategoryService.Save();
                    res = requestMessage.CreateResponse(HttpStatusCode.Created, category);
                }
                return res;
            });
        }

        public HttpResponseMessage Put(HttpRequestMessage requestMessage, PostCategoryViewModel postCategoryVm)
        {
            return this.CreateHttpReponse(requestMessage, () =>
            {
                HttpResponseMessage res = null;
                if (!ModelState.IsValid)
                {
                    res = requestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var newPostCategoryDb = _postCategoryService.GetPostCategoryById(postCategoryVm.ID);
                    newPostCategoryDb.UpdatePostCategory(postCategoryVm);

                    _postCategoryService.Update(newPostCategoryDb);
                    _postCategoryService.Save();
                    res = requestMessage.CreateResponse(HttpStatusCode.OK);
                }
                return res;
            });
        }

        public HttpResponseMessage Delete(HttpRequestMessage requestMessage, int id)
        {
            return this.CreateHttpReponse(requestMessage, () =>
            {
                HttpResponseMessage res = null;
                if (!ModelState.IsValid)
                {
                    res = requestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    _postCategoryService.Delete(id);
                    _postCategoryService.Save();
                    res = requestMessage.CreateResponse(HttpStatusCode.OK);
                }
                return res;
            });
        }
    }
}