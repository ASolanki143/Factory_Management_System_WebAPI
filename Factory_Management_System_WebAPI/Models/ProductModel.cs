namespace Factory_Management_System_WebAPI.Models
{
    public class ProductModel
    {
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? Material { get; set; }
        public double? Price { get; set; }
        public bool? IsActive { get; set; }
        public int? AdminID { get; set; }
    }
}
