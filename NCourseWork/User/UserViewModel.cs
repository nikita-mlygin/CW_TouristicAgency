using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MapsterMapper;
using MediatR;
using NCourseWork.Common.Layouts;
using NCourseWork.Domain.User;
using NCourseWork.MVVM;
using NCourseWork.Purchase.List;
using NCourseWork.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.User
{
    internal partial class UserViewModel : ObservableObject, IBasePage, ISinglePage
    {
        private readonly IUserService userService;
        private readonly INavigationService navigationService;
        
        [ObservableProperty]
        private string login = String.Empty;
        
        [ObservableProperty]
        private string password = String.Empty;

        public UserViewModel(IUserService userService, INavigationService navigationService)
        {
            this.userService = userService;
            this.navigationService = navigationService;
        }

        [RelayCommand]
        private async Task TryLogin()
        {
            if (await userService.Login(Login, Password) is not null)
            {
                navigationService.ResetHistory();
                navigationService.BasePageSetting = new DefaultLayout();
                navigationService.Navigate<PurchaseListViewModel>();
            }
        }
    }
}
