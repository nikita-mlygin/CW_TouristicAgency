using CommunityToolkit.Mvvm.ComponentModel;
using NCourseWork.MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Services.Navigation
{
    internal partial class BaseLayout : ObservableObject, ILayout
    {
        [ObservableProperty]
        private IBasePage current = null!;
    }
}
