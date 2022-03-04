using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Data;
using OnlineShop.Models;
using OnlineShop.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
      
        private ApplicationDbContext _db;

        public OrderController(ApplicationDbContext db)
        {
            _db = db;

        }

        public IActionResult CheckOut() {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> CheckOut(Orders anOrders)
        {
            List<Products> Products = HttpContext.Session.Get<List<Products>>("Products");

            if (Products!=null) {

                foreach (var product in Products) {
                    OrderDetails orderDetails = new OrderDetails();
                    orderDetails.ProductId = product.Id;
                    anOrders.OrderDetails.Add(orderDetails);
                }
            }

            anOrders.OrderNo = GetOrderNo();
            _db.orders.Add(anOrders);
            await _db.SaveChangesAsync();
            HttpContext.Session.Set("Products",new List<Products>());
            return View();
        }

        public String GetOrderNo() {

            int rowCount = _db.orders.ToList().Count()+ 1;

            return rowCount.ToString("000");
        }


       
       

    }
}
