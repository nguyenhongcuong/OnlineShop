using System.Web.Mvc;
using AutoMapper;
using OnlineShop.Model.Models;
using OnlineShop.Service;
using OnlineShop.Web.Models;

namespace OnlineShop.Web.Controllers
{
    public class ContactController : Controller
    {
        private IContactDetailService _contactDetailService;

        public ContactController(IContactDetailService contactDetailService)
        {
            _contactDetailService = contactDetailService;
        }
        // GET: Contact
        public ActionResult Index()
        {
            var contactDetail = _contactDetailService.GetDefaultContactDetail();
            var contactDetailViewModel = Mapper.Map<ContactDetail, ContactDetailViewModel>(contactDetail);
            return View(contactDetailViewModel);
        }
    }
}