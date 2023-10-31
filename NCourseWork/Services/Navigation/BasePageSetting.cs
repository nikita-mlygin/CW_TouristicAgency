using CommunityToolkit.Mvvm.ComponentModel;
using NCourseWork.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xceed.Wpf.AvalonDock.Layout.Serialization;

namespace NCourseWork.Services.Navigation
{
    internal abstract class BasePageSetting : ObservableObject, IPageSetting
    {
        private readonly IList<Type> layouts = new List<Type>();
        private bool isDefault = false;

        public IList<Type> Layouts => layouts;

        public bool IsDefault => isDefault;

        public BasePageSetting Add<TLayout>()
            where TLayout : ILayout
        {
            layouts.Add(typeof(TLayout));

            return this;
        }

        public BasePageSetting UseDefault()
        {
            if (layouts.Count != 0)
            {
                throw new InvalidOperationException("Can use default layout with any layouts");
            }

            isDefault = true;

            return this;
        }
    }
}
