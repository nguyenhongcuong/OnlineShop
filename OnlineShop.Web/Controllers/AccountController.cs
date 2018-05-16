﻿using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BotDetect.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using OnlineShop.Common;
using OnlineShop.Data;
using OnlineShop.Model.Models;
using OnlineShop.Web.App_Start;
using OnlineShop.Web.Models;

namespace OnlineShop.Web.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager , ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? System.Web.HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [CaptchaValidation("CaptchaCode" , "RegisterCaptcha" , "Mã xác nhận không đúng!")]
        public async Task<ActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var checkEmail = _userManager.FindByEmailAsync(registerViewModel.Email);
                if (checkEmail.Result != null)
                {
                    ModelState.AddModelError("emailCheckError" , @"Email đã tồn tại");
                    return View(registerViewModel);
                }

                var usernameCheck = _userManager.FindByNameAsync(registerViewModel.UserName);
                if (usernameCheck.Result != null)
                {
                    ModelState.AddModelError("usernameCheckError" , @"Tài khoản này đã tồn tại");
                    return View(registerViewModel);
                }
                var user = new ApplicationUser
                {
                    UserName = registerViewModel.UserName ,
                    Email = registerViewModel.Email ,
                    EmailConfirmed = true ,
                    BirthDay = DateTime.Now ,
                    FullName = registerViewModel.FullName ,
                    PhoneNumber = registerViewModel.PhoneNumber ,
                    Address = registerViewModel.Address ,

                };

                await _userManager.CreateAsync(user , registerViewModel.Password);


                var adminUser = await _userManager.FindByEmailAsync(registerViewModel.Email);
                if (adminUser != null)
                    await _userManager.AddToRolesAsync(adminUser.Id , new string[] { "User" });
                TempData["RegisterSuccess"] = "OK";
                string content = System.IO.File.ReadAllText(Server.MapPath(
                    @"~/Assets/Client/mail_template/mailregister.html"));
                content = content.Replace("{{username}}" , registerViewModel.UserName);
                content = content.Replace("{{link}}" , "http://localhost:50792/dang-nhap");

                MailHelper.SendMail(registerViewModel.Email , "Đăng ký thành công tài khoản" , content);
                return RedirectToAction("Register" , "Account");
            }


            return View(registerViewModel);
        }
    }
}