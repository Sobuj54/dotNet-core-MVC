using BulkyBook.Business.Services.IServices;
using BulkyBook.DataAccess;
using BulkyBook.Models;
using BulkyBook.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> Upsert()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ProductVM productVM = new()
            {
                Product = new Product(),
                CategoryList = categories.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };

            return View(productVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Upsert")]
        public async Task<IActionResult> UpsertPost(Product product, IFormFile? file)
        {
            if (!ModelState.IsValid) return View();

            //await _productService.CreateProductAsync(product);
            TempData["success"] = "Product created successfully.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync(id.Value);
            if (product == null) {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Update")]
        public async Task<IActionResult> UpdatePost(Product product)
        {
            if (!ModelState.IsValid) return View();

            await _productService.UpdateProductAsync(product);
            TempData["success"] = "Product updated successfully.";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var product = await _productService.GetProductByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _productService.DeleteProductAsync(id);
            TempData["success"] = "Product deleted successfully.";
            return RedirectToAction("Index");
        }

        #region API CALLS
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProductsAsync(true);
            return Json(new { data = products });
        }
        #endregion
    }
}
