using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IServices;
using Models;
using System.Data;
using MvcPager;

namespace Common.Services
{
    public class ArticleServices : IArticleServices
    {
        DefaultContext db = new DefaultContext();
        public IQueryable<Article> GetAll()
        {
            IQueryable<Article> query = db.Articles;
            return query;
        }

        public IQueryable<Article> GetList(bool published = true)
        {
            return GetAll().Where(m => m.Published == published);
        }

        public IQueryable<Article> GetAll(int categoryId)
        {
            return GetAll().Where(m => m.CategoryId == categoryId);
        }

        public IQueryable<Article> GetChildren(int? parentId)
        {
            return GetAll().Where(m => m.Category.ParentId == parentId);
        }

        public IPagedList<Article> GetAll(int categoryId, int pageIndex, int pageSize = 10)
        {
            return GetAll(categoryId).ToPagedList(pageIndex, pageSize);
        }

        public int Create(Article entity)
        {
            entity.ReadCount = 0;
            if (string.IsNullOrEmpty(entity.ArticleId))
            {
                entity.ArticleId = Core.UniqueIdGenerator.GetInstance().GetBase32UniqueId(10);
            }
            entity.UUID = Core.UniqueIdGenerator.GetInstance().GetBase32UniqueId(10);
            entity.LastModifiedDate = DateTime.Now;
            entity.CreationDate = DateTime.Now;
            var orderId = GetAll().Max(c => c.OrderId);
            if (orderId.HasValue)
            {
                entity.OrderId = orderId.Value + 1;
            }
            else
            {
                entity.OrderId = 1;
            }

            db.Articles.Add(entity);
            return db.SaveChanges();
        }

        public int CreateList(List<Article> list)
        {
            list.ForEach(item => db.Set<Article>().Add(item));
            db.GetValidationErrors();
            return db.SaveChanges();
        }

        public int Edit(Article entity)
        {
            entity.LastModifiedDate = DateTime.Now;
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public int Delete(Article entity)
        {
            db.Articles.Remove(entity);
            return db.SaveChanges();
        }

        public int DeleteList(params System.Linq.Expressions.Expression<Func<Article, bool>>[] conditions)
        {
            foreach (var predicate in conditions)
            {
                db.Set<Article>().Remove(db.Set<Article>().Single(predicate));
            }
            return db.SaveChanges();
        }

        public Article Find(params object[] key)
        {
            var entity = db.Set<Article>().Find(key);
            return entity;
        }

        public IQueryable<Article> Where(System.Linq.Expressions.Expression<Func<Article, bool>> condition)
        {
            return db.Set<Article>().Where(condition);
        }

        public IQueryable<Category> GetCategories()
        {
            return db.Set<Category>();
        }

        public int Read(int id)
        {
            var entity = Find(id);
            entity.ReadCount += 1;
            Edit(entity);
            return entity.ReadCount;
        }

        ~ArticleServices()
        {
            db.Dispose();
        }
    }
}
