using Reports.Controls;
using SharedDbWorker;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reports
{
    /// <summary>
    /// Презентер мастера отчетов
    /// </summary>
    public class ReportDesignerPresenter
    {
        private Connection conn;
        private BackgroundWorker worker;
        private IMainForm mainForm;
        private IReportDesignerForm view;
        private ProgressForm progressForm;
        private ReportDesignerLoader loader;
        private ExcelEngine excelEngine;
        private ExcelRDExport export;
        private Report reportData;
        private OpenSaveFileForm openSaveFileForm;

        public ReportDesignerPresenter(
            Connection conn, IMainForm mainForm, IReportDesignerForm view, Report reportData)
        {
            this.conn = conn;
            this.mainForm = mainForm;
            this.view = view;
            this.reportData = reportData;
            this.worker = new BackgroundWorker();
            this.worker.DoWork += new DoWorkEventHandler(OnExecuteReport);
            this.worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnCompleteReport);
            this.worker.WorkerReportsProgress = true;
            view.CreateReport += view_CreateReport;

            var lf = new LoadingForm
            {
                WaitForThis = new Task(() =>
                {
                    InitExport();
                })
            };
            lf.ShowDialog();
        }

        private void view_CreateReport(object sender, ReportDesignerEventArgs e)
        {
            if (!worker.IsBusy)
            {
                mainForm.Disable();
                progressForm = new ProgressForm();
                progressForm.Owner = (Form)mainForm;
                progressForm.Show();
                var argument = new Tuple<IList<ReportField>, IDictionary<string, string>, string>(
                    e.SelectedFields, e.ParametersStringValues, e.SqlQuery);
                worker.RunWorkerAsync(argument);
            }
        }

        private void InitExport()
        {
            excelEngine = new ExcelEngine();
            loader = new ReportDesignerLoader(excelEngine);
            loader.Load(reportData.Id);
            view.SetReportFields(loader.ReportFields);
            view.SetReportQueries(loader.ReportQueries);

            if (loader.AttachedFields != null)
                view.AddFields(loader.AttachedFields);
            if (loader.AttachedtParameters != null)
                view.AddParameters(loader.AttachedtParameters);

            export = new ExcelRDExport(conn, loader.WorkBook, reportData.Name);
        }

        private void OnExecuteReport(object sender, DoWorkEventArgs e)
        {
            var argument = (Tuple<IList<ReportField>, IDictionary<string, string>, string>)e.Argument;
            IList<ReportField> selectedFields = argument.Item1;
            IDictionary<string, string> paramsStringValues = argument.Item2;
            string sqlQuery = argument.Item3;
            export.InitParamsStringValues(paramsStringValues);
            export.InitFields(selectedFields);
            export.InitQuery(sqlQuery);
            
            e.Result = export.Execute(progressForm);
        }

        public void OnCompleteReport(object sender, RunWorkerCompletedEventArgs e)
        {
            progressForm.Close();
            conn.Dispose();

            var result = (Tuple<string, IWorkbook>)e.Result;
            string errorMessage = result.Item1;
            IWorkbook workBook = result.Item2;
            
            mainForm.Enable();

            if (!string.IsNullOrEmpty(errorMessage))
            {
                var errorMessageForm = new QueryForm(errorMessage);
                errorMessageForm.Text = "Ошибки при выполнении запроса";
                errorMessageForm.ShowDialog();
            } 
            else if (workBook != null)
            {
                openSaveFileForm = new OpenSaveFileForm(workBook);
                openSaveFileForm.ShowDialog((Form)mainForm);
            }

            loader.Dispose();
        }
    }
}
