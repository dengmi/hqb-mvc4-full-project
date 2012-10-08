using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Models;
using Common;

namespace Tsingbo.Web.APIs
{
    public class ArticleController : ApiController
    {
        private DefaultContext db = new DefaultContext();

        // GET api/Article
        public IEnumerable<Article> GetArticles()
        {
            var articles = db.Articles.Include(a => a.Category);
            return articles.AsEnumerable();
        }

        // GET api/Article/5
        public Article GetArticle(int id)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return article;
        }

        // PUT api/Article/5
        public HttpResponseMessage PutArticle(int id, Article article)
        {
            if (ModelState.IsValid && id == article.Id)
            {
                db.Entry(article).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Article
        public HttpResponseMessage PostArticle(Article article)
        {
            if (ModelState.IsValid)
            {
                db.Articles.Add(article);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, article);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = article.Id }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Article/5
        public HttpResponseMessage DeleteArticle(int id)
        {
            Article article = db.Articles.Find(id);
            if (article == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Articles.Remove(article);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, article);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}