using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class City : Entity
    {
        public int CityId { get; private set; }
        public string Name { get; private set; }
        public string NameEn { get; private set; }
        public string Fullname { get; private set; }
        public string FullnameEn { get; private set; }
        public string CodeName { get; private set; }

        [InverseProperty("City")]
        public virtual ICollection<District>? Districts { get; set; } = new List<District>();

        [InverseProperty("City")]
        public virtual ICollection<Store>? Stores { get; set; } = new List<Store>();

        public City(
            int cityId,
            string name,
            string nameEn,
            string fullname,
            string fullnameEn,
            string codeName
        ) : base(cityId)
        {
            CityId = cityId;
            Name = name;
            NameEn = nameEn;
            Fullname = fullname;
            FullnameEn = fullnameEn;
            CodeName = codeName;
        }

        public void SetName( string name )
        {
            Name = name;
        }

        public void SetNameEn( string nameEn )
        {
            NameEn = nameEn;
        }

        public void SetFullname( string fullname )
        {
            Fullname = fullname;
        }

        public void SetFullnameEn( string fullnameEn )
        {
            FullnameEn = fullnameEn;
        }

        public void SetCodeName( string codeName )
        {
            CodeName = codeName;
        }
    }
}
