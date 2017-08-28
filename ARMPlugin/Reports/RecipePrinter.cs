using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ARMPlugin.FormsAndControls;
using Model.Classes;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Forms;
using ImageList = Model.Classes.ImageList;
using DevExpress.XtraReports.UI;
using DevExpress.XtraPrinting;

namespace ARMPlugin.FormsAndControls
{
    public static class RecipePrinter
    {
        public static void Print(Recipe recipe)
        {
            var cccc = new RecipePrintConfirmation();
            if (cccc.ShowDialog() == DialogResult.OK)
            {
                ImageList list = new ImageList();
                for (int i = 0; i < cccc.Count; i++)
                {
                    var rrr = new RecipeReport_okud3108805(recipe,i+1);
                    rrr.CreateDocument();
                    Image image = null;

                    using (var stream = new MemoryStream())
                    {
                        var imgOptions = new ImageExportOptions
                        {
                            Format = ImageFormat.Png,
                            Resolution = 250,
                            PageBorderWidth = 0
                        };
                        rrr.ExportToImage(stream, imgOptions);
                        image = Image.FromStream(stream);
                    }
                    list.Images.Add(new ImageContainer()
                    {
                        Image = image
                    });
                }
                var zzz = new ImageReport(list);
                zzz.ShowPreview();
                
            }
        }
    }
}
