using System;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace Reports.Controls
{
    public class VerticalLayoutPanel : Panel
    {
        private VerticalLayout engine = null;

        public VerticalLayoutPanel()
        {
            base.SetScrollState(2, false);
            this.DoubleBuffered = true;
        }
        
        public override LayoutEngine LayoutEngine
        {
            get
            {
                if (engine == null)
                    engine = new VerticalLayout();
                return engine;
            }
        }
    }
}
