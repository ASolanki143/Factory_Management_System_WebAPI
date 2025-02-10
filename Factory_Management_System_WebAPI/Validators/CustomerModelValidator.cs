using System;
using FluentValidation;
using Factory_Management_System_WebAPI.Models;

namespace Factory_Management_System_WebAPI.Validators
{
    public class CustomerModelValidator : AbstractValidator<CustomerModel>
    {
        public CustomerModelValidator()
        {
            RuleFor(customer => customer.CustomerID)
                .NotEmpty().WithMessage("Customer ID is required.")
                .GreaterThan(0).WithMessage("Customer ID must be a positive number.");

            RuleFor(customer => customer.CustomerName)
                .NotEmpty().WithMessage("Customer name is required.")
                .MaximumLength(100).WithMessage("Customer name cannot exceed 100 characters.");

            RuleFor(customer => customer.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(customer => customer.MobileNo)
                .NotEmpty().WithMessage("Mobile number is required.");

            RuleFor(customer => customer.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters.");

            RuleFor(customer => customer.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[@$!%*?&#]").WithMessage("Password must contain at least one special character.");

            RuleFor(customer => customer.CompanyName)
                .NotEmpty().WithMessage("Company name is required.")
                .MaximumLength(100).WithMessage("Company name cannot exceed 100 characters.");

            RuleFor(customer => customer.GSTNo)
                .NotEmpty().WithMessage("GST number is required.")
                .Matches("^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[A-Z0-9]{1}[Z]{1}[A-Z0-9]{1}$").WithMessage("Invalid GST number format.");

            RuleFor(customer => customer.CompanyNo)
                .NotEmpty().WithMessage("Company contact number is required.");

            RuleFor(customer => customer.CompanyEmail)
                .NotEmpty().WithMessage("Company email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(customer => customer.BankName)
                .NotEmpty().WithMessage("Bank name is required.")
                .MaximumLength(50).WithMessage("Bank name cannot exceed 50 characters.");

            RuleFor(customer => customer.AccountNo)
                .NotEmpty().WithMessage("Account number is required.");

            RuleFor(customer => customer.IFSCCode)
                .NotEmpty().WithMessage("IFSC code is required.")
                .Matches("^[A-Z]{4}0[A-Z0-9]{6}$").WithMessage("Invalid IFSC code format.");

            RuleFor(customer => customer.AdminID)
                .NotEmpty().WithMessage("Admin ID is required.")
                .GreaterThan(0).WithMessage("Admin ID must be a positive number.");
        }
    }
}