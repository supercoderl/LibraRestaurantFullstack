using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class District : Entity
    {
        public int DistrictId { get; private set; } 
        public string Name { get; private set; }
        public string NameEn { get; private set; }
        public string Fullname { get; private set; }
        public string FullnameEn { get; private set; }
        public string CodeName { get; private set; }
        public int CityId { get; private set; }

        [ForeignKey("CityId")]
        [InverseProperty("Districts")]
        public virtual City? City { get; set; }

        [InverseProperty("District")]
        public virtual ICollection<Ward>? Wards { get; set; } = new List<Ward>();

        [InverseProperty("District")]
        public virtual ICollection<Store>? Stores { get; set; } = new List<Store>();

        public District(
            int districtId,
            string name,
            string nameEn,
            string fullname,
            string fullnameEn,
            string codeName,
            int cityId
        ) : base(districtId)
        {
            DistrictId = districtId;
            Name = name;
            NameEn = nameEn;
            Fullname = fullname;
            FullnameEn = fullnameEn;
            CodeName = codeName;
            CityId = cityId;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetNameEn(string nameEn)
        {
            NameEn = nameEn;
        }

        public void SetFullname(string fullname)
        {
            Fullname = fullname;
        }

        public void SetFullnameEn(string fullnameEn)
        {
            FullnameEn = fullnameEn;
        }

        public void SetCodeName(string codeName)
        {
            CodeName = codeName;
        }

        public void SetCityId(int cityId)
        {
            CityId = cityId;
        }
    }
}
