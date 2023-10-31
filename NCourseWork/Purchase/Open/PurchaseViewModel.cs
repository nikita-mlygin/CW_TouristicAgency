using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MapsterMapper;
using MediatR;
using Microsoft.Identity.Client;
using NCourseWork.Application.Purchase.Get;
using NCourseWork.Application.Purchase.Get.ById;
using NCourseWork.Application.Purchase.Update;
using NCourseWork.Application.Tour.Get;
using NCourseWork.Application.Tour.Get.ById;
using NCourseWork.MVVM;
using NCourseWork.Services.Navigation;
using NCourseWork.Tour.Select;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Purchase.Open
{
    internal partial class PurchaseViewModel : ObservableObject, IBasePage<Guid>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly INavigationService navigationService;

        public PurchaseViewModel(IMediator mediator, IMapper mapper, INavigationService navigationService)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.navigationService = navigationService;
        }

        private Guid id;
        public Guid PageData { get => id; set => id = value; }

        [ObservableProperty]
        private bool isLoading = true;

        [ObservableProperty]
        private bool isEdit = false;

        [ObservableProperty]
        private bool isSaving = false;

        [ObservableProperty]
        private PurchaseFullInfo? info;

        [ObservableProperty]
        private PurchaseUpdateInfo? updateInfo;

        [RelayCommand]
        private async Task GetData()
        {
            IsLoading = true;

            Info = await mediator.Send(new GetPurchaseByIdQuery(PageData)) ?? throw new ArgumentException("Purchase with this id is not found");

            IsLoading = false;
        }

        [RelayCommand]
        private void StartEdit()
        {
            UpdateInfo = mapper.Map<PurchaseUpdateInfo>(Info!);
            IsEdit = true;
        }

        [RelayCommand]
        private async Task Save()
        {
            IsSaving = true;
            var id = PageData;

            var cmd = mapper.Map<UpdatePurchaseCommand>(UpdateInfo!);
            cmd.Id = id;
            await mediator.Send(cmd);

            Info = await mediator.Send(new GetPurchaseByIdQuery(id)) ?? throw new ApplicationException();

            IsSaving = false;
            IsEdit = false;
        }

        [RelayCommand]
        private void CancelEdit()
        {
            IsEdit = false;
        }

        [RelayCommand]
        private Task SelectTour()
        {
            navigationService.Navigate<TourSelectViewModel>(d =>
            {
                d.OnItemSelectConfirm += async (id) =>
                {
                    UpdateInfo!.SelectedTourViewInfo = await mediator.Send(new GetTourByIdQuery(id));
                    UpdateInfo!.TourId = id;
                    navigationService.Navigate(this);
                };
            });

            return Task.CompletedTask;
        }
    }
}
