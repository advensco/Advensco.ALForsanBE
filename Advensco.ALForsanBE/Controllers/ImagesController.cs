using Advensco.ALForsanBE.Models;
using Advensco.ALForsanBE.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Advensco.ALForsanBE.Controllers
{
    public class ImagesController : Controller
    {

        private Entities db = new Entities();
        // GET: Images
        public ActionResult Index()
        {
            var vm = db.Images.ToList();
            return View(vm);
        }



        // GET: Images/Create
        public ActionResult Create()
        {
            var vm = new ImageVM()
            {
                Galleries = db.Galleries.ToList()
            };
            return View(vm);
        }

        [HttpPost]
        public ActionResult Create(ImageVM image, HttpPostedFileBase Path)
        {
            if (ModelState.IsValid)
            {
                var img = new Image()
                {
                    Title = image.Title,
                    Caption = image.Caption,
                    GalleryId = image.GalleryId,
                };

                if (Path != null && Path.ContentLength > 0)
                {
                    try
                    {
                        var ext = Path.FileName.Substring(Path.FileName.IndexOf('.') + 1);
                        if (ext == "jpg" || ext == "png" || ext == "jpeg" || ext == "JPG" || ext == "PNG" || ext == "JPEG")
                        {
                            string folderPath = "~/Content/upload_images/";
                            Guid imageGUID = Guid.NewGuid();

                            string path = Server.MapPath(folderPath) + imageGUID + "." + ext;
                            Path.SaveAs(path);
                            img.Path = folderPath.Substring(1) + imageGUID + "." + ext;
                            ViewBag.Message = "Image uploaded successfully";
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR uploading Image";
                    }
                }
                db.Images.Add(img);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(image);
        }

        // GET: Images/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = db.Images.Where(g => g.Id == id).FirstOrDefault();

            if (image == null)
            {
                return HttpNotFound();
            }

            var vm = new ImageVM()
            {
                Id = image.Id,
                Caption = image.Caption,
                GalleryId = image.GalleryId.HasValue ? image.GalleryId.Value : 0,
                Path = image.Path,
                Title = image.Title,
                Galleries = db.Galleries.ToList()
            };




            return View(vm);
        }

        [HttpPost]
        public ActionResult Edit(ImageVM image, HttpPostedFileBase Path)
        {
            var img = new Image()
            {
                Id = image.Id,
                Caption = image.Caption,
                GalleryId = image.GalleryId,
                Title = image.Title,
            };

            if (Path != null && Path.ContentLength > 0)
            {
                try
                {
                    var ext = Path.FileName.Substring(Path.FileName.IndexOf('.') + 1);
                    if (ext == "jpg" || ext == "png" || ext == "jpeg" || ext == "JPG" || ext == "PNG" || ext == "JPEG")
                    {
                        string folderPath = "~/Content/upload_images/";
                        Guid imageGUID = Guid.NewGuid();

                        string path = Server.MapPath(folderPath) + imageGUID + "." + ext;
                        Path.SaveAs(path);
                        img.Path = folderPath.Substring(1) + imageGUID + "." + ext;
                        ViewBag.Message = "Image uploaded successfully";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR uploading Image";
                }
            }
            if (ModelState.IsValid)
            {
                db.Entry(img).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(image);
        }

        // GET: Images/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image img = db.Images.Where(g => g.Id == id).FirstOrDefault();
            if (img == null)
            {
                return HttpNotFound();
            }
            db.Images.Remove(img);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
