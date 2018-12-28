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

namespace OnlineShop.Web.API
{
    [RoutePrefix("api/slide")]
    public class SlideController : ApiControllerBase
    {
        ISlideService _slideService;

        public SlideController(IErrorService errorService, ISlideService slideService) : base(errorService)
        {
            this._slideService = slideService;
        }

        [Route("get_all")]
        [HttpGet]
        public HttpResponseMessage GetAllSlides(HttpRequestMessage request)
        {
            Func<HttpResponseMessage> func = delegate ()
            {
                var slideModel = _slideService.GetAll();
                var slideViewModel = Mapper.Map<IEnumerable<Slide>, IEnumerable<SlideViewModel>>(slideModel);
                return request.CreateResponse(HttpStatusCode.OK, slideViewModel);
            };
            return CreateHttpReponse(request, func);
        }

        [Route("create_slide")]
        [HttpPost]
        public HttpResponseMessage CreateSlide(HttpRequestMessage request, SlideViewModel slideVm)
        {
            if (ModelState.IsValid)
            {
                return CreateHttpReponse(request, () =>
                {
                    var slideModel = new Slide();
                    slideModel.UpdateSlide(slideVm);
                    _slideService.Add(slideModel);
                    _slideService.Save();

                    return request.CreateResponse(HttpStatusCode.Created, slideVm);
                });
            }
            return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [Route("load_details/{id:int}")]
        [HttpGet]
        public HttpResponseMessage LoadSlideDetail(HttpRequestMessage request, int id)
        {
            return CreateHttpReponse(request, () =>
            {
                var slideModel = _slideService.GetSingle(id);
                var slideViewModel = Mapper.Map<Slide, SlideViewModel>(slideModel);

                return request.CreateResponse(HttpStatusCode.OK, slideViewModel);
            });
        }

        [Route("update_slide")]
        [HttpPut]
        public HttpResponseMessage UpdateSlide(HttpRequestMessage request, SlideViewModel slideVm)
        {
            if (ModelState.IsValid)
            {
                return CreateHttpReponse(request, () =>
                {
                    var slideModel = new Slide();
                    slideModel.UpdateSlide(slideVm);

                    _slideService.Update(slideModel);
                    _slideService.Save();
                    return request.CreateResponse(HttpStatusCode.OK, slideVm);
                });
            }
            return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [Route("delete_slide/{id}")]
        [HttpDelete]
        public HttpResponseMessage DeleteSlide(HttpRequestMessage request, int id)
        {
            if (ModelState.IsValid)
            {
                return CreateHttpReponse(request, () =>
                {
                    var slideModel = _slideService.Delete(id);
                    _slideService.Save();
                    var slideViewModel = Mapper.Map<Slide, SlideViewModel>(slideModel);

                    return request.CreateResponse(HttpStatusCode.OK, slideViewModel);
                });
            }
            return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
    }
}
