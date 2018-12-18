﻿using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using OnlineShop.Common;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Model;
using OnlineShop.Service;
using OnlineShop.Web.App_Start;
using OnlineShop.Web.Infrastructure.Core;
using OnlineShop.Web.Infrastructure.ExtendedService;
using OnlineShop.Web.Infrastructure.Extensions;
using OnlineShop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace OnlineShop.Web.API
{
    [RoutePrefix("api/application_roles")]
    [Authorize]
    public class ApplicationRoleController : ApiControllerBase
    {
        IApplicationRoleService _appRoleService;
        ApplicationUserManager _userManager;
        IApplicationGroupService _appgroupService;
        IApplicationUserRoleService _applicationUserRoleService;

        public ApplicationRoleController(IErrorService errorService,
            IApplicationRoleService appRoleService,
            ApplicationUserManager userManager,
            IApplicationGroupService appgroupService,
            IApplicationUserRoleService applicationUserRoleService) : base(errorService)
        {
            this._appRoleService = appRoleService;
            this._userManager = userManager;
            this._appgroupService = appgroupService;
            this._applicationUserRoleService = applicationUserRoleService;
        }

        [Route("get_all")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, string filter)
        {
            Func<HttpResponseMessage> func = delegate ()
            {
                var pageSize = int.Parse(ConfigHelper.GetValueByKey("pageSize"));
                var totalRow = 0;

                var roleModel = _appRoleService.GetAll(page, pageSize, out totalRow, filter);
                var roleViewModel = Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(roleModel);

                var pagination = new PaginationSet<ApplicationRoleViewModel>
                {
                    Items = roleViewModel,
                    MaxPage = int.Parse(ConfigHelper.GetValueByKey("pageSize")),
                    Page = page,
                    TotalCount = totalRow,
                    TotalPage = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };

                return request.CreateResponse(HttpStatusCode.OK, pagination);
            };

            return CreateHttpReponse(request, func);
        }

        [Route("add_role")]
        [HttpPost]
        public HttpResponseMessage CreateRole(HttpRequestMessage request, ApplicationRoleViewModel roleVm)
        {
            if (ModelState.IsValid)
            {
                Func<HttpResponseMessage> func = () =>
                {
                    var appRoleModel = new ApplicationRole();
                    appRoleModel.UpdateRoles(roleVm, "add");
                    _appRoleService.Add(appRoleModel);
                    _appRoleService.Save();

                    var appRoleViewModel = Mapper.Map<ApplicationRole, ApplicationRoleViewModel>(appRoleModel);
                    return request.CreateResponse(HttpStatusCode.Created, appRoleViewModel);
                };
                return CreateHttpReponse(request, func);
            }
            return request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [Route("get_role_detail/{roleId}")]
        [HttpGet]
        public HttpResponseMessage GetDetail(HttpRequestMessage request, string roleId)
        {
            return CreateHttpReponse(request, () =>
            {
                var roleModel = _appRoleService.GetDetails(roleId);
                var roleViewModel = Mapper.Map<ApplicationRole, ApplicationRoleViewModel>(roleModel);

                return request.CreateResponse(HttpStatusCode.OK, roleViewModel);
            });
        }

        [Route("update_role")]
        [HttpPut]
        public HttpResponseMessage UpdateRole(HttpRequestMessage request, ApplicationRoleViewModel appRoleVm)
        {
            if (ModelState.IsValid)
            {
                Func<HttpResponseMessage> func = delegate ()
                {
                    var roleModel = new ApplicationRole();
                    roleModel.UpdateRoles(appRoleVm, "update");

                    _appRoleService.Update(roleModel);
                    _appRoleService.Save();
                    return request.CreateResponse(HttpStatusCode.OK, appRoleVm);
                };
                return CreateHttpReponse(request, func);
            }
            return request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [Route("delete_role/{roleId}")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteRole(HttpRequestMessage request, string roleId)
        {
            var isSucceeded = await _applicationUserRoleService.RemoveUsersFromRoleByRoleId(roleId);
            if (isSucceeded == false)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error: Remove roles failed.");
            }
            _appRoleService.Delete(roleId);
            _appRoleService.Save();
            return request.CreateResponse(HttpStatusCode.OK, roleId);
        }

        [Route("delete_multi_roles")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteMulti(HttpRequestMessage request, string idString)
        {
            if (ModelState.IsValid)
            {
                var listIds = new JavaScriptSerializer().Deserialize<List<string>>(idString);
                foreach (var id in listIds)
                {
                    var isSucceeded = await _applicationUserRoleService.RemoveUsersFromRoleByRoleId(id);
                    if (isSucceeded == false)
                    {
                        return request.CreateErrorResponse(HttpStatusCode.BadRequest, "Error: Remove roles failed.");
                    }
                    _appRoleService.Delete(id);
                }
                _appRoleService.Save();
                return request.CreateResponse(HttpStatusCode.OK, listIds.Count);
            }
            return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }
    }
}
