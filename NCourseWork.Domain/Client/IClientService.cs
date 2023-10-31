using NCourseWork.Domain.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Domain.Client
{
    public interface IClientService
    {
        public Task<Client> ChangePurchaseCount(Client client, int addCount);
    }
}
