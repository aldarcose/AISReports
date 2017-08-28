using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Reports.Controls
{
    /// <summary>
    /// Пара контролов, связанных в один 
    /// </summary>
    public class ControlPair : Control
    {
        private Control control1 = null;
        private Control control2 = null;
        private bool externalChange = true;

        public ControlPair()
        {
            base.SetStyle(ControlStyles.Selectable, false);
        }

        /// <inheritdoc/>
        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (externalChange)
            {
                if (control1 == null)
                    control1 = e.Control;
                else if (control2 == null)
                    control2 = e.Control;
            }
            base.OnControlAdded(e);
        }

        /// <inheritdoc/>
        protected override void OnControlRemoved(ControlEventArgs e)
        {
            if (externalChange)
            {
                if (control1 == e.Control)
                    control1 = null;
                else if (control2 == e.Control)
                    control2 = null;
            }
            base.OnControlRemoved(e);
        }

        /// <inheritdoc/>
        protected override void OnLayout(LayoutEventArgs levent)
        {
            if ((control1 != null) && (control2 != null))
            {
                int control2X = this.control1.Width + this.control1.Margin.Horizontal;
                int control2Width = (base.Size.Width - control2X) - this.control2.Margin.Horizontal;
                int control2Height = base.Size.Height - this.control2.Margin.Vertical;
                this.control1.SetBounds(
                    this.control1.Margin.Left, 
                    this.control1.Margin.Top, 0, 0, 
                    BoundsSpecified.Location);
                this.control2.SetBounds(
                    control2X + this.control2.Margin.Left, 
                    control2.Margin.Top, 
                    control2Width, control2Height, 
                    BoundsSpecified.All);
            }
            base.OnLayout(levent);
        }

        /// <summary>
        /// Первый контрол
        /// </summary>
        [Category("Appearance"), Description("Первый контрол"), Browsable(true), DefaultValue((string)null)]
        public Control Control1
        {
            get
            {
                return control1;
            }
            set
            {
                if (value != null && value == control2)
                    throw new ArgumentException("Value must be different of control2");

                if (control1 != value)
                {
                    externalChange = false;
                    if (control1 != null)
                        Controls.Remove(this.control1);
                    control1 = value;
                    if (control1 != null)
                        Controls.Add(control1);
                    
                    externalChange = true;
                    Height = Math.Max(Height, control1.Height + Margin.Vertical);
                }
            }
        }

        /// <summary>
        /// Второй контрол
        /// </summary>
        [Category("Appearance"), DefaultValue((string)null), Description("Второй контрол"), Browsable(true)]
        public Control Control2
        {
            get { return control2; }
            set
            {
                if (value != null && value == control1)
                    throw new ArgumentException("Value must be different of control1");
                
                if (control2 != value)
                {
                    externalChange = false;
                    if (control2 != null)
                        Controls.Remove(this.control2);
                    control2 = value;
                    if (control2 != null)
                        Controls.Add(this.control2);

                    externalChange = true;
                    Height = Math.Max(Height, this.control2.Height + Margin.Vertical);
                }
            }
        }
    }
}
