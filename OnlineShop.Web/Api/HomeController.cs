using System.Web.Http;
using OnlineShop.Service;
using OnlineShop.Web.Infrastructure.Core;

namespace OnlineShop.Web.Api
{
    [RoutePrefix("api/home")]
    [Authorize]
    public class HomeController : BaseApiController
    {
        private IErrorService _errorService;
        public HomeController(IErrorService errorService) : base(errorService)
        {
            _errorService = errorService;
        }

        [HttpGet]
        [Route("TestMethod")]
        public string TestMethod()
        {
            return "Hello, Angular 2";
        }
    }
}
