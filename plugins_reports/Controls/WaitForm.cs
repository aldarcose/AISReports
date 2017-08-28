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
    public partial class WaitForm : Form, IProgressControl
    {
        public WaitForm()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.progressBar1.Style = ProgressBarStyle.Marquee;
        }

        public void SetStatus(string text)
        {
            this.InvokeIfNeeded(() => toolStripStatusLabel1.Text = text);
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

        // Disable close(x) button
        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
    }
}
