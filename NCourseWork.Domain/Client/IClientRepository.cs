namespace NCourseWork.Domain.Client
{
    using Purchase;

    public interface IClientRepository
    {
        Task<Client?> GetClientByIdAsync(Guid clientId);
        Task<IEnumerable<Client>> GetClientsWithNameFilterAsync(string? FirstName = "", string? LastName = "", string? MiddleName = "");
        Task AddClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task UpdateWithoutStatusAsync(Client client);
        Task DeleteClientAsync(Guid clientId);
        Task<IEnumerable<Client>> GetClientsWithOneNameFilterAsync(string name);
        Task<Client> GetClientByPurchaseAsync(Purchase purchase);
    }
}
