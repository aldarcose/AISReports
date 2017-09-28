using SharedDbWorker;
using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;

namespace Reports
{
    public interface IOperation
    {
        Tuple<string, IWorkbook> Execute(IProgressControl pc);

        void SetQueries(List<ReportQuery> list);

        void InitParameters(Dictionary<string, Tuple<string, object>> paramValues);
    }
}
