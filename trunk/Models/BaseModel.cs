using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Models
{
   public class BaseModel
    {
       [Key]
       public virtual int Id { get; set; }

       [StringLength(20)]
       public string UUID { get; set; }


       [DataType(DataType.DateTime)]
       public DateTime CreationDate { get; set; }

       [DataType(DataType.DateTime)]
       public DateTime LastModifiedDate { get; set; }

       [StringLength(10)]
       public string Lang { get; set; }

       [StringLength(50)]
       public string UserName { get; set; }

       [StringLength(50)]
       public string LastModifiedUserName { get; set; }
       
       public bool Published { get; set; }
    }
}
