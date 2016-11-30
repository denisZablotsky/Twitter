using System.Linq;
using System.Web.Mvc;
using Twitter.Models;
using Twitter.Data;
using Twitter.Services;
using System;
using System.Collections.Generic;
using System.Web;
using Enyim.Caching;
using HtmlAgilityPack;

namespace Twitter.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository userRepository;
        private ISecurityService _security;
        private ICommentRepository commentRep;
        private ITweetRepository tweetRep;
        private MemcachedClient MemcachedClient;

        public HomeController(IUserRepository userRep, ICommentRepository commRep, ITweetRepository tweerR)
        {
            MemcachedClient = new MemcachedClient();
            userRepository = userRep;
            commentRep = commRep;
            tweetRep = tweerR;
            _security = new SecurityService();
        }
        // GET: Home
        public ActionResult Index()
        {
            IQueryable<User> Users = userRepository.Users;
            if (Users.Any())
                return View(Users);
            return View("Index", null);
        }
        public ActionResult Registration()
        {
            User user = new Models.User();
            return View(user);
        }
        [HttpPost]
        public ActionResult Signup(User user)
        {
            userRepository.CreateUser(user);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult News()
        {
            ICollection<Tweet> news = tweetRep.GetLastNews().ToList();
            return PartialView("_tweetList", news);
        }
        public ActionResult Cached()
        {
            MemcachedClient client = new MemcachedClient();
            bool f = false;
            f = client.Store(Enyim.Caching.Memcached.StoreMode.Set, "ab", "denis", TimeSpan.FromMinutes(20));
            string name = client.Get<string>("ab");
            //name = "aaaa";
            return View("Cached",(object)name);
        }
        // Authentication
        public PartialViewResult AuthenticateSection()
        {
            if (_security.IsAuthenticate())
            {
                return PartialView("_authenticatedSection", _security.GetCurrentUser());
            }
            else
            {
                return PartialView("_loginSection", new User());
            }
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            if (!_security.Authenticate(user.Name, user.Password))
                return RedirectToAction("Index", "Home");
            user = userRepository.GetUserByName(user.Name);
            _security.Login(user);
            return RedirectToAction("Index", "Me");
        }
        [HttpPost]
        public ActionResult Logout(User user)
        {
            _security.Logout();
            return RedirectToAction("Index", "Home");
        }

        public PartialViewResult CreateComment(Tweet tweet)
        {
            if (!_security.IsAuthenticate())
                return null;
            else
                return PartialView("_createComment", tweet);     
        }
        public PartialViewResult addComment(Tweet tweet)
        {
            Comment comment = new Comment();
            comment.TweetId = tweet.Id;
            return PartialView("_addCommentForm", comment);
        }
        [HttpPost]
        public PartialViewResult addComment(int id, Comment comment)
        {
            User user = _security.GetCurrentUser();
            comment.AuthorName = user.Name;
            comment.AuthourId = user.Id;
            comment.CreatingDate = DateTime.Now;
            commentRep.CreateComment(comment);
            Tweet tweet = tweetRep.GetTweetById(comment.TweetId);
            return PartialView("_commentsList", tweet.Comments.AsQueryable());
        }
        public PartialViewResult Ava(User user)
        {
            string avatar = user.AvatarLink;
            return PartialView("_ava", avatar);
        }
        public PartialViewResult LoadAva()
        {
            if (!_security.IsAuthenticate())
                return null;
            else
                return PartialView("_loadAva");
        }
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if(file != null && file.ContentLength > 0)
            {
                User user = _security.GetCurrentUser();
                string link = Url.Content("~/Users/user/Image") + user.Name + "_" + file.FileName;
                file.SaveAs(link);
                userRepository.SaveLink(user.Id, link);
            }
            return RedirectToAction("Index", "Me");
        }
        public PartialViewResult Attachment(string link)
        {
            string attachment = MemcachedClient.Get<string>(link);
            if(attachment == null)
            {
                HtmlWeb htmlWeb = new HtmlWeb();
                HtmlDocument htmlDoc = null;
                try
                {
                    htmlDoc = htmlWeb.Load(link);
                }
                catch(Exception e)
                {
                    htmlDoc = null;
                }
                if (htmlDoc == null)
                    return PartialView("_attachment", null);
                HtmlNode root = htmlDoc.DocumentNode;
                HtmlNodeCollection nodes = root.SelectNodes("//meta");
                foreach(HtmlNode node in nodes)
                {
                    string attr = node.GetAttributeValue("property", null);
                    if (attr != null)
                    {
                        if (attr == "og:title") attachment += node.GetAttributeValue("content", null) + "&";
                        if (attr == "og:description") attachment += node.GetAttributeValue("content", null) + "&";
                        if (attr == "og:url") attachment += node.GetAttributeValue("content", null) + "&";
                        if (attr == "og:image") attachment += node.GetAttributeValue("content", null) + "&";
                    }
                }
                MemcachedClient.Store(Enyim.Caching.Memcached.StoreMode.Set, link, attachment, DateTime.Now.AddDays(30));
            }
            return PartialView("_attachment", attachment);
        }
    }
}