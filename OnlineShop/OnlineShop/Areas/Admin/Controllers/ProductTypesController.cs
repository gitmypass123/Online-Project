using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ProductTypesController : Controller
    {
        private ApplicationDbContext _db;
        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
           
            return View(_db.ProductTypes.ToList());
        }



        public ActionResult Create() {


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypes productTypes) {

            if (ModelState.IsValid) {

                _db.ProductTypes.Add(productTypes);
                await _db.SaveChangesAsync();
                TempData["save"] = "Product type save hase sucess full";
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }









        public ActionResult Edit(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }

            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {

                return NotFound();
            }

            return View(productType);
        }


        public ActionResult E(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }

            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {

                return NotFound();
            }

            return View(productType);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductTypes productTypes)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    _db.Update(productTypes);
                    await _db.SaveChangesAsync();
                    TempData["Update"] = "Product type Update hase been sucess full";
                    return RedirectToAction(nameof(Index));
                }

            }

            catch (Exception e)
            {

                Console.WriteLine(e);
            }

            finally
            {
                Console.WriteLine("No matter");
            }

            return View(productTypes);

        }





        public ActionResult Details(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }

            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {

                return NotFound();
            }

            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public  IActionResult Details(ProductTypes productTypes)
        {


            return RedirectToAction(nameof(Index));

        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }

            var productType = _db.ProductTypes.Find(id);
            if (productType == null)
            {

                return NotFound();
            }

            return View(productType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id, ProductTypes productTypes)
        {

            if (id==null) {

                return NotFound();
            }


            if (id!=productTypes.Id )
            {

                return NotFound();
            }

            var productType = _db.ProductTypes.Find(id);

            if (productType==null) {

                return NotFound();
            }

            if (ModelState.IsValid)
                {

                    _db.Remove(productType);
                    await _db.SaveChangesAsync();
                TempData["Delete"] = "Product type Delete hase sucess full";
                return RedirectToAction(nameof(Index));
                }

            


            return View(productTypes);

        }

    }
}
