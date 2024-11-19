using System;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.Customers;

public sealed class CustomerViewModel
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string? Email {  get; set; }
    public string? Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public static CustomerViewModel FromCustomer(Customer customer)
    {
        return new CustomerViewModel
        {
            CustomerId = customer.CustomerId,
            Name = customer.Name,
            Phone = customer.Phone,
            Email = customer.Email,
            Address = customer.Address,
            CreatedAt = customer.CreatedAt,
            UpdatedAt = customer.UpdatedAt,
        };
    }
}