using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TrashCollector.Controllers
{
    public class RolesController : UsersController
    {
        // GET: Roles
        public ActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {


                if (!isAdminUser())
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            var Roles = context.Roles.ToList();
            return View(Roles);

        }
    }
}