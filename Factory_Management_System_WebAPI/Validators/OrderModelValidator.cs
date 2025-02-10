using System;
using FluentValidation;
using Factory_Management_System_WebAPI.Models;

namespace Factory_Management_System_WebAPI.Validators
{
    public class OrderModelValidator : AbstractValidator<OrderModel>
    {
        public OrderModelValidator()
        {
            RuleFor(order => order.OrderID)
                .NotEmpty().WithMessage("Order ID is required.")
                .GreaterThan(0).WithMessage("Order ID must be a positive number.");

            RuleFor(order => order.CustomerID)
                .NotEmpty().WithMessage("Customer ID is required.")
                .GreaterThan(0).WithMessage("Customer ID must be a positive number.");

            //RuleFor(order => order.CustomerName)
            //    .NotEmpty().WithMessage("Customer name is required.")
            //    .MaximumLength(100).WithMessage("Customer name cannot exceed 100 characters.");

            //RuleFor(order => order.Status)
            //    .NotEmpty().WithMessage("Status is required.")
            //    .IsInEnum().WithMessage("Invalid status value.");

            //RuleFor(order => order.RequestedDate)
            //    .NotEmpty().WithMessage("Requested date is required.")
            //    .LessThanOrEqualTo(DateTime.Now).WithMessage("Requested date cannot be in the future.");

            //RuleFor(order => order.CompletedDate)
            //    .GreaterThanOrEqualTo(order => order.RequestedDate ?? DateTime.MinValue)
            //    .When(order => order.CompletedDate.HasValue && order.RequestedDate.HasValue)
            //    .WithMessage("Completed date cannot be earlier than the requested date.");

            //RuleFor(order => order.ApprovedDate)
            //    .GreaterThanOrEqualTo(order => order.RequestedDate ?? DateTime.MinValue)
            //    .When(order => order.ApprovedDate.HasValue && order.RequestedDate.HasValue)
            //    .WithMessage("Approved date cannot be earlier than the requested date.");

            //RuleFor(order => order.Amount)
            //    .NotEmpty().WithMessage("Amount is required.")
            //    .GreaterThanOrEqualTo(0).WithMessage("Amount cannot be negative.");

            //RuleFor(order => order.CGST)
            //    .GreaterThanOrEqualTo(0).WithMessage("CGST cannot be negative.");

            //RuleFor(order => order.SGST)
            //    .GreaterThanOrEqualTo(0).WithMessage("SGST cannot be negative.");

            //RuleFor(order => order.TotalAmount)
            //    .NotEmpty().WithMessage("Total amount is required.")
            //    .GreaterThanOrEqualTo(0).WithMessage("Total amount cannot be negative.")
            //    .Must((order, total) => total == (order.Amount ?? 0) + (order.CGST ?? 0) + (order.SGST ?? 0))
            //    .WithMessage("Total amount must be equal to the sum of Amount, CGST, and SGST.");

            //RuleFor(order => order.RequestStatus)
            //    .NotEmpty().WithMessage("Request status is required.")
            //    .IsInEnum().WithMessage("Invalid request status value.");

            //RuleFor(order => order.AdminID)
            //    .NotEmpty().WithMessage("Admin ID is required.")
            //    .GreaterThan(0).WithMessage("Admin ID must be a positive number.");
        }
    }
}
