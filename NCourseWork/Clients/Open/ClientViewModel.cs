using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using MapsterMapper;
using MediatR;
using NCourseWork.Application.Client.Delete;
using NCourseWork.Application.Client.Get.ById;
using NCourseWork.Application.Client.Update;
using NCourseWork.MVVM;
using NCourseWork.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NCourseWork.Clients.Open
{
    internal partial class ClientViewModel : ObservableObject, IBasePage<Guid>
    {
        private readonly IMediator mediator;
        private readonly IEnumerable<AbstractValidator<ClientUpdateInputData>> validators;
        private readonly IMapper mapper;
        private readonly INavigationService navigationService;

        public ClientViewModel(IMediator mediator, IEnumerable<AbstractValidator<ClientUpdateInputData>> validators, IMapper mapper, INavigationService navigationService)
        {
            this.mediator = mediator;
            this.validators = validators;
            this.mapper = mapper;
            this.navigationService = navigationService;
        }

        public Guid PageData { get => clientId; set => clientId = value; }

        private Guid clientId;

        [ObservableProperty]
        private ClientViewData? viewData;

        [ObservableProperty]
        private ClientUpdateInputData? inputData;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SetIsEditCommand))]
        [NotifyCanExecuteChangedFor(nameof(CancelChangesCommand))]
        [NotifyCanExecuteChangedFor(nameof(SaveChangesStartCommand))]
        private bool isEdit;

        [ObservableProperty]
        private bool isDataLoading = true;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SetIsEditCommand))]
        [NotifyCanExecuteChangedFor(nameof(SaveChangesStartCommand))]
        [NotifyCanExecuteChangedFor(nameof(CancelChangesCommand))]
        private bool isUpdateLoading;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveChangesStartCommand))]
        private string? validationError;

        [RelayCommand(CanExecute = nameof(CanSetIsEdit))]
        private void SetIsEdit()
        {
            IsEdit = true;

            InputData = mapper.Map<ClientUpdateInputData>(ViewData!);
        }
        private bool CanSetIsEdit() => !IsEdit;

        [RelayCommand]
        private async Task SaveChangesStart()
        {
            IsUpdateLoading = true;

            ValidationContext<ClientUpdateInputData> validationContext = new(InputData ?? throw new ArgumentNullException(nameof(InputData), "Input data must be not null."));

            var errors = validators.Select(x => x.Validate(validationContext))
                .Where(x => !x.IsValid)
                .SelectMany(x => x.Errors)
                .ToArray();

            if (errors.Any())
            {
                IsEdit = false;
                IsUpdateLoading = false;
                ValidationError = errors.First().ErrorMessage;
                return;
            }

            var updateCmd = mapper.Map<UpdateClientCommand>(InputData);
            updateCmd.Id = this.clientId;

            await mediator.Send(updateCmd);

            var newClientData = await mediator.Send(new GetClientByIdQuery(this.clientId));

            this.ViewData = mapper.Map<ClientViewData>(newClientData ?? throw new ArgumentNullException(nameof(newClientData), "Client is not fount"));

            IsUpdateLoading = false;
            IsEdit = false;
        }
        private bool CanSaveChangesStart() => ValidationError is null && IsEdit && !IsUpdateLoading;

        [RelayCommand]
        private void CancelChanges()
        {
            IsEdit = false;
        }
        private bool CanCancelChanges() => IsEdit && !IsUpdateLoading;

        [RelayCommand]  
        private async Task GetClientData()
        {
            IsDataLoading = true;
            var clientData = await mediator.Send(new GetClientByIdQuery(clientId)) ?? throw new NullReferenceException("Client data not found");
            ViewData = mapper.Map<ClientViewData>(clientData);

            IsDataLoading = false;
        }

        [RelayCommand]
        private async Task DeleteClient()
        {
            await mediator.Send(new DeleteClientCommand(PageData));

            navigationService.Close();
        }
    }
}
