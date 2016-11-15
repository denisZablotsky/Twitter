using System.Web.Mvc;
using Twitter.Data;
using Twitter.Models;

namespace Twitter.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository userRepository;
        private ITweetRepository tweetRepository;
        public UserController(IUserRepository userRep, ITweetRepository tweetRep)
        {
            userRepository = userRep;
            tweetRepository = tweetRep;
        }
        // GET: User
        public ActionResult Index(int id)
        {                
            User user = userRepository.GetUserById(id);
            if (user == null)
                return RedirectToAction("Index", "Home");
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
            return RedirectToAction("Index", "User", new { id = tweet.UserId });
        }
    }
}