using System;
using FluentValidation;
using Factory_Management_System_WebAPI.Models;

namespace Factory_Management_System_WebAPI.Validators
{
    public class BillModelValidator : AbstractValidator<BillModel>
    {
        public BillModelValidator()
        {
            //RuleFor(bill => bill.InvoiceNo)
            //    .NotEmpty().WithMessage("Invoice number is required.")
            //    .MaximumLength(50).WithMessage("Invoice number cannot exceed 50 characters.");

            //RuleFor(bill => bill.BillDate)
            //    .NotEmpty().WithMessage("Bill date is required.")
            //    .LessThanOrEqualTo(DateTime.Now).WithMessage("Bill date cannot be in the future.");

            //RuleFor(bill => bill.Status)
            //    .NotEmpty().WithMessage("Status is required.")
            //    .IsInEnum().WithMessage("Invalid status value.");

            RuleFor(bill => bill.OrderID)
                .NotEmpty().WithMessage("Order ID is required.")
                .GreaterThan(0).WithMessage("Order ID must be a positive number.");

            //RuleFor(bill => bill.Amount)
            //    .NotEmpty().WithMessage("Amount is required.")
            //    .GreaterThanOrEqualTo(0).WithMessage("Amount cannot be negative.");

            //RuleFor(bill => bill.CGST)
            //    .GreaterThanOrEqualTo(0).WithMessage("CGST cannot be negative.");

            //RuleFor(bill => bill.SGST)
            //    .GreaterThanOrEqualTo(0).WithMessage("SGST cannot be negative.");

            //RuleFor(bill => bill.TotalAmount)
            //    .NotEmpty().WithMessage("Total amount is required.")
            //    .GreaterThanOrEqualTo(0).WithMessage("Total amount cannot be negative.")
            //    .Must((bill, total) => total == (bill.Amount ?? 0) + (bill.CGST ?? 0) + (bill.SGST ?? 0))
            //    .WithMessage("Total amount must be equal to the sum of Amount, CGST, and SGST.");

            //RuleFor(bill => bill.CompanyName)
            //    .NotEmpty().WithMessage("Company name is required.")
            //    .MaximumLength(100).WithMessage("Company name cannot exceed 100 characters.");

            //RuleFor(bill => bill.CustomerName)
            //    .NotEmpty().WithMessage("Customer name is required.")
            //    .MaximumLength(100).WithMessage("Customer name cannot exceed 100 characters.");

        }
    }
}
