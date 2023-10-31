namespace NCourseWork.Domain.Purchase
{
    using NCourseWork.Domain.Client;
    using NCourseWork.Domain.Tour;
    using System;

    public class Purchase
    {
        public Purchase()
        {
        }

        public Purchase(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int WeekCount { get; set; }
        public double TotalPrice { get; set; }
        public double TotalDiscount { get; set; }
        public Client Client { get; set; } = null!;
        public Tour Tour { get; set; } = null!;

        public double Price { get => TotalPrice * (1 - TotalDiscount); }
    }
}
