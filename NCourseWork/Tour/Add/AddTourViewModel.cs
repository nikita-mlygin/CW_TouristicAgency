using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MapsterMapper;
using MediatR;
using NCourseWork.Application.Hotel.Get;
using NCourseWork.Application.Hotel.Get.ById;
using NCourseWork.Application.Hotel.Get.NameFilter;
using NCourseWork.Application.Tour.Add;
using NCourseWork.Common.Components.ComboBox;
using NCourseWork.Hotel.Add;
using NCourseWork.MVVM;
using NCourseWork.Services.Navigation;
using NCourseWork.Tour.Open;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Tour.Add
{
    internal partial class AddTourViewModel : ObservableObject, IBasePage
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly INavigationService navigationService;

        public AddTourViewModel(IMediator mediator, IMapper mapper, INavigationService navigationService)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.navigationService = navigationService;

            hotelComboBox = null!;
        }

        [ObservableProperty]
        private Guid? hotelId;

        [ObservableProperty]
        private double pricePerWeek;

        [ObservableProperty]
        private CustomComboBoxWithRealSearch<MyComboBoxDataSet<HotelListInfo>, HotelListInfo> hotelComboBox;

        [RelayCommand]
        private async Task GetData()
        {
            HotelComboBox ??= new CustomComboBoxWithRealSearch<MyComboBoxDataSet<HotelListInfo>, HotelListInfo>(
                hotel => new MyComboBoxDataSet<HotelListInfo>(hotel.HotelName, hotel),
                await mediator.Send(new GetHotelsByNameFilterQuery("")),
                TimeSpan.FromSeconds(1),
                async filter =>
                {
                    return await mediator.Send(new GetHotelsByNameFilterQuery(filter));
                },
                newValue =>
                {
                    HotelId = newValue?.Value.Id;
                });
        }

        [RelayCommand]
        private void AddHotel()
        {
            navigationService.Navigate<AddHotelViewModel>(
                vm =>
                {
                    vm.OnAdd += async id =>
                    {
                        navigationService.Navigate(-1);

                        var newHotel = await mediator.Send(new GetHotelByIdQuery(id));

                        if (newHotel is null)
                        {
                            return;
                        }

                        HotelComboBox.SetNewValue(new(newHotel.HotelName, new HotelListInfo(newHotel.Id, newHotel.HotelName)));
                    };
                });
        }

        [RelayCommand]
        private async Task Add()
        {
            var cmd = mapper.Map<AddTourCommand>(this);
            var newId = await mediator.Send(cmd);

            if (OnAddEvent is not null)
            {
                await OnAddEvent.Invoke(newId);
                return;
            }

            navigationService.Navigate<OpenTourViewModel, Guid>(newId);
        }

        [RelayCommand]
        private async Task Cancel()
        {
            if (OnCancel is not null)
            {
                await OnCancel.Invoke();
                return;
            }

            navigationService.Navigate(-1);
        }

        public delegate Task AddEvent(Guid id);
        public event AddEvent? OnAddEvent;

        public delegate Task CancelEvent();
        public event CancelEvent? OnCancel;
    }
}
