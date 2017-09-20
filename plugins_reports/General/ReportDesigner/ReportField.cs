using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports
{
    public class ReportField
    {
        private string name;
        private string groupName;
        private string expression;
        private List<string> tables;

        public ReportField(
            string name, 
            string groupName, 
            string expression, 
            string sections)
        {
            this.name = name;
            this.groupName = groupName;
            this.expression = expression;
            this.tables  = new List<string>(sections.Split(';')) ;
        }

        public string Name 
        { 
            get { return name; } 
        }

        public string GroupName
        {
            get { return groupName; }
        }

        public string Expression
        {
            get { return expression; }
        }

        public List<string> Tables
        {
            get { return tables; }
        }
    }
}
