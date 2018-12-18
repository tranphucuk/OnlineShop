using OnlineShop.Data.Infrastructure;
using OnlineShop.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.Repositories
{
    public interface IApplicationRoleRepository : IRepository<ApplicationRole>
    {
        IEnumerable<ApplicationRole> GetListRolesSelectedByGroupId(int groupId);

        IEnumerable<string> GetListUserIdRoleByRoleId(string roleId);
    }

    class ApplicationRoleRepository : RepositoryBase<ApplicationRole>, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<ApplicationRole> GetListRolesSelectedByGroupId(int groupId)
        {
            var query = from r in DbContext.ApplicationRoles
                        join rg in DbContext.ApplicationRoleGroups
                        on r.Id equals rg.RoleId
                        where rg.GroupId == groupId
                        select r;
            return query.AsEnumerable();
        }

        public IEnumerable<string> GetListUserIdRoleByRoleId(string roleId)
        {
            var query = from rg in DbContext.ApplicationRoleGroups
                        join ar in DbContext.ApplicationRoles
                        on rg.RoleId equals ar.Id
                        join ug in DbContext.ApplicationUserGroups
                        on rg.GroupId equals ug.GroupId
                        where rg.RoleId == roleId
                        select ug.UserId;
            return query.AsEnumerable();
        }
    }
}
