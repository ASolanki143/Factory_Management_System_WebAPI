using System;
using FluentValidation;
using Factory_Management_System_WebAPI.Models;

namespace Factory_Management_System_WebAPI.Validators
{
    public class OrderItemModelValidator : AbstractValidator<OrderItemModel>
    {
        public OrderItemModelValidator()
        {
            //RuleFor(orderItem => orderItem.OrderItemID)
            //    .NotEmpty().WithMessage("Order Item ID is required.")
            //    .GreaterThan(0).WithMessage("Order Item ID must be a positive number.");

            RuleFor(orderItem => orderItem.OrderID)
                .NotEmpty().WithMessage("Order ID is required.")
                .GreaterThan(0).WithMessage("Order ID must be a positive number.");

            RuleFor(orderItem => orderItem.ProductID)
                .NotEmpty().WithMessage("Product ID is required.")
                .GreaterThan(0).WithMessage("Product ID must be a positive number.");

            RuleFor(orderItem => orderItem.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be greater than zero.");

            //RuleFor(orderItem => orderItem.Amount)
            //    .NotEmpty().WithMessage("Amount is required.")
            //    .GreaterThanOrEqualTo(0).WithMessage("Amount cannot be negative.");

            //RuleFor(orderItem => orderItem.ProductName)
            //    .NotEmpty().WithMessage("Product name is required.")
            //    .MaximumLength(100).WithMessage("Product name cannot exceed 100 characters.");

            //RuleFor(orderItem => orderItem.ProductPrice)
            //    .NotEmpty().WithMessage("Product price is required.")
            //    .GreaterThanOrEqualTo(0).WithMessage("Product price cannot be negative.");

            //RuleFor(orderItem => orderItem.Material)
            //    .NotEmpty().WithMessage("Material is required.")
            //    .MaximumLength(50).WithMessage("Material cannot exceed 50 characters.");

            //RuleFor(orderItem => orderItem.AdminID)
            //    .NotEmpty().WithMessage("Admin ID is required.")
            //    .GreaterThan(0).WithMessage("Admin ID must be a positive number.");
        }
    }
}
