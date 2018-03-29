using OnlineShop.Data.Infrastructure;
using OnlineShop.Model.Models;

namespace OnlineShop.Data.Repositories
{
    public interface IErrorRepository : IRepository<ErrorLog>
    {

    }
    public class ErrorRepository : RepositoryBase<ErrorLog>, IErrorRepository
    {
        public ErrorRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }
    }
}
