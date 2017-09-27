using System;
using System.Collections.Generic;

namespace Reports
{
    public interface IReportParametersForm
    {
        ReportParameterCollection Value { get; set; }

        event EventHandler<ParametersValuesEventArgs> OK;
    }

    public class ParametersValuesEventArgs : EventArgs
    {
        private Dictionary<string, Tuple<string, object>> parametersValues;

        public ParametersValuesEventArgs(
            Dictionary<string, Tuple<string, object>> parametersValues)
        {
            this.parametersValues = parametersValues;
        }

        public Dictionary<string, Tuple<string, object>> ParametersValues
        {
            get { return parametersValues; }
        }
    }

    public class ReportDesignerEventArgs : ParametersValuesEventArgs
    {
        private string sqlQuery;
        private IList<ReportField> selectedFields;
        private IDictionary<string, string> parametersStringValues;

        public ReportDesignerEventArgs(
            Dictionary<string, Tuple<string, object>> parametersValues, 
            IList<ReportField> selectedFields,
            IDictionary<string, string> parametersStringValues,
            string sqlQuery)
            : base(parametersValues)
        {
            this.selectedFields = selectedFields;
            this.parametersStringValues = parametersStringValues;
            this.sqlQuery = sqlQuery;
        }

        public IList<ReportField> SelectedFields
        {
            get { return selectedFields; }
        }

        public IDictionary<string, string> ParametersStringValues
        {
            get { return parametersStringValues; }
        }

        public string SqlQuery
        {
            get { return sqlQuery; }
        }
    }
}
