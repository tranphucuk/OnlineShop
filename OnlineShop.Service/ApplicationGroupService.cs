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
    public interface IApplicationGroupService
    {
        ApplicationGroup GetDetail(int id);

        IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter);
        IEnumerable<ApplicationGroup> GetAll();

        ApplicationGroup Add(ApplicationGroup appGroup);

        void Update(ApplicationGroup appGroup);

        ApplicationGroup Delete(int id);

        bool AddUserToGroups(IEnumerable<ApplicationUserGroup> groups, string userId);

        IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId);

        IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId);

        void Save();
    }
    public class ApplicationGroupService : IApplicationGroupService
    {
        private IUnitOfWork _unitOfWork;
        private IApplicationUserGroupRepository _appUserGroupRepository;
        private IApplicationGroupRepository _appGroupRepository;

        public ApplicationGroupService(IUnitOfWork unitOfWork, IApplicationUserGroupRepository appUserGroupRepository, IApplicationGroupRepository appGroupRepository)
        {
            this._unitOfWork = unitOfWork;
            this._appUserGroupRepository = appUserGroupRepository;
            this._appGroupRepository = appGroupRepository;
        }

        public ApplicationGroup Add(ApplicationGroup appGroup)
        {
            if (_appGroupRepository.CheckContains(x => x.Name == appGroup.Name))
            {
                throw new NameDuplicatedException("Name could not be duplicated. Please provide a new name.");
            }
            return _appGroupRepository.Add(appGroup);
        }

        public bool AddUserToGroups(IEnumerable<ApplicationUserGroup> groups, string userId)
        {
            _appUserGroupRepository.DeleteMulti(x => x.UserId == userId);
            foreach (var ug in groups)
            {
                _appUserGroupRepository.Add(ug);
            }
            return true;
        }

        public ApplicationGroup Delete(int id)
        {
            var appGroup = _appGroupRepository.GetSingleEntity(id);
            return _appGroupRepository.Delete(appGroup);
        }

        public IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter)
        {
            var query = _appGroupRepository.GetAll();
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Name.ToLower().StartsWith(filter.ToLower()));
            totalRow = query.Count();
            return query.OrderBy(x => x.Name).Skip(page * pageSize).Take(pageSize);
        }

        public IEnumerable<ApplicationGroup> GetAll()
        {
            return _appGroupRepository.GetAll();
        }

        public ApplicationGroup GetDetail(int id)
        {
            return _appGroupRepository.GetSingleEntity(id);
        }

        public IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId)
        {
            return _appGroupRepository.GetListGroupByUserId(userId);
        }

        public IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId)
        {
            return _appGroupRepository.GetListUserByGroupId(groupId);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ApplicationGroup appGroup)
        {
            if (_appGroupRepository.CheckContains(x => x.Name == appGroup.Name && x.ID != appGroup.ID))
                throw new NameDuplicatedException("Name could not be duplicated. Please provide a new name.");
            _appGroupRepository.Update(appGroup);
        }
    }
}
