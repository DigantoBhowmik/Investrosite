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
    public class UserAccess : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var flag = base.AuthorizeCore(httpContext);
            if (flag)
            {
                var username = httpContext.User.Identity.Name;
                var db = new investrositeEntities1();

                var user = (from c in db.Entrepreneurs
                             where c.Email == username
                             select c).FirstOrDefault();


                if (user != null)
                    return true;

            }
            return false;
        }
    }
}