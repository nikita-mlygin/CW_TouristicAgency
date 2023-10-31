using NCourseWork.MVVM;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace NCourseWork.Services.Navigation
{
    public interface INavigationService : INotifyPropertyChanged
    {
        public IPageSetting? BasePageSetting { get; set; }
        public IBasePage Current { get; }

        public void Init();

        public void Navigate(int step);
        public bool CanNavigate(int step);

        public void Navigate<TPage, TData>(TData data, Action<TPage>? d = null) where TPage : IBasePage<TData>;
        public void Navigate<TPage>(Action<TPage>? d = null) where TPage : IBasePage;
        public void Navigate<TPage>(TPage viewModel, Action<TPage>? d = null) where TPage : IBasePage;
        public void Navigate<TPage, TData>(TPage viewModel, TData data, Action<TPage>? d = null) where TPage : IBasePage<TData>;
        
        public void ResetHistory();
        public void Close();
    }
}
