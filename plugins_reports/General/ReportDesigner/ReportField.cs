﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reports
{
    public class ReportField
    {
        private string name;
        private string caption;
        private string groupCaption;
        private string expression;
        private List<string> tables;

        public ReportField(
            string name, 
            string caption,
            string groupCaption, 
            string expression, 
            string sections)
        {
            this.name = name;
            this.caption = caption;
            this.groupCaption = groupCaption;
            this.expression = expression;
            this.tables  = new List<string>(sections.Split(';')) ;
        }

        public string Name 
        { 
            get { return name; } 
        }

        public string Caption
        {
            get { return caption; }
        }

        public string GroupCaption
        {
            get { return groupCaption; }
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
