using AutoMapper;
using OnlineShop.Model.Model;
using OnlineShop.Service;
using OnlineShop.Web.Infrastructure.Core;
using OnlineShop.Web.Infrastructure.Extensions;
using OnlineShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace OnlineShop.Web.API
{
    [RoutePrefix("api/page")]
    [Authorize]
    public class PageController : ApiControllerBase
    {
        IPageService _pageService;

        public PageController(IErrorService errorService, IPageService pageService) : base(errorService)
        {
            this._pageService = pageService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword)
        {
            return CreateHttpReponse(request, () =>
            {
                var pageModel = _pageService.GetAll(keyword);
                var responseData = Mapper.Map<IEnumerable<Page>, IEnumerable<PageViewModel>>(pageModel);

                return request.CreateResponse(HttpStatusCode.OK, responseData);
            });
        }

        [Route("create")]
        [HttpPost]
        public HttpResponseMessage Post(HttpRequestMessage request, PageViewModel PageViewModel)
        {
            return CreateHttpReponse(request, () =>
            {
                if (ModelState.IsValid)
                {
                    var page = new Page();
                    page.UpdatePage(PageViewModel);

                    var pageModel = _pageService.Add(page);
                    _pageService.Save();
                    var responseData = Mapper.Map<Page, PageViewModel>(pageModel);

                    return request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                return request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            });
        }

        [HttpGet]
        [Route("pageid/{id:int}")]
        public HttpResponseMessage GetPageById(HttpRequestMessage request, int id)
        {
            return CreateHttpReponse(request, () =>
            {
                var pageViewModel = _pageService.GetPagebyId(id);
                if (pageViewModel != null)
                {
                    return request.CreateResponse(HttpStatusCode.OK, pageViewModel);
                }
                else
                {
                    return request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
            });
        }

        [HttpPut]
        [Route("update")]
        public HttpResponseMessage Update(HttpRequestMessage request, PageViewModel pageVm)
        {
            return CreateHttpReponse(request, () =>
            {
                if (ModelState.IsValid)
                {
                    var page = new Page();
                    page.UpdatePage(pageVm);

                    _pageService.Update(page);
                    _pageService.Save();

                    var responseData = Mapper.Map<Page, PageViewModel>(page);
                    return request.CreateResponse(HttpStatusCode.OK, responseData);
                }
                return request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            });
        }

        [Route("delete")]
        [HttpDelete]
        public HttpResponseMessage Delete(HttpRequestMessage request, int pageId)
        {
            return CreateHttpReponse(request, () =>
            {
                if (ModelState.IsValid)
                {
                    var page = _pageService.Delete(pageId);
                    _pageService.Save();

                    var responseData = Mapper.Map<Page, PageViewModel>(page);

                    return request.CreateResponse(HttpStatusCode.Accepted, responseData);
                }
                return request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            });
        }


        [Route("deleteMulti")]
        [HttpDelete]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string listId)
        {
            return CreateHttpReponse(request, () =>
            {
                if (ModelState.IsValid)
                {
                    var ListIdPages = new JavaScriptSerializer().Deserialize<List<int>>(listId);
                    if (ListIdPages.Count > 0)
                    {
                        foreach (var id in ListIdPages)
                        {
                            _pageService.Delete(id);
                        }
                        _pageService.Save();
                    }
                    return request.CreateResponse(HttpStatusCode.Accepted, ListIdPages.Count);
                }
                return request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            });
        }
    }
}
