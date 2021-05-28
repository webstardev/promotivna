using MarkomPos.Model.ViewModel;
using MarkomPos.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MarkomPos.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View("~/Views/Account/Login.cshtml");
        }

        public ActionResult Authenticate(LoginVm loginVm)
        {
            if (ModelState.IsValid)
            {
                using (var accountRepository = new AccountRepository())
                {
                    var result = accountRepository.IsValid(loginVm);
                    if (result)
                    {
                        FormsAuthentication.SetAuthCookie(loginVm.Username, false);
                        return RedirectToAction("Index", "UnitOfMeasures");
                    }
                }
            }
            ViewBag.IsValid = false;
            return View("~/Views/Account/Login.cshtml");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}