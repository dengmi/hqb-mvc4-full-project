using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

namespace Common
{
   public  class DataContextInitializer:DropCreateDatabaseAlways<DefaultContext>
    {
       protected override void Seed(DefaultContext context)
       {
           context.Categories.SqlQuery("", null);

           base.Seed(context);
       }

    }
}
