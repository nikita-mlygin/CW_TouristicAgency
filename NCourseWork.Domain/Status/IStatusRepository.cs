namespace NCourseWork.Domain.Status
{
    public interface IStatusRepository
    {
        Task<Status?> GetStatusByIdAsync(Guid statusId);
        Task<Status> GetFirstStatusIdAsync();
        Task<Status> GetStatusForPurchaseCount(int count);
        Task AddStatusAsync(Status status);
        Task UpdateStatusAsync(Status status);
        Task RemoveStatusAsync(Guid statusId);
    }

}
