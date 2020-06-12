using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Advensco.ALForsanBE.Models;
using System.IO;

namespace Advensco.ALForsanBE.Controllers
{
    public class ImagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Images
        public async Task<ActionResult> Index()
        {
            return View(await db.Images.ToListAsync());
        }

        // GET: Images/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = await db.Images.FindAsync(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // GET: Images/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Images/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Path,Title,Caption")] ImageVM image)
        {
            if (ModelState.IsValid)
            {
                string filePath = string.Empty;
                string fileContentType = string.Empty;

                byte[] uploadedFile = new byte[image.Path.InputStream.Length];
                image.Path.InputStream.Read(uploadedFile, 0, uploadedFile.Length);

                // Initialization.  
                fileContentType = image.Path.ContentType;
                string folderPath = "~/Content/upload_images/";
                this.WriteBytesToFile(this.Server.MapPath(folderPath), uploadedFile, image.Path.FileName);
                filePath = folderPath + image.Path.FileName;



                db.Images.Add(new Image(){Caption = image.Caption, Id = image.Id, Path = filePath , Title = image.Title});
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(image);
        }

        // GET: Images/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = await db.Images.FindAsync(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // POST: Images/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Path,Title,Caption")] Image image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(image).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(image);
        }

        // GET: Images/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Image image = await db.Images.FindAsync(id);
            if (image == null)
            {
                return HttpNotFound();
            }
            return View(image);
        }

        // POST: Images/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Image image = await db.Images.FindAsync(id);
            db.Images.Remove(image);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private void WriteBytesToFile(string rootFolderPath, byte[] fileBytes, string filename)
        {
            try
            {
                // Verification.  
                if (!Directory.Exists(rootFolderPath))
                {
                    // Initialization.  
                    string fullFolderPath = rootFolderPath;

                    // Settings.  
                    string folderPath = new Uri(fullFolderPath).LocalPath;

                    // Create.  
                    Directory.CreateDirectory(folderPath);
                }

                // Initialization.                  
                string fullFilePath = rootFolderPath + filename;

                // Create.  
                FileStream fs = System.IO.File.Create(fullFilePath);

                // Close.  
                fs.Flush();
                fs.Dispose();
                fs.Close();

                // Write Stream.  
                BinaryWriter sw = new BinaryWriter(new FileStream(fullFilePath, FileMode.Create, FileAccess.Write));

                // Write to file.  
                sw.Write(fileBytes);

                // Closing.  
                sw.Flush();
                sw.Dispose();
                sw.Close();
            }
            catch (Exception ex)
            {
                // Info.  
                throw ex;
            }
        }
    }
}
