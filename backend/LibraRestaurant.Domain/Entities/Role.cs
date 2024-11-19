using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class Role : Entity
    {
        public int RoleId { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }

        [InverseProperty("Role")]
        public virtual ICollection<EmployeeRole>? EmployeeRoles { get; set; } = new List<EmployeeRole>();

        public Role(
            int roleId,
            string name,
            string? description
        ) : base( roleId )
        {
            RoleId = roleId;
            Name = name;
            Description = description;
        }

        public void SetName( string name )
        {
            Name = name;
        }

        public void SetDescription( string? description )
        {
            Description = description;
        }
    }
}
