using HW_18.Services;
using Microsoft.AspNetCore.Mvc;

namespace HW_18.Controllers
{
    public class StoreController : Controller
    {
        private readonly IGetStores _iGetStores;
        public StoreController(IGetStores iGetStores)
        {
            _iGetStores = iGetStores;
        }
        public async Task<IActionResult> Index(string zip, string name)
        {
            var res = await _iGetStores.Execute(zip, name);
            return View(res);
        }
    }
}
