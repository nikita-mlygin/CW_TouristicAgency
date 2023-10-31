using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NCourseWork.Common.Components.ComboBox
{
    internal partial class CustomComboBoxWithRealSearch<TDataSet, TValue> : ObservableObject
        where TDataSet : MyComboBoxDataSet<TValue>
    {
        [ObservableProperty]
        private string searchString = "";
        partial void OnSearchStringChanged(string value)
        {
            if (SelectedItem is not null && value == SelectedItem.Name)
            {
                return;
            }

            StartSearchNewDataSet(value ?? "");

            SearchInCurrentDataSet(value ?? "");
        }

        [ObservableProperty]
        private TDataSet? selectedItem = null;
        partial void OnSelectedItemChanged(TDataSet? value)
        {
            onSelectedItemChange?.Invoke(value);
        }

        [ObservableProperty]
        private ObservableCollection<TDataSet> visibleValues;

        [ObservableProperty]
        private ObservableCollection<TDataSet> currentValues;

        public Action<TDataSet?>? onSelectedItemChange;

        partial void OnCurrentValuesChanged(ObservableCollection<TDataSet> value)
        {
            VisibleValues = value;
        }

        [ObservableProperty]
        private TimeSpan delay = TimeSpan.FromMicroseconds(100);

        private CancellationTokenSource? searchCancellationTokenSource;

        private readonly Func<string, Task<IEnumerable<TDataSet>>> updateResponse;

        public CustomComboBoxWithRealSearch(ObservableCollection<TDataSet> currentValues, TimeSpan delay, Func<string, Task<IEnumerable<TDataSet>>> updateResponse, Action<TDataSet?>? onSelectedItemChange = null)
        {
            this.currentValues = currentValues;
            visibleValues = currentValues;
            this.delay = delay;
            this.updateResponse = updateResponse;
            this.onSelectedItemChange = onSelectedItemChange;
        }

        public CustomComboBoxWithRealSearch(Func<TValue, TDataSet> mapFunc, IEnumerable<TValue> currentValues, TimeSpan delay, Func<string, Task<IEnumerable<TValue>>> updateResponse, Action<TDataSet?>? onSelectedItemChange = null)
        {
            this.currentValues = new ObservableCollection<TDataSet>(currentValues.Select(x => mapFunc(x)));
            visibleValues = this.currentValues;
            this.delay = delay;
            this.updateResponse = async (filter) => {
                return new ObservableCollection<TDataSet>((await updateResponse(filter)).Select(x => mapFunc(x)));
            };

            this.onSelectedItemChange = onSelectedItemChange;
        }


        protected virtual void StartSearchNewDataSet(string value)
        {
            searchCancellationTokenSource?.Cancel();
            searchCancellationTokenSource = new CancellationTokenSource();
            var token = searchCancellationTokenSource.Token;

            Task.Delay(Delay, token).ContinueWith(async _ =>
            {
                if (!token.IsCancellationRequested)
                {
                    var newDataSet = await updateResponse.Invoke(value);

                    if (!token.IsCancellationRequested)
                    {
                        CurrentValues = new ObservableCollection<TDataSet>(newDataSet);
                    }
                }
            });
        }

        private void SearchInCurrentDataSet(string value)
        {
            VisibleValues = new ObservableCollection<TDataSet>(CurrentValues.Where(x => x.Name.Contains(value)));
        }

        [RelayCommand]
        public void SetNewValue(TDataSet newValue)
        {
            if (!CurrentValues.Contains(newValue))
            {
                CurrentValues.Add(newValue);
                VisibleValues.Add(newValue);
            }

            SelectedItem = newValue;
            SearchString = newValue.Name;
        }

        [RelayCommand]
        public void Reset()
        {
            SelectedItem = null;
            SearchString = "";
        }
    }
}
