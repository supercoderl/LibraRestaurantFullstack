using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class Customization : Entity
    {
        public int CustomizationId { get; private set; }
        public string Name { get; private set; }
        public string DisplayName { get; private set; }
        public string? Description { get; private set; }
        public bool IsActive { get; private set; }

        public Customization(
            int customizationId,
            string name,
            string displayName,
            string? description,
            bool isActive
        ) : base (customizationId)
        {
            CustomizationId = customizationId;
            Name = name;
            DisplayName = displayName;
            Description = description;
            IsActive = isActive;
        }

        public void SetName( string name )
        {
            Name = name;
        }

        public void SetDisplayName( string displayName )
        {
            DisplayName = displayName;
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
