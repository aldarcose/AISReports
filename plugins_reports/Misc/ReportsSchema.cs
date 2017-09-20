using System;
using System.Collections.Generic;

namespace Reports
{
    public class ReportSchema
    {
        public ReportSchema()
        {
            Reports = new List<Report>();
            Folders = new List<Folder>();
        }

        public List<Report> Reports { get; set; }

        public List<Folder> Folders { get; set; }
    }

    public class Folder
    {
        public Folder()
        {
            Reports = new List<Report>();
            Folders = new List<Folder>();
        }

        public Folder(string name, int level)
            : this()
        {
            this.Name = name;
            this.Level = level;
        }

        public string Name { get; set; }

        public int Level { get; set; }

        public List<Report> Reports { get; set; }

        public List<Folder> Folders { get; set; }
    }

    public class Report
    {
        private int id;
        private string name;
        private bool isDesigned;
        private List<ReportParameter> parameters;

        public Report()
        {
            this.parameters = new List<ReportParameter>();
        }

        public Report(int id, string name, bool isDesigned)
            : this()
        {
            this.id = id;
            this.name = name;
            this.isDesigned = isDesigned;
        }

        public int Id { get { return id; } }

        public string Name { get { return name; } }

        /// <summary>Отчет выполнен с помощью мастера отчетов</summary>
        public bool IsDesigned { get { return isDesigned; } }

        public List<ReportParameter> Parameters { get { return parameters; } }
    }

    public class ReportParameter
    {
        public ReportParameter() { }

        public ReportParameter(
            string name, string caption, ReportParameterType type, string sql) 
        {
            this.Name = name;
            this.Caption = caption;
            this.Type = type;
            this.Sql = sql;
        }

        public static ReportParameterType ParseParameterType(string text)
        {
            switch (text)
            {
                case "date" : return ReportParameterType.Date;
                case "date_period" : case "datetime_period" : return ReportParameterType.Period;
                case "int_period": return ReportParameterType.IntPeriod;
                case "float_period": return ReportParameterType.FloatPeriod;
                case "select": return ReportParameterType.Query;
                case "select2": return ReportParameterType.Enum;
                case "text": return ReportParameterType.Text;
                case "boolean": return ReportParameterType.Boolean;
                case "check": return ReportParameterType.CheckExpression;
                case "text_period": return ReportParameterType.VarText;
                case "diagn": return ReportParameterType.Diagn;
                case "time_period": return ReportParameterType.TimePeriod;
                default:
                    return ReportParameterType.Unknown;
            }
        }

        public string Name { get; set; }

        public string Caption { get; set; }

        public ReportParameterType Type { get; set; }

        public string Sql { get; set; }
    }

    public class ReportParameterCollection : List<ReportParameter>
    {
        public ReportParameterCollection(IList<ReportParameter> list)
            : base(list) 
        { 
        }
    }

    /// <summary>
    /// Тип параметра экспорта
    /// </summary>
    public enum ReportParameterType
    {
        /// <summary>Неизвестный</summary>
        Unknown = 0,

        /// <summary>Дата</summary>
        Date = 1,

        /// <summary>Период между дат</summary>
        Period = 2,

        /// <summary>Отрезок целых чисел</summary>
        IntPeriod = 3,

        /// <summary>Отрезок вещественных чисел</summary>
        FloatPeriod = 4,

        /// <summary>Запрос</summary>
        Query = 5,

        /// <summary>Перечисление</summary>
        Enum = 6,

        /// <summary>Текст</summary>
        Text = 7,

        /// <summary>
        /// Логический тип
        /// </summary>
        /// <remarks>
        /// Используется в мастере отчетов. Выражения для "True" и "False" разделяются через строку '~|~'.
        /// </remarks>
        Boolean = 8,

        /// <summary>
        /// Выражение
        /// </summary>
        /// <remarks>
        /// Используется в мастере отчетов. Похоже на тип Boolean, с тем лишь отличием что здесь присутствует выражение только 
        /// при значении "True".
        /// </remarks>
        CheckExpression = 9,

        /// <summary>
        /// Текст с вариантами
        /// </summary>
        VarText = 10,

        /// <summary>
        /// Временной период
        /// </summary>
        TimePeriod = 11,

        /// <summary>
        /// Диагноз по МКБ
        /// </summary>
        Diagn = 12,
    }
}
