namespace OnlineShop.Web.Models
{
    public class ProductTagViewModel
    {
        public int ProductId { get; set; }
        public int TagId { get; set; }
        public virtual ProductViewModel ProductViewModel { get; set; }
        public virtual TagViewModel TagViewModel { get; set; }
    }
}