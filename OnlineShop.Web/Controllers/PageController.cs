using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using OnlineShop.Model.Models;
using OnlineShop.Service;
using OnlineShop.Web.Models;

namespace OnlineShop.Web.Controllers
{
    public class PageController : Controller
    {
        private IPageService _pageService;

        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }
        // GET: Page
        public ActionResult Index(string alias)
        {
            var page = _pageService.GetByAlias(alias);
            var pageViewModel = Mapper.Map<Page, PageViewModel>(page);
            return View(pageViewModel);
        }
    }
}