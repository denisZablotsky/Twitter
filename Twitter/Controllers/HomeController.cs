﻿using System.Linq;
using System.Web.Mvc;
using Twitter.Models;
using Twitter.Data;

namespace Twitter.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository userRepository;
        public HomeController(IUserRepository userRep)
        {
            userRepository = userRep;
        }
        // GET: Home
        public ActionResult Index()
        {
            IQueryable<User> Users = userRepository.Users;
            if (Users.Any())
                return View(Users);
            return View("Index", null);
        }
    }
}