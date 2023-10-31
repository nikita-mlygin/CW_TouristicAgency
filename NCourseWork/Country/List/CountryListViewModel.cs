using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MapsterMapper;
using MediatR;
using Microsoft.Xaml.Behaviors.Core;
using NCourseWork.Application.Country.Delete;
using NCourseWork.Application.Country.Get;
using NCourseWork.Application.Country.Get.All;
using NCourseWork.Application.Country.Get.WithNameFilter;
using NCourseWork.Application.Hotel.Get;
using NCourseWork.Common.Components.ComboBox;
using NCourseWork.Country.Add;
using NCourseWork.Country.Open;
using NCourseWork.Domain.Hotel;
using NCourseWork.Hotel;
using NCourseWork.MVVM;
using NCourseWork.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Country.List
{
    internal partial class CountryListViewModel : ObservableObject, IBasePage, ISinglePage
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly INavigationService navigationService;

        public CountryListViewModel(IMediator mediator, IMapper mapper, INavigationService navigationService)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.navigationService = navigationService;
        }

        [ObservableProperty]
        private ObservableCollection<CountryListItemInfo>? countryList;

        [ObservableProperty]
        private string? nameFilter;

        [ObservableProperty]
        private string? climateNameFilter;

        [ObservableProperty]
        private bool isFilter = false;

        [RelayCommand]
        private async Task GetData()
        {
            await UpdateInfo();
        }

        [RelayCommand]
        private async Task UpdateInfo()
        {
            if (NameFilter is not null || ClimateNameFilter is not null)
            {
                IsFilter = true;
                CountryList = new ObservableCollection<CountryListItemInfo>(await mediator.Send(mapper.Map<GetCountryWithFilterQuery>(this)));
            }
            else
            {
                CountryList = new ObservableCollection<CountryListItemInfo>(await mediator.Send(new GetAllCountryQuery()));
            }
        }

        [RelayCommand]
        private async Task ResetFilter()
        {
            IsFilter = false;

            NameFilter = null;
            ClimateNameFilter = null;

            await UpdateInfo();
        }

        [RelayCommand]
        private void OpenCountry(Guid id)
        {
            navigationService.Navigate<OpenCountryViewModel, Guid>(id);
        }

        [RelayCommand]
        private async Task Delete(Guid id)
        {
            await mediator.Send(new DeleteCountryCommand(id));
            CountryList?.Remove(CountryList.Where(x => x.Id == id).First());
        }

        [RelayCommand]
        private void Create()
        {
            navigationService.Navigate<AddCountryViewModel>(vm =>
            {
                vm.OnAdd += id =>
                {
                    navigationService.Close();
                    return Task.CompletedTask;
                };
            });
        }
    }
}
