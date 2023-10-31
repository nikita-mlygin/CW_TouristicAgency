using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MapsterMapper;
using MediatR;
using Microsoft.IdentityModel.Abstractions;
using NCourseWork.Application.Country.Get;
using NCourseWork.Application.Country.Get.All;
using NCourseWork.Application.Country.Get.WithNameFilter;
using NCourseWork.Application.Tour.Delete;
using NCourseWork.Application.Tour.Get;
using NCourseWork.Application.Tour.Get.All;
using NCourseWork.Application.Tour.Get.WithFilter;
using NCourseWork.Common.Components.ComboBox;
using NCourseWork.Domain.Hotel;
using NCourseWork.Hotel;
using NCourseWork.MVVM;
using NCourseWork.Services.Navigation;
using NCourseWork.Tour.Add;
using NCourseWork.Tour.Open;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Tour.List
{
    internal partial class TourListViewModel : ObservableObject, IBasePage, ISinglePage
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly INavigationService navigationService;

        public TourListViewModel(IMediator mediator, IMapper mapper, INavigationService navigationService)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.navigationService = navigationService;
        }


        [ObservableProperty]
        private ObservableCollection<TourListItemInfo>? tourList;


        [ObservableProperty]
        private Guid? countryFilter;


        [ObservableProperty]
        private HotelClass? hotelClassFilter;

        [ObservableProperty]
        private MyComboBoxDataSet<HotelClass>? hotelClassFilterItemSelected;
        partial void OnHotelClassFilterItemSelectedChanged(MyComboBoxDataSet<HotelClass>? value)
        {
            HotelClassFilter = value?.Value;
        }


        [ObservableProperty]
        private string? hotelNameFilter;

        [ObservableProperty]
        private int? minPriceFilter;

        [ObservableProperty]
        private int? maxPriceFilter;


        [ObservableProperty]
        private bool isFilter = false;


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

        [RelayCommand]
        private async Task GetData()
        {
            CountryFilterComboBox = new CustomComboBoxWithRealSearch<MyComboBoxDataSet<CountryListItemInfo>, CountryListItemInfo>(
                x => new MyComboBoxDataSet<CountryListItemInfo>(x.Name, x),
                await mediator.Send(new GetAllCountryQuery()),
                TimeSpan.FromSeconds(1),
                async filter =>
                {
                    return await mediator.Send(new GetCountryWithFilterQuery(filter));
                },
                nv => CountryFilter = nv?.Value.Id);

            await UpdateData();
        }

        [RelayCommand]
        private async Task UpdateData()
        {
            if (IsFilterCheck())
            {
                IsFilter = true;
                TourList = new ObservableCollection<TourListItemInfo>(await mediator.Send(mapper.Map<GetTourWithFilterQuery>(this)));
                return;
            }

            TourList = new ObservableCollection<TourListItemInfo>(await mediator.Send(new GetAllTourQuery()));
        }

        private bool IsFilterCheck()
        {
            return CountryFilter is not null || HotelNameFilter is not null || HotelClassFilter is not null || MinPriceFilter is not null || MaxPriceFilter is not null;
        }

        [RelayCommand]
        private async Task ResetFilter()
        {
            (CountryFilter, HotelNameFilter, HotelClassFilter, MinPriceFilter, MaxPriceFilter) = (null, null, null, null, null);
            await UpdateData();
        }

        [RelayCommand]
        private async Task Delete(Guid id)
        {
            await mediator.Send(new DeleteTourCommand(id));
            TourList?.Remove(TourList.Where(x => x.Id == id).First());
        }

        [RelayCommand]
        private void OpenTour(Guid id)
        {
            navigationService.Navigate<OpenTourViewModel, Guid>(id);
        }

        [RelayCommand]
        private void Create()
        {
            navigationService.Navigate<AddTourViewModel>(vm =>
            {
                vm.OnAddEvent += async id =>
                {
                    await UpdateData();
                    navigationService.Close();
                };
            });
        }
    }
}
