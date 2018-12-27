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
    public interface IPageService
    {
        Page GetPageByAlias(string alias);
        Page GetPagebyId(int id);
        IEnumerable<Page> GetAll(string keyword);
        IEnumerable<Page> GetAll();
        Page Add(Page page);
        void Update(Page page);
        Page Delete(int id);
        void Save();
    }

    public class PageService : IPageService
    {
        IPageRepository _pageRepository;
        IUnitOfWork _unitOfWork;

        public PageService(IPageRepository pageRepository, IUnitOfWork unitOfWork)
        {
            this._pageRepository = pageRepository;
            this._unitOfWork = unitOfWork;
        }

        public Page Add(Page page)
        {
            return _pageRepository.Add(page);
        }

        public Page Delete(int id)
        {
            return _pageRepository.Delete(id);
        }

        public IEnumerable<Page> GetAll(string keyword)
        {
            if (string.IsNullOrEmpty(keyword) || keyword.StartsWith(" "))
            {
                return _pageRepository.GetAll();
            }
            return _pageRepository.GetMulti(x => x.Name.Contains(keyword));
        }

        public IEnumerable<Page> GetAll()
        {
            return _pageRepository.GetAll();
        }

        public Page GetPageByAlias(string alias)
        {
            return _pageRepository.GetSingleEntity(x => x.Status == true && x.Alias == alias);
        }

        public Page GetPagebyId(int id)
        {
            return _pageRepository.GetSingleEntity(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Page page)
        {
            _pageRepository.Update(page);
        }
    }
}
