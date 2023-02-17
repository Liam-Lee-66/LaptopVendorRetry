namespace LaptopVendorRetry.Models.ViewModels
{
    public class AddLaptopViewModel
    {
        public string Model { get; set; }
        public int BrandId { get; set; }
        public double Price { get; set; }
        public int Year { get; set; }   

        public List<Brand> AllBrands { get; set; } = new();
    }
}
