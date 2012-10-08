using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Models
{
    public class HomeModel
    {
        public IQueryable<Category> Categories { get; set; }

        public IQueryable<Article> Articles { get; set; }

        public Category Category { get; set; }

        public Article Article { get; set; }
    }
}
