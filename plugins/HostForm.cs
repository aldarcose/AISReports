using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Helpers;
using DevExpress.XtraNavBar;
using Microsoft.Practices.Unity;
using Model;
using Model.Classes;
using Model.Interface;
using Shared.FormsAndControls;
using SharedPlugin;
using SharedPlugin.Classes;


namespace plugins
{
    public partial class HostForm : RibbonForm, IHostForm
    {
        private IUnityContainer _container = null;

        private Operator LoggedOperator { get; set; }

        public HostForm()
        {
            LoggedOperator = null;
            InitializeComponent();
        }

        public void InitForm(Operator loggedOperator)
        {
            LoggedOperator = loggedOperator;

            SkinHelper.InitSkinGallery(rgbiSkins, true);

            loggedUserStatus.Caption =
                string.Format("Пользователь: {0} {1}{2}",
                    LoggedOperator.LastName,
                    string.IsNullOrEmpty(LoggedOperator.FirstName) ? "" : LoggedOperator.FirstName[0].ToString() + ".",
                    string.IsNullOrEmpty(LoggedOperator.MiddleName) ? "" : LoggedOperator.MiddleName[0].ToString() + "."
                    );

            splitContainerControl.Panel2.ControlAdded += Panel2_ControlAdded;
            splitContainerControl.Panel2.ControlRemoved += Panel2_ControlRemoved;

            LoadPlugins();
            
        }

        void Panel2_ControlRemoved(object sender, ControlEventArgs e)
        {
            var control = e.Control as PluginControl;
            if (control == null) return;

            var controlTemplateName = control.ControlTemplate.Name;
            var findedPage = ribbonControl.Pages.GetPageByText(controlTemplateName);
            if (findedPage != null)
            {
                findedPage.Visible = false;
            }
        }
        void Panel2_ControlAdded(object sender, ControlEventArgs e)
        {
            var control = e.Control as PluginControl;
            if (control == null) return;
            var controlTemplate = control.ControlTemplate;
            if (controlTemplate != null)
            {
                var findedPage = ribbonControl.Pages.GetPageByText(controlTemplate.Name);
                if (findedPage == null)
                {
                    if (controlTemplate.Commands.Count > 0)
                    {
                        findedPage = new RibbonPage(control.ControlTemplate.Name);

                        foreach (var controlCommand in controlTemplate.Commands)
                        {
                            AddCommandOnPage(findedPage, controlCommand);
                        }

                        ribbonControl.Pages.Add(findedPage);
                    }
                }
                else
                {
                    findedPage.Visible = true;
                }

                if (findedPage!=null)
                    ribbonControl.SelectedPage = findedPage;
            }
        }

        private void AddCommandOnPage(RibbonPage page, ControlCommand command)
        {
            var group = page.Groups.GetGroupByText(command.Category) ?? new RibbonPageGroup(command.Category);
            
            BarButtonItem btn = null;
            foreach (BarButtonItemLink itemLink in group.ItemLinks)
            {
                if (itemLink != null)
                {
                    if (itemLink.Caption.Equals(command.Name))
                    {
                        btn = itemLink.Item;
                        break;
                    }
                }
            }

            if (btn == null)
            {
                btn = new BarButtonItem();
                command.Button = btn;
                command.ButtonContainer = page;

                btn.Caption = command.Name;
                btn.Description = command.Description;
                btn.Enabled = command.CanExecute(null);
                btn.ItemClick += (o,e) => command.Execute(null);
                command.UiUpdated += CommandUiUpdated;

                group.ItemLinks.Add(btn);
                page.Groups.Add(group);

            }
        }

        void CommandUiUpdated(object sender, ControlCommandEventArgs e)
        {
            var btn = e.Command.Button as BarButtonItem;
            
            if (btn != null)
            {
                btn.Enabled = e.Command.CanExecute(null);
                btn.Refresh();

                var page = e.Command.ButtonContainer as RibbonPage;
                if (page != null) page.Ribbon.Refresh();
            }
        }

        void LoadPlugins()
        {
            LoadPluginsToContainer();

            if (_container != null)
            {
                LoadPluginsIntoForm();
            }
        }

        void LoadPluginsToContainer()
        {
            _container = new UnityContainer();
            var pluginFilePath = Path.Combine(Environment.CurrentDirectory, "plugins");
            if (Directory.Exists(pluginFilePath))
            {
                string[] files = Directory.GetFiles(pluginFilePath, "*Plugin.dll");

                foreach (var file in files)
                {
                    Assembly assembly = Assembly.LoadFrom(file);

                    foreach (var T in assembly.GetTypes())
                    {
                        foreach (var iface in T.GetInterfaces())
                        {
                            if (iface == typeof(IPlugin))
                            {
                                var pluginInstance = (IPlugin)Activator.CreateInstance(T);
                                _container.RegisterInstance<IPlugin>(pluginInstance.GetName(), pluginInstance);
                            }
                        }
                    }
                }
            }
        }

        void LoadPluginsIntoForm()
        {
            var loadedPlugins = _container.ResolveAll<IPlugin>();

            foreach (var loadedPlugin in loadedPlugins)
            {
                var accessReqs = loadedPlugin.GetAccessRequirements();

                // если удовлетворяет всем требованиям по доступу
                //var canLoad = accessReqs.All(_loggedOperator.AccessList.Contains);
                var canLoad = true;

                if (canLoad)
                {
                    loadedPlugin.SetHostForm(this);
                    var category = loadedPlugin.GetCategoryName();
                    if (!string.IsNullOrEmpty(category))
                    {
                        var group = navBarControl.Groups.FirstOrDefault(t => t.Caption.Equals(category));
                        if (group == null)
                        {
                            group = new NavBarGroup(category);
                            navBarControl.Groups.Add(group);
                        }

                        var name = loadedPlugin.GetName();
                        var item = new NavBarItem(name);
                        item.AppearancePressed.Font = new Font(item.AppearancePressed.Font, FontStyle.Bold);
                        item.AllowAutoSelect = true;
                        item.Tag = loadedPlugin.GetPluginControl();
                        item.LinkClicked += ItemOnLinkClicked;

                        navBarControl.Items.Add(item);

                        group.Expanded = true;
                        group.ItemLinks.Add(item);
                    }
                }
            }
        }

        private void ItemOnLinkClicked(object sender, NavBarLinkEventArgs navBarLinkEventArgs)
        {
            var navItem = sender as NavBarItem;
            if (navItem == null) return;

            var controlTemplate = navItem.Tag as ControlTemplate;
            if (controlTemplate == null) return;

            var control = controlTemplate.PluginControlContainer;
            control.Dock = DockStyle.Fill;

            if (!splitContainerControl.Panel2.Controls.Contains(control))
            {
                ClearWorkPanel();
                control.OnControlAttaching();
                splitContainerControl.Panel2.Controls.Add(control);
            }
        }

        private void ClearWorkPanel()
        {
            foreach (Control control in splitContainerControl.Panel2.Controls)
            {
                var pluginControl = control as PluginControl;
                if (pluginControl!=null)
                    pluginControl.OnControlDeattaching();

                splitContainerControl.Panel2.Controls.Remove(control);
            }
        }

        public long GetOperatorId()
        {
            return LoggedOperator.GetId();
        }

        public Operator GetOperator()
        {
            return LoggedOperator;
        }

        string formCaptionText;
        
        public String FormCaptionText
        {
            get { return formCaptionText; }
            set { formCaptionText = value; SetCaptionText(); }
        }

        private void SetCaptionText()
        {
            if (string.IsNullOrEmpty(formCaptionText))
                return;
            this.Text = formCaptionText;
        }

    }
}