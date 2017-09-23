using System;
using System.Collections.Generic;

namespace Reports
{
    public class ReportDesignerQuery : ReportQuery
    {
        private bool isJoinExpression;

        public ReportDesignerQuery(string rawSQL)
            : base(rawSQL)
        {
        }

        /// <inheritdoc/>
        protected override void ParseSQL()
        {
            if (rawSQL.IndexOf("join", StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                this.innerSQL = rawSQL.Substring(rawSQL.IndexOf("join",
                    StringComparison.InvariantCultureIgnoreCase));
                this.name = GetQueryName(rawSQL, "select");
                this.isJoinExpression = true;
            }

            base.ParseSQL();
        }

        public bool IsJoinExpression
        {
            get { return isJoinExpression; }
        }
    }
}
