using CommunityToolkit.Mvvm.Input;
using NCourseWork.Clients.Add;
using NCourseWork.Clients.List;
using NCourseWork.Clients.Open;
using NCourseWork.Country.Add;
using NCourseWork.Country.Open;
using NCourseWork.Hotel.Open;
using NCourseWork.MVVM;
using NCourseWork.Purchase.Add;
using NCourseWork.Purchase.Open;
using NCourseWork.Services.Navigation;
using NCourseWork.Status.Open;
using NCourseWork.Tour.Add;
using NCourseWork.Tour.Open;
using NCourseWork.Tour.Select;
using NCourseWork.User;
using System;
using System.Windows;

namespace NCourseWork.ViewModels.Main
{
    internal partial class MainViewModel : BaseViewModel, IBasePage
    {
        private readonly INavigationService navigationService;

        public MainViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            navigationService.Navigate<UserViewModel>();
        }
      
        public INavigationService NavigationService { get => navigationService; }
    }
}
