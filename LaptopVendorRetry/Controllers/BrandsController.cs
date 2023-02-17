using LaptopVendorRetry.Data;
using LaptopVendorRetry.Models.ViewModels;
using LaptopVendorRetry.Models;
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

        public IActionResult Add()
        {
            AddBrandViewModel vm = new AddBrandViewModel();

            return View(vm);
        }
        [HttpPost]
        public IActionResult Add(AddBrandViewModel vm)
        {
            if (CheckIfFormDone(vm))
            {
                // valid form
                using LaptopVendorContext context = new LaptopVendorContext();
                Brand newBrand = new Brand()
                {
                    Name = vm.Name,
                };

                context.Brands.Add(newBrand);
                context.SaveChanges();

                return View("Result", 0);
            }
            else
            {
                // invalid form
                return View("Result", 1);
            }
        }

        public IActionResult Remove()
        {
            using LaptopVendorContext context = new LaptopVendorContext();
            RemoveBrandViewModel vm = new();

            vm.AllBrands = context.Brands.ToList();

            return View(vm);
        }
        [HttpPost]
        public IActionResult Remove(RemoveBrandViewModel vm)
        {
            if (CheckIfFormDone(vm))
            {
                using LaptopVendorContext context = new LaptopVendorContext();

                context.Brands.Remove(FindBrand(vm.BrandId));
                context.SaveChanges();

                return View("Result", 2);
            }
            else
            {
                return View("Result", 3);
            }
        }

        public IActionResult Result(int scenario)       // 0 = added brand successfuly, 1 = added brand failed, 2 = removing brand successfully, 3 = removing brand failed 
        {
            return View(scenario);
        }


        // Private functions
        private Brand FindBrand(int brandId)
        {
            using LaptopVendorContext context = new LaptopVendorContext();

            Brand found = context.Brands.FirstOrDefault(x => x.Id == brandId);

            return found;
        }
        private bool CheckIfFormDone(AddBrandViewModel vm)
        {
            if (string.IsNullOrEmpty(vm.Name) || string.IsNullOrWhiteSpace(vm.Name))
            {
                return false;
            }
            return true;
        }
        private bool CheckIfFormDone(RemoveBrandViewModel vm)
        {
            if (FindBrand(vm.BrandId) == null)
            {
                return false;
            }
            return true;
        }
    }
}
