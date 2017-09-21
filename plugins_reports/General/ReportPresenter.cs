using Reports.Controls;
using SharedDbWorker;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reports
{
    public class ReportPresenter
    {
        // Соединение с базой данных
        private Connection conn;
        private IReportParametersForm parameters;
        private BackgroundWorker worker;
        private ReportLoader reportLoader;
        private IOperation export;
        private ExcelEngine excelEngine;
        private ProgressForm progressForm;
        private OpenSaveFileForm openSaveFileForm;
        private IMainForm mainForm;

        public ReportPresenter(Connection conn, IReportParametersForm parameters, IMainForm mainForm)
        {
            this.conn = conn;
            this.parameters = parameters;
            this.mainForm = mainForm;
            parameters.OK += view_OK;
            this.worker = new BackgroundWorker();
            this.worker.DoWork += new DoWorkEventHandler(OnExecuteReport);
            this.worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnCompleteReport);
            this.worker.WorkerReportsProgress = true;

            var lf = new LoadingForm
            {
                WaitForThis = new Task(() =>
                {
                    InitExport();
                })
            };
            lf.ShowDialog();
        }

        private void InitExport()
        {
            excelEngine = new ExcelEngine();
            reportLoader = new ReportLoader(excelEngine);
            reportLoader.Load(parameters.Report.Id);
            export = new ExcelExport(conn, reportLoader.WorkBook);
            export.SetQueries(reportLoader.ReportQueries);
        }

        private void view_OK(object sender, ParametersValuesEventArgs e)
        {
            if (!worker.IsBusy)
            {
                mainForm.Disable();
                progressForm = new ProgressForm();
                progressForm.Owner = (Form)mainForm;
                progressForm.Show();
                worker.RunWorkerAsync(e.ParametersValues);
            }
        }

        private void OnExecuteReport(object sender, DoWorkEventArgs e)
        {
            var paramValues = e.Argument as Dictionary<string, Tuple<string, object>>;
            export.InitParameters(paramValues);
            e.Result = export.Execute(progressForm);
        }

        private void OnCompleteReport(object sender, RunWorkerCompletedEventArgs e)
        {
            progressForm.Close();
            conn.Dispose();
            IWorkbook workBook = (IWorkbook)e.Result;

            mainForm.Enable();

            openSaveFileForm = new OpenSaveFileForm(workBook);
            openSaveFileForm.ShowDialog((Form)mainForm);

            reportLoader.Dispose();
        }
    }
}
