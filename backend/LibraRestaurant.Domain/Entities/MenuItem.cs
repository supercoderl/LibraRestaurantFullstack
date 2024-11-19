using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class MenuItem : Entity
    {
        public int ItemId { get; private set; }
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string? Summary { get; private set; }
        public string SKU { get; private set; }
        public string? Picture { get; private set; }
        public double Price { get; private set; }
        public int Quantity { get; private set; }
        public string? Recipe { get; private set; }
        public string? Instruction { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime? LastUpdatedAt { get; private set; }

        [InverseProperty("Item")]
        public virtual ICollection<OrderLine>? OrderLines { get; set; } = new List<OrderLine>();

        [InverseProperty("Item")]
        public virtual ICollection<CategoryItem>? CategoryItems { get; set; } = new List<CategoryItem>();

        [InverseProperty("Item")]
        public virtual ICollection<OrderLog>? OrderLogs { get; set; } = new List<OrderLog>();

        [InverseProperty("Item")]
        public virtual ICollection<Discount>? Discounts { get; set; } = new List<Discount>();

        [InverseProperty("Item")]
        public virtual ICollection<Review>? Reviews { get; set; } = new List<Review>();

        public MenuItem(
            int itemId,
            string title,
            string slug,
            string? summary,
            string sKU,
            string? picture,
            double price,
            int quantity,
            string? recipe,
            string? instruction,
            DateTime createdAt,
            DateTime? lastUpdatedAt
        )
        {
            ItemId = itemId;
            Title = title;
            Slug = slug;
            Summary = summary;
            SKU = sKU;
            Picture = picture;
            Price = price;
            Quantity = quantity;
            Recipe = recipe;
            Instruction = instruction;
            CreatedAt = createdAt;
            LastUpdatedAt = lastUpdatedAt;
        }

        public void SetTitle(string title)
        {
            Title = title;
        }

        public void SetSlug(string slug)
        {
            Slug = slug;
        }

        public void SetSummary(string? summary)
        {
            Summary = summary;
        }

        public void SetSKU(string sKU)
        {
            SKU = sKU;
        }

        public void SetPicture(string? picture)
        {
            Picture = picture;
        }

        public void SetPrice(double price)
        {
            Price = price;
        }

        public void SetQuantity(int quantity)
        {
            Quantity = quantity;
        }

        public void SetRecipe(string? recipe)
        {
            Recipe = recipe;
        }

        public void SetInstruction(string? instruction)
        {
            Instruction = instruction;
        }

        public void SetCreatedAt(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }

        public void SetLastUpdatedAt(DateTime? lastUpdatedAt)
        {
            LastUpdatedAt = lastUpdatedAt;
        }
    }
}
