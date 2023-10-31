using Mapster;
using NCourseWork.Common.Layouts.Navigation;
using NCourseWork.Common.Layouts.StepNavigation;
using NCourseWork.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Common.Layouts
{
    internal class DefaultLayout : BasePageSetting
    {
        public DefaultLayout()
        {
            this.Add<NavigationLayout>()
                .Add<StepNavigationLayout>();
        }
    }
}
