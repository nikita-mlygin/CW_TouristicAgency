namespace NCourseWork.Tour.Select
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using MapsterMapper;
    using MediatR;
    using Microsoft.Identity.Client;
    using NCourseWork.Application.Country.Get;
    using NCourseWork.Application.Country.Get.WithNameFilter;
    using NCourseWork.Application.Tour.Get;
    using NCourseWork.Application.Tour.Get.All;
    using NCourseWork.Application.Tour.Get.ByCountry;
    using NCourseWork.Common.Components.ComboBox;
    using NCourseWork.Domain.Country;
    using NCourseWork.MVVM;
    using NCourseWork.Services.Navigation;
    using NCourseWork.Tour.Open;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Markup;
    using Xceed.Wpf.Toolkit.Core.Converters;

    internal partial class TourSelectViewModel : ObservableObject, IBasePage
    {
        private readonly IMediator mediator;
        private readonly INavigationService navigationService;
        private readonly IMapper mapper;

        public TourSelectViewModel(IMediator mediator, INavigationService navigationService, IMapper mapper)
        {
            this.mediator = mediator;
            this.navigationService = navigationService;
            this.mapper = mapper;

            countryComboBox = null!;
        }

        [ObservableProperty]
        private bool isLoading = true;

        [ObservableProperty]
        private IEnumerable<TourListItemInfo>? info;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmSelectCommand))]
        private TourListItemInfo? selectedItem;

        [ObservableProperty]
        private CustomComboBoxWithRealSearch<MyComboBoxDataSet<CountryListItemInfo>, CountryListItemInfo> countryComboBox;

        [ObservableProperty]
        private CountryListItemInfo? countryFilter;

        partial void OnCountryFilterChanged(CountryListItemInfo? value)
        {
            UpdateDataCommand.Execute(null);
        }

        [RelayCommand]
        private async Task GetData()
        {
            IsLoading = true;

            await UpdateData();

            CountryComboBox ??= new CustomComboBoxWithRealSearch<MyComboBoxDataSet<CountryListItemInfo>, CountryListItemInfo>(
                (value) => new MyComboBoxDataSet<CountryListItemInfo>(value.Name, value),
                await mediator.Send(new GetCountryWithFilterQuery("")),
                TimeSpan.FromSeconds(1),
                async (filter) => await mediator.Send(new GetCountryWithFilterQuery(filter)),
                (value) => { CountryFilter = value?.Value; });

            IsLoading = false;
        }

        [RelayCommand(CanExecute = nameof(ConfirmSelectCanExecute))]
        private async Task ConfirmSelect()
        {
            if (OnItemSelectConfirm is not null)
            {
                await OnItemSelectConfirm.Invoke(SelectedItem!.Id);
            }
        }

        [RelayCommand]
        private async Task Cancel()
        {
            if (OnCancelSelection is not null)
            {
                await OnCancelSelection.Invoke();
            } else
            {
                navigationService.Navigate(-1);
            }
        }

        private bool ConfirmSelectCanExecute()
        {
            return SelectedItem is not null;
        }

        [RelayCommand]
        private async Task UpdateData()
        {
            if (CountryFilter is not null)
            {
                IsLoading = true;
                Info = await mediator.Send(new GetToursByCountryQuery(CountryFilter.Id));
                IsLoading = false;
            } else
            {
                Info = await mediator.Send(new GetAllTourQuery());
            }
        }

        [RelayCommand]
        private void OpenTour(Guid Id)
        {
            navigationService.Navigate<OpenTourViewModel, Guid>(Id, vm =>
            {
                vm.CanSelect = true;
                vm.OnIsSelected += async (id) =>
                {
                    if (OnItemSelectConfirm is not null)
                    {
                        await OnItemSelectConfirm?.Invoke(id)!;
                    }
                };
            });
        }

        public delegate Task ItemSelectConfirmEvent(Guid Id);
        public event ItemSelectConfirmEvent? OnItemSelectConfirm;

        public delegate Task CancelSelectionEvent();
        public event CancelSelectionEvent? OnCancelSelection;
    }
}
