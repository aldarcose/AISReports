using Syncfusion.XlsIO;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Reports.Controls
{
    public partial class OpenSaveFileForm : Form
    {
        private IWorkbook workBook;

        public OpenSaveFileForm()
        {
            InitializeComponent();
        }

        public OpenSaveFileForm(IWorkbook workBook)
            : this()
        {
            this.workBook = workBook;
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            string extension = workBook.Version == ExcelVersion.Excel97to2003 ? "xls" : "xlsx";
            string path = Path.Combine(Path.GetTempPath(), string.Format("{0}.{1}", Guid.NewGuid(), extension));
            workBook.SaveAs(path, ExcelSaveType.SaveAsXLS);
            Process.Start(path);
            Close();
        }

        private void SaveFileButton_Click(object sender, EventArgs e)
        {
            Close();

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            string extension = workBook.Version == ExcelVersion.Excel97to2003 ? "xls" : "xlsx";
            saveFileDialog.Filter = string.Format("Excel files (*.{0})|*.{0}", extension);
            saveFileDialog.FilterIndex = 2;
            saveFileDialog.RestoreDirectory = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string file = saveFileDialog.FileName;
                if (string.IsNullOrEmpty(file))
                    throw new InvalidOperationException("Не указан файл");
                
                workBook.SaveAs(file, ExcelSaveType.SaveAsXLS);
            }
        }
    }
}
