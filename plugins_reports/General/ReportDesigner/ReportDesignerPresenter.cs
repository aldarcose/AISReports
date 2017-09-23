using SharedDbWorker;
using Syncfusion.XlsIO;
using System.ComponentModel;

namespace Reports.General.ReportDesigner
{
    /// <summary>
    /// Презентер мастера отчетов
    /// </summary>
    public class ReportDesignerPresenter
    {
        private Connection conn;
        private BackgroundWorker worker;
        private IMainForm mainForm;
        private ReportDesignerLoader loader;
        private ExcelEngine excelEngine;

        public ReportDesignerPresenter(Connection conn, IMainForm mainForm)
        {
            this.conn = conn;
            this.mainForm = mainForm;
            this.worker = new BackgroundWorker();
            this.worker.DoWork += new DoWorkEventHandler(OnExecuteReport);
            this.worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnCompleteReport);
            this.worker.WorkerReportsProgress = true;
        }

        public Report Report { get; set; }

        private void InitExport()
        {
            excelEngine = new ExcelEngine();
            loader = new ReportDesignerLoader(excelEngine);
            loader.Load(Report.Id);
        }

        private void OnExecuteReport(object sender, DoWorkEventArgs e)
        {
        }

        public void OnCompleteReport(object sender, RunWorkerCompletedEventArgs e)
        {
        }
    }
}
