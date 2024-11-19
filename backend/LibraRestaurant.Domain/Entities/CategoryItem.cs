using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class CategoryItem : Entity
    {
        public int CategoryItemId { get; private set; }
        public int CategoryId { get; private set; }
        public int ItemId { get; private set; }
        public string? Description { get; private set; }

        [ForeignKey("CategoryId")]
        [InverseProperty("CategoryItems")]
        public virtual Category? Category { get; set; }

        [ForeignKey("ItemId")]
        [InverseProperty("CategoryItems")]
        public virtual MenuItem? Item { get; set; }

        public CategoryItem(
            int categoryItemId,
            int categoryId,
            int itemId,
            string? description
        ) : base( categoryItemId)
        {
            CategoryItemId = categoryItemId;
            CategoryId = categoryId;
            ItemId = itemId;
            Description = description;
        }

        public void SetCategoryId(int categoryId)
        {
            CategoryId = categoryId;
        }

        public void SetItemId(int itemId) 
        { 
            ItemId = itemId; 
        }

        public void SetDescription(string? description)
        {
            Description = description;
        }
    }
}
