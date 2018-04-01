using System.Collections.Generic;
using System.Linq;
using OnlineShop.Data.Infrastructure;
using OnlineShop.Model.Models;

namespace OnlineShop.Data.Repositories
{

    public interface IPostRepository : IRepository<Post>
    {
        IEnumerable<Post> GetAllByTagPaging(string tagId, int? page, int? pageSize, out int totalRow);
    }
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Post> GetAllByTagPaging(string tagId, int? page, int? pageSize, out int totalRow)
        {
            var query = from p in DbContext.Posts
                        join pt in DbContext.PostTags
                        on p.Id equals pt.PostId
                        where pt.TagId == tagId && p.Status
                        orderby p.CreatedDate descending
                        select p;
            totalRow = query.Count();
            query =
                query.Skip((page.GetValueOrDefault() - 1) * pageSize.GetValueOrDefault())
                    .Take(pageSize.GetValueOrDefault());
            return query;
        }
    }
}
