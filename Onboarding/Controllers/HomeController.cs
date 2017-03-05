using Onboarding.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Onboarding.Controllers
{
    public class HomeController : Controller
    {
        FruitEntities db = new FruitEntities();
        public ActionResult Index()
        {
            List<Fruit> fruitList = new List<Fruit>();
            var data = db.Products.Select(p => p).ToList();
            foreach(var d in data)
            {
                Fruit eachFruit = new Fruit();
                eachFruit.Id = d.id;
                eachFruit.Name = d.name;
                eachFruit.Description = d.description;
                eachFruit.Price = (double)d.price;
                eachFruit.Image = d.image;
                fruitList.Add(eachFruit);
            }
            return View(fruitList);
        }

        public ActionResult ListFruit()
        {
            var data = db.Products.Select(p => p).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            //get fruit from table by a given id
            var fruitDetail = db.Products.FirstOrDefault(p => p.id == id);
            Fruit fruit = new Fruit { Id = fruitDetail.id };
            return View(fruit);
            //Fruit fruit = new Fruit
            //{
            //    Id = fruitDetail.id,
            //    Name = fruitDetail.name,
            //    Description = fruitDetail.description,
            //    Price = (decimal)fruitDetail.price,
            //    Image = fruitDetail.image
            //};
        }

        public ActionResult FindFruit(int id)
        {
            var fruitDetail = db.Products.FirstOrDefault(p => p.id == id);
            return Json(fruitDetail, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create()
        {
            return View();
        }

        //Knockout pass JSON here, save data in database
        [HttpPost]
        public ActionResult AddFruit(Product json)
        {
            Product newFruit = new Product {
                name = json.name,
                description = json.description,
                image = json.image,
                price = json.price
            };
            db.Products.Add(newFruit);
            db.SaveChanges();
            return Json(new { success = true });
        }
    }
}