using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Reports.Controls
{
    /// <summary>
    /// Редактор отрезка целых чисел
    /// </summary>
    public partial class FloatPeriodEdit : UserControl, IParameter
    {
        public FloatPeriodEdit()
        {
            InitializeComponent();
        }

        /// <summary>Значение с</summary>
        public decimal ValueIn
        {
            get { return floatInEdit.Value; }
        }

        /// <summary>Значение по</summary>
        public decimal ValueOut
        {
            get { return floatOutEdit.Value; }
        }

        public object Value
        {
            get { return new Tuple<decimal, decimal>(ValueIn, ValueOut); }
        }
    }
}
