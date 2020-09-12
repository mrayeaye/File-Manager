using Filer.Data.Services;
using Filer.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace FM.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFile db;

        public HomeController(IFile db)
        {
            this.db = db;
        }
       
        [HttpGet]
        public ActionResult Index()
        {
            //var model = db.getAllDirectories();
            //if (model == null)
            //    return View("Not Found");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(HttpPostedFileBase DeFile) 
        {
            //if (ModelState.IsValid)
            // {
            //     db.addFiles(file);
            //      RedirectToAction("Index");
            // }
            mFile UploadedFile = new mFile();
            if (DeFile == null)
                ViewBag.Message = "No File Selected";
            else if (DeFile.ContentLength > 0)
            {
                string FileName = Path.GetFileName(DeFile.FileName);
                UploadedFile.Name = FileName;
                UploadedFile.Size = DeFile.ContentLength;
                Stream stream = DeFile.InputStream;
                BinaryReader br = new BinaryReader(stream);
                UploadedFile.bytes = br.ReadBytes((int)stream.Length);
                UploadedFile.Dir_Id = -1;
                db.addFiles(UploadedFile);
                string FolderPath = Path.Combine(Server.MapPath("~/Data"), FileName);
                DeFile.SaveAs(FolderPath);
                ViewBag.Message = "File Received";
            }
            
            return View();
        }

        public ActionResult Storage()
        {
            return View();
        }




    }
}