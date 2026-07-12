using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DBcontext _context;

        public CategoryController(DBcontext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public IActionResult CreatePost(Category category)
        {
            if (!String.IsNullOrEmpty(category.Name) && _context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower()))
            {
                ModelState.AddModelError("", "Category name already exists.");
            }
            if (!ModelState.IsValid) return View();

            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
