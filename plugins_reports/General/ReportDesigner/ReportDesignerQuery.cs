using System;
using System.Collections.Generic;

namespace Reports
{
    public class ReportDesignerQuery : ReportQuery
    {
        private bool isJoinExpression;
        private const string LEFTJOINEXPR = "left join";

        public ReportDesignerQuery(string rawSQL)
            : base(rawSQL)
        {
        }

        /// <inheritdoc/>
        protected override void ParseSQL()
        {
            if (rawSQL.IndexOf(LEFTJOINEXPR, StringComparison.InvariantCultureIgnoreCase) >= 0)
            {
                this.innerSQL = rawSQL.Substring(rawSQL.IndexOf(LEFTJOINEXPR,
                    StringComparison.InvariantCultureIgnoreCase));
                this.name = GetQueryName(rawSQL, LEFTJOINEXPR);
                this.isJoinExpression = true;
            }

            if (!isJoinExpression) base.ParseSQL();
        }

        public bool IsJoinExpression
        {
            get { return isJoinExpression; }
        }
    }
}
