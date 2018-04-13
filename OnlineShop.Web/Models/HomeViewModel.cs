using System.Collections.Generic;

namespace OnlineShop.Web.Models
{
    public class HomeViewModel
    {
        public IEnumerable<SlideViewModel> SlideViewModels { get; set; }
        public IEnumerable<ProductViewModel> LastProductViewModels { get; set; }
        public IEnumerable<ProductViewModel> TopSaleProductViewModels { get; set; }

    }
}