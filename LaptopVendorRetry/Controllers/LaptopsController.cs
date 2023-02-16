using Microsoft.AspNetCore.Mvc;
using LaptopVendorRetry.Data;

namespace LaptopVendorRetry.Controllers
{
    public class LaptopsController : Controller
    {
        public IActionResult Index()
        {
            using LaptopVendorContext context = new LaptopVendorContext();
            return View(context.Laptops.ToList());
        }
    }
}
