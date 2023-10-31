using CommunityToolkit.Mvvm.Input;
using NCourseWork.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Common.Layouts.StepNavigation
{
    internal partial class StepNavigationLayout : BaseLayout
    {
        private readonly INavigationService navigationService;

        public StepNavigationLayout(INavigationService navigationService)
        {
            this.navigationService = navigationService;

            navigationService.PropertyChanged += OnNavigate;
        }

        private void OnNavigate(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(navigationService.Current))
            {
                OnPropertyChanged(nameof(CanBack));
            }
        }

        [RelayCommand]
        private void Back()
        {
            navigationService.Navigate(-1);
        }

        public bool CanBack { get => navigationService.CanNavigate(-1); }
    }
}
