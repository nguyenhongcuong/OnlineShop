namespace OnlineShop.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private OnlineShopDbContext _dbContext;
        public OnlineShopDbContext Init()
        {
            return _dbContext ?? (_dbContext = new OnlineShopDbContext());
        }

        protected override void DisposeCore()
        {
            if (_dbContext != null)
                _dbContext.Dispose();
        }
    }
}
