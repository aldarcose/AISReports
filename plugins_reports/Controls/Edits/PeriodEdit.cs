using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Reports.Controls
{
    public partial class PeriodEdit : UserControl, IParameter
    {
        public PeriodEdit()
        {
            InitializeComponent();
        }

        public void SetFormat(DateTimePickerFormat format)
        {
            dateInEdit.Format = format;
            dateOutEdit.Format = format;
        }

        /// <summary>Дата с</summary>
        public DateTime DateIn
        {
            get { return dateInEdit.Value; }
        }

        /// <summary>Дата по</summary>
        public DateTime DateOut
        {
            get { return dateOutEdit.Value; }
        }

        public object Value
        {
            get { return new Tuple<DateTime, DateTime>(DateIn, DateOut); }
        }
    }
}
