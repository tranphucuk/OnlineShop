﻿using AutoMapper;
using OnlineShop.Common;
using OnlineShop.Common.Exceptions;
using OnlineShop.Model.Model;
using OnlineShop.Service;
using OnlineShop.Web.App_Start;
using OnlineShop.Web.Infrastructure.Core;
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

namespace OnlineShop.Web.API
{
    [RoutePrefix("app/application_group")]
    [Authorize]
    public class ApplicationGroupController : ApiControllerBase
    {
        IApplicationGroupService _applicationGroupService;
        IApplicationRoleService _applicationRoleService;
        ApplicationUserManager _applicationUserManager;

        public ApplicationGroupController(IErrorService errorService,
            IApplicationGroupService applicationGroupService,
            IApplicationRoleService applicationRoleService,
            ApplicationUserManager applicationUserManager) : base(errorService)
        {
            this._applicationGroupService = applicationGroupService;
            this._applicationRoleService = applicationRoleService;
            this._applicationUserManager = applicationUserManager;
        }

        [Route("get_all")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, int page, string filter)
        {
            return CreateHttpReponse(request, () =>
            {
                int totalRow = 0;
                var pageSize = int.Parse(ConfigHelper.GetValueByKey("pageSize"));
                var model = _applicationGroupService.GetAll(page, pageSize, out totalRow, filter);
                var appGroupViewModel = Mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(model);

                var pagination = new PaginationSet<ApplicationGroupViewModel>
                {
                    Items = appGroupViewModel,
                    MaxPage = int.Parse(ConfigHelper.GetValueByKey("maxPage")),
                    Page = page,
                    TotalCount = totalRow,
                    TotalPage = (int)Math.Ceiling((decimal)totalRow / pageSize),
                };

                var response = request.CreateResponse(HttpStatusCode.OK, pagination);
                return response;
            });
        }

        [Route("create_group")]
        [HttpPost]
        public HttpResponseMessage CreateGroup(HttpRequestMessage request, ApplicationGroupViewModel groupVm)
        {
            return CreateHttpReponse(request, () =>
            {
                if (ModelState.IsValid)
                {
                    var newAppGroup = new ApplicationGroup();
                    newAppGroup.UpdateAppGroup(groupVm);
                    try
                    {
                        // add group
                        var appGroupModel = _applicationGroupService.Add(newAppGroup);
                        _applicationGroupService.Save();

                        var listRole = new List<ApplicationRoleGroup>();
                        foreach (var role in groupVm.Roles)
                        {
                            listRole.Add(new ApplicationRoleGroup()
                            {
                                GroupId = appGroupModel.ID,
                                RoleId = role.Id
                            });
                        }
                        _applicationRoleService.AddRolesToGroup(listRole, appGroupModel.ID);
                        _applicationRoleService.Save();

                        return request.CreateResponse(HttpStatusCode.Created, groupVm);
                    }
                    catch (NameDuplicatedException nameEx)
                    {
                        return request.CreateResponse(HttpStatusCode.BadRequest, nameEx.Message);
                    }
                }
                return request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            });
        }

        [Route("get_group_id/{groupId:int}")]
        [HttpGet]
        public HttpResponseMessage GetGroupById(HttpRequestMessage request, int groupId)
        {
            Func<HttpResponseMessage> func = delegate ()
            {
                var appGroupModel = _applicationGroupService.GetDetail(groupId);
                var listRole = _applicationRoleService.GetListRolesByGroupId(groupId);
                var appGroupVm = Mapper.Map<ApplicationGroup, ApplicationGroupViewModel>(appGroupModel);
                appGroupVm.Roles = Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(listRole);
                return request.CreateResponse(HttpStatusCode.Accepted, appGroupVm);
            };

            return CreateHttpReponse(request, func);
        }

        [Route("update_group")]
        [HttpPut]
        public async Task<HttpResponseMessage> Update(HttpRequestMessage request, ApplicationGroupViewModel appGroupVm)
        {
            if (ModelState.IsValid)
            {
                var appGroupModel = new ApplicationGroup();
                appGroupModel.UpdateAppGroup(appGroupVm);

                var listRole = new List<ApplicationRoleGroup>();
                foreach (var role in appGroupVm.Roles)
                {
                    listRole.Add(new ApplicationRoleGroup()
                    {
                        GroupId = appGroupModel.ID,
                        RoleId = role.Id
                    });
                }
                _applicationGroupService.Update(appGroupModel);
                _applicationRoleService.AddRolesToGroup(listRole, appGroupModel.ID);
                _applicationRoleService.Save();

                var listRoles = _applicationRoleService.GetListRolesByGroupId(appGroupModel.ID);
                var listUserInGroup = _applicationGroupService.GetListUserByGroupId(appGroupModel.ID);
                var listRoleName = listRoles.Select(x => x.Name);
                foreach (var user in listUserInGroup)
                {
                    foreach (var roleName in listRoleName)
                    {
                        await _applicationUserManager.RemoveFromRoleAsync(user.Id, roleName);
                        await _applicationUserManager.AddToRoleAsync(user.Id, roleName);
                    }
                }

                return request.CreateResponse(HttpStatusCode.OK, appGroupVm);
            }
            return request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpDelete]
        [Route("delete_group")]
        public HttpResponseMessage DeleteGroup(HttpRequestMessage request, int groupId)
        {
            if (ModelState.IsValid)
            {
                Func<HttpResponseMessage> func = delegate ()
                {
                    var appGroup = _applicationGroupService.Delete(groupId);
                    _applicationGroupService.Save();
                    var appGroupViewModel = Mapper.Map<ApplicationGroup, ApplicationGroupViewModel>(appGroup);
                    return request.CreateResponse(HttpStatusCode.OK, appGroupViewModel);
                };
                return CreateHttpReponse(request, func);
            }
            return request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [HttpDelete]
        [Route("delete_multi_groups")]
        public HttpResponseMessage DeleteAllGroups(HttpRequestMessage request, string groupIds)
        {
            if (ModelState.IsValid)
            {
                return CreateHttpReponse(request, () =>
                {
                    var listIds = new JavaScriptSerializer().Deserialize<List<int>>(groupIds);
                    foreach (var id in listIds)
                    {
                        _applicationGroupService.Delete(id);
                    }
                    _applicationGroupService.Save();

                    return request.CreateResponse(HttpStatusCode.OK, listIds.Count);
                });
            }
            return request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [Route("get_list_roles")]
        [HttpGet]
        public HttpResponseMessage GetListRoles(HttpRequestMessage request)
        {
            return CreateHttpReponse(request, () =>
            {
                var listRoleModel = _applicationRoleService.GetAll();
                var listRoleViewModel = Mapper.Map<IEnumerable<ApplicationRole>, IEnumerable<ApplicationRoleViewModel>>(listRoleModel);

                return request.CreateResponse(HttpStatusCode.OK, listRoleViewModel);
            });
        }
    }
}
