using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MapsterMapper;
using MediatR;
using NCourseWork.Application.Country.Get.WithNameFilter;
using NCourseWork.Application.Hotel.Add;
using NCourseWork.Common.Components.ComboBox;
using NCourseWork.Domain.Hotel;
using NCourseWork.MVVM;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace NCourseWork.Hotel.Add
{
    internal partial class AddHotelViewModel : ObservableObject, IBasePage
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public AddHotelViewModel(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        [ObservableProperty]
        private AddHotelViewInfo addInfo = new();

        [ObservableProperty]
        private bool isLoading = true;

        [ObservableProperty]
        private CustomComboBoxWithRealSearch<MyComboBoxDataSet<Guid>, Guid> countryComboBox = null!;

        [ObservableProperty]
        private ObservableCollection<MyComboBoxDataSet<HotelClass>> hotelClassComboBox = new()
        {
            new (HotelClassHelper.GetHotelClassName(Domain.Hotel.HotelClass.OneStar), Domain.Hotel.HotelClass.OneStar),
            new (HotelClassHelper.GetHotelClassName(Domain.Hotel.HotelClass.TwoStar), Domain.Hotel.HotelClass.TwoStar),
            new (HotelClassHelper.GetHotelClassName(Domain.Hotel.HotelClass.ThreeStar), Domain.Hotel.HotelClass.ThreeStar),
            new (HotelClassHelper.GetHotelClassName(Domain.Hotel.HotelClass.FourStar), Domain.Hotel.HotelClass.FourStar),
            new (HotelClassHelper.GetHotelClassName(Domain.Hotel.HotelClass.FiveStar), Domain.Hotel.HotelClass.FiveStar),
        };

        [ObservableProperty]
        private MyComboBoxDataSet<HotelClass>? hotelClassComboBoxSelectedValue;
        partial void OnHotelClassComboBoxSelectedValueChanged(MyComboBoxDataSet<HotelClass>? value)
        {
            HotelClass = value?.Value;
        }

        public HotelClass? HotelClass { get; set; }

        [RelayCommand]
        private async Task Add()
        {
            IsLoading = true;
            var newId = await mediator.Send(mapper.Map<AddHotelCommand>(AddInfo!));

            OnAdd?.Invoke(newId);

            IsLoading = false;
        }

        [RelayCommand]
        private async Task GetData()
        {
            IsLoading = true;

            CountryComboBox = new CustomComboBoxWithRealSearch<MyComboBoxDataSet<Guid>, Guid>(
                new System.Collections.ObjectModel.ObservableCollection<MyComboBoxDataSet<Guid>>(
                    (await mediator.Send(new GetCountryWithFilterQuery(""))).Select(x => new MyComboBoxDataSet<Guid>(x.Name, x.Id))),
                TimeSpan.FromSeconds(1),
                async filter =>
                {
                    var res = (await mediator.Send(new GetCountryWithFilterQuery(filter)))
                        .Select(x => new MyComboBoxDataSet<Guid>(x.Name, x.Id));

                    return res;
                },
                (selectedValue) =>
                {
                    if (selectedValue is not null)
                    {
                        this.AddInfo.CountryId = selectedValue.Value;
                    }
                });

            IsLoading = false;
        }

        public delegate Task AfterAdd(Guid id);
        public event AfterAdd? OnAdd;
    }
}
