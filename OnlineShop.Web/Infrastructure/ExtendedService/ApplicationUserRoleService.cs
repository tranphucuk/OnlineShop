using Microsoft.AspNet.Identity;
using OnlineShop.Data;
using OnlineShop.Model.Model;
using OnlineShop.Service;
using OnlineShop.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OnlineShop.Web.Infrastructure.ExtendedService
{
    public interface IApplicationUserRoleService
    {
        /// <summary>
        /// Remove a list of user which have permission in a specific role
        /// </summary>
        /// <param name="roleId">role ID string</param>
        /// <returns></returns>
        Task<bool> RemoveUsersFromRoleByRoleId(string roleId);
        /// <summary>
        /// Update roles for a list of User. Ex: add, delete roles.
        /// </summary>
        /// <param name="listUser">User list of a specific group</param>
        /// <param name="roleNameArray">An array of role names</param>
        /// <returns></returns>
        Task<bool> UpdateRolesToListUser(IEnumerable<ApplicationUser> listUser, string[] roleNameArray);
        /// <summary>
        /// Remove all roles of a user list.
        /// </summary>
        /// <param name="listUser">User list of a specific group</param>
        /// <param name="roleNameArray">An array of role names</param>
        /// <returns></returns>
        Task<bool> RemoveAllRolesToUserList(IEnumerable<ApplicationUser> listUser);
    }

    public class ApplicationUserRoleService : IApplicationUserRoleService
    {
        IApplicationGroupService _applicationGroupService;
        IApplicationRoleService _appRoleService;
        ApplicationUserManager _userManager;
        public ApplicationUserRoleService(IApplicationRoleService appRoleService, ApplicationUserManager userManager, IApplicationGroupService applicationGroupService)
        {
            this._applicationGroupService = applicationGroupService;
            this._appRoleService = appRoleService;
            this._userManager = userManager;
        }

        public async Task<bool> UpdateRolesToListUser(IEnumerable<ApplicationUser> listUser, string[] roleNameArray)
        {
            var totalRoleNames = _appRoleService.GetAll().Select(x => x.Name).ToArray();

            foreach (var user in listUser)
            {
                await _userManager.RemoveFromRolesAsync(user.Id, totalRoleNames);
                await _userManager.AddToRolesAsync(user.Id, roleNameArray);
            }
            return true;
        }

        public async Task<bool> RemoveUsersFromRoleByRoleId(string roleId)
        {
            var roleDetails = _appRoleService.GetDetails(roleId);
            var listUserId = _appRoleService.GetListUserIdByRoleId(roleId);
            IdentityResult rs = null;
            foreach (var userId in listUserId)
            {
                rs = await _userManager.RemoveFromRoleAsync(userId, roleDetails.Name);
            }
            return rs.Succeeded;
        }

        public async Task<bool> RemoveAllRolesToUserList(IEnumerable<ApplicationUser> listUser)
        {
            var totalRoleNames = _appRoleService.GetAll().Select(x => x.Name).ToArray();
            foreach (var user in listUser)
            {
                await _userManager.RemoveFromRolesAsync(user.Id, totalRoleNames);
            }
            return true;
        }
    }
}