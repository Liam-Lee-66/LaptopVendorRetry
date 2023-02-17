using Microsoft.AspNetCore.Mvc;
using LaptopVendorRetry.Data;
using LaptopVendorRetry.Models.ViewModels;
using LaptopVendorRetry.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LaptopVendorRetry.Controllers
{
    public class LaptopsController : Controller
    {
        public IActionResult Index()
        {
            using LaptopVendorContext context = new LaptopVendorContext();
            return View(context.Laptops.ToList());
        }

        public IActionResult Add()
        {
            using LaptopVendorContext context = new LaptopVendorContext();
            AddLaptopViewModel vm = new AddLaptopViewModel();

            vm.AllBrands = context.Brands.ToList();

            return View(vm);
        }
        [HttpPost]
        public IActionResult Add(AddLaptopViewModel vm)
        {
            if (CheckIfFormDone(vm))
            {
                // valid form
                using LaptopVendorContext context = new LaptopVendorContext();
                Laptop newlaptop = new Laptop()
                {
                    Model = vm.Model,
                    BrandId = vm.BrandId,
                    Price = vm.Price,
                    Year= vm.Year
                };

                context.Laptops.Add(newlaptop);
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
            RemoveLaptopViewModel vm = new();

            vm.AllLaptops = context.Laptops.ToList();

            return View(vm);
        }
        [HttpPost]
        public IActionResult Remove(RemoveLaptopViewModel vm)
        {
            if (CheckIfFormDone(vm))
            {
                using LaptopVendorContext context = new LaptopVendorContext();

                context.Laptops.Remove(FindLaptop(vm.SelectedLaptopId));
                context.SaveChanges();

                return View("Result", 2);
            }
            else
            {
                return View("Result", 3);
            }
        }

        public IActionResult Result(int scenario)       // 0 = added laptop successfuly, 1 = added laptop failed, 2 = removing laptop successfully, 3 = removing laptop failed 
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
        private Laptop FindLaptop(int laptopId)
        {
            using LaptopVendorContext context = new LaptopVendorContext();

            Laptop found = context.Laptops.FirstOrDefault(x => x.Id == laptopId);

            return found;
        }
        private bool CheckIfFormDone(AddLaptopViewModel vm)
        {
            if (string.IsNullOrEmpty(vm.Model) || string.IsNullOrWhiteSpace(vm.Model) || FindBrand(vm.BrandId) == null || vm.Price < 0)
            {
                return false;
            }
            return true;
        }
        private bool CheckIfFormDone(RemoveLaptopViewModel vm)
        {
            if (FindLaptop(vm.SelectedLaptopId) == null)
            {
                return false;
            }
            return true;
        }
    }
}
