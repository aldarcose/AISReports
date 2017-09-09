using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reports.Controls
{
    public partial class LoadingForm : Form
    {
        public Task WaitForThis { set; get; }

        public LoadingForm()
        {
            InitializeComponent();
        }

        private void LoadingForm_Shown(object sender, EventArgs e)
        {
            Refresh();
            WaitForThis.Start();
            try
            {
                WaitForThis.Wait();
                Close();
            }
            catch (AggregateException ex)
            {
                Hide();
                MessageBox.Show(String.Join("\n\n", ex.InnerExceptions.Select(i => i.InnerException == null ? i.Message : i.InnerException.Message)), "Ошибка инициализации", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }
    }
}
