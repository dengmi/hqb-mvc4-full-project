using System.Web.Mvc;

namespace Tsingbo.Web.Areas.Forum
{
    public class ForumAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Forum";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Forum_default",
                "Forum/{controller}/{action}/{id}",
                new { controller = "Article", action = "Index", id = UrlParameter.Optional },
                new[] { "Tsingbo.Web.Areas.Forum.Controllers" }
            );
        }
    }
}
