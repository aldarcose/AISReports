using SharedDbWorker;
using SharedDbWorker.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Reports.Controls
{
    /// <summary>
    /// Редактор в виде ComboBox
    /// </summary>
    public class ComboBoxEdit : ComboBox, IParameter
    {
        private List<object> objects = new List<object>();
        private ObjectToText itemToText;

        public ComboBoxEdit()
        {
        }

        [Browsable(false)]
        public string QuerySql { get; set; }

        [Browsable(false)]
        public ObjectToText ItemToText
        {
            get { return itemToText; }
            set { itemToText = value; }
        }

        public List<object> Objects
        {
            get { return objects; }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public virtual object Value
        {
            get
            {
                if (Items.Count == 0)
                    return null;

                if (itemToText != null)
                {
                    if (SelectedIndex >= 0 && SelectedIndex < objects.Count)
                        return objects[SelectedIndex];
                    return SelectedItem;
                }
                return SelectedItem;
            }
            set
            {
                if (value == null && Items.Count > 0)
                {
                    SelectedIndex = 0;
                }
                else if (itemToText != null)
                {
                    if (objects.Contains(value))
                    {
                        int indx = objects.IndexOf(value);
                        SelectedIndex = indx;
                    }
                    else
                    {
                        SelectedIndex = -1;
                        Text = ItemToText(value);
                    }
                }
                else if (Items.Contains(value))
                {
                    SelectedItem = value;
                }
                else
                {
                    SelectedIndex = -1;
                    Text = value.ToString();
                }
            }
        }

        public void DoQuery()
        {
            if (string.IsNullOrEmpty(QuerySql)) return;
            List<object> list = new List<object>();
            List<DbResult> results = null;
            try
            {
                using (var db = new DbWorker())
                {
                    var q = new DbQuery("ComboBox");
                    q.Sql = QuerySql;
                    results = db.GetResults(q);
                }
            }
            catch(Exception ex)
            {
                Trace.TraceWarning("Ошибка выполнения запроса: {0}", ex.Message);
            }

            if (results == null || results.Count == 0) 
                return;
            
            foreach (var dbResult in results) 
                list.Add(dbResult);

            SetObjects(list);
        }

        public void SetObjects(List<object> list)
        {
            base.Items.Clear();
            this.objects.Clear();
            foreach (object o in list)
                objects.Add(o);
            
            foreach (object val in this.objects)
            {
                if (this.ItemToText != null)
                    base.Items.Add(this.ItemToText(val));
                else
                    base.Items.Add(val ?? string.Empty);
            }

            this.SelectedIndex = 0;
            this.SetListWidth();
        }

        private void SetListWidth()
        {
            Graphics g = base.CreateGraphics();
            float max = 0f;
            foreach (object o in base.Items)
            {
                string s = GetItemText(o);
                max = Math.Max(max, g.MeasureString(s, this.Font).Width);
            }
            if (max > base.DropDownWidth)
                base.DropDownWidth = (int)max;
        }

        [Browsable(false)]
        public delegate string ObjectToText(object o);
    }
}
