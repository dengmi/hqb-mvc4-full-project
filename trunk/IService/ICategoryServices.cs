using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;

namespace IServices
{
    public interface ICategoryServices
    {
        int Create(Category entity);


        int Edit(Category entity);

        int Delete(Category entity);

        int DeleteList(params Expression<Func<Category, bool>>[] conditions);

        Category Find(params object[] key);

        IQueryable<Category> GetAll();

        IQueryable<Category> GetList(bool published = true);

        IQueryable<Category> GetChildren(int parentId = 0);

        IQueryable<Category> Where(Expression<Func<Category, bool>> condition);

        List<MvcHtmlString> GetTreeList();
        List<MvcHtmlString> GetTreeList(string name, string topText, string topValue,int selectedValue);

        List<MvcHtmlString> GetAllTreeList(string name, string topText, string topValue, int selectedValue);
    }
}
