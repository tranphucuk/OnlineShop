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
        IEnumerable<ApplicationRole> GetListRolesByGroupId(int groupId);
    }

    class ApplicationRoleRepository : RepositoryBase<ApplicationRole>, IApplicationRoleRepository
    {
        public ApplicationRoleRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public IEnumerable<ApplicationRole> GetListRolesByGroupId(int groupId)
        {
            var query = from r in DbContext.ApplicationRoles
                        join rg in DbContext.ApplicationRoleGroups
                        on r.Id equals rg.RoleId
                        where rg.GroupId == groupId
                        select r;
            return query;
        }
    }
}
