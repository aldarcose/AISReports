using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using plugins.FormsAndControls;

namespace plugins
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();
            UserLookAndFeel.Default.SetSkinStyle("McSkin");
            //DevExpress.Utils.AppearanceObject.DefaultFont = new Font("Verdana", 12);
            Application.Run(new AuthForm());
        }
    }
}