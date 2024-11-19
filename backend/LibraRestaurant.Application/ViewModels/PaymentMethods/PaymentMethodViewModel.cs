using System;
using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;

namespace LibraRestaurant.Application.ViewModels.PaymentMethods;

public sealed class PaymentMethodViewModel
{
    public int PaymentMethodId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Picture {  get; set; }
    public bool IsActive {  get; set; } = true;

    public static PaymentMethodViewModel FromPaymentMethod(PaymentMethod menu)
    {
        return new PaymentMethodViewModel
        {
            PaymentMethodId = menu.PaymentMethodId,
            Name = menu.Name,
            Description = menu.Description,
            Picture = menu.Picture,
            IsActive = menu.IsActive,
        };
    }
}