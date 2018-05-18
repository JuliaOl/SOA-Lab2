using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.

    /* Serwis powinien być dostępny pod nazwą (końcówką adresu zazwyczaj tworzonej jako
    /Service1/) /FirstOrder/*/
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class Service1 : IService1
    {
        public int GetMoneyFromImperium()
        {
            Random random = new Random();
            return random.Next(3000, 5000);
        }
    }
}
