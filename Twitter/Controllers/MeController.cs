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
        public MeController()
        {
            _security = new SecurityService();
        }
        // GET: Me
        public ActionResult Index()
        {
            if (!_security.IsAuthenticate())
                return RedirectToAction("Index", "Home");
            User user = _security.GetCurrentUser();
            user.Tweets = user.Tweets.OrderByDescending(x => x.CreatingDate).ToList();
            return View(user);
        }
        public ActionResult Timeline()
        {
            if (!_security.IsAuthenticate())
                return RedirectToAction("Index", "Home");
            User user = _security.GetCurrentUser();
            ICollection<Tweet> timeline = new List<Tweet>();
            foreach(User following in user.Followings)
            {
                timeline = timeline.Concat(following.Tweets.Where(x => x.CreatingDate >= DateTime.Now.AddDays(-7)).ToList()).ToList();
            }
            timeline = timeline.OrderByDescending(x => x.CreatingDate).ToList();
            return View(timeline);
        }
    }
}