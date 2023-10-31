using NCourseWork.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Services.Navigation
{
    public interface IPageSetting
    {
        public IList<Type> Layouts { get; }
        public bool IsDefault { get; }
    }

    public interface IPageSetting<TPage> : IPageSetting
        where TPage : IBasePage
    {
    }
}
