using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twitter.Models;
using Twitter.Services;

namespace Twitter.Controllers
{
    public class MeController : Controller
    {
        ISecurityService _security;
        public MeController(ISecurityService security)
        {
            _security = security;
        }
        // GET: Me
        public ActionResult Index()
        {
            if (!_security.IsAuthenticate())
                return RedirectToAction("Index", "Home");
            User user = _security.GetCurrentUser();
            return View(user);
        }
    }
}