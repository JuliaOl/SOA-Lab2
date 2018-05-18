using SpaceYDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using WcfServiceApp1;

namespace SpaceClient
{
    class Program
    {
        static void Main(string[] args)
        {
          
            List<Starship> _starships = new List<Starship>();
            bool _anySystem = true;
            int _gold = 900;
            int _imperiumMoneyAskCount = 5;
            bool _gameOn = true;
            //Random random = new Random();

            ServiceReference1.Service1Client firstOrder = new ServiceReference1.Service1Client();
            ServiceReference2.Service1Client client = new ServiceReference2.Service1Client();
            client.InitializeGame();
            while (_gameOn)
            {
                Console.WriteLine("Ilosc zlota: {0}\nIlosc prosb o zloto: {1}\nMenu:\n" +
                    "A - popros imperium o zloto\nB - kup statek za zloto\nC - wyslij statek do systemu\n" +
                    "E - zakoncz gre", _gold, _imperiumMoneyAskCount);
                var c = Console.ReadLine();
                switch (c.ToUpper())
                {
                    case "A":
                        if (_imperiumMoneyAskCount > 0)
                        {
                            //tu powinno byc polaczenie z drugim serwisem
                            //_gold += random.Next(3000, 5000);
                            _gold += firstOrder.GetMoneyFromImperium();
                            _imperiumMoneyAskCount--;
                        } else
                        {
                            Console.WriteLine("Prosba o zloto niedostepna!");
                        }
                            break;
                    case "B":
                        Console.WriteLine("Aktualne złoto: {0} Wpisz za ile złota chcesz kupić statek", _gold);
                        int spending = Int32.Parse(Console.ReadLine());
                        if (spending <= _gold)
                        {
                            _starships.Add(client.GetStarship(spending));
                            _gold -= spending;
                        }
                        else Console.WriteLine("Masz za malo pieniedzy!");
                        break;
                    case "C":
                        SpaceSystem system = client.GetSystem();
                        if (system != null)
                        {
                            Console.WriteLine("System {0}, odleglość {1}.", system.Name, system.BaseDistance);
                            if (_starships.LongCount() == 0)
                            {
                                Console.WriteLine("Brak statkow");
                            } else
                            { 
                                Console.WriteLine("Statków gotowych do podróży: {0}", _starships.LongCount());
                                Console.WriteLine("Wybierz statek wpisując jego numer (albo wyjdź wpisując literę E):");
                                wyswietlStatki(_starships);
                                var input = Console.ReadLine();
                                if (input.ToLower().Equals("e"))
                                {
                                    break;
                                } else
                                {
                                    Starship result = client.SendStarship(_starships[Int32.Parse(input) - 1], system.Name);
                                    _starships.RemoveAt(Int32.Parse(input) - 1);
                                    if (result.Gold != 0)
                                    {
                                        _gold += result.Gold;
                                        result.Gold = 0;
                                        if (result.Crew.Any()) _starships.Add(result);
                                    }
   
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Brak systemow");
                            _anySystem = false;
                        }
                        break;
                    case "E":
                        _gameOn = false;
                        break;
                    default:
                        Console.WriteLine("Wybierz poprawną opcję");
                        break;
                }  
            }
            if (_anySystem)
                Console.WriteLine("Przegrałeś, spróbuj jeszcze raz!");
            else
                Console.WriteLine("Gratulacje, wygrałeś!");
            Console.ReadKey();
            client.Close();
        }

        private static void wyswietlStatki(List<Starship> starships)
        {
            starships.ForEach(delegate(Starship starship)
            {
                Console.Write("{0}. {1}", starships.IndexOf(starship) + 1, starship.ShipPower);
                foreach (Person person in starship.Crew)
                {
                    Console.Write(", {0} {1} {2}", person.Name, person.Nick, person.Age);
                }
                Console.WriteLine();
            });
        }
    }
}
