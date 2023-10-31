using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MapsterMapper;
using MediatR;
using NCourseWork.Application.Country.Get.ById;
using NCourseWork.Application.Country.Get.WithNameFilter;
using NCourseWork.Application.Country.Update;
using NCourseWork.Application.Hotel.Get.ById;
using NCourseWork.Application.Hotel.Get.NameFilter;
using NCourseWork.Application.Tour.Get;
using NCourseWork.Application.Tour.Get.ById;
using NCourseWork.Application.Tour.Update;
using NCourseWork.Common.Components.ComboBox;
using NCourseWork.Country.Add;
using NCourseWork.Hotel.Add;
using NCourseWork.MVVM;
using NCourseWork.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Tour.Open
{
    internal partial class OpenTourViewModel : ObservableObject, IBasePage<Guid>
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly INavigationService navigationService;

        public OpenTourViewModel(IMapper mapper, IMediator mediator, INavigationService navigationService)
        {
            this.mapper = mapper;
            this.mediator = mediator;

            hotelComboBox = null!;
            this.navigationService = navigationService;
        }

        [ObservableProperty]
        private CustomComboBoxWithRealSearch<MyComboBoxDataSet<Guid>, Guid> hotelComboBox;

        [ObservableProperty]
        private TourFullInfo? info;

        [ObservableProperty]
        private UpdateTourViewInfo? updateTourInfo;


        [ObservableProperty]
        private bool isLoading = true;

        [ObservableProperty]
        private bool isEdit = false;

        [ObservableProperty]
        private bool isSaving = false;

        private Guid tourId;

        public Guid PageData { get => tourId; set => tourId = value; }

        [ObservableProperty]
        private bool canSelect = false;

        public delegate Task IsSelected(Guid id);
        public event IsSelected? OnIsSelected;

        [RelayCommand]
        private async Task GetData()
        {
            IsLoading = true;
            Info = await mediator.Send(new GetTourByIdQuery(PageData));

            HotelComboBox = new CustomComboBoxWithRealSearch<MyComboBoxDataSet<Guid>, Guid>(
                new System.Collections.ObjectModel.ObservableCollection<MyComboBoxDataSet<Guid>>(
                    (await mediator.Send(new GetHotelsByNameFilterQuery("")))
                        .Select(x => new MyComboBoxDataSet<Guid>(x.HotelName, x.Id))),
                TimeSpan.FromSeconds(1),
                async (filter) =>
                {
                    return (await mediator.Send(new GetHotelsByNameFilterQuery(filter)))
                        .Select(x => new MyComboBoxDataSet<Guid>(x.HotelName, x.Id));
                },
                (newValue) =>
                {
                    if (UpdateTourInfo is not null)
                    {
                        UpdateTourInfo.HotelId = newValue?.Value ?? default;
                    }
                });

            IsLoading = false;
        }

        [RelayCommand]
        private void StartEdit()
        {
            UpdateTourInfo = mapper.Map<UpdateTourViewInfo>(Info!);
            HotelComboBox.SetNewValue(new MyComboBoxDataSet<Guid>(Info!.HotelFullInfo.HotelName, Info.HotelFullInfo.Id));
            IsEdit = true;
        }

        [RelayCommand]
        private void CancelEdit()
        {
            IsEdit = false;
        }

        [RelayCommand]
        private void AddNewHotel()
        {
            navigationService.Navigate<AddHotelViewModel>((d) => d.OnAdd += OnAddHotelAsync);
        }

        private async Task OnAddHotelAsync(Guid id)
        {
            navigationService.Navigate(-1);

            var newHotel = await mediator.Send(new GetHotelByIdQuery(id));

            if (newHotel is null)
            {
                return;
            }

            HotelComboBox.SetNewValue(new(newHotel.HotelName, newHotel.Id));
        }

        [RelayCommand]
        private async Task ConfirmUpdate()
        {
            IsSaving = true;

            var id = PageData;

            var cmd = mapper.Map<UpdateTourCommand>(UpdateTourInfo!);

            cmd.Id = id;

            await mediator.Send(cmd);

            Info = await mediator.Send(new GetTourByIdQuery(id));

            IsSaving = false;
            IsEdit = false;
        }

        [RelayCommand]
        private async Task Select()
        {
            if (OnIsSelected is not null)
            {
                await OnIsSelected.Invoke(PageData);
            }
        }

        [RelayCommand]
        private Task Back()
        {
            navigationService.Navigate(-1);
            return Task.CompletedTask;
        }
    }
}
