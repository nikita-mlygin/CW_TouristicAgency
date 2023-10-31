using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MapsterMapper;
using MediatR;
using Microsoft.Identity.Client;
using NCourseWork.Application.Country.Add;
using NCourseWork.Application.Country.Get;
using NCourseWork.Application.Country.Get.WithNameFilter;
using NCourseWork.Common.Components.ComboBox;
using NCourseWork.MVVM;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NCourseWork.Country.Add
{
    internal partial class AddCountryViewModel : ObservableObject, IBasePage
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public AddCountryViewModel(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [ObservableProperty]
        private AddCountryCommand inputData = new();

        [ObservableProperty]
        private bool isLoading;

        [RelayCommand]
        private async Task AddCountry()
        {
            var data = await mediator.Send(this.InputData!);

            if (OnAdd is not null)
            {
                await this.OnAdd.Invoke(data);
            }
        }

        public delegate Task AfterAdd(Guid id);

        public event AfterAdd? OnAdd;
    }
}
