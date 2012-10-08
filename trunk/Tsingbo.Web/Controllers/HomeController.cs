using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using IServices;
using Models;
using System.Web.Helpers;

namespace Tsingbo.Web.Controllers
{
    public class HomeController : Controller
    {
        [Inject]
        public ICategoryServices categorySevices { get; set; }
        [Inject]
        public IArticleServices articleSevices { get; set; }

        public ActionResult Index()
        {
            HomeModel entity = new HomeModel();

            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";
            var topCategories = categorySevices.GetAll().Where(item => item.ParentId == null || item.ParentId == 0);
            entity.Categories = topCategories;
            entity.Articles = articleSevices.GetAll();
            return View(entity);
        }

        [HttpGet]
        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        public ActionResult About(int id)
        {
            WebMail.SmtpServer = "smtp.qq.com";
            WebMail.SmtpPort = 25;
            WebMail.EnableSsl = false;
            WebMail.UserName = "609089038";
            WebMail.Password = ".....";

            WebMail.From = "609089038@qq.com";
            WebMail.Send("609089038@qq.com", "RSVP Notification", "参加");

            return View();
        }

        public ActionResult Articles(int id = 0)
        {
            var list = articleSevices.GetAll(id);
            return View(list);
        }
        public ActionResult Details(int id = 0)
        {
            Article article = articleSevices.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReadCount = articleSevices.Read(id);
            ViewBag.Title = article.Title;
            return View(article);
        }
        [HandleError]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            Response.StatusCode = 404;
            throw new HttpException(404, "Page Not Found");
        }
    }
}
