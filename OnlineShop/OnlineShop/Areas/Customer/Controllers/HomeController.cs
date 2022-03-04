﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineShop.Data;
using OnlineShop.Models;
using OnlineShop.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace OnlineShop.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
       

        private ApplicationDbContext _db;


        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index(int? page)
        {
            return View(_db.Products.Include(c=>c.productTypes).Include(c=>c.SpecialTag).ToList().ToPagedList(page??1,6));
         
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public ActionResult Details(int? id)
        {

            if (id == null)
            {

                return NotFound();
            }
            var product = _db.Products.Include(c => c.productTypes).Include(c => c.SpecialTag).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {

                return NotFound();

            }
            return View(product);
        }

        [HttpPost]
        [ActionName("Details")]
        public ActionResult ProductDetails(int? id)
        {
            List <Products> Products = new List<Products>();

            if (id == null)
            {

                return NotFound();
            }
            var product = _db.Products.Include(c => c.productTypes).Include(c => c.SpecialTag).FirstOrDefault(c => c.Id == id);
            if (product == null)
            {

                return NotFound();

            }
            Products = HttpContext.Session.Get<List<Products>>("Products");
            if (Products==null) {

                Products = new List<Products>();


            }
            Products.Add(product);
            HttpContext.Session.Set("Products", Products);
            return View(product);
        }

    [ActionName("Remove")]
        public IActionResult RemoveToCart(int? id) {

          List<Products>Products = HttpContext.Session.Get<List<Products>>("Products");

            if (Products!=null) {

                var product = Products.FirstOrDefault(c=>c.Id==id);

                if (product!=null) {

                    Products.Remove(product);
                    HttpContext.Session.Set("Products", Products);
                }
            }

            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        public IActionResult Remove(int? id)
        {

            List<Products> Products = HttpContext.Session.Get<List<Products>>("Products");

            if (Products != null)
            {

                var product = Products.FirstOrDefault(c => c.Id == id);

                if (product != null)
                {

                    Products.Remove(product);
                    HttpContext.Session.Set("Products", Products);
                }
            }

            return RedirectToAction(nameof(Index));

        }


        public IActionResult Cart() {
      List<Products> Products = HttpContext.Session.Get<List<Products>>("Products");

            if (Products==null) {

                Products = new List<Products>();
            }
            return View(Products);
        }
    }
}
