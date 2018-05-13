using System.Collections.Generic;
using System.Linq;
using OnlineShop.Data.Infrastructure;
using OnlineShop.Model.Models;

namespace OnlineShop.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        IEnumerable<Product> GetProductsByTag(string tagId , int page , int pageSize , out int totalRow);
    }
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Product> GetProductsByTag(string tagId , int page , int pageSize , out int totalRow)
        {
            var query = from p in DbContext.Products
                        join pt in DbContext.ProductTags
                            on p.Id equals pt.ProductId
                        where pt.TagId.Equals(tagId)
                        select p;
            totalRow = query.Count();
            return query.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}
