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
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        Starship GetStarship(int money);
        [OperationContract]
        SpaceSystem GetSystem();
        [OperationContract]
        Starship SendStarship(Starship starship, string systemName);
        [OperationContract]
        void InitializeGame();

    }
}
