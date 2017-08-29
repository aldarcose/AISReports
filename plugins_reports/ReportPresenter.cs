using Reports.Controls;
using SharedDbWorker;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace Reports
{
    public class ReportPresenter
    {
        private IReportParametersForm view;
        private BackgroundWorker worker;
        private ReportLoader reportLoader;
        private IOperation export;
        private ExcelEngine excelEngine;
        private WaitForm waitForm;
        private OpenSaveFileForm openSaveFileForm;

        public ReportPresenter(IReportParametersForm view)
        {
            this.view = view;
            view.OK += view_OK;
            this.worker = new BackgroundWorker();
            this.worker.DoWork += new DoWorkEventHandler(OnExecuteReport);
            this.worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnCompleteReport);
            this.worker.WorkerReportsProgress = true;

            this.waitForm = new WaitForm();
            InitExport();
        }

        private void InitExport()
        {
            excelEngine = new ExcelEngine();
            reportLoader = new ReportLoader(excelEngine);
            reportLoader.Load(view.Report.Id);
            export = new ExcelExport(reportLoader.WorkBook);
            export.SetQueries(reportLoader.ExportQueries);
        }

        private void view_OK(object sender, ParametersValuesEventArgs e)
        {
            worker.RunWorkerAsync(e.ParametersValues);
            waitForm.ShowDialog();
        }

        private void OnExecuteReport(object sender, DoWorkEventArgs e)
        {
            var paramValues = e.Argument as Dictionary<string, string>;
            export.InitParameters(paramValues);
            e.Result = export.Execute(waitForm);
        }

        private void OnCompleteReport(object sender, RunWorkerCompletedEventArgs e)
        {
            waitForm.Dispose();
            IWorkbook workBook = (IWorkbook)e.Result;
            openSaveFileForm = new OpenSaveFileForm(workBook);
            openSaveFileForm.ShowDialog();
            reportLoader.Dispose();
        }
    }
}
