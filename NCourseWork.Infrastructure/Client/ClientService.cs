namespace NCourseWork.Infrastructure.Client
{
    using NCourseWork.Domain.Client;
    using NCourseWork.Domain.Status;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    internal class ClientService : IClientService
    {
        private readonly IClientRepository clientRepository;
        private readonly IStatusRepository statusRepository;

        public ClientService(IClientRepository clientRepository, IStatusRepository statusRepository)
        {
            this.clientRepository = clientRepository;
            this.statusRepository = statusRepository;
        }

        public async Task<Client> ChangePurchaseCount(Client client, int addCount)
        {
            client = await clientRepository.GetClientByIdAsync(client.Id) ?? throw new ApplicationException("Client not found");

            client.CountOfOrders += addCount;

            client.Status = await statusRepository.GetStatusForPurchaseCount(client.CountOfOrders);

            return client;
        }
    }
}
