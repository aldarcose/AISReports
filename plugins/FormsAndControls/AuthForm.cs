using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Model;
using SharedDbWorker;

namespace plugins.FormsAndControls
{
    public partial class AuthForm : XtraForm
    {
        HostForm _hostForm = null;
        public AuthForm()
        {
            InitializeComponent();
            _hostForm = new HostForm();
        }

        private void btn_login_Click(object sender, System.EventArgs e)
        {
            var login = te_login.Text;
            var password = te_password.Text;
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                XtraMessageBox.Show("Заполните все поля");
                return;
            }

            this.Enabled = false;

            var user = CheckUser();

            if (user != null)
            {
                _hostForm.InitForm(user);

                _hostForm.Closed += (o, ex) => this.Close();

                _hostForm.Shown += (o, ex) => this.Hide();

                _hostForm.Show();
            }
            this.Enabled = true;
        }

        private Operator CheckUser()
        {
            var login = te_login.Text;
            var password = te_password.Text;

            Operator @operator = null;
            try
            {
                using(var dbWorker = new DbWorker())
                {
                    var id = dbWorker.GetUserId(login, password);
                    if (id > 0)
                    {
                        @operator = new Operator();
                        if (!@operator.LoadData(id))
                        {
                            XtraMessageBox.Show("Ошибка при загрузке данных пользователя", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                
                
            }
            catch (AuthException ex)
            {
                XtraMessageBox.Show(ex.Error, "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception)
            {
                
                throw;
            }

            return @operator;
        }

        private void AuthForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _hostForm = null;
        }
    }
}
