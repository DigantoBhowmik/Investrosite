using Investrosite.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Investrosite.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AdminAccess : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //this.Roles = "Admin";
            var flag = base.AuthorizeCore(httpContext);
            if (flag)
            {
                var username = httpContext.User.Identity.Name;
                //                httpContext.User.Identity.IsAuthenticated
                var db = new investrositeEntities1();

                var admin = (from c in db.Admins
                             where c.Email == username
                             select c).FirstOrDefault();


                if (admin != null)
                    return true;

            }
            return false;
        }
    }
}