using ECommerce.Controllers.Base;
using ECommerce.Core.Model;
using ECommerce.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class OrderController : AndControllerBase
    {
        // GET: Order
        [Route("SiparisVer")]
        public ActionResult AddressList()
        {
            var db = new AndDB();
            var data = db.UserAddresses.Where(x => x.UserID == LoginUserID).ToList();

            return View(data);
        }
         public ActionResult CreateUserAddress()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUserAddress(UserAddress entity)
        {
            entity.CreateDate = DateTime.Now;
            entity.CreateUserID = LoginUserID;
            entity.IsActive = true;
            entity.UserID = LoginUserID;
            var db = new AndDB();
            db.UserAddresses.Add(entity);
            db.SaveChanges();
            return RedirectToAction("AddressList");
        }
        
        public ActionResult CreateOrder(int id)
        {
            var db = new AndDB();
            var sepet = db.Baskets.Include("Products").Where(x => x.UserID == LoginUserID).ToList();

            Order order = new Order();
            order.CreateDate = DateTime.Now;
            order.CreateUserID = LoginUserID;
            order.StatusID = 1;
            order.TotalProductPrice = sepet.Sum(x => x.Products.Price);
            order.TotalTaxPrice = sepet.Sum(x => x.Products.Tax);
            order.TotalDiscountPrice = sepet.Sum(x => x.Products.Discount);
            order.TotalPrice = order.TotalProductPrice + order.TotalTaxPrice;
            order.UserAddressID = id;
            order.UserID = LoginUserID;
            order.OrderProducts = new List<OrderProduct>();

            foreach (var item in sepet)
            {
                order.OrderProducts.Add(new OrderProduct
                {
                    CreateDate = DateTime.Now,
                    CreateUserID = LoginUserID,
                    ProductID=item.ProductID,
                    Quantity=item.Quantity
                });
                db.Baskets.Remove(item);
            }
            db.Orders.Add(order);

            db.SaveChanges();
            var orderr = db.Orders.Where(x => x.UserID == LoginUserID).OrderByDescending(x=>x.CreateDate).FirstOrDefault();
            return RedirectToAction("Detail",new { id=orderr.ID});
        
        }
        public ActionResult Detail(int id) {
            var db=new AndDB();
            var data = db.Orders.Include("OrderProducts")
                .Include("OrderProducts.Product")
                .Include("OrderPayments")
                .Include("Status")
                .Include("UserAddress")
                .Where(x => x.ID == id).FirstOrDefault();
            return View(data);
        }

        [Route("Siparişlerim")]
        public ActionResult Index()
        {
            var db = new AndDB();
            var data = db.Orders.Include("Status").Where(x => x.UserID == LoginUserID).ToList();
            return View(data);
        }


        public ActionResult Pay(int id) {
            var db = new AndDB();
            var order = db.Orders.Where(x => x.ID == id).FirstOrDefault();
            order.StatusID = 6;
            db.SaveChanges();
            return RedirectToAction("Detail",new {id=order.ID });
        }
    }
}