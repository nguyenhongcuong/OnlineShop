using OnlineShop.Data.Infrastructure;
using OnlineShop.Model.Models;

namespace OnlineShop.Data.Repositories
{
    public interface IOrderDetailRepository
    {
        
    }
    public class OrderDetailRepository:RepositoryBase<OrderDetail>,IOrderDetailRepository
    {
        public OrderDetailRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
