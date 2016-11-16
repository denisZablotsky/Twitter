using System.Linq;
using System.Web.Mvc;
using Twitter.Models;
using Twitter.Data;
using Twitter.Services;

namespace Twitter.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository userRepository;
        private ISecurityService _security;

        public HomeController(IUserRepository userRep)
        {
            userRepository = userRep;
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
    }
}