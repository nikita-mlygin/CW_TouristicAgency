namespace NCourseWork.Domain.Purchase
{
    public interface IPurchaseRepository
    {
        Task<Purchase?> GetPurchaseByIdAsync(Guid purchaseId);
        Task AddPurchaseAsync(Purchase purchase);
        Task UpdatePurchaseAsync(Purchase purchase);
        Task RemovePurchaseAsync(Guid purchaseId);
        Task<IEnumerable<Purchase>> GetWithFilterAsync(Guid? clientFilter, DateTime? startDateTimeFiller, DateTime? endDateTimeFiller);
    }

}
