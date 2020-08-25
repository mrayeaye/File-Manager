using Filer.Data.Services;
using Filer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FM.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFile db;

        public HomeController(IFile db)
        {
            this.db = db;
        }
       
        public ActionResult Index()
        {
            var model = db.getAllFiles();
            if (model == null)
                return View("Not Found");
            return View(model);
        }

    }
}