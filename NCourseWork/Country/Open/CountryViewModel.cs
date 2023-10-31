using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MapsterMapper;
using MediatR;
using NCourseWork.Application.Country.Get;
using NCourseWork.Application.Country.Get.ById;
using NCourseWork.Application.Country.Update;
using NCourseWork.MVVM;
using System;
using System.Threading.Tasks;

namespace NCourseWork.Country.Open
{
    internal partial class OpenCountryViewModel : ObservableObject, IBasePage<Guid>
    {
        #region Services
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        #endregion

        public OpenCountryViewModel(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        #region Properties
        private Guid id;
        public Guid PageData { get => id; set => SetProperty(ref id, value); }

        [ObservableProperty]
        private InputCountryValues? inputValues = null;

        [ObservableProperty]
        private CountryFullInfo? viewInfo = null;

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

            ViewInfo = await mediator.Send(new GetCountryByIdQuery(id)) ?? throw new ArgumentException(nameof(ViewInfo), "Country not found.");

            IsLoading = false;
        }

        [RelayCommand]
        private void StartEdit()
        {
            InputValues = mapper.Map<InputCountryValues>(ViewInfo!);
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

            var cmd = mapper.Map<UpdateCountryInfoCommand>(InputValues!);

            cmd.Id = ViewInfo!.Id;

            await mediator.Send(cmd);

            IsAwaitUpdateChanges = false;

            await GetData();

            IsEdit = false;
        }
        #endregion
    }
}
