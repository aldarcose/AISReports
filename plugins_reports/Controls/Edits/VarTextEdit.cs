using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Reports.Controls
{
    public partial class VarTextEdit : UserControl, IParameter
    {
        private ComparisonType comparisonType;

        public VarTextEdit()
        {
            InitializeComponent();
            cbOperationType.DisplayMember = "Description";
            cbOperationType.ValueMember = "Value";
            cbOperationType.DataSource = Enum.GetValues(typeof(ComparisonType))
            .Cast<Enum>()
            .Select(value => new
            {
                (Attribute.GetCustomAttribute(
                    value.GetType().GetField(value.ToString()),
                    typeof(DescriptionAttribute)) as DescriptionAttribute).Description,
                    value
            })
            .OrderBy(item => item.value)
            .ToList();
        }

        /// <summary>Текст</summary>
        public new string Text
        {
            get { return textBox.Text; }
        }

        /// <summary>Тип сравнения</summary>
        public ComparisonType ComparisonType
        {
            get
            {
                Enum.TryParse<ComparisonType>(
                    cbOperationType.SelectedValue.ToString(),
                    out comparisonType);
                return comparisonType; 
            }
        }

        public object Value
        {
            get { return new Tuple<ComparisonType, string>(ComparisonType, Text); }
        }
    }

    public enum ComparisonType
    {
        [Description("содержит")]
        Contains = 0,
        [Description("начинается с")]
        StartsWith = 1,
        [Description("заканчивается на")]
        EndsWith = 2,
        [Description("равно")]
        Equels = 3,
        [Description("пусто")]
        IsEmpty = 4,
        [Description("не пусто")]
        IsNotEmty = 5
    }
}
