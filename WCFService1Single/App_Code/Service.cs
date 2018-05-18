using SpaceYDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service" in code, svc and config file together.
public class Service : IService
{
    List<SpaceSystem> _systems;

    void InitializeGame()
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
        SpaceSystem system1 = new SpaceSystem(gold1,min1);
        system1.Name = name1; system1.BaseDistance = base1;
        SpaceSystem system2 = new SpaceSystem(gold2, min2);
        system2.Name = name2; system2.BaseDistance = base2;
        SpaceSystem system3 = new SpaceSystem(gold3, min3);
        system3.Name = name3; system3.BaseDistance = base3;
        SpaceSystem system4 = new SpaceSystem(gold4, min4);
        system4.Name = name4; system4.BaseDistance = base4;
        _systems.Add(system1); _systems.Add(system2); _systems.Add(system3); _systems.Add(system4);     
    }

    Starship SendStarship(Starship starship, string systemName)
    {
        bool exists = false;
        int dist = 0;
        int min = 0;
        foreach (SpaceSystem sys in _systems)
        {
            if (sys.Name == systemName)
            {
                exists = true;
                dist = sys.BaseDistance;
            }
                
        }
        if (exists)
        {
            if (starship.ShipPower <= 20)
            {
                foreach (Person p in starship.Crew)
                {
                    p.Age = p.Age + (2 * dist) / 12;
                }
            }
            else if (starship.ShipPower > 20 && starship.ShipPower <= 30)
            {
                foreach (Person p in starship.Crew)
                {
                    p.Age = p.Age + (2 * dist) / 6;
                }
            }
            else if (starship.ShipPower > 30)
            {
                foreach (Person p in starship.Crew)
                {
                    p.Age = p.Age + (2 * dist) / 4;
                }
            }

            foreach (Person p in starship.Crew)
            {
                if (p.Age > 90)
                    starship.Crew.Remove(p);
            }
            //if (starship.ShipPower >= MinShipPower) ???
        } else
        {
            foreach (Person p in starship.Crew)
            {
                starship.Crew.Remove(p);
            }
        }
        return starship;
    }

    SpaceSystem GetSystem()
    {
        if (_systems.Any<SpaceSystem>())
        {
            return _systems.First<SpaceSystem>();
        } else
        {
            return null;
        }
    }

    Starship GetStarship(int money)
    {
        Random rand = new Random();
        Starship starship = new Starship();
        if (money > 1000 && money <= 3000)
        {
            starship.ShipPower = rand.Next(10, 25);
        } else if (money > 3000 && money <= 10000)
        {
            starship.ShipPower = rand.Next(20, 35);
        } else if (money > 10000)
        {
            starship.ShipPower = rand.Next(35, 60);
        }
        starship.Gold = 0;
        //zaloga statku
        return starship;
    }

    public string GetData(int value)
	{
		return string.Format("You entered: {0}", value);
	}

	public CompositeType GetDataUsingDataContract(CompositeType composite)
	{
		if (composite == null)
		{
			throw new ArgumentNullException("composite");
		}
		if (composite.BoolValue)
		{
			composite.StringValue += "Suffix";
		}
		return composite;
	}
}
