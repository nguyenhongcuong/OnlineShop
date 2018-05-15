using System;
using System.Configuration;
using System.Text;
using System.Web.Mvc;
using AutoMapper;
using BotDetect.Web.Mvc;
using OnlineShop.Model.Models;
using OnlineShop.Service;
using OnlineShop.Web.Infrastructure.Extensions;
using OnlineShop.Web.Models;
using INVOICE_FA11_SERVICE.Service;
using OnlineShop.Common;

namespace OnlineShop.Web.Controllers
{
    public class ContactController : Controller
    {
        private IContactDetailService _contactDetailService;
        private IFeedbackService _feedbackService;

        public ContactController(IContactDetailService contactDetailService , IFeedbackService feedbackService)
        {
            _contactDetailService = contactDetailService;
            _feedbackService = feedbackService;
        }

        public ActionResult _ContactDetailPartial()
        {
            var contactDetail = _contactDetailService.GetDefaultContactDetail();
            var contactDetailViewModel = Mapper.Map<ContactDetail , ContactDetailViewModel>(contactDetail);
            return PartialView(contactDetailViewModel);

        }
        // GET: Contact
        [HttpGet]
        public ActionResult Index()
        {
            return View(new FeedbackViewModel
            {
                CreatdDate = DateTime.Now ,
                Status = false
            });
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode" , "ContactCaptcha" , "Mã xác nhận không đúng!")]
        public ActionResult Index(FeedbackViewModel feedbackViewModel)
        {
            bool check = false;
            feedbackViewModel.CreatdDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                feedbackViewModel.CreatdDate = DateTime.Now;
                Feedback feedbackCreate = new Feedback();
                feedbackCreate.UpdateFeedback(feedbackViewModel);
                _feedbackService.Create(feedbackCreate);
                _feedbackService.Save();
                ViewBag.SuccessMsg = "Gửi phản hồi thành công. Chúng tôi sẽ xem xét phản hồi của bạn.";

                string content = System.IO.File.ReadAllText(Server.MapPath(
                    @"~/Assets/Client/mail_template/mailcontact.html"));
                content = content.Replace("{{name}}" , feedbackViewModel.Name);
                content = content.Replace("{{email}}" , feedbackViewModel.Email);
                content = content.Replace("{{message}}" , feedbackViewModel.Message);

                string adminMail = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();


                MailHelper.SendMail(adminMail , "Thông tin liên hệ từ website OnlineShop" , content);

                return RedirectToAction("Index");
            }
            return View(feedbackViewModel);

        }



    }
}