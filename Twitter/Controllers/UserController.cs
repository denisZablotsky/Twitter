using System.Web.Mvc;
using Twitter.Data;
using Twitter.Models;
using Twitter.Services;
using System.Linq;
using System.Collections.ObjectModel;

namespace Twitter.Controllers
{
    public class UserController : Controller
    {
        private IUserRepository userRepository;
        private ITweetRepository tweetRepository;
        private IHashtagRepository hashRepository;
        private ISecurityService _security;
        public UserController(IUserRepository userRep, ITweetRepository tweetRep, IHashtagRepository hashRep)
        {
            userRepository = userRep;
            tweetRepository = tweetRep;
            _security = new SecurityService();
            hashRepository = hashRep;
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
            tweet.Likes = 0;
            Collection<string> hashtags = FindHastags(tweet);
            Tweet Tweet = tweetRepository.CreateTweet(tweet);
            foreach(string hashtag in hashtags)
            {
                Hashtag hash = new Hashtag();
                hash.Tag = hashtag;
                Hashtag HashFromDb = hashRepository.GetHashtagByTag(hashtag);
                if (HashFromDb != null)
                    hash = HashFromDb;
                tweetRepository.AddHashtag(Tweet.Id, hash);
            }
            return RedirectToAction("Index", "Me");
        }
        public ActionResult Like(int id)
        {
            Tweet tweet = tweetRepository.GetTweetById(id);
            tweetRepository.Like(tweet.Id);
            return RedirectToAction("Index", "User", new { id = tweet.UserId });
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

        private Collection<string> FindHastags(Tweet tweet)
        {
            Collection<string> hashtags = new Collection<string>();
            string[] words = tweet.Text.Split(' ');
            foreach(string word in words)
            {
                if (word.StartsWith("#"))
                    hashtags.Add(word);
            }
            return hashtags;
        }
    }
}