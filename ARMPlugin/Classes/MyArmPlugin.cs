using System.Collections.Generic;
using System.Windows.Forms;
using ARMPlugin.FormsAndControls;
using DevExpress.XtraEditors;
using Model;
using Model.Interface;
using SharedPlugin;
using SharedPlugin.Classes;
using SharedUtils.FormsAndControls;

namespace ARMPlugin.Classes
{
    public class MyArmPlugin : IPlugin
    {
        private const string _category = "АРМ врача";
        private const string _name = "Пациент";
        private ControlTemplate _controlTemplate = null;
        public MyArmPlugin()
        {
            var control = new ARMControl();
            _controlTemplate = new ControlTemplate("Пациент", control);

            var findCommand = new ControlCommand(control, "Поиск", "Найти", control.FindPatient);
            findCommand.Description = "Вызывает форму поиска";

            var saveCommand = new ControlCommand(control, "Данные", "Сохранить", control.CanSaveChanges, control.SaveChanges);
            saveCommand.Description = "Сохраняет данные пациента";

            var showRecipeFormCommand = new ControlCommand(control, "Рецепт", "Выписать", control.CanShowRecipeForm, control.ShowRecipeForm);
            findCommand.Description = "Вызывает форму выписки рецепта";

            var addTalonCommand = new ControlCommand(control, "Новый", "Создать талон", control.CanCreateTalon, control.CreateTalon);
            findCommand.Description = "Создает новый талон для пациента";

            var checkPolicyCommand = new ControlCommand(control, "Сервис", "Проверка полиса", ShowTfomsCheckService);
            checkPolicyCommand.Description = "Сервис проверки полиса";
            
            var checkAttachCommand = new ControlCommand(control, "Сервис", "Проверка прикрепления", ShowTfomsCheckAttachService);
            checkAttachCommand.Description = "Сервис проверки прикрепления";

            var queueCommand = new ControlCommand(control, "Очередь", "Запись в другое МО ", control.GetDoctorQueue);
            queueCommand.Description = "Сервис записи к врачу";

            var localqueueCommand = new ControlCommand(control, "Очередь", "Запись к врачу", control.GetLocalDoctorQueue);
            queueCommand.Description = "Сервис записи к врачу";

            var waitingListCommand = new ControlCommand(control, "Очередь", "Лист ожидания", control.GetWaitingList);

            var reestrUpdate = new ControlCommand(control, "Отчеты", "Выгрузить услуги", control.UpdateServices);
            var reestrDDCommand = new ControlCommand(control, "Отчеты", "Реестр ДД", control.GetDDReestrXML);
            var reestrCommand = new ControlCommand(control, "Отчеты", "Реестр без ДД", control.GetReestrXML);
            var reestrUnresidentCommand = new ControlCommand(control, "Отчеты", "Реестр иногородние", control.GetUnresidentReestrXML);

            _controlTemplate.Commands.Add(findCommand);
            _controlTemplate.Commands.Add(showRecipeFormCommand);
            _controlTemplate.Commands.Add(saveCommand);
            _controlTemplate.Commands.Add(addTalonCommand);
            _controlTemplate.Commands.Add(checkPolicyCommand);
            _controlTemplate.Commands.Add(checkAttachCommand);
            _controlTemplate.Commands.Add(queueCommand);
            _controlTemplate.Commands.Add(localqueueCommand);
            _controlTemplate.Commands.Add(waitingListCommand);
            //отчеты для реестров
            _controlTemplate.Commands.Add(reestrUpdate);
            _controlTemplate.Commands.Add(reestrCommand);
            _controlTemplate.Commands.Add(reestrDDCommand);
            _controlTemplate.Commands.Add(reestrUnresidentCommand);

            control.Changed += control_Changed;
            
        }

        void ShowTfomsCheckService(object obj)
        {
            var f = new CheckPolicyPatientInfoForm();

            if (_controlTemplate.PluginControlContainer != null)
            {
                var armControl = _controlTemplate.PluginControlContainer as ARMControl;
                if (armControl != null && armControl.CurrentPatient != null)
                {
                    if (
                        XtraMessageBox.Show("Использовать данные текущего пациента?", "Запрос", MessageBoxButtons.YesNo,
                            MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        f.InitFields(armControl.CurrentPatient.LastName, armControl.CurrentPatient.FirstName, armControl.CurrentPatient.MidName, armControl.CurrentPatient.BirthDate);
                    }
                }
            }
            f.Show();
        }

        void ShowTfomsCheckAttachService(object obj)
        {
            var f = new CheckAttachPatientInfoForm();
            f.Show();
        }

        void control_Changed(object sender, System.EventArgs e)
        {
            foreach (var controlCommand in _controlTemplate.Commands)
            {
                controlCommand.OnUiUpdated();
            }
        }

        public void SetHostForm(IHostForm hostForm)
        {
            if (_controlTemplate!=null)
                _controlTemplate.HostForm = hostForm;
        }

        public string GetCategoryName()
        {
            return _category;
        }

        public string GetName()
        {
            return _name;
        }

        public ControlTemplate GetPluginControl()
        {
            return _controlTemplate;
        }

        public List<AccessRequirements> GetAccessRequirements()
        {
            return null;
        }

        public void SetHostFormCaptionText(string text)
        {
            if (_controlTemplate!=null && _controlTemplate.HostForm!=null)
            {
                _controlTemplate.HostForm.FormCaptionText = text;
            }
        }
    }
}
