using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Test.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        /// <summary>
        /// 区域名称
        /// </summary>
        public override string AreaName
        {
            get { return "Admin"; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void RegisterArea(AreaRegistrationContext context)
        {
            //{controller}/{action}/{id}  每一个都需要使用{}，包括id，易错的地方

            context.MapRoute(
                "AdminDefault",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional, area = "Admin" },
                new string[] { "Test.Admin.Controllers" }
                );
        }
    }
}