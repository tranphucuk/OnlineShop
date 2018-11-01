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
    public interface IPostService
    {
        void Add(Post post);
        void Update(Post post);
        void Delete(int id);
        IEnumerable<Post> GetAll();
        IEnumerable<Post> GetAllPaging(int page, int PageSize, out int totalRow);
        IEnumerable<Post> GetAllByCategoryPaging(int categoryId, int page, int PageSize, out int totalRow);
        Post GetById(int id);
        IEnumerable<Post> GetAllPostbyTag(string tag, int page, int PageSize, out int totalRow);
        void SaveChanges();
    }

    public class PostService : IPostService
    {
        public PostService(IPostRepository postRepository, IUnitOfWork unitOfWork)
        {
            this._postRepository = postRepository;
            this._UnitOfWork = unitOfWork;
        }

        IPostRepository _postRepository;
        IUnitOfWork _UnitOfWork;

        public void Add(Post post)
        {
            _postRepository.Add(post);
        }

        public void Delete(int id)
        {
            _postRepository.Delete(id);
        }

        public IEnumerable<Post> GetAll()
        {
            return _postRepository.GetAll(new string[] { "CategoryID" });
        }

        public IEnumerable<Post> GetAllPaging(int page, int PageSize, out int totalRow)
        {
            return _postRepository.GetMultiPaging(x => x.Status, out totalRow, page, PageSize);
        }

        public IEnumerable<Post> GetAllPostbyTag(string tag, int page, int PageSize, out int totalRow)
        {
            return _postRepository.GetAllPostbyTag(tag, page, PageSize, out totalRow);
        }

        public Post GetById(int id)
        {
            return _postRepository.GetSingleEntity(id);
        }

        public void SaveChanges()
        {
            _UnitOfWork.Commit();
        }

        public void Update(Post post)
        {
            _postRepository.Update(post);
        }

        public IEnumerable<Post> GetAllByCategoryPaging(int categoryId, int page, int PageSize, out int totalRow)
        {
            return _postRepository.GetMultiPaging(x => x.Status && x.CategoryID == categoryId, out totalRow, page, PageSize, new string[] { "PostCategory" });
        }
    }
}
