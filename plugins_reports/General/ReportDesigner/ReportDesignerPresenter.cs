using Reports.Controls;
using SharedDbWorker;
using Syncfusion.XlsIO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

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
        private ReportDesignerLoader loader;
        private ExcelEngine excelEngine;
        private Report report;

        public ReportDesignerPresenter(
            Connection conn, IMainForm mainForm, IReportDesignerForm view, Report report)
        {
            this.conn = conn;
            this.mainForm = mainForm;
            this.view = view;
            this.report = report;
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
            loader = new ReportDesignerLoader(excelEngine);
            loader.Load(report.Id);
            view.SetReportFields(loader.ReportFields);
            view.SetReportQueries(loader.ReportQueries);
        }

        private void OnExecuteReport(object sender, DoWorkEventArgs e)
        {
        }

        public void OnCompleteReport(object sender, RunWorkerCompletedEventArgs e)
        {
        }
    }
}
