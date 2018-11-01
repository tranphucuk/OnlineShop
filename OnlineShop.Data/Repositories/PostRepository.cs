using OnlineShop.Data.Infrastructure;
using OnlineShop.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.Repositories
{
    public interface IPostRepository : IRepository<Post>
    {
        /// <summary>
        /// get All posts by specific tag
        /// </summary>
        /// <param name="tag"></param>
        /// <param name="page"></param>
        /// <param name="PageSize"></param>
        /// <param name="totalRow"></param>
        /// <returns></returns>
        IEnumerable<Post> GetAllPostbyTag(string tag, int page, int PageSize, out int totalRow);
    }

    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Post> GetAllPostbyTag(string tag, int pageIndex, int PageSize, out int totalRow)
        {
            var query = from p in DbContext.Posts
                        join pt in DbContext.PostTags
                        on p.ID equals pt.PostID
                        where pt.TagID == tag
                        orderby p.CreatedDate descending
                        select p;
            totalRow = query.Count();
            query = query.Skip((pageIndex - 1) * PageSize).Take(PageSize);
            return query;
        }
    }
}
