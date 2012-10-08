using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Models
{
    public class Category : BaseModel
    {
        [StringLength(20)]
        public string Name { get; set; }

        [ConfirmValidator("Id")]
        //[NotEqual("Id")]
        public int? ParentId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        //public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
