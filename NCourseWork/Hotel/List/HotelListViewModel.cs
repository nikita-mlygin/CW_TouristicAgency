using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MapsterMapper;
using MediatR;
using Microsoft.Xaml.Behaviors.Core;
using NCourseWork.Application.Country.Get;
using NCourseWork.Application.Country.Get.All;
using NCourseWork.Application.Country.Get.WithNameFilter;
using NCourseWork.Application.Hotel.Delete;
using NCourseWork.Application.Hotel.Get;
using NCourseWork.Application.Hotel.Get.All;
using NCourseWork.Application.Hotel.Get.WithFilter;
using NCourseWork.Common.Components.ComboBox;
using NCourseWork.Domain.Hotel;
using NCourseWork.Hotel.Add;
using NCourseWork.Hotel.Open;
using NCourseWork.MVVM;
using NCourseWork.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace NCourseWork.Hotel.List
{
    internal partial class HotelListViewModel : ObservableObject, IBasePage, ISinglePage
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly INavigationService navigationService;

        [ObservableProperty]
        private ObservableCollection<HotelListInfo>? hotelList;
        
        [ObservableProperty]
        private string? hotelNameFilter;

        [ObservableProperty]
        private MyComboBoxDataSet<HotelClass>? hotelClassFilterComboBoxSelectedItem;
        partial void OnHotelClassFilterComboBoxSelectedItemChanged(MyComboBoxDataSet<HotelClass>? value)
        {
            HotelClassFilter = value?.Value;
        }

        [ObservableProperty]
        private HotelClass? hotelClassFilter;
        
        [ObservableProperty]
        private Guid? countryFilter;

        
        [ObservableProperty]
        private bool isFilter;

        [ObservableProperty]
        private CustomComboBoxWithRealSearch<MyComboBoxDataSet<CountryListItemInfo>, CountryListItemInfo>? countryFilterComboBox;

        [ObservableProperty]
        private ObservableCollection<MyComboBoxDataSet<HotelClass>> hotelClassFilterComboBox = new()
        {
            new (HotelClassHelper.GetHotelClassName(HotelClass.OneStar), HotelClass.OneStar),
            new (HotelClassHelper.GetHotelClassName(HotelClass.TwoStar), HotelClass.TwoStar),
            new (HotelClassHelper.GetHotelClassName(HotelClass.ThreeStar), HotelClass.ThreeStar),
            new (HotelClassHelper.GetHotelClassName(HotelClass.FourStar), HotelClass.FourStar),
            new (HotelClassHelper.GetHotelClassName(HotelClass.FiveStar), HotelClass.FiveStar),
        };

        public HotelListViewModel(IMediator mediator, IMapper mapper, INavigationService navigationService)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.navigationService = navigationService;
        }

        [RelayCommand]
        private async Task GetData()
        {
            await UpdateData();

            CountryFilterComboBox ??= new CustomComboBoxWithRealSearch<MyComboBoxDataSet<CountryListItemInfo>, CountryListItemInfo>(
               x => new MyComboBoxDataSet<CountryListItemInfo>(x.Name, x),
               await mediator.Send(new GetAllCountryQuery()),
               TimeSpan.FromSeconds(1),
               async filter => await mediator.Send(new GetCountryWithFilterQuery(filter)),
               nV => CountryFilter = nV?.Value.Id);
        }

        [RelayCommand]
        private async Task UpdateData()
        {
            if (IsFilterCheck())
            {
                IsFilter = true;
                HotelList = new ObservableCollection<HotelListInfo>(await mediator.Send(mapper.Map<GetHotelsWithFilterQuery>(this)));
                return;
            }

            HotelList = new ObservableCollection<HotelListInfo>(await mediator.Send(new GetAllHotelQuery()));
        }

        private bool IsFilterCheck()
        {
            return HotelNameFilter is not null || CountryFilter is not null || HotelClassFilter is not null;
        }

        [RelayCommand]
        private async Task ResetFilter()
        {
            IsFilter = false;
            HotelNameFilter = null;

            CountryFilterComboBox?.Reset();
            CountryFilter = null;

            HotelClassFilterComboBoxSelectedItem = null;
            HotelClassFilter = null;

            await UpdateData();
        }

        [RelayCommand]
        private void OpenHotel(Guid id)
        {
            navigationService.Navigate<HotelViewModel, Guid>(id);
        }

        [RelayCommand]
        private async Task DeleteHotel(Guid id)
        {
            await mediator.Send(new DeleteHotelCommand(id));
            HotelList?.Remove(HotelList.Where(x => x.Id == id).First());
        }

        [RelayCommand]
        private void Create()
        {
            navigationService.Navigate<AddHotelViewModel>(vm =>
            {
                vm.OnAdd += async value =>
                {
                    await UpdateData();
                    navigationService.Close();
                };
            });
        }
    }
}
