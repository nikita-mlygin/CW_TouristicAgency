using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCourseWork.Common.Components.ComboBox
{
    internal class MyComboBoxDataSet<TValue>
    {
        public MyComboBoxDataSet()
        {
        }

        public MyComboBoxDataSet(string name, TValue value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; } = null!;
        public TValue Value { get; set; } = default!;

        public override bool Equals(object? obj)
        {
            if (obj is MyComboBoxDataSet<TValue> dataset)
            {
                return dataset.Name == Name && Equals(dataset.Value, Value);
            }

            return false;
        }
    }
}
