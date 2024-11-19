using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class Menu : Entity
    {
        public int MenuId { get; private set; }
        public Guid StoreId { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public bool IsActive { get; private set; }

        [ForeignKey("StoreId")]
        [InverseProperty("Menus")]
        public virtual Store? Store { get; set; }

        public Menu(
            int menuId,
            Guid storeId,
            string name,
            string? description,
            bool isActive
        ) : base(menuId)
        {
            MenuId = menuId;
            StoreId = storeId;
            Name = name;
            Description = description;
            IsActive = isActive;
        }

        public void SetStoreId( Guid storeId )
        {
            StoreId = storeId;
        }

        public void SetName( string name )
        {
            Name = name;
        }

        public void SetDescription( string? description )
        {
            Description = description;
        }

        public void SetActive( bool isActive )
        {
            IsActive = isActive;
        }
    }
}
