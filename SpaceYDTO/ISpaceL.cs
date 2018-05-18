using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SpaceYDTO
{

    [ServiceContract]
    public interface ISpaceL
    {

        [OperationContract]
        SpaceSystem GetDataUsingDataContract(SpaceSystem spaceSystem);
        Starship GetDataUsingDataContract(Starship starship);
        Person GetDataUsingDataContract(Person person);

    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "SpaceYDTO.ContractType".
   
    [DataContract]
    public class SpaceSystem
    {
        
        public readonly int MinShipPower;
        public readonly int Gold;

        public SpaceSystem(int gold, int min)
        {
            this.MinShipPower = min;
            this.Gold = gold;
        }

        [DataMember]
        public string Name
        {
            get; set;
        }
        
        [DataMember]
        public int BaseDistance
        {
            get; set;
        }
    }

    [DataContract]
    public class Starship
    {
        [DataMember]
        public List<Person> Crew
        {
            get; set;
        }
        [DataMember]
        public int Gold
        {
            get; set;
        }
        
        [DataMember]
        public int ShipPower
        {
            get; set;
        }
    }

    [DataContract]
    public class Person
    {
        public Person (string name, string nick, float age)
        {
            this.Name = name;
            this.Nick = nick;
            this.Age = age;
        }
        public Person(string nick, float age)
        {
            this.Nick = nick;
            this.Age = age;
            this.Name = "";
        }
        public string Name;
        public string Nick;
        public float Age;
    }
}
