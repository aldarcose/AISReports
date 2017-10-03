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

                object argument = null;
                if (e.SelectedFields != null)
                {
                    argument = new Tuple<IList<ReportField>, IDictionary<string, string>, string>(
                        e.SelectedFields, e.ParametersStringValues, e.SqlQuery);
                }
                else
                {
                    argument = new Tuple<IDictionary<string, string>, IDictionary<string, string>>(
                        e.ParametersStringValues, e.ParametersValues2);
                }
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
            if (loader.AttachedParameters != null)
                view.AddParameters(loader.AttachedParameters);

            export = new ExcelRDExport(conn, loader.WorkBook, reportData.Name);
            if (loader.ReportFields.Count == 0)
                export.SetQueries(loader.ReportQueries);
        }

        private void OnExecuteReport(object sender, DoWorkEventArgs e)
        {
            object argument = e.Argument;
            if (argument is Tuple<IList<ReportField>, IDictionary<string, string>, string>)
            {
                var arg = argument as Tuple<IList<ReportField>, IDictionary<string, string>, string>;
                IList<ReportField> selectedFields = arg.Item1;
                IDictionary<string, string> paramsStringValues = arg.Item2;
                string sqlQuery = arg.Item3;
                export.InitParamsStringValues(paramsStringValues);
                export.InitFields(selectedFields);
                export.InitQuery(sqlQuery);
            }
            else
            {
                var tuple = (Tuple<IDictionary<string, string>, IDictionary<string, string>>)argument;
                var paramStringValues = tuple.Item1;
                var paramsValues2 = tuple.Item2;
                export.InitParameters(paramsValues2);
                export.InitParamsStringValues(paramStringValues);
            }
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
