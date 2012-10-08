using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Extensions
{
    public class ChangeViewEngine : RazorViewEngine
    {
        public ChangeViewEngine(string controllerName, string viewName)
        {
            var viewFormat = string.Format("~/Views/{0}/{1}.cshtml", controllerName, viewName);
            this.ViewLocationFormats = new[] { viewFormat };
        }
    }
}
