using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Domain.Purchase
{
    public interface IPurchaseService
    {
        Task<Purchase> UpdateCostAndDiscount(Purchase purchase);
    }
}
