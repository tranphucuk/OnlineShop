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
    [RoutePrefix("api/logo")]
    public class LogoController : ApiControllerBase
    {
        ILogoService _logoService;

        public LogoController(IErrorService errorService, ILogoService logoService) : base(errorService)
        {
            this._logoService = logoService;
        }

        [Route("get_all")]
        [HttpGet]
        public HttpResponseMessage GetAllLogos(HttpRequestMessage request)
        {
            return CreateHttpReponse(request, () =>
            {
                var listLogos = _logoService.GetAllLogos();
                var logoViewModels = Mapper.Map<IEnumerable<Logo>, IEnumerable<LogoViewModel>>(listLogos);

                return request.CreateResponse(HttpStatusCode.OK, logoViewModels);
            });
        }

        [Route("load_logo/{id:int}")]
        [HttpGet]
        public HttpResponseMessage LoadLogo(HttpRequestMessage request, int id)
        {
            return CreateHttpReponse(request, () =>
            {
                var logoModel = _logoService.GetSingleLogo(id);
                var logoViewModel = Mapper.Map<Logo, LogoViewModel>(logoModel);
                return request.CreateResponse(HttpStatusCode.OK, logoViewModel);
            });
        }

        [Route("update_logo")]
        [HttpPut]
        public HttpResponseMessage UpdateLogo(HttpRequestMessage request, LogoViewModel logoVm)
        {
            if (ModelState.IsValid)
            {
                Func<HttpResponseMessage> func = () =>
                {
                    var logo = new Logo();
                    logo.UpdateLogo(logoVm);
                    logo.CreatedDate = DateTime.Now;
                    _logoService.UpdateLogo(logo);
                    _logoService.Save();
                    return request.CreateResponse(HttpStatusCode.OK, logoVm);
                };
                return CreateHttpReponse(request, func);
            }
            return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
    }
}
