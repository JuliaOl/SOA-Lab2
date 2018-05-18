using SpaceYDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfServiceApp1
{
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.

    /* Serwis powinien być dostępny pod nazwą
    (końcówką adresu zazwyczaj tworzonej jako /Service1/) /Cosmos/*/
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service1 : IService1
    {
        public List<SpaceSystem> _systems = new List<SpaceSystem>();

        public void InitializeGame()
        {
            Random r = new Random();
            string name1 = "milky";
            string name2 = "honey";
            string name3 = "heaven";
            string name4 = "polly";
            int min1 = r.Next(10, 40);
            int min2 = r.Next(10, 40);
            int min3 = r.Next(10, 40);
            int min4 = r.Next(10, 40);
            int base1 = r.Next(20, 120);
            int base2 = r.Next(20, 120);
            int base3 = r.Next(20, 120);
            int base4 = r.Next(20, 120);
            int gold1 = r.Next(3000, 7000);
            int gold2 = r.Next(3000, 7000);
            int gold3 = r.Next(3000, 7000);
            int gold4 = r.Next(3000, 7000);
            SpaceSystem system1 = new SpaceSystem(gold1, min1)
            {
                Name = name1,
                BaseDistance = base1
            };

            SpaceSystem system2 = new SpaceSystem(gold2, min2)
            {
                Name = name2,
                BaseDistance = base2
            };
            SpaceSystem system3 = new SpaceSystem(gold3, min3)
            {
                Name = name3,
                BaseDistance = base3
            };
            SpaceSystem system4 = new SpaceSystem(gold4, min4)
            {
                Name = name4,
                BaseDistance = base4
            };
            _systems.Add(system1); _systems.Add(system2); _systems.Add(system3); _systems.Add(system4);
        }

        public Starship SendStarship(Starship starship, string systemName)
        {
            SpaceSystem system = _systems.Find(x => x.Name == systemName);
            if (system != null)
            {
                if (starship.ShipPower <= 20)
                {
                    foreach (Person p in starship.Crew)
                    {
                        p.Age = p.Age + (2 * system.BaseDistance) / 12;
                    }
                }
                else if (starship.ShipPower > 20 && starship.ShipPower <= 30)
                {
                    foreach (Person p in starship.Crew)
                    {
                        p.Age = p.Age + (2 * system.BaseDistance) / 6;
                    }
                }
                else if (starship.ShipPower > 30)
                {
                    foreach (Person p in starship.Crew)
                    {
                        p.Age = p.Age + (2 * system.BaseDistance) / 4;
                    }
                }

                foreach (Person p in starship.Crew)
                {
                    if (p.Age > 90)
                        starship.Crew.Remove(p);
                }

                if (starship.ShipPower >= system.MinShipPower)
                {
                    starship.Gold = system.Gold;
                    _systems.Remove(system);
                    return starship;
                }
                else
                {
                    return starship;
                }
            }
            else
            {
                foreach (Person p in starship.Crew)
                {
                    starship.Crew.Remove(p);
                }
                return starship;
            }
        }

        public SpaceSystem GetSystem()
        {
            if (_systems.Any<SpaceSystem>())
            {
                return _systems.First<SpaceSystem>();
            }
            else
            {
                return null;
            }
        }

        public Starship GetStarship(int money)
        {
            Random rand = new Random();
            Starship starship = new Starship();
            if (money > 1000 && money <= 3000)
            {
                starship.ShipPower = rand.Next(10, 25);
            }
            else if (money > 3000 && money <= 10000)
            {
                starship.ShipPower = rand.Next(20, 35);
            }
            else if (money > 10000)
            {
                starship.ShipPower = rand.Next(35, 60);
            }
            starship.Gold = 0;

            List<Person> crew = new List<Person>();
            for (int i=0; i<4; i++)
            {
                Person person = new Person("buddy", 20);
                crew.Add(person);
            }
            crew[0].Name = "Florian";
            crew[1].Name = "Amelia";
            crew[2].Name = "Neil";
            crew[3].Name = "Joanna";

            starship.Crew = crew;
            return starship;
        }

    }
}
