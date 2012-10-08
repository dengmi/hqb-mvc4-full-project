using IServices;
using Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tsingbo.Web.Areas.Forum.Controllers
{
    public class ArticleController : Controller
    {
        [Inject]
        public IArticleServices service { get; set; }

        //
        // GET: /Forum/Article/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Read(int id = 0)
        {
            Article article = service.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReadCount = service.Read(id);
            ViewBag.Title = article.Title;
            return View(article);
        }

    }
}
