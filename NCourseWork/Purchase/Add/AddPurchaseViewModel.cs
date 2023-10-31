namespace NCourseWork.Purchase.Add
{
    using CommunityToolkit.Mvvm.ComponentModel;
    using CommunityToolkit.Mvvm.Input;
    using MapsterMapper;
    using MediatR;
    using NCourseWork.Application.Client.Get;
    using NCourseWork.Application.Client.Get.ByName;
    using NCourseWork.Application.Purchase.Add;
    using NCourseWork.Application.Tour.Get;
    using NCourseWork.Application.Tour.Get.ById;
    using NCourseWork.Common.Components.ComboBox;
    using NCourseWork.Domain.Client;
    using NCourseWork.Domain.Tour;
    using NCourseWork.MVVM;
    using NCourseWork.Purchase.Open;
    using NCourseWork.Services.Navigation;
    using NCourseWork.Tour.Select;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading.Tasks;

    internal partial class AddPurchaseViewModel : ObservableObject, IBasePage
    {
        public AddPurchaseViewModel(IMediator mediator, IMapper mapper, INavigationService navigationService)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.navigationService = navigationService;

            clientComboBox = null!;
        }

        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly INavigationService navigationService;

        [ObservableProperty]
        private DateTime purchaseDate = DateTime.Now;

        [ObservableProperty]
        private Guid? tourId;

        [ObservableProperty]
        private int weekCount;

        [ObservableProperty]
        private Guid? clientId;

        [ObservableProperty]
        private TourFullInfo? selectedTour;

        [ObservableProperty]
        private CustomComboBoxWithRealSearch<MyComboBoxDataSet<ClientListItemInfo>, ClientListItemInfo> clientComboBox;

        [RelayCommand]
        private async Task GetData()
        {
            ClientComboBox = new CustomComboBoxWithRealSearch<MyComboBoxDataSet<ClientListItemInfo>, ClientListItemInfo>(
                client => new MyComboBoxDataSet<ClientListItemInfo>($"{client.FirstName} {client.LastName} {client.MiddleName}", client), 
                await mediator.Send(new GetClientByNameQuery()),
                TimeSpan.FromSeconds(1),
                async filter =>
                {
                    var (firstName, lastName, middleName) = filter.Split(' ') switch {
                        [var v1] => (v1, null, null),
                        [var v1, var v2] => (v1, v2, null),
                        [var v1, var v2, var v3] => (v1, v2, v3),
                        _ => (null, null, null)
                    };

                    return await mediator.Send(new GetClientByNameQuery(firstName, lastName, middleName)); 
                },
                newValue =>
                {
                    ClientId = newValue?.Value.Id;
                });
        }

        [RelayCommand]
        private async Task Add()
        {
            var cmd = mapper.Map<AddPurchaseCommand>(this);

            var newEntityId = await mediator.Send(cmd);

            if (OnAddPurchase is not null)
            {
                await OnAddPurchase.Invoke(newEntityId);
            }
            else
            {
                navigationService.Navigate<PurchaseViewModel, Guid>(newEntityId);
            }
        }

        [RelayCommand]
        private async Task Cancel()
        {
            if (OnCancelAddPurchase is not null)
            {
                await OnCancelAddPurchase();
            }
            else
            {
                navigationService.Navigate(-1);
            }
        }

        [RelayCommand]
        private void SelectTour() {
            navigationService.Navigate<TourSelectViewModel>(
                vm =>
                {
                    vm.OnItemSelectConfirm += async id =>
                    {
                        TourId = id;
                        SelectedTour = await mediator.Send(new GetTourByIdQuery(id));
                        navigationService.Navigate(-1);
                    };
                });
        }

        [RelayCommand]
        private void AddClient() { }

        public delegate Task AddPurchaseEvent(Guid id);
        public event AddPurchaseEvent? OnAddPurchase;

        public delegate Task CancelAddPurchaseEvent();
        public event CancelAddPurchaseEvent? OnCancelAddPurchase;
    }
}
