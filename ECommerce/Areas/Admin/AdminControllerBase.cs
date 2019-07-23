using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ECommerce.Areas.Admin
{
    public class AdminControllerBase:Controller
    {
        protected override void Initialize(RequestContext requestContext)
        {
            var IsLogin=false;
            if(requestContext.HttpContext.Session["AdminLoginUser"]==null)
            {
                //Admin girişi yapılmadı
                requestContext.HttpContext.Response.Redirect("/Admin/AdminLogin");
            }
            else
            {
                //admin girişi yapıldı
            }
            base.Initialize(requestContext);
        }
    }
}