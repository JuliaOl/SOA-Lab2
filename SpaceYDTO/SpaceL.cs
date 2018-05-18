using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SpaceYDTO
{
    public class SpaceL : ISpaceL
    {
        
        public SpaceSystem GetDataUsingDataContract(SpaceSystem spaceSystem)
        {
            if (spaceSystem == null)
            {
                throw new ArgumentNullException("spaceSystem");
            }
            
            return spaceSystem;
        }
        public Starship GetDataUsingDataContract(Starship starship)
        {
            if (starship == null)
            {
                throw new ArgumentNullException("starship");
            }

            return starship;
        }
        public Person GetDataUsingDataContract(Person person)
        {
            if (person == null)
            {
                throw new ArgumentNullException("person");
            }

            return person;
        }

    }
}
