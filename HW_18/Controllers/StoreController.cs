using HW_18.Services;
using Microsoft.AspNetCore.Mvc;

namespace HW_18.Controllers
{
    public class StoreController : Controller
    {
        private readonly IGetStores _iGetStores;
        private readonly IGetProducts _iGetProducts;
        public StoreController(IGetStores iGetStores, IGetProducts iGetProducts)
        {
            _iGetStores = iGetStores;
            _iGetProducts = iGetProducts;
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
    }
}
