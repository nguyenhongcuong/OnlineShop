using OnlineShop.Data.Infrastructure;
using OnlineShop.Model.Models;

namespace OnlineShop.Data.Repositories
{
    public interface IFooterRepository
    {
        
    }
    public class FooterRepository:RepositoryBase<Footer>,IFooterRepository 
    {
        public FooterRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
