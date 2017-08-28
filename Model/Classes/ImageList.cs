using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Classes
{
    public class ImageList
    {
        public ImageList ()
        {
            Images = new List<ImageContainer>();
        }
        public List<ImageContainer> Images { get; set; }
    }

    public class ImageContainer
    {
        public Image Image { get; set; }
    }
}
