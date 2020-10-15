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
        
        public ActionResult Landing()
        {
            //if (ModelState.IsValid)
            // {
            //     db.addFiles(file);
            //      RedirectToAction("Index");
            // }
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase DeFile = files[i];
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
                            //DeFile.SaveAs(FolderPath);
                            ViewBag.Message = "File Received";
                        }
                        return Json("File Uploaded Successfully!");
                    }
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
            return View();
        }

        public ActionResult Storage()
        {
            var model = db.getAllFiles();          
                return View(model);          
        }

        [HttpGet]
        public ActionResult Landing(FormCollection form)
        {
            
            return View();
        }




    }
}