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
            var data = db.Product.Select(p => p).ToList();
            foreach (var d in data)
            {
                Fruit eachFruit = new Fruit();
                eachFruit.Id = d.Id;
                eachFruit.Name = d.Name;
                eachFruit.Description = d.Description;
                eachFruit.Price = (decimal)d.Price;
                eachFruit.Image = d.Image;
                fruitList.Add(eachFruit);
            }
            return View(fruitList);
        }

        public ActionResult ListFruit()
        {
            var data = db.Product.Select(p => p).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            //get fruit from table by a given id
            var fruitDetail = db.Product.FirstOrDefault(p => p.Id == id);
            if (fruitDetail == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Fruit fruit = new Fruit { Id = fruitDetail.Id };
            return View(fruit);
        }

        public ActionResult FindFruit(int id)
        {
            var fruitDetail = db.Product.FirstOrDefault(p => p.Id == id);
            if (fruitDetail == null)
            {
                return RedirectToAction("Index", "Home");
            }
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
            Product newFruit = new Product
            {
                Name = json.Name,
                Description = json.Description,
                Image = json.Image,
                Price = json.Price
            };
            db.Product.Add(newFruit);
            db.SaveChanges();
            db.Dispose();
            return Json(new { success = true });
        }

        //edit fruit GET data from database,transfer to ViewModel
        public ActionResult Edit(int id)
        {
            var editFruit = db.Product.FirstOrDefault(p => p.Id == id);
            if (editFruit == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Fruit fruit = new Fruit { Id = editFruit.Id };
            return View(fruit);
        }

        [HttpPost]
        public ActionResult Edit(Product json)
        {
            var editFruit = db.Product.FirstOrDefault(p => p.Id == json.Id);
            if (editFruit == null)
            {
                return RedirectToAction("Index", "Home");
            } 
            editFruit.Name = json.Name;
            editFruit.Description = json.Description;
            editFruit.Image = json.Image;
            editFruit.Price = json.Price;
            db.SaveChanges();
            db.Dispose();
            return Json(new { success = true });
        }
         
        public ActionResult Delete(int id)
        {
            var deleteFruit = db.Product.FirstOrDefault(p => p.Id == id);
            if (deleteFruit == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Fruit fruit = new Fruit { Id = deleteFruit.Id};
            return View(fruit);
        }

        [HttpPost]
        public ActionResult Delete(Product json)
        {
            var deleteFruit = db.Product.FirstOrDefault(p => p.Id == json.Id);
            if (deleteFruit == null)
            {
                return RedirectToAction("Index", "Home");
            }
            db.Product.Remove(deleteFruit);
            db.SaveChanges();
            db.Dispose();
            return Json(new { success = true });
        }
    }
}