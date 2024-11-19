using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraRestaurant.Domain.Entities
{
    public class Language : Entity
    {
        public int LanguageId { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }

        public Language(
            int languageId,
            string name,
            string? description
        ) : base (languageId)
        {
            LanguageId = languageId;
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
