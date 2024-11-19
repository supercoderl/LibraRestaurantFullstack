using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class Ward : Entity
    {
        public int WardId { get; private set; }
        public string Name { get; private set; }
        public string NameEn { get; private set; }
        public string Fullname { get; private set; }
        public string FullnameEn { get; private set; }
        public string CodeName { get; private set; }
        public int DistrictId { get; private set; }

        [ForeignKey("DistrictId")]
        [InverseProperty("Wards")]
        public virtual District? District { get; set; }

        [InverseProperty("Ward")]
        public virtual ICollection<Store>? Stores { get; set; } = new List<Store>();

        public Ward(
            int wardId,
            string name,
            string nameEn,
            string fullname,
            string fullnameEn,
            string codeName,
            int districtId
        ) : base(wardId)
        {
            WardId = wardId;
            Name = name;
            NameEn = nameEn;
            Fullname = fullname;
            FullnameEn = fullnameEn;
            CodeName = codeName;
            DistrictId = districtId;
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

        public void SetDistrictId(int districtId)
        {
            DistrictId = districtId;
        }
    }
}
