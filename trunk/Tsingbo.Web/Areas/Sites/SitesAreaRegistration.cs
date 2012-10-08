using System.Web.Mvc;

namespace Tsingbo.Web.Areas.Sites
{
    public class SitesAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Sites";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Admin", "Admin", new { controller = "Home", action = "Index", area = "Sites" });
            context.MapRoute(
                "Sites_default",
                "Sites/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                new[] { "Tsingbo.Web.Areas.Sites.Controllers" }
            );
        }
    }
}
