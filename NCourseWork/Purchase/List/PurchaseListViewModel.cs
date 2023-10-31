using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MapsterMapper;
using MediatR;
using NCourseWork.Application.Client.Get;
using NCourseWork.Application.Client.Get.ByName;
using NCourseWork.Application.Purchase.Delete;
using NCourseWork.Application.Purchase.Get;
using NCourseWork.Application.Purchase.Get.WithFilter;
using NCourseWork.Common.Components.ComboBox;
using NCourseWork.Domain.Client;
using NCourseWork.MVVM;
using NCourseWork.Purchase.Add;
using NCourseWork.Purchase.Open;
using NCourseWork.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace NCourseWork.Purchase.List
{
    internal partial class PurchaseListViewModel : ObservableObject, IBasePage, ISinglePage
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly INavigationService navigationService;

        [ObservableProperty]
        private ObservableCollection<PurchaseListInfo>? purchaseList;

        [ObservableProperty]
        private Guid? clientFilter;
        
        [ObservableProperty]
        private DateTime? startDateTimeFilter;
        
        [ObservableProperty]
        private DateTime? endDateTimeFilter;

        [ObservableProperty]
        private bool isFilter = false;

        
        [ObservableProperty]
        private CustomComboBoxWithRealSearch<MyComboBoxDataSet<ClientListItemInfo>, ClientListItemInfo>? clientFilterComboBox;

        public PurchaseListViewModel(IMediator mediator, IMapper mapper, INavigationService navigationService)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.navigationService = navigationService;
        }

        [RelayCommand]
        private async Task GetData()
        {
            ClientFilterComboBox ??= new CustomComboBoxWithRealSearch<MyComboBoxDataSet<ClientListItemInfo>, ClientListItemInfo>(
                x => new MyComboBoxDataSet<ClientListItemInfo>($"{x.FirstName} {x.LastName} {x.MiddleName}", x),
                await mediator.Send(new GetClientByNameQuery()),
                TimeSpan.FromSeconds(1),
                async filter =>
                {
                    var (fn, ln, mn) = filter.Split(' ') switch
                    {
                        [var v1] => (v1, null, null),
                        [var v1, var v2] => (v1, v2, null),
                        [var v1, var v2, var v3] => (v1, v2, v3),
                        _ => (null, null, null)
                    };

                    return await mediator.Send(new GetClientByNameQuery(fn, ln, mn));
                },
                nv => ClientFilter = nv?.Value.Id);

            await UpdateData();
        }

        [RelayCommand]
        private async Task UpdateData()
        {
            if (IsFilterCheck())
            {
                IsFilter = true;
                PurchaseList = new ObservableCollection<PurchaseListInfo>(await mediator.Send(mapper.Map<GetPurchaseWithFilterQuery>(this)));
                return;
            }

            PurchaseList = new ObservableCollection<PurchaseListInfo>(await mediator.Send(new GetPurchaseWithFilterQuery()));
        }

        private bool IsFilterCheck()
        {
            return ClientFilter is not null || StartDateTimeFilter is not null || EndDateTimeFilter is not null;
        }

        [RelayCommand]
        private async Task ResetFilter()
        {
            (ClientFilter, StartDateTimeFilter, EndDateTimeFilter) = (null, null, null);
            ClientFilterComboBox?.Reset();
            await UpdateData();
        }

        [RelayCommand]
        private async Task Delete(Guid id)
        {
            await mediator.Send(new DeletePurchaseCommand(id));
            PurchaseList?.Remove(PurchaseList.Where(x => x.Id == id).First());
        }

        [RelayCommand]
        private void OpenPurchase(Guid id)
        {
            navigationService.Navigate<PurchaseViewModel, Guid>(id);
        }

        [RelayCommand]
        private void Create()
        {
            navigationService.Navigate<AddPurchaseViewModel>(vm =>
            {
                vm.OnAddPurchase += async id =>
                {
                    await UpdateData();
                    navigationService.Close();
                };
            });
        }
    }
}
