using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twitter.Models;
using Twitter.Services;
using Twitter.Data;

namespace Twitter.Controllers
{
    public class MeController : Controller
    {
        ISecurityService _security;
        IUserRepository userRep;

        public MeController(IUserRepository rep)
        {
            _security = new SecurityService();
            userRep = rep;
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
        [HttpPost]
        public ActionResult UploadAvatar(HttpPostedFileBase file)
        {
            if(file != null && file.ContentLength > 0)
            {
                User user = _security.GetCurrentUser();
                try
                {
                    string path = Server.MapPath("~/Image/") + user.Name + "-ava.jpg";
                    file.SaveAs(path);
                    userRep.SetAvatarLink(user.Id, path);
                }
                catch(Exception e)
                {

                }
            }
            return RedirectToAction("Index", "Me");
        }
    }
}