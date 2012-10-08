using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Tsingbo.Web.Models;
using Common;
using IServices;
using Ninject;

namespace Tsingbo.Web.Areas.Sites.Controllers
{
    public class CategoryController : Controller
    {
        [Inject]
        public ICategoryServices service { get; set; }
        //
        // GET: /Sites/Category/

        public ActionResult Index()
        {
            var list = service.GetAll();
            //ViewBag.TopCategories = list;// service.Where(item => item.ParentId == null || item.ParentId == 0);
            return View(list.ToList());
        }

        //
        // GET: /Sites/Category/Details/5

        public ActionResult Details(int id = 0)
        {
            Category category = service.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // GET: /Sites/Category/Create

        public ActionResult Create()
        {
            var categories = service.GetAll();
            //ViewBag.ParentId = new SelectList(categories, "Id", "Name");
            ViewBag.Parent = service.GetTreeList("ParentId", " ", "", 0);

            return View();
        }

        public ActionResult AjaxCreate()
        {
            var categories = service.GetAll();
            ViewBag.Parent = service.GetTreeList("ParentId", " ", "", 0);

            return View();
        }

        [HttpPost]
        public ActionResult AjaxCreate(Category category)
        {
            if (ModelState.IsValid)
            {
                service.Create(category);
                //return RedirectToAction("Index");
                return Json(true);
            }
            var categories = service.GetAll();
            //ViewBag.ParentId = new SelectList(categories, "Id", "Name");
            ViewBag.Parent = service.GetTreeList("ParentId", " ", "", 0);
            return View(category);
        }

        //
        // POST: /Sites/Category/Create

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                service.Create(category);
                return RedirectToAction("Index");
            }
            var categories = service.GetAll();
            //ViewBag.ParentId = new SelectList(categories, "Id", "Name");
            ViewBag.Parent = service.GetTreeList("ParentId", " ", "", 0);
            return View(category);
        }

        //
        // GET: /Sites/Category/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Category category = service.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            var categories = service.GetAll();
            //ViewBag.ParentId = new SelectList(categories, "Id", "Name", category.ParentId);
            //ViewBag.Parent = service.GetTreeList("ParentId"," ", "", category.Id);

            return View(category);
        }

        //
        // POST: /Sites/Category/Edit/5
        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                category.LastModifiedDate = DateTime.Now;
                category.LastModifiedUserName = User.Identity.Name;
                service.Edit(category);
                return RedirectToAction("Index");
            }
            var categories = service.GetAll();
            //ViewBag.ParentId = new SelectList(categories, "Id", "Name", category.ParentId);
            //ViewBag.Parent = service.GetTreeList("ParentId", " ", "", category.Id);

            return View(category);
        }

        //
        // GET: /Sites/Category/Delete/5
        public ActionResult Delete(int id = 0)
        {
            Category category = service.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        //
        // POST: /Sites/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = service.Find(id);
            service.Delete(category);
            return RedirectToAction("Index");
        }
    }
}