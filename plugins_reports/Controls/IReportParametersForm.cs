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
        private IDictionary<string, string> parametersValues2;

        public ReportDesignerEventArgs(
            IList<ReportField> selectedFields,
            IDictionary<string, string> parametersStringValues,
            string sqlQuery) 
            : base(null)
        {
            this.selectedFields = selectedFields;
            this.parametersStringValues = parametersStringValues;
            this.sqlQuery = sqlQuery;
        }

        public ReportDesignerEventArgs(
            IDictionary<string, string> parametersValues2,
            IDictionary<string, string> parametersStringValues)
            : this(null, parametersStringValues, null)
        {
            this.parametersValues2 = parametersValues2;
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

        public IDictionary<string, string> ParametersValues2
        {
            get { return parametersValues2; }
        }
    }
}