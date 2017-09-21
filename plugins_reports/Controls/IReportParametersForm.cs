using System;
using System.Collections.Generic;

namespace Reports
{
    public interface IReportParametersForm
    {
        ReportParameterCollection Value { get; set; }
        Report Report { get; set; }

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
}
