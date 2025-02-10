namespace Factory_Management_System_WebAPI.Models
{
    public class OrderModel
    {
        public int? OrderID { get; set; }
        public int? CustomerID { get; set; }
        public string? CustomerName { get; set; }
        public string? Status { get; set; }
        public DateTime? RequestedDate { get; set; }
        public DateTime? CompletedDate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public double? Amount { get; set; }
        public double? CGST { get; set; }
        public double? SGST { get; set; }
        public double? TotalAmount { get; set; }
        public string? RequestStatus { get; set; }
        public int? AdminID { get; set; }

        public int? ItemCount { get; set; }
    }
}
