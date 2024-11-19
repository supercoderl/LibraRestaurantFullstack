using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class ItemCustomization : Entity
    {
        public int ItemCustomizationId { get; private set; }
        public int ItemId { get; private set; }
        public int CustomizationId { get; private set; }
        public string? Comments { get; private set; }

        public ItemCustomization(
            int itemCustomizationId,
            int itemId,
            int customizationId,
            string? comment
        ) : base(itemCustomizationId)
        {
            ItemCustomizationId = itemCustomizationId;
            ItemId = itemId;
            CustomizationId = customizationId;
            Comments = comment;
        }

        public void SetItemId( int itemId )
        {
            ItemId = itemId;
        }

        public void SetCustomizationId( int customizationId )
        {
            CustomizationId = customizationId;
        }

        public void SetComments( string? comments )
        {
            Comments = comments;
        }
    }
}
