using OnlineShop.Common.Exceptions;
using OnlineShop.Data.Infrastructure;
using OnlineShop.Data.Repositories;
using OnlineShop.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Service
{
    public interface IApplicationRoleService
    {
        ApplicationRole GetDetails(string id);
        IEnumerable<ApplicationRole> GetAll(int page, int pageSize, out int totalRow, string filter);
        IEnumerable<ApplicationRole> GetAll();
        ApplicationRole Add(ApplicationRole appRole);
        void Update(ApplicationRole appRole);
        void Delete(string id);

        // add to to group
        bool AddRolesToGroup(IEnumerable<ApplicationRoleGroup> roles, int groupId);
        //get list roles by groupId
        IEnumerable<ApplicationRole> GetListRolesByGroupId(int groupId);

        void Save();
    }

    public class ApplicationRoleService : IApplicationRoleService
    {
        IUnitOfWork _unitOfWork;
        IApplicationRoleRepository _applicationRoleRepository;
        IApplicationRoleGroupRepository _applicationRoleGroupRepository;

        public ApplicationRoleService(IUnitOfWork unitOfWork, IApplicationRoleRepository applicationRoleRepository, IApplicationRoleGroupRepository applicationRoleGroupRepository)
        {
            this._unitOfWork = unitOfWork;
            this._applicationRoleGroupRepository = applicationRoleGroupRepository;
            this._applicationRoleRepository = applicationRoleRepository;
        }

        public ApplicationRole Add(ApplicationRole appRole)
        {
            if (_applicationRoleRepository.CheckContains(x => x.Description == appRole.Description))
                throw new NameDuplicatedException("Name could not be duplicated. Please provide a new name.");
            return _applicationRoleRepository.Add(appRole);
        }

        public bool AddRolesToGroup(IEnumerable<ApplicationRoleGroup> roles, int groupId)
        {
            _applicationRoleGroupRepository.DeleteMulti(x => x.GroupId == groupId);
            foreach (var r in roles)
            {
                _applicationRoleGroupRepository.Add(r);
            }
            return true;
        }

        public void Delete(string id)
        {
            _applicationRoleRepository.DeleteMulti(x => x.Id == id);
        }

        public IEnumerable<ApplicationRole> GetAll(int page, int pageSize, out int totalRow, string filter)
        {
            var query = _applicationRoleRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Description.Contains(filter));
            totalRow = query.Count();

            return query.OrderBy(x => x.Description).Skip(page * pageSize).Take(pageSize);
        }

        public IEnumerable<ApplicationRole> GetAll()
        {
            return _applicationRoleRepository.GetAll();
        }

        public ApplicationRole GetDetails(string id)
        {
            return _applicationRoleRepository.GetSingleEntity(x => x.Id == id);
        }

        public IEnumerable<ApplicationRole> GetListRolesByGroupId(int groupId)
        {
            return _applicationRoleRepository.GetListRolesByGroupId(groupId);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ApplicationRole appRole)
        {
            if (_applicationRoleRepository.CheckContains(x => x.Id != appRole.Id && x.Description == appRole.Description))
                throw new NameDuplicatedException("Name could not be duplicated. Please provide a new name.");
            _applicationRoleRepository.Update(appRole);
        }
    }
}
