using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using OnlineShop.Model.Models;
using OnlineShop.Service;
using OnlineShop.Web.Models;

namespace OnlineShop.Web.Controllers
{
    public class ShoppingCartController : Controller
    {
        private IProductService _productService;

        public ShoppingCartController(IProductService productService)
        {
            _productService = productService;
        }
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Add(int productId)
        {
            var cart = new List<ShoppingCartViewModel>();
            if (Session[Common.CommonConstants.SessionCart] != null)
            {
                cart = (List<ShoppingCartViewModel>)Session[Common.CommonConstants.SessionCart];
            }

            if (cart.Any(x => x.ProductId == productId))
            {
                foreach (var cartViewModel in cart)
                {
                    if (cartViewModel.ProductId == productId)
                        cartViewModel.Quantity += 1;
                }
            }
            else
            {
                Product product = _productService.GetById(productId);
                ProductViewModel productViewModel = Mapper.Map<Product, ProductViewModel>(product);
                ShoppingCartViewModel cartViewModel = new ShoppingCartViewModel
                {
                    ProductId = productId,
                    Quantity = 1,
                    ProductViewModel = productViewModel
                };
                cart.Add(cartViewModel);
            }

            Session[Common.CommonConstants.SessionCart] = cart;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult GetAll()
        {
            var cart = new List<ShoppingCartViewModel>();
            if (Session[Common.CommonConstants.SessionCart] != null)
            {
                cart = (List<ShoppingCartViewModel>)Session[Common.CommonConstants.SessionCart];
            }

            var amount = cart.Sum(x => x.Quantity * x.ProductViewModel.Price);

            return Json(new
            {
                data = cart,
                status = true,
                amount = amount
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteAll()
        {
            Session.Remove(Common.CommonConstants.SessionCart);
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult Update(int productId, int quantity)
        {
            var cartSession = (List<ShoppingCartViewModel>)Session[Common.CommonConstants.SessionCart];

            foreach (var item in cartSession)
            {
                if (item.ProductId == productId)
                {
                    item.Quantity = quantity;
                }
            }

            Session[Common.CommonConstants.SessionCart] = cartSession;
            return Json(new
            {
                status = true
            });
        }

        [HttpPost]
        public JsonResult Delete(int productId)
        {
            var cartSession = (List<ShoppingCartViewModel>)Session[Common.CommonConstants.SessionCart];

            foreach (var item in cartSession)
            {
                if (item.ProductId == productId)
                {
                    cartSession.Remove(item);
                }
            }

            Session[Common.CommonConstants.SessionCart] = cartSession;
            return Json(new
            {
                status = true
            });
        }
    }
}