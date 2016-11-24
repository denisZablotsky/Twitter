using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twitter.Models;
using Twitter.Data;

namespace Twitter.Controllers
{
    public class HashtagController : Controller
    {
        private IHashtagRepository hashRepository;
        public HashtagController(IHashtagRepository hashRep)
        {
            hashRepository = hashRep;
        }
        // GET: Hashtag
        public ActionResult Index(string id)
        {
            if(id != null)
            {
                return View(hashRepository.GetHashtagByTag(id));
            }
            return View("Index", null);
        }
    }
}