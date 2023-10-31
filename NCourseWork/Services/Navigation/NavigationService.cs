using CommunityToolkit.Mvvm.ComponentModel;
using NCourseWork.Country.Add;
using NCourseWork.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks.Dataflow;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Input;

namespace NCourseWork.Services.Navigation
{
    internal class NavigationService : ObservableObject, INavigationService
    {
        private class NullLayout : BaseLayout
        { }

        /// <summary>
        /// Index of active page in history.
        /// </summary>
        // When first add, its must be 0
        private int currentIndex = -1;
        private readonly Func<Type, IBasePage> factory;

        private readonly IList<IBasePage> history = new List<IBasePage>();
        private ILayout lastLayout;
        private ILayout current;
        private IPageSetting? defaultPageSettings = null;

        public IBasePage Current { get => current; }

        public IPageSetting? BasePageSetting { get => defaultPageSettings; set => SetupSettings(value); }

        private void SetupSettings(IPageSetting? settings)
        {
            defaultPageSettings = settings;
            var prevCurrent = lastLayout.Current;

            if (settings is null)
            {
                defaultPageSettings = null;
                lastLayout = new NullLayout
                {
                    Current = prevCurrent
                };
                current = lastLayout;

                return;
            }

            lastLayout = factory(settings.Layouts[0]) as ILayout ?? throw new ApplicationException("Layout setup error");
            current = lastLayout;

            for (var i = 1; i < settings.Layouts.Count; i++)
            {
                lastLayout!.Current = factory(settings.Layouts[i]) as ILayout ?? throw new ApplicationException("Layout setup error");
                lastLayout = lastLayout.Current as ILayout ?? throw new ApplicationException("Layout setup error");
            }

            lastLayout.Current = prevCurrent;
        }

        public void Navigate<TPage>(Action<TPage>? d = null) where TPage : IBasePage
        {
            var viewModel = (TPage)factory.Invoke(typeof(TPage));
            d?.Invoke(viewModel);
            Navigate(viewModel as IBasePage);
        }
        
        public void Navigate<TPage, TData>(TData data, Action<TPage>? d = null) where TPage : IBasePage<TData>
        {
            var nCurrent = (TPage)factory.Invoke(typeof(TPage));
            d?.Invoke(nCurrent);
            nCurrent.PageData = data;
            this.Navigate(nCurrent as IBasePage);
        }

        public void Navigate<TPage>(TPage viewModel, Action<TPage>? d = null) where TPage : IBasePage
        {
            d?.Invoke(viewModel);

            this.Navigate(viewModel as IBasePage);
        }

        public void Navigate<TPage, TData>(TPage viewModel, TData data, Action<TPage>? d = null) where TPage : IBasePage<TData>
        {
            viewModel.PageData = data;
            d?.Invoke(viewModel);
            this.Navigate(viewModel as IBasePage);
        }

        private void Navigate(IBasePage page, IPageSetting? pageSetting = null) 
        {
            var index = history.IndexOf(page);

            if (index != -1)
            {
                this.Navigate(index - currentIndex);
                return;
            }

            for (int i = history.Count - 1; i > currentIndex; i--)
            {
                history.RemoveAt(i);
            }

            history.Add(page);
            currentIndex++;

            lastLayout.Current = history[currentIndex];

            OnPropertyChanged(nameof(Current));
        }


        public void Navigate(int step)
        {
            _ = history.ElementAtOrDefault(currentIndex + step) ?? throw new ArgumentOutOfRangeException(nameof(step), "No prev page");
            currentIndex += step;
            lastLayout.Current = history[currentIndex];
            OnPropertyChanged(nameof(Current));
        }

        public bool CanNavigate(int step)
        {
            return history.ElementAtOrDefault(currentIndex + step) is not null;
        }

        public void Close()
        {
            history.RemoveAt(currentIndex);
            this.currentIndex--;
            this.lastLayout.Current = history[currentIndex];
            OnPropertyChanged(nameof(Current));
        }

        public void ResetHistory()
        {
            history.Clear();
            lastLayout.Current = null!;
            currentIndex = -1;
        }

        public void Init()
        {
            var dataTemplate = new DataTemplate
            {
                DataType = typeof(NullLayout)
            };

            var frameworkElementFactory = new FrameworkElementFactory(typeof(NullLayoutView));

            dataTemplate.VisualTree = frameworkElementFactory;

            System.Windows.Application.Current.Resources.Add(new DataTemplateKey(typeof(NullLayout)), dataTemplate);
            OnPropertyChanged(nameof(Current));
        }

        public NavigationService(Func<Type, IBasePage> factory, IPageSetting? basePageSetting = null)
        {
            this.factory = factory;
            lastLayout = new NullLayout();
            BasePageSetting = basePageSetting;
            current = lastLayout;
        }
    }
}
