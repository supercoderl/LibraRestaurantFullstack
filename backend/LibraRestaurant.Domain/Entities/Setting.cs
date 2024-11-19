using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class Setting : Entity
    {
        public int SettingId { get; private set; }
        public Guid StoreId { get; private set; }
        public int LanguageId { get; private set; }
        public int CurrencyId { get; private set; }
        public bool IsBusyMode { get; private set; }
        public bool IsOrderingPause { get; private set; }

        public Setting(
            int settingId,
            Guid storeId,
            int languageId,
            int currencyId,
            bool isBusyMode,
            bool isOrderingPause
        ) : base(settingId)
        {
            SettingId = settingId;
            StoreId = storeId;
            LanguageId = languageId;
            CurrencyId = currencyId;
            IsBusyMode = isBusyMode;
            IsOrderingPause = isOrderingPause;
        }

        public void SetStoreId( Guid storeId )
        {
            StoreId = storeId;
        }

        public void SetLanguageId( int languageId )
        {
            LanguageId = languageId;
        }

        public void SetCurrencyId( int currencyId )
        {
            CurrencyId = currencyId;
        }

        public void SetBusyMode( bool isBusyMode )
        {
            IsBusyMode = isBusyMode;
        }

        public void SetOrderingPause( bool isOrderingPause )
        {
            IsOrderingPause = isOrderingPause;
        }
    }
}
