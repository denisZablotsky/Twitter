using System.Linq;
using System.Web.Mvc;
using Twitter.Models;
using Twitter.Data;
using Twitter.Services;
using System;
using System.Collections.Generic;

namespace Twitter.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository userRepository;
        private ISecurityService _security;
        private ICommentRepository commentRep;
        private ITweetRepository tweetRep;

        public HomeController(IUserRepository userRep, ICommentRepository commRep, ITweetRepository tweerR)
        {
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
    }
}