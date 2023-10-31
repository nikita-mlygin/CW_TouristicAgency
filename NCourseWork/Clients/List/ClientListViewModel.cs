using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MapsterMapper;
using MediatR;
using NCourseWork.Application.Client.Delete;
using NCourseWork.Application.Client.Get;
using NCourseWork.Application.Client.Get.ByName;
using NCourseWork.Clients.Open;
using NCourseWork.MVVM;
using NCourseWork.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation.Peers;

namespace NCourseWork.Clients.List
{
    internal partial class ClientListViewModel : ObservableObject, IBasePage, ISinglePage
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly INavigationService navigationService;

        [ObservableProperty]
        private string? nameFilter;

        [ObservableProperty]
        private bool isFilter;

        [ObservableProperty]
        private ObservableCollection<ClientListItemInfo>? clientList;

        public ClientListViewModel(IMediator mediator, IMapper mapper, INavigationService navigationService)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.navigationService = navigationService;
        }

        [RelayCommand]
        private async Task UpdateData()
        {
            if (NameFilter is not null)
            {
                IsFilter = true;

                var (fn, ln, mn) = NameFilter.Split(' ') switch
                {
                    [var v1] => (v1, null, null),
                    [var v1, var v2] => (v1, v2, null),
                    [var v1, var v2, var v3] => (v1, v2, v3),
                    _ => (null, null, null)
                };

                ClientList = new ObservableCollection<ClientListItemInfo>(await mediator.Send(new GetClientByNameQuery(fn, ln, mn)));
            } else
            {
                ClientList = new ObservableCollection<ClientListItemInfo>(await mediator.Send(new GetClientByNameQuery()));
            }
        }

        [RelayCommand]
        private async Task GetData()
        {
            await UpdateData();
        }

        [RelayCommand]
        private async Task ResetFilter()
        {
            IsFilter = false;
            NameFilter = null;

            await UpdateData();
        }

        [RelayCommand]
        private void OpenClientView(Guid id)
        {
            navigationService.Navigate<ClientViewModel, Guid>(id);
        }

        [RelayCommand]
        private async Task Delete(Guid id)
        {
            await mediator.Send(new DeleteClientCommand(id));
            ClientList?.Remove(ClientList.Where(x => x.Id == id).First());
        }
    }
}
