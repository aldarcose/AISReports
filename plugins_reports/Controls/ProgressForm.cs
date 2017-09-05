using SharedDbWorker;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reports.Controls
{
    public partial class ProgressForm : Form, IProgressControl
    {
        private string status;

        public ProgressForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.progressBar1.Style = ProgressBarStyle.Marquee;
        }

        public void SetStatus(string text)
        {
            this.InvokeIfNeeded(() => label2.Text = text);
        }

        public void SetMaximum(int maximum)
        {
            this.InvokeIfNeeded(() => progressBar1.Style = ProgressBarStyle.Continuous);
            this.InvokeIfNeeded(() => progressBar1.Maximum = maximum);
        }

        public void SetProgress(int progress)
        {
            this.InvokeIfNeeded(() => progressBar1.Value = progress);
        }
    }
}
