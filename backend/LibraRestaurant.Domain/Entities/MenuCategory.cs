using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class MenuCategory : Entity
    {
        public int MenuCategoryId { get; private set; }
        public int MenuId { get; private set; }
        public int CategoryId { get; private set; }
        public string? Description { get; private set; }

        public MenuCategory(
            int menuCategoryId,
            int menuId,
            int categoryId,
            string? description
        )
        {
            MenuCategoryId = menuCategoryId;
            MenuId = menuId;
            CategoryId = categoryId;
            Description = description;
        }

        public void SetMenuId( int menuId )
        {
            MenuId = menuId;
        }

        public void SetCategoryId( int categoryId )
        {
            CategoryId = categoryId;
        }

        public void SetDescription( string? description )
        {
            Description = description;
        }
    }
}
