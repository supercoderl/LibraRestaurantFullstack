using LibraRestaurant.Domain.Entities;
using LibraRestaurant.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Application.ViewModels.MenuItems
{
    public sealed class ItemViewModel
    {
        public int ItemId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        public string? Summary {  get; set; }
        public string SKU { get; set; } = string.Empty;
        public string? Picture { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string? Recipe { get; set; }
        public string? Instruction { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }

        public List<int>? CategoryIds { get; set; }
        public DiscountWithItem? Discount { get; set; }
        public DiscountStatus DiscountStatus { get; set; }
        public double RatingScore {  get; set; }

        public static ItemViewModel FromItem(MenuItem item)
        {
            var currentTime = DateTime.Now;

            // Lấy tất cả các discount và phân loại theo trạng thái
            var discountsWithStatus = item.Discounts?.Select(d => new
            {
                Discount = d,
                Status = d.DiscountType != null ?
                     (d.DiscountType.StartTime > currentTime ? DiscountStatus.NotYet :
                       (d.DiscountType.EndTime < currentTime ? DiscountStatus.Expired : DiscountStatus.Active)) : DiscountStatus.Unknow
            }).ToList();

            // Tìm discount hợp lệ (đang trong thời gian)
            var discount = discountsWithStatus?.SingleOrDefault();

            return new ItemViewModel
            {
                ItemId = item.ItemId,
                Title = item.Title,
                Slug = item.Slug,
                Summary = item.Summary,
                SKU = item.SKU,
                Picture = item.Picture,
                Price = item.Price,
                Quantity = item.Quantity,
                Recipe = item.Recipe,
                Instruction = item.Instruction,
                CreatedAt = item.CreatedAt,
                LastUpdatedAt = item.LastUpdatedAt,
                CategoryIds = item.CategoryItems?.Select(ci => ci.CategoryId).ToList(),
                Discount = discount is not null ? new DiscountWithItem().FromDiscountWithItem(discount.Discount) : null,
                DiscountStatus = discount is not null ? discount.Status : DiscountStatus.Unknow,
                RatingScore = double.Parse(string.Format("{0:0.0}", item?.Reviews?.Select(r => r.Rating).DefaultIfEmpty(0).Average() ?? 0))
            };
        }
    }

    public class MenuItemSplitted
    {
        public int Id { get; set; }
        public string Picture { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public double Price { get; set; }
    }

    public class DiscountWithItem
    {
        public int DiscountId { get; set; }
        public bool IsPercentage { get; set; }
        public double DiscountValue { get; set; }
        public int DiscountTypeId { get; set; }

        public DiscountWithItem FromDiscountWithItem(Discount discount)
        {
            return new DiscountWithItem
            {
                DiscountId = discount.DiscountId,
                IsPercentage = discount.DiscountType?.IsPercentage ?? false,
                DiscountValue = discount.DiscountType?.Value ?? 0,
                DiscountTypeId = discount.DiscountTypeId
            };
        }
    }
}
