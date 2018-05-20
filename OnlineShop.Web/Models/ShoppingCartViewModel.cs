using System;

namespace OnlineShop.Web.Models
{
    [Serializable]
    public class ShoppingCartViewModel
    {
        public int ProductId { get; set; }
        public ProductViewModel ProductViewModel { get; set; }
        public int Quantity { get; set; }
    }
}