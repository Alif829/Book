using BookWeb.Data;
using BookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.Controllers
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
            IEnumerable<Category> objCategoryList = _db.Categories;
            return View(objCategoryList);
        }

        //Get
        public IActionResult Create()
        {
            
            return View();
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category Obj)
        {
            if(Obj.Name==Obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Display Order can't match Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(Obj);
                _db.SaveChanges();
                TempData["success"] = "Category Successfully Added";
                return RedirectToAction("Index");
            }
            return View(Obj);
        }

        //Get
        public IActionResult Edit(int? id)
        {
            if(id==null || id==0)
            {
                return  NotFound();
            }
            var categoryFromDb= _db.Categories.Find(id);//finds id
           // var categoryFromDbSingle=_db.Categories.SingleOrDefault(u=>u.Id==id);//if id is not single throws an exception
           // var categoryFromDbFirst=_db.Categories.FirstOrDefault(u=>u.Id==id);//u creates a generic id which goes to u.id

            if(categoryFromDb==null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category Obj)
        {
            if (Obj.Name == Obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "Display Order can't match Name");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Update(Obj);
                _db.SaveChanges();
                TempData["success"] = "Category Successfully Updated";
                return RedirectToAction("Index");
            }
            return View(Obj);
        }

        //Get
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Categories.Find(id);//finds id
                                                         // var categoryFromDbSingle=_db.Categories.SingleOrDefault(u=>u.Id==id);//if id is not single throws an exception
                                                         // var categoryFromDbFirst=_db.Categories.FirstOrDefault(u=>u.Id==id);//u creates a generic id which goes to u.id

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        //Post
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category Obj)
        {
            //var categoryObj=_db.Categories.Find(Obj.Id);
            if (Obj==null)
            {
                return NotFound();
            }
            _db.Categories.Remove(Obj);
            _db.SaveChanges();
            TempData["success"] = "Category Successfully Deleted";
            return RedirectToAction("Index");
        }

    }
}
