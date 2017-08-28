using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Model.Classes;
using Model;
using Model.Classes.DopDisp;
using DevExpress.XtraLayout;
using DevExpress.XtraEditors.Controls;

namespace ARMPlugin.FormsAndControls
{
    public partial class DopDispQuestionsForm : DevExpress.XtraEditors.XtraForm
    {
        Patient patient;
        Operator loggedUser;
        QuestinaryType qtype;
        Questionary repo;
        QuestionaryAnswer answer;

        public DopDispQuestionsForm(Patient patient, Operator loggedUser, QuestinaryType qType, long totalId)
        {
            InitializeComponent();
            this.patient = patient;
            this.loggedUser = loggedUser;
            this.qtype = qType;
            repo = new Questionary();
            InitForm();

            answer = repo.GetAnswer(totalId);
            if (answer==null)
            {
                InitAnswer(answer);
            }
        }

        public DopDispQuestionsForm(Patient patient, Operator loggedUser, QuestinaryType qType, QuestionaryAnswer answer)
        {
            InitializeComponent();
            this.patient = patient;
            this.loggedUser = loggedUser;
            this.qtype = qType;
            repo = new Questionary();
            InitForm();
            this.answer = answer;
            InitAnswer(answer);
        }

        private void InitAnswer(QuestionaryAnswer answer)
        {
            var props = repo.GetItems();
            foreach(var prop in props)
            {
                var value = (prop.GetValue(answer, null)!=null) ? prop.GetValue(answer, null).ToString() : null;
                var name = prop.Name;
                var controls = this.Controls.Find(name, true);
                if (controls!=null)
                {
                    foreach(var control in controls)
                    {
                        if (control.GetType()==typeof(RadioGroup))
                        {
                            var rGroup = control as RadioGroup;
                            var item = rGroup.Properties.Items.Where(c => c.Description == value).FirstOrDefault();
                            if (item!=null)
                            {
                                rGroup.EditValue = value;
                            }
                        }

                        if (control.Name.StartsWith("add"))
                        {
                            (control as TextEdit).Text = value;
                        }
                    }
                }

            }
        }

        private void ReadAnwers()
        {

            var props = repo.GetItems();
            foreach (var prop in props)
            {
                //var value = (prop.GetValue(answer, null) != null) ? prop.GetValue(answer, null).ToString() : null;
                var name = prop.Name;
                var controls = this.Controls.Find(name, true);
                if (controls!=null)
                {
                    foreach(var control in controls)
                    {
                        if (control.GetType()==typeof(RadioGroup))
                        {
                            var rGroup = control as RadioGroup;
                            prop.SetValue(answer, rGroup.EditValue, null);
                        }
                        if (control.GetType()==typeof(TextEdit))
                        {
                            var txtEdit = control as TextEdit;
                            if (!string.IsNullOrWhiteSpace(txtEdit.Text))
                                prop.SetValue(answer, txtEdit.Text, null);
                        }
                    }

                }

            }
            

        }


        private void InitForm()
        {

            var questions = repo.GetQuestions(qtype);
            foreach(var item in questions)
            {
                LayoutControlItem lItem = this.layoutControl1.Root.AddItem();
                //Control lbl = new LabelControl();
                //lbl.Text = item.Number+". "+ item.Text;
                //lbl.MaximumSize = new Size(this.Size.Width, 100);
                //lItem.Control = lbl;
                lItem.Text = item.Number + ". " + item.Text;
                lItem.SizeConstraintsType = SizeConstraintsType.Default;
                lItem.Name = "lbl" + item.Number;

                if (item.AnswersArray!=null)
                {
                    RadioGroup rgroup = new RadioGroup();
                    rgroup.Name = "answer" + item.Number; 
                    foreach (string ans in item.AnswersArray)
                    {
                        var radioItem = new RadioGroupItem();
                        radioItem.Description = ans;
                        radioItem.Value = ans;
                        rgroup.Properties.Items.Add(radioItem);
                    }
                    var lAnswer = layoutControl1.Root.AddItem();
                    lAnswer.TextAlignMode = TextAlignModeItem.AutoSize;
                    lAnswer.TextVisible = false;
                    lAnswer.SizeConstraintsType = SizeConstraintsType.Custom;
                    var size = new Size(400,50);
                    lAnswer.MaxSize = size;
                    lAnswer.MinSize = size;
                    lAnswer.Control = rgroup;
                }

                if (!string.IsNullOrEmpty(item.AdditionalInfo))
                {
                    var teAdd = new TextEdit();
                    teAdd.Name = "additional" + item.Number;
                    var lAdd = layoutControl1.Root.AddItem();
                    lAdd.SizeConstraintsType = SizeConstraintsType.SupportHorzAlignment;
                    //lAdd.MaxSize = new Size(300, 50);
                    //lAdd.MinSize = new Size(200, 20);
                    lAdd.TextAlignMode = TextAlignModeItem.AutoSize;
                    lAdd.Text = item.AdditionalInfo;
                    lAdd.Control = teAdd;
                }

                //LabelControl lblCntrl = new LabelControl();
                //lblCntrl.Text = item.Text;
                //layoutControlGroup.AddItem(lblCntrl);

            }//foreach

            var lbuttonSave = this.layoutControl1.Root.AddItem();
            lbuttonSave.SizeConstraintsType = SizeConstraintsType.Custom;
            lbuttonSave.MinSize = new Size(200, 30);
            lbuttonSave.MaxSize = new Size(200, 30);
            SimpleButton btnSave = new SimpleButton();
            btnSave.Text = "Сохранить";
            btnSave.Click += Save_Click;
            lbuttonSave.Control = btnSave;

            var lbuttonCancel = this.layoutControl1.Root.AddItem();
            lbuttonCancel.SizeConstraintsType = SizeConstraintsType.Custom;
            lbuttonCancel.MinSize = new Size(200, 30);
            lbuttonCancel.MaxSize = new Size(200, 30);
            SimpleButton btnSCancel = new SimpleButton();
            btnSCancel.Text = "Отмена";
            btnSCancel.Click += (o,e)=> { this.Close(); };
            lbuttonCancel.Control = btnSCancel;


        }

        private void Save_Click(object sender, EventArgs e)
        {
            ReadAnwers();
            repo.AddOrUpdate(answer);
            this.Close();
        }

    }
}