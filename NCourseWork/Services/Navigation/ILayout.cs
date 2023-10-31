using NCourseWork.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Services.Navigation
{
    internal interface ILayout : INotifyPropertyChanged, IBasePage
    {
        public IBasePage Current { get; set; }
    }
}
