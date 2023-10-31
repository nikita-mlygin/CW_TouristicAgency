using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.MVVM
{
    public interface IBasePage
    {
    }

    public interface IBasePage<T> : IBasePage
    {
        public T PageData { get; set; }
    }
}
