using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Reports.Controls
{
    /// <summary>
    /// Редактор отрезка целых чисел
    /// </summary>
    public partial class IntPeriodEdit : UserControl, IParameter
    {
        public IntPeriodEdit()
        {
            InitializeComponent();
        }

        /// <summary>Значение с</summary>
        public int ValueIn
        {
            get { return (int)intInEdit.Value; }
        }

        /// <summary>Значение по</summary>
        public int ValueOut
        {
            get { return (int)intOutEdit.Value; }
        }

        public object Value
        {
            get { return new Tuple<int, int>(ValueIn, ValueOut); }
            set
            {
            }
        }
    }
}
