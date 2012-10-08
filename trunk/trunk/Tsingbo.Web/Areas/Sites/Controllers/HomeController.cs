using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using IServices;

namespace Tsingbo.Web.Areas.Sites.Controllers
{
    public class HomeController : Controller
    {
        [Inject]
        public ICategoryServices categoryService { get; set; }
        //
        // GET: /Sites/Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Content()
        {
            return View();
        }

        public ActionResult Config()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Categories()
        {
            var list = categoryService.Where(c=>c.ParentId==null);
            return PartialView(list);
        }
    }
}
