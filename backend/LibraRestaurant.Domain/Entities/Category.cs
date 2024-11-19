using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class Category : Entity
    {
        public int CategoryId { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public bool IsActive { get; private set; }
        public string? Picture {  get; private set; }

        [InverseProperty("Category")]
        public virtual ICollection<CategoryItem>? CategoryItems { get; set; } = new List<CategoryItem>();

        [InverseProperty("Category")]
        public virtual ICollection<Discount>? Discounts { get; set; } = new List<Discount>();

        public Category(
            int categoryId,
            string name,
            string? description,
            bool isActive,
            string? picture
        ) : base( categoryId )
        {
            CategoryId = categoryId;
            Name = name;
            Description = description;
            IsActive = isActive;
            Picture = picture;
        }

        public void SetName(string name )
        {
            Name = name;
        }

        public void SetDescription(string? description )
        {
            Description = description;
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;
        }

        public void SetPicture(string? picture)
        {
            Picture = picture;
        }
    }
}
