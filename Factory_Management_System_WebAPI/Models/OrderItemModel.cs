namespace Factory_Management_System_WebAPI.Models
{
    public class OrderItemModel
    {
        public int? OrderItemID { get; set; }
        public int? OrderID { get; set; }
        public int? ProductID { get; set; }
        public int? Quantity { get; set; }
        public double? Amount { get; set; }
        public string? ProductName { get; set; }
        public double? ProductPrice { get; set; }
        public string? Material { get; set; }
        public int? AdminID { get; set; }
    }
}
