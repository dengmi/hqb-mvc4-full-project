using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Globalization;
using System.Threading;

namespace Extensions
{
    public class Globalization : System.Web.HttpApplication
    {
        public static void Initial()
        {
            var MySession = HttpContext.Current.Session;
            if (MySession != null)
            {
                CultureInfo lang = (CultureInfo)MySession["Lang"];
                if (lang == null)
                {
                    lang = new CultureInfo("zh-CN");
                    MySession["Lang"] = lang;
                }
                Thread.CurrentThread.CurrentUICulture = lang;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang.Name);
            }
        }
    }
}
