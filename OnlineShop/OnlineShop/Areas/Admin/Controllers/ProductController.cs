

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using OnlineShop.Data;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private ApplicationDbContext _db;
        private IHostingEnvironment _he;
    


        public ProductController(ApplicationDbContext db, IHostingEnvironment he)
        {
            _db = db;
            _he = he;
            
        }
        public IActionResult Index()
        {
            return View(_db.Products.Include(c=>c.productTypes).Include(f=>f.SpecialTag).ToList());
        }

        [HttpPost]
        public IActionResult Index(decimal? lowAmount,decimal? largeAmount) {
            var product = _db.Products.Include(c => c.productTypes).Include(c => c.SpecialTag)
            .Where(c => c.price >= lowAmount && c.price <= largeAmount).ToList();

            if (lowAmount==null || largeAmount==null) {

           product = _db.Products.Include(c => c.productTypes).Include(c => c.SpecialTag).ToList();

            }

            return View(product);
        }
         
        //Using Drop downlist
        public IActionResult Create()
        {

            ViewData["ProductTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Products product, IFormFile image)
        {

            if (ModelState.IsValid)
            {
                var SerchProduct = _db.Products.FirstOrDefault(c=>c.Name==product.Name);

                if (SerchProduct!=null) {

                    ViewBag.message = "This is Alrady exit";
                    ViewData["ProductTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
                    ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");

                    return View(product);
                
                }

                if (image != null)
                {

                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }


                if (image == null)
                {

                    product.Image = "Images/a.JPG";
                }

                _db.Products.Add(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }



        public ActionResult Edit(int? id) {

            ViewData["ProductTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
            ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");

            if (id==null) {

                return NotFound();
            }
            var product = _db.Products.Include(c => c.productTypes).Include(c => c.SpecialTag).FirstOrDefault(c=>c.Id==id);
            if (product==null) {

                return NotFound();
            }
            return View(product);

        }
        [HttpPost]
        public async Task <IActionResult> Edit(Products product, IFormFile image) {

            if (ModelState.IsValid)
            {

                if (image != null)
                {

                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }


                if (image == null)
                {

                    product.Image = "Images/a.JPG";
                }

                _db.Products.Update(product);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }


        public ActionResult Details(int? id) {

            if (id==null) {

                return NotFound();
            }
            var product = _db.Products.Include(c => c.productTypes).Include(c => c.SpecialTag).FirstOrDefault(c=>c.Id==id);
            if (product==null) {

                return NotFound();
            
            }
            return View(product);
        }



        
        public ActionResult Delete(int? id) {


            if (id == null)
            {

                return NotFound();

            }
            var product = _db.Products.Include(c => c.SpecialTag).Include(c => c.productTypes).Where(c => c.Id == id).FirstOrDefault();
            if (product == null)
            {

                return NotFound();

            }

            return View(product);
        }


        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int? id) {

            if (id==null) {

                return NotFound();
            }
            var product = _db.Products.FirstOrDefault(c=>c.Id==id);

            if (product==null) {

                return NotFound();
            }
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }

}

