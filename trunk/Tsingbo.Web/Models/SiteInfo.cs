using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tsingbo.Web.Models
{
    public class SiteInfo
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string Tags { get; set; }
        public string Power
        {
            get { return "Tsingbo"; }
        }
        public string Version
        {
            get { return "1.0"; }
        }
    }
}