using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Tsingbo.Web.Models;
using Ninject;
using IServices;
using System.IO;

namespace Tsingbo.Web.Areas.Sites.Controllers
{
    public class ContentController : Controller
    {
        private const string allowExt = ".jpg,.png,.gif,.bmp";
        [Inject]
        public IArticleServices service { get; set; }
        [Inject]
        public ICategoryServices categoryService { get; set; }
        //
        // GET: /Sites/Content/

        public ActionResult Index(int id = 0, int pageSize = 10, int pageIndex = 1)
        {
            IQueryable<Article> list = null;
            if (id == 0)
            {
                list = service.GetAll();
            }
            else
            {
                list = service.GetAll(id);//.Include(a => a.Category);
            }
            return View(list.OrderByDescending(c => c.CreationDate).ToList());
        }

        //
        // GET: /Sites/Content/Details/5

        public ActionResult Details(int id = 0)
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

        //
        // GET: /Sites/Content/Create

        public ActionResult Create()
        {
            //var categories = service.GetCategories();
            //ViewBag.Category = categoryService.GetTreeList("CategoryId", "", "", 0);
            //ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            return View();
        }

        //
        // POST: /Sites/Content/Create

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Create(Article article)
        {
            if (ModelState.IsValid)
            {
                HttpPostedFileBase file = Request.Files["Upload"];
                if (file != null)
                {
                    var fileExt = file.FileName.Substring(file.FileName.LastIndexOf("."));
                    if (!allowExt.Contains(fileExt))
                    {
                        ModelState.AddModelError("Image", "不支持所上传的类型！");
                        return View(article);
                    }
                    var dir = System.IO.Path.Combine(Server.MapPath("~/Uploads"), "image");
                    if (Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }
                    var fileName = Core.UniqueIdGenerator.GetInstance().GetBase32UniqueId(10) + fileExt;
                    var savePath = System.IO.Path.Combine(dir, fileName);
               
                    file.SaveAs(savePath);
                    article.Image = string.Format("/Uploads/image/{0}", fileName);
                }
                else
                {
                    article.Image = null;
                }
                var lang = Session["lang"] == null ? "zh-CN" : Session["lang"].ToString();
                article.Lang = lang;
                article.UserName = User.Identity.Name;
                service.Create(article);
                return RedirectToAction("Index");
            }

            //ViewBag.SelectedValue = article.Category.Id;
            return View(article);
        }

        //
        // GET: /Sites/Content/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Article article = service.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            //ViewBag.CategoryId = new SelectList(service.GetCategories(), "Id", "Name", article.CategoryId);
            return View(article);
        }

        //
        // POST: /Sites/Content/Edit/5

        [HttpPost]
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Edit(Article article)
        {
            if (ModelState.IsValid)
            {
                article.LastModifiedUserName = User.Identity.Name;
                service.Edit(article);
                return RedirectToAction("Index");
            }
            //ViewBag.CategoryId = new SelectList(service.GetCategories(), "Id", "Name", article.CategoryId);
            return View(article);
        }

        //
        // GET: /Sites/Content/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Article article = service.Find(id);
            if (article == null)
            {
                return HttpNotFound();
            }
            return View(article);
        }

        //
        // POST: /Sites/Content/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Article article = service.Find(id);
            service.Delete(article);
            return RedirectToAction("Index");
        }

    }
}
