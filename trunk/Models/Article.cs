using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Models
{
    public class Article : BaseModel
    {
        public int TypeId { get; set; }

        //[StringLength(50)]
        //public string CatePath { get; set; }

        [StringLength(20)]
        public string ArticleId { get; set; }

        //public int ParentId { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(500)]
        [DataType(DataType.MultilineText)]
        public string Summary { get; set; }

        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        public int ReadCount { get; set; }

        public int? OrderId { get; set; }

        public int Like { get; set; }

        public bool AllowComment { get; set; }

        //是否推荐
        public bool Favor { get; set; }

        //是否置顶
        public bool IsTop { get; set; }

        [DataType(DataType.ImageUrl)]
        //[AllowFileExt()]
        public string Image { get; set; }

        public virtual Category Category { get; set; }

        public int CategoryId { get; set; }
    }
}
