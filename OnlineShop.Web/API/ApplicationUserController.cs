using AutoMapper;
using Microsoft.AspNet.Identity;
using OnlineShop.Common.Exceptions;
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

namespace OnlineShop.Web.API
{
    [RoutePrefix("api/application_users")]
    [Authorize]
    public class ApplicationUserController : ApiControllerBase
    {
        ApplicationUserManager _userManager;
        IApplicationGroupService _appGroupService;
        IApplicationRoleService _appRoleService;
        IApplicationUserRoleService _applicationUserRoleService;

        public ApplicationUserController(IErrorService errorService,
            ApplicationUserManager userManager,
            IApplicationGroupService appGroupService,
            IApplicationRoleService appRoleService,
            IApplicationUserRoleService applicationUserRoleService) : base(errorService)
        {
            this._userManager = userManager;
            this._appGroupService = appGroupService;
            this._appRoleService = appRoleService;
            this._applicationUserRoleService = applicationUserRoleService;
        }

        [Route("get_all_user")]
        //[Authorize(Roles = "admin page")]
        [HttpGet]
        public HttpResponseMessage GetAllUser(HttpRequestMessage request)
        {
            Func<HttpResponseMessage> func = delegate ()
            {
                var listUser = _userManager.Users;
                var listUserViewModel = Mapper.Map<IEnumerable<ApplicationUser>, IEnumerable<ApplicationUserViewModel>>(listUser);

                return request.CreateResponse(HttpStatusCode.OK, listUserViewModel);
            };
            return CreateHttpReponse(request, func);
        }

        [Route("create_user")]
        [HttpPost]
        //[Authorize(Roles = "admin page")]
        public async Task<HttpResponseMessage> CreateUser(HttpRequestMessage request, ApplicationUserViewModel appUserVm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var appUserModel = new ApplicationUser();
                    appUserModel.UpdateUser(appUserVm);
                    appUserModel.Id = Guid.NewGuid().ToString();
                    var result = await _userManager.CreateAsync(appUserModel, appUserVm.Password);
                    if (result.Succeeded)
                    {
                        var listGroupUser = new List<ApplicationUserGroup>();
                        foreach (var group in appUserVm.Groups)
                        {
                            listGroupUser.Add(new ApplicationUserGroup()
                            {
                                GroupId = group.ID,
                                UserId = appUserModel.Id
                            });

                            var listRole = _appRoleService.GetListSelectedRolesByGroupId(group.ID);
                            foreach (var role in listRole)
                            {
                                await _userManager.RemoveFromRoleAsync(appUserModel.Id, role.Name);
                                await _userManager.AddToRoleAsync(appUserModel.Id, role.Name);
                            }
                        }

                        _appGroupService.AddUserToGroups(listGroupUser, appUserModel.Id);
                        _appGroupService.Save();
                        return request.CreateResponse(HttpStatusCode.Created, appUserVm);
                    }
                    else
                    {
                        return request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join(",", result.Errors));
                    }
                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
                catch (Exception ex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [Route("Get_single_user/{userId}")]
        [HttpGet]
        //[Authorize(Roles = "admin page")]
        public async Task<HttpResponseMessage> GetDetail(HttpRequestMessage request, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(userId) + " is invalid");
            }
            var userModel = await _userManager.FindByIdAsync(userId);
            if (userModel == null)
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, nameof(userId) + " not found");
            else
            {
                var listGroup = _appGroupService.GetListGroupByUserId(userModel.Id);
                var GroupViewModel = Mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(listGroup);
                var userViewModel = Mapper.Map<ApplicationUser, ApplicationUserViewModel>(userModel);
                userViewModel.Groups = GroupViewModel;
                return request.CreateResponse(HttpStatusCode.OK, userViewModel);
            }
        }

        [Route("update_user")]
        [HttpPut]
        //[Authorize(Roles = "admin page")]
        public async Task<HttpResponseMessage> UpdateUser(HttpRequestMessage request, ApplicationUserViewModel appUserVm)
        {
            if (ModelState.IsValid)
            {
                var userModel = await _userManager.FindByIdAsync(appUserVm.Id);
                try
                {
                    userModel.UpdateUser(appUserVm);
                    var result = await _userManager.UpdateAsync(userModel);
                    if (result.Succeeded)
                    {
                        var userRoles = _userManager.GetRoles(userModel.Id).ToArray();
                        if (userRoles.Count() > 0)
                        {
                            _userManager.RemoveFromRoles(userModel.Id, userRoles.ToArray());
                        }
                        var listGroupUser = new List<ApplicationUserGroup>();
                        foreach (var group in appUserVm.Groups)
                        {
                            var listRoleNameSelected = _appRoleService.GetListSelectedRolesByGroupId(group.ID).Select(x => x.Name);
                            listGroupUser.Add(new ApplicationUserGroup()
                            {
                                GroupId = group.ID,
                                UserId = userModel.Id
                            });
                            await _userManager.AddToRolesAsync(userModel.Id, listRoleNameSelected.ToArray());
                        }
                        _appGroupService.AddUserToGroups(listGroupUser, userModel.Id);
                        _appGroupService.Save();
                        return request.CreateResponse(HttpStatusCode.OK, appUserVm.UserName);
                    }
                    else
                    {
                        return request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join(",", result.Errors));
                    }
                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
                catch (Exception ex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [Route("delete_user/{userId}")]
        [HttpDelete]
        //[Authorize(Roles = "admin page")]
        public async Task<HttpResponseMessage> DeleteUser(HttpRequestMessage request, string userId)
        {
            var currentUser = User.Identity.Name;
            var userModel = await _userManager.FindByIdAsync(userId);
            if (ModelState.IsValid)
            {
                var userRoles = _userManager.GetRoles(userModel.Id).ToArray();
                try
                {
                    if (userRoles.Count() > 0)
                    {
                        _userManager.RemoveFromRoles(userModel.Id, userRoles.ToArray());
                    }
                    var result = await _userManager.DeleteAsync(userModel);
                    if (result.Succeeded)
                    {

                        var userViewModel = Mapper.Map<ApplicationUser, ApplicationUserViewModel>(userModel);
                        return request.CreateResponse(HttpStatusCode.OK, userViewModel.UserName);
                    }
                    else
                    {
                        return request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join(",", result.Errors));
                    }

                }
                catch (Exception ex)
                {
                    _userManager.AddToRoles(userModel.Id, userRoles.ToArray());
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, CurrentUserDeleteException.message);
                }
            }
            return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
        }

        [Route("get_list_group")]
        [HttpGet]
        //[Authorize(Roles = "admin page")]
        public HttpResponseMessage GetListGroup(HttpRequestMessage request)
        {
            return CreateHttpReponse(request, () =>
            {
                var groupModel = _appGroupService.GetAll();
                var groupViewModel = Mapper.Map<IEnumerable<ApplicationGroup>, IEnumerable<ApplicationGroupViewModel>>(groupModel);

                return request.CreateResponse(HttpStatusCode.OK, groupViewModel);
            });
        }
    }
}
