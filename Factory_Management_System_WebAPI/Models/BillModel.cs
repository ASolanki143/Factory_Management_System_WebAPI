namespace Factory_Management_System_WebAPI.Models
{
    public class BillModel
    {
        public int? BillID { get; set; }
        public string? InvoiceNo { get; set; }
        public DateTime? BillDate { get; set; }
        public string? Status { get; set; }
        public int? OrderID { get; set; }
        public double? Amount { get; set; }
        public double? CGST {  get; set; }
        public double? SGST { get; set; }
        public double? TotalAmount { get; set; }
        public string? CompanyName { get; set; }
        public string? CustomerName { get; set; }
        public string? CompanyNo {  get; set; }

        public int? AdminID { get; set; }
        public string? AdminName { get; set; }
    }
}
