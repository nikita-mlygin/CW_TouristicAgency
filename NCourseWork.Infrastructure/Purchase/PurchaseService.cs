namespace NCourseWork.Infrastructure.Purchase
{
    using NCourseWork.Domain.Purchase;
    using System.Threading.Tasks;

    internal class PurchaseService : IPurchaseService
    {
        public Task<Purchase> UpdateCostAndDiscount(Purchase purchase)
        {
            var client = purchase.Client;
            var discount = client.Status.DiscountPercentage;

            var totalPrice = purchase.WeekCount * purchase.Tour.PricePerWeek;
            
            purchase.TotalPrice = totalPrice;
            purchase.TotalDiscount = discount;

            return Task.FromResult(purchase);
        }
    }
}
