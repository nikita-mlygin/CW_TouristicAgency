using CommunityToolkit.Mvvm.ComponentModel;
using NCourseWork.Application.Tour.Get;
using System;

namespace NCourseWork.Purchase.Open
{
    internal partial class PurchaseUpdateInfo : ObservableObject
    {
        public Guid TourId { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(NewTotalPrice))]
        private int weekCount;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(NewTotalPrice))]
        private TourFullInfo? selectedTourViewInfo;

        public double NewTotalPrice
        {
            get =>
                WeekCount * (SelectedTourViewInfo?.PricePerWeek ?? 0);
        }
    }
}