using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MapsterMapper;
using MediatR;
using NCourseWork.Application.Client.Add;
using NCourseWork.Clients.Open;
using NCourseWork.MVVM;
using NCourseWork.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Clients.Add
{
    internal partial class AddClientViewModel : ObservableObject, IBasePage
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly INavigationService navigation;
        
        [ObservableProperty]
        private string? firstName;
        
        [ObservableProperty]
        private string? lastName;
        
        [ObservableProperty]
        private string? middleName;
        
        [ObservableProperty]
        private string? phoneNumber;
        
        [ObservableProperty]
        private string? address;

        public AddClientViewModel(IMediator mediator, IMapper mapper, INavigationService navigation)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.navigation = navigation;
        }

        [RelayCommand]
        private async Task Add()
        {
            var newId = await mediator.Send(mapper.Map<AddClientCommand>(this));
            
            if (OnAdd is not null)
            {
                await OnAdd(newId);
                return;
            }

            navigation.Navigate<ClientViewModel, Guid>(newId);
        }

        [RelayCommand]
        private async Task Cancel()
        {
            if (OnCancel is not null)
            {
                await OnCancel();
                return;
            }

            navigation.Navigate(-1);
        }

        public delegate Task AddEvent(Guid id);
        public event AddEvent? OnAdd;

        public delegate Task CancelEvent();
        public event CancelEvent? OnCancel;
    }
}
