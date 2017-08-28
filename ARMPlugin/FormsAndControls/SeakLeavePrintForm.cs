using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPrinting;
using System.Xml.Linq;

namespace ARMPlugin.FormsAndControls
{
    public partial class SeakLeavePrintForm : DevExpress.XtraEditors.XtraForm
    {
        SeakLeavePrinter slPrinter;
        Dictionary<string, string> values;
        public SeakLeavePrintForm(Dictionary<string, string> values)
        {
            InitializeComponent();
            this.values = values;
            slPrinter = new SeakLeavePrinter(printingSystem, values);
        }
    }

    public class SeakLeavePrinter: Link
    {
        int top = 0;
        Rectangle r = new Rectangle(0, 0, 150, 50);
        string caption = "Test";
        SeakLeavePrintPattern pattern;
        Dictionary<string, string> values;

        public SeakLeavePrinter(PrintingSystem ps, Dictionary<string, string> values)
        {
            Margins = new System.Drawing.Printing.Margins(0, 0, 0, 0);
            PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.pattern = LoadPattern(@"blank\blist.xml");
            this.values = values;
            CreateDocument(ps);
            ShowPreviewDialog();
        }

        public SeakLeavePrinter()
        {
            //pattern = LoadPattern();
        }

        protected override void BeforeCreate()
        {
            base.BeforeCreate();
            if (this.PrintingSystem!=null)
            {
                BrickGraphics g = this.PrintingSystem.Graph;
                g.BackColor = Color.White;
                g.BorderColor = Color.White;
                g.Font = g.DefaultFont;
                g.StringFormat = g.StringFormat.ChangeAlignment(StringAlignment.Near);
                g.PageUnit = GraphicsUnit.Millimeter;
            }
            
            InitControlValues(values);
        }

        protected override void CreateDetail(BrickGraphics graph)
        {
            CreateRows(graph);
        }

        
        public SeakLeavePrintPattern LoadPattern(string xmlFile)
        {
            var xmlDoc = XDocument.Load(xmlFile);
            var xml = xmlDoc.Element("document");
            var pattern = new SeakLeavePrintPattern();
            pattern.PageSize = xml.Attribute("pagesize").Value;
            pattern.Orientation = xml.Attribute("orientation").Value;
            pattern.Step = xml.Attribute("step").Value;
            pattern.Font = xml.Element("font").Attribute("name").Value;
            pattern.FontSize = Convert.ToInt32(xml.Element("font").Attribute("size").Value);
            pattern.FontBold = Convert.ToBoolean(xml.Element("font").Attribute("bold").Value);
            pattern.FontItalic = Convert.ToBoolean(xml.Element("font").Attribute("italic").Value);
            pattern.Lines = new List<StringLine>();
            var xmlLines = xml.Descendants("stringline");
            var lines = new List<StringLine>();
            foreach(var xmlLine in xmlLines)
            {
                var line = new StringLine();
                var controls = new List<PrintControl>();
                if (!string.IsNullOrEmpty(xmlLine.Attribute("y").Value.Replace(".", ",")))
                    line.Y = Convert.ToDecimal(xmlLine.Attribute("y").Value.Replace(".", ","));
                line.Number = xmlLine.Attribute("number").Value;
                foreach(var el in xmlLine.Descendants())
                {
                    var cntl = new PrintControl();
                    switch(el.Name.ToString())
                    {
                        case "text": cntl.PrintControlType = PrintControlType.TEXT; break;
                        case "date": cntl.PrintControlType = PrintControlType.DATE; break;
                        case "checkbox": cntl.PrintControlType = PrintControlType.CHECKBOX; break;
                    }
                    cntl.Name = el.Attribute("name").Value;
                    cntl.Length = Convert.ToInt32( (el.Attribute("length") != null) ? el.Attribute("length").Value : null );
                    cntl.X = Convert.ToDecimal(el.Attribute("x").Value.Replace(".", ","));
                    cntl.Y = Convert.ToDecimal(el.Attribute("y") != null ? el.Attribute("y").Value.Replace(".", ",") : null);
                    if (!cntl.Y.HasValue || cntl.Y.Value==0)
                    {
                        cntl.Y = line.Y;
                    }
                    controls.Add(cntl);
                }
                line.Controls = controls;
                lines.Add(line);
            }
            pattern.Lines = lines;
            return pattern;
        }


        protected void CreateRows(BrickGraphics graph)
        {
            graph.Font = new Font(pattern.Font, pattern.FontSize.Value, FontStyle.Bold);
            foreach(var line in pattern.Lines)
            {
                foreach(var cntl in line.Controls)
                {
                    
                    if(!string.IsNullOrEmpty(cntl.Value))
                    {
                        var size = graph.MeasureString(cntl.Value);
                        size.Width = size.Width + 10;
                        graph.DrawString(cntl.Value, new RectangleF(new PointF((float)cntl.X, (float)cntl.Y), size ));
                    }
                    
                }
            }
            
            
        }

        public void InitControlValues(Dictionary<string, string> values)
        {
            foreach(var line in pattern.Lines)
            {
                foreach(var cntl in line.Controls)
                {
                    if (values.ContainsKey(cntl.Name) && !string.IsNullOrEmpty(values[cntl.Name]))
                        if (cntl.Length.HasValue && values[cntl.Name].Length>cntl.Length && cntl.PrintControlType==PrintControlType.TEXT)
                            cntl.Value = values[cntl.Name].Substring(0, cntl.Length.Value);
                        else
                            cntl.Value = values[cntl.Name];

                }
            }
        }

        

    }

    public class SeakLeavePrintPattern
    {
        public string PageSize { get; set; }
        public string Orientation { get; set; }
        public string Step { get; set; }
        public int? FontSize { get; set; }
        public bool? FontBold { get; set; }
        public string Font { get; set; }
        public bool? FontItalic { get; set; }
        public IEnumerable<StringLine> Lines { get; set; }
    }

    public class StringLine 
    {
        public decimal? Y { get; set; }
        public string Number { get; set; }
        public IEnumerable<PrintControl> Controls { get; set; }
    }

    public class PrintControl
    {
        public PrintControlType PrintControlType {get;set;}
        public decimal? X {get;set;}
        public decimal? Y {get;set;}
        public int? Length {get;set;}
        public string Name {get;set;}
        public string Value { get; set; }
    }

    public enum PrintControlType
    {
        TEXT=0,
        CHECKBOX=1,
        DATE=2
    }

}