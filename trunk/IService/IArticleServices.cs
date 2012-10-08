using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using System.Linq.Expressions;
using MvcPager;

namespace IServices
{
    public interface IArticleServices
    {
        int Create(Article entity);

        int CreateList(List<Article> list);

        int Edit(Article entity);

        int Delete(Article entity);

        int DeleteList(params Expression<Func<Article, bool>>[] conditions);

        Article Find(params object[] key);

        IQueryable<Article> GetAll();

        IQueryable<Article> GetList(bool published = true);

        IQueryable<Article> GetAll(int categoryId);

        IPagedList<Article> GetAll(int categoryId, int pageIndex, int pageSize = 10);

        IQueryable<Article> GetChildren(int? parentId);

        IQueryable<Article> Where(Expression<Func<Article, bool>> condition);

        IQueryable<Category> GetCategories();

        int Read(int id);

    }
}
