using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MapsterMapper;
using MediatR;
using NCourseWork.Application.Country.Get;
using NCourseWork.Application.Country.Get.ById;
using NCourseWork.Application.Country.Get.WithNameFilter;
using NCourseWork.Application.Hotel.Get;
using NCourseWork.Application.Hotel.Get.ById;
using NCourseWork.Application.Hotel.Update;
using NCourseWork.Clients.Open;
using NCourseWork.Common.Components.ComboBox;
using NCourseWork.Country.Add;
using NCourseWork.Domain.Hotel;
using NCourseWork.MVVM;
using NCourseWork.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Hotel.Open
{
    internal partial class HotelViewModel : ObservableObject, IBasePage<Guid>
    {
        private readonly INavigationService navigationService;
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public HotelViewModel(IMediator mediator, IMapper mapper, INavigationService navigationService)
        {
            this.mediator = mediator;
            this.mapper = mapper;

            countryComboBox = null!;
            this.navigationService = navigationService;
        }

        private Guid hotelId;
        public Guid PageData { get => hotelId; set => SetProperty(ref hotelId, value); }

        [ObservableProperty]
        private CustomComboBoxWithRealSearch<MyComboBoxDataSet<Guid>, Guid> countryComboBox;

        [ObservableProperty]
        private HotelFullInfo? viewInfo = null;

        [ObservableProperty]
        private HotelUpdateViewInfo? updateInfo = null;

        [ObservableProperty]
        private bool isLoading = true;

        [ObservableProperty]
        private bool isEdit = false;

        [ObservableProperty]
        private bool isUpdateLoading = false;

        [RelayCommand]
        private Task StartEdit()
        {
            IsEdit = true;

            UpdateInfo = mapper.Map<HotelUpdateViewInfo>(ViewInfo!);

            CountryComboBox.SetNewValue(new MyComboBoxDataSet<Guid>(ViewInfo!.Country.Name, ViewInfo.Country.Id));

            return Task.CompletedTask;
        }

        [RelayCommand]
        private Task CancelUpdate()
        {
            IsEdit = false;
            return Task.CompletedTask;
        }

        [RelayCommand]
        private async Task UpdateData()
        {
            var cmd = mapper.Map<UpdateHotelCommand>(UpdateInfo!);

            cmd.Id = this.PageData;

            await mediator.Send(cmd);

            IsEdit = false;

            this.ViewInfo = await mediator.Send(new GetHotelByIdQuery(PageData));
        }

        [RelayCommand]
        private async Task GetData()
        {
            IsLoading = true;

            CountryComboBox = new CustomComboBoxWithRealSearch<MyComboBoxDataSet<Guid>, Guid>(
                new System.Collections.ObjectModel.ObservableCollection<MyComboBoxDataSet<Guid>>(
                    (await mediator.Send(new GetCountryWithFilterQuery("")))
                        .Select(x => new MyComboBoxDataSet<Guid>(x.Name, x.Id))),
                TimeSpan.FromSeconds(1),
                async (filter) =>
                {
                    return (await mediator.Send(new GetCountryWithFilterQuery(filter)))
                        .Select(x => new MyComboBoxDataSet<Guid>(x.Name, x.Id));
                },
                (newValue) =>
                {
                    if (UpdateInfo is not null)
                    {
                        UpdateInfo.CountryId = newValue?.Value ?? default;
                    }
                });

            ViewInfo = await mediator.Send(new GetHotelByIdQuery(hotelId));

            IsLoading = false;
        }

        [RelayCommand]
        private void AddNewCountry()
        {
            navigationService.Navigate<AddCountryViewModel>((d) => d.OnAdd += OnAddCountryAsync);
        }

        private async Task OnAddCountryAsync(Guid id)
        {
            navigationService.Navigate(this);

            var newCountry = await mediator.Send(new GetCountryByIdQuery(id));

            if (newCountry is null)
            {
                return;
            }

            CountryComboBox.SetNewValue(new(newCountry.Name, newCountry.Id));
        }
    }
}
