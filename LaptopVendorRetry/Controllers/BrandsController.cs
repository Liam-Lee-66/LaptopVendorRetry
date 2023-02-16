using LaptopVendorRetry.Data;
using Microsoft.AspNetCore.Mvc;

namespace LaptopVendorRetry.Controllers
{
    public class BrandsController : Controller
    {
        public IActionResult Index()
        {
            using LaptopVendorContext context = new LaptopVendorContext();
            return View(context.Brands.ToList());
        }
    }
}
