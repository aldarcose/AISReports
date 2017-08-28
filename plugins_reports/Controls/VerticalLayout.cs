using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace Reports.Controls
{
    public class VerticalLayout : LayoutEngine
    {
        /// <inheritdoc/>
        public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
        {
            Control parent = container as Control;
            Rectangle parentDisplayRectangle = parent.DisplayRectangle;
            Size clientSize = parent.ClientSize;
            Rectangle realParentRectangle = new Rectangle(parentDisplayRectangle.X, 
                    parentDisplayRectangle.Y, clientSize.Width, clientSize.Height);
            List<Control> list = new List<Control>();
            foreach (Control c in parent.Controls)
                if (c.Visible) list.Add(c);

            int layoutMinHeight = 0;
            Rectangle proposal = new Rectangle(0, 0, 
                realParentRectangle.Width, 
                realParentRectangle.Height);
            
            foreach (Control c in list)
               layoutMinHeight += c.Size.Height + c.Margin.Top + c.Margin.Bottom;

            int heightFreeSpace = Math.Max(realParentRectangle.Height - layoutMinHeight, 0);
            Point nextControlLocation = realParentRectangle.Location;
            foreach (Control c in list)
            {
                nextControlLocation.Offset(c.Margin.Left, c.Margin.Top);
                c.Location = nextControlLocation;
                c.Width = realParentRectangle.Width - (c.Margin.Left + c.Margin.Right);
                nextControlLocation.X = realParentRectangle.X;
                nextControlLocation.Y += c.Height + c.Margin.Bottom;
            }

            return false;
        }
    }
}
