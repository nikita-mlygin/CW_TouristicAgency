using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MapsterMapper;
using MediatR;
using Microsoft.Identity.Client;
using NCourseWork.Application.Status.Get;
using NCourseWork.Application.Status.Update;
using NCourseWork.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Status.Open
{
    internal partial class OpenStatusViewModel : ObservableObject, IBasePage<Guid>
    {
        #region Services
        private readonly IMediator mediator;
        private readonly IMapper mapper; 
        #endregion

        public OpenStatusViewModel(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        #region Properties
        private Guid id;
        public Guid PageData { get => id; set => SetProperty(ref id, value); }

        [ObservableProperty]
        private OpenStatusInputValue? inputValues = null;

        [ObservableProperty]
        private StatusFullInfo? viewInfo = null;

        [ObservableProperty]
        private bool isLoading = true;

        [ObservableProperty]
        private bool isAwaitUpdateChanges = false;

        [ObservableProperty]
        private bool isEdit = false;
        #endregion

        #region Commands
        [RelayCommand]
        private async Task GetData()
        {
            IsLoading = true;

            ViewInfo = await mediator.Send(new GetStatusByIdQuery(id)) ?? throw new ArgumentException(nameof(ViewInfo), "Status not found.");

            IsLoading = false;
        }

        [RelayCommand]
        private void StartEdit()
        {
            InputValues = mapper.Map<OpenStatusInputValue>(ViewInfo!);
            IsEdit = true;
        }

        [RelayCommand]
        private void CancelEdit()
        {
            IsEdit = false;
        }

        [RelayCommand]
        private async Task ConfirmUpdate()
        {
            IsAwaitUpdateChanges = true;

            var cmd = mapper.Map<UpdateStatusCommand>(InputValues!);

            cmd.Id = ViewInfo!.Id;

            await mediator.Send(cmd);

            IsAwaitUpdateChanges = false;

            await GetData();

            IsEdit = false;
        } 
        #endregion
    }
}
