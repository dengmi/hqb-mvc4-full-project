using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IServices;
using System.Data;
using Models;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace Common.Services
{
    public class CategoryServices : ICategoryServices
    {
        public static int selValue { get; set; }

        DefaultContext db = new DefaultContext();
        #region MyRegion
        public int Create(Category entity)
        {
            entity.UUID = Core.UniqueIdGenerator.GetInstance().GetBase32UniqueId(10);
            entity.LastModifiedDate = DateTime.Now;
            entity.CreationDate = DateTime.Now;
            db.Categories.Add(entity);
            return db.SaveChanges();
        }

        public int Edit(Category entity)
        {
            entity.LastModifiedDate = DateTime.Now;
            db.Entry(entity).State = EntityState.Modified;
            return db.SaveChanges();
        }

        public int Delete(Category entity)
        {
            db.Categories.Remove(entity);
            return db.SaveChanges();
        }

        public int DeleteList(params Expression<Func<Category, bool>>[] conditions)
        {
            foreach (var predicate in conditions)
            {
                db.Set<Category>().Remove(db.Set<Category>().Single(predicate));
            }
            return db.SaveChanges();
        }

        public Category Find(params object[] key)
        {
            return db.Set<Category>().Find(key);
        }

        public IQueryable<Category> GetAll()
        {
            //过滤掉Id跟ParentId相同的数据，防止死循环
            return db.Categories.Where(c => c.Id != c.ParentId || c.ParentId == null).Take(1000);
        }

        public IQueryable<Category> GetList(bool published = true)
        {
            return GetAll().Where(item => item.Published == published);
        }

        public IQueryable<Category> GetChildren(int parentId = 0)
        {
            return GetAll().Where(item => item.ParentId == parentId);
        }

        public IQueryable<Category> Where(Expression<Func<Category, bool>> condition)
        {
            return GetAll().Where(condition);
        }

        public List<MvcHtmlString> GetTreeList(string name, string topText, string topValue, int selectedValue)
        {
            var list = new List<MvcHtmlString>();
            list.Add(new MvcHtmlString(string.Format("<select id=\"{0}\" name=\"{0}\">", name)));
            if (!string.IsNullOrEmpty(topText))
            {
                var first = new MvcHtmlString(string.Format("<option value=\"{0}\">{1}</option>", topValue, topText));

                list.Add(first);
            }
            list = RenderTreeList(list, 0, 0, selectedValue);
            list.Add(new MvcHtmlString("</select>"));
            return list;
        }

        public List<MvcHtmlString> GetTreeList()
        {
            var list = new List<MvcHtmlString>();
            list.Add(new MvcHtmlString("<select>"));
            list = RenderTreeList(list, 0, 0, 0);
            list.Add(new MvcHtmlString("</select>"));
            return list;
        }

        private List<MvcHtmlString> RenderTreeList(List<MvcHtmlString> list, int parentId, int level, int selectedValue)
        {
            IQueryable<Category> items = null;
            if (parentId == 0)
            {
                items = GetAll().Where(w => w.ParentId == null || w.ParentId == 0);
            }
            else
            {
                items = GetChildren(parentId);
            }
            level++;

            foreach (var item in items.ToList())
            {
                if (selectedValue == item.ParentId)
                {
                    //在编辑状态时则过滤掉选中项的子项，即不允许设计子项为父项的父项
                    continue;
                }
                //var tmp = GetAll().Where(c => c.Id == selectedValue).FirstOrDefault();

                //var isSelected = (item.ParentId == (tmp == null ? 0 : tmp.ParentId) && selectedValue != 0);
                var isSelected = parentId == selectedValue;
                list.Add(RenderOption(item.Id, item.Name, isSelected, level));

                RenderTreeList(list, item.Id, level, selectedValue);
            }
            return list;
        }


        #endregion



        public List<MvcHtmlString> GetAllTreeList(string name, string topText, string topValue, int selectedValue)
        {
            var list = new List<MvcHtmlString>();
            list.Add(new MvcHtmlString(string.Format("<select id=\"{0}\" name=\"{0}\">", name)));
            if (!string.IsNullOrEmpty(topText))
            {
                var first = new MvcHtmlString(string.Format("<option value=\"{0}\">{1}</option>", topValue, topText));

                list.Add(first);
            }
            list = RenderTreeList(list, 0, 0, selectedValue);
            list.Add(new MvcHtmlString("</select>"));
            return list;
        }

        public List<MvcHtmlString> RenderAllTreeList(List<MvcHtmlString> list, int id, int selectedValue, int level, bool isEdit)
        {
            var parent = Find(selectedValue);
            selValue = parent == null ? 0 : parent.ParentId ?? 0;

            //items为指定parentid的集合
            IQueryable<Category> items = null;
            if (level == 0)
            {
                items = GetAll().Where(c => c.ParentId == null);
            }
            else
            {
                items = GetChildren(id);
            }
            level++;

            foreach (var item in items.ToList())
            {
                if (isEdit && (selValue == item.ParentId || item.ParentId == selectedValue))
                {
                    //在编辑状态时则过滤掉选中项的子项，即不允许设计子项为父项的父项
                    continue;
                }

                //var isSelected = (item.ParentId == (tmp == null ? 0 : tmp.ParentId) && selectedValue != 0);
                var isSelected = item.Id == selValue;
                list.Add(RenderOption(item.Id, item.Name, isSelected, level));
                list = RenderAllTreeList(list, item.Id, selectedValue, level, isEdit);
            }
            return list;
        }


        private MvcHtmlString RenderOption(int value, string text, bool selected, int level)
        {
            var space = string.Empty;
            if (level > 1)
            {
                for (int i = 1; i < level; i++)
                {
                    space += "&nbsp;&nbsp;";
                }
                space += "└";
                text = space + text;
            }
            return new MvcHtmlString(string.Format("<option value=\"{0}\"{1}>{2}</option>", value, selected ? " selected = \"selected\"" : string.Empty, text));
        }


    }


}
