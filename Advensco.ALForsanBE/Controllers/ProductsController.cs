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
    public class ProductsController : Controller
    {
        private Entities db = new Entities();
        // GET: Products
        public ActionResult Index()
        {
            var vm = db.Products.ToList();
            return View(vm);
        }

        public ActionResult View(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryVM gallery = db.Galleries.Where(g => g.Id == id).Select(g => new GalleryVM()
            {
                Id = g.Id,
                Title = g.Title,
                Images = g.Images.ToList()
            }).FirstOrDefault();

            if (gallery == null)
            {
                return HttpNotFound();
            }
            return View(gallery);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    try
                    {
                        var ext = Image.FileName.Substring(Image.FileName.IndexOf('.') + 1);
                        if (ext == "jpg" || ext == "png" || ext == "jpeg" || ext == "JPG" || ext == "PNG" || ext == "JPEG")
                        {
                            string folderPath = "~/Content/upload_images/";
                            Guid imageGUID = Guid.NewGuid();

                            string path = Server.MapPath(folderPath)+ imageGUID + "." + ext;
                            Image.SaveAs(path);
                            product.ImagePath = folderPath.Substring(1) + imageGUID + "." + ext;
                            ViewBag.Message = "Image uploaded successfully";
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR uploading Image";
                    }
                }
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Galleries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Where(g => g.Id == id).FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null && Image.ContentLength > 0)
                {
                    try
                    {
                        var ext = Image.FileName.Substring(Image.FileName.IndexOf('.') + 1);
                        if (ext == "jpg" || ext == "png" || ext == "jpeg" || ext == "JPG" || ext == "PNG" || ext == "JPEG")
                        {
                            string folderPath = "~/Content/upload_images/";
                            Guid imageGUID = Guid.NewGuid();

                            string path = Server.MapPath(folderPath) + imageGUID + "." + ext;
                            Image.SaveAs(path);
                            product.ImagePath = folderPath.Substring(1) + imageGUID + "." + ext;
                            ViewBag.Message = "Image uploaded successfully";
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR uploading Image";
                    }
                }

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Where(g => g.Id == id).FirstOrDefault();
            if (product == null)
            {
                return HttpNotFound();
            }
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}