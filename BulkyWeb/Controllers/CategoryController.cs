using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db; 
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Name cannot be the same as the Display Order");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }           
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            ////works on only the primary key in search if the id parameter can be found in the primary key of DB
            Category? categoryFromDb = _db.Categories.Find(id);

            ////will search the record and return the first instance of the search item..even if not the primary key.
            ////can be used to search for other parameters like name and others that are not primary keys
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);

            ////link operation that also searches anything not just only the primary key
            //Category? categoryFromDb2 = _db.Categories.Where( u => u.Id == id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            ////works on only the primary key in search if the id parameter can be found in the primary key of DB
            Category? categoryFromDb = _db.Categories.Find(id);

            ////will search the record and return the first instance of the search item..even if not the primary key.
            ////can be used to search for other parameters like name and others that are not primary keys
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);

            ////link operation that also searches anything not just only the primary key
            //Category? categoryFromDb2 = _db.Categories.Where( u => u.Id == id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
