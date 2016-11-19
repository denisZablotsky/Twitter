using System.Web.Mvc;
using Twitter.Data;
using Twitter.Models;
using Twitter.Services;
using System.Linq;

namespace Twitter.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository userRepository;
        private ITweetRepository tweetRepository;
        private ISecurityService _security;
        public UserController(IUserRepository userRep, ITweetRepository tweetRep)
        {
            userRepository = userRep;
            tweetRepository = tweetRep;
            _security = new SecurityService();
        }
        // GET: User
        public ActionResult Index(int id)
        {                
            User user = userRepository.GetUserById(id);
            if (user == null)
                return RedirectToAction("Index", "Home");
            user.Tweets = user.Tweets.OrderByDescending(x => x.CreatingDate).ToList();
            return View(user);
        }
        public PartialViewResult CreateTweet(int id)
        {
            Tweet tweet = new Tweet();
            tweet.UserId = id;
            return PartialView(tweet);
        }
        [HttpPost]
        public ActionResult CreateTweet(Tweet tweet)
        {
            tweet.UserId = tweet.Id;
            tweetRepository.CreateTweet(tweet);
            return RedirectToAction("Index", "Me");
        }
        public PartialViewResult FollowSection(int id)
        {
            if (!_security.IsAuthenticate())
                return PartialView("_reg");
            User currentUser = _security.GetCurrentUser();
            User user = userRepository.GetUserById(id);
            bool flag = false;
            foreach(User u in currentUser.Followings)
            {
                if (u.Id == user.Id) flag = true;
            }
            if (flag == true)
                return PartialView("_unfollow", user.Id);
            return PartialView("_follow", user.Id);
        }
        [HttpGet]
        public ActionResult Follow(int id)
        {
            userRepository.Follow(_security.GetCurrentUser().Id, id);
            return RedirectToAction("Index", "User", new { id = id });
        }
        [HttpGet]
        public ActionResult Unfollow(int id)
        {
            userRepository.Unfollow(_security.GetCurrentUser().Id, id);
            return RedirectToAction("Index", "User", new { id = id });
        }
    }
}