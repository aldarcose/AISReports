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
        private Dictionary<string, string> parametersValues;

        public ParametersValuesEventArgs(
            Dictionary<string, string> parametersValues)
        {
            this.parametersValues = parametersValues;
        }

        public Dictionary<string, string> ParametersValues
        {
            get { return parametersValues; }
        }
    }
}
