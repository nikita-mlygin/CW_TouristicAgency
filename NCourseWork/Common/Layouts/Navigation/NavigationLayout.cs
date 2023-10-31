using CommunityToolkit.Mvvm.Input;
using NCourseWork.Clients.List;
using NCourseWork.Country.List;
using NCourseWork.Hotel.List;
using NCourseWork.Purchase.List;
using NCourseWork.Services.Navigation;
using NCourseWork.Tour.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Common.Layouts.Navigation
{
    internal partial class NavigationLayout : BaseLayout
    {
        private readonly INavigationService navigationService;

        public NavigationLayout(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        [RelayCommand]
        private void NavigateToClient()
        {
            navigationService.Navigate<ClientListViewModel>();
        }

        [RelayCommand]
        private void NavigateToCountry()
        {
            navigationService.Navigate<CountryListViewModel>();
        }

        [RelayCommand]
        private void NavigateToHotel()
        {
            navigationService.Navigate<HotelListViewModel>();
        }

        [RelayCommand]
        private void NavigateToTour()
        {
            navigationService.Navigate<TourListViewModel>();
        }

        [RelayCommand]
        private void NavigateToPurchase()
        {
            navigationService.Navigate<PurchaseListViewModel>();
        }
    }
}
