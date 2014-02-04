using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using Administrator.WebService;
namespace Administrator
{
    static class Program
    {
        public static Service1 service = new Service1();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frm_AddActor("Technician"));
            if (!(bool)service.FileExists("Administrator.xml"))
            {
                frm_AdminFirstConfiguration frm = new frm_AdminFirstConfiguration();
                frm.ShowDialog();
                if ((bool)frm.Tag)
                    Application.Run(new AdministratorApp());
            }
            else
            {
                frm_Login frm = new frm_Login();
                frm.ShowDialog();
                if (frm.Tag!=null && (bool)frm.Tag)
                    Application.Run(new AdministratorApp());
                
            }
        }
    }
}
