using HW_18.Models;
using HW_18.Services;
using Microsoft.AspNetCore.Mvc;

namespace HW_18.Controllers
{
    public class StoreController : Controller
    {
        private readonly IGetStores _iGetStores;
        private readonly IGetStoreProducts _iGetProducts;
        private readonly IGetProduct _getProduct;
        private readonly IUpdateProduct _updateProduct;
        public StoreController(IGetStores iGetStores, IGetStoreProducts iGetProducts, IGetProduct getProduct, IUpdateProduct updateProduct)
        {
            _iGetStores = iGetStores;
            _iGetProducts = iGetProducts;
            _getProduct = getProduct;
            _updateProduct = updateProduct;
        }
        public async Task<IActionResult> Index(string zip, string name)
        {
            var res = await _iGetStores.Execute(zip, name);
            return View(res);
        }
        public async Task<IActionResult> Products(int id, bool isAsc = true)
        {
            var res = await _iGetProducts.Execute(id, isAsc);
            return View(res);
        }
        public async Task<IActionResult> EditProduct(int id)
        {
            var res = await _getProduct.Execute(id);
            return View(res.FirstOrDefault());
        }
        [HttpPost]
        public async Task<IActionResult> EditProduct([FromForm] Product product)
        {
            var res = await _updateProduct.Execute(product);
            return RedirectToAction("Products", new {id = 1});
        }

    }
}
