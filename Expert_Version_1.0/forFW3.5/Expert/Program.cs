using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MARS_Expert.WebService;
using MARS_Expert.Manager;
//using MARS_Expert.webService1;
namespace MARS_Expert
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
             //Application.Run(new CustomMessageBox());
            {
                frm_Login frm = new frm_Login();
                frm.ShowDialog();
                try
                {
                    if (frm.Tag != null)
                    {
                        // Application.Run(new AdministratorApp());
                        // Make the form.

                        //Manager.ExpertManager.Expert expert = new Manager.ExpertManager.Expert();
                        //expert = expert.getExpert(frm.getLogin());
                        //Manager.Helper.service = new Service1();
                        //if (!Manager.Helper.service.ConnectExpert(expert.getLogin(), expert.getFirstName(), expert.getLastName(), ""))
                        //    MessageBox.Show("Expert Not Connected", "Program");
                        RenderForm frmRender = new RenderForm((string)frm.Tag);

                        // Initialize Direct3D.
                        if (frmRender.InitializeGraphics())
                        {
                            //frmRender.Show();
                            // While the form is valid,
                            // render the scene and process messages.
                            //MessageBox.Show(frmRender.Created+"   "+frmRender.isRunning);
                            //while (frmRender.Created && frmRender.isRunning)
                            //{
                            //    frmRender.Render();
                            //    Application.DoEvents();
                            //}
                            Application.Run(frmRender);
                        }
                    }
                }
                catch (Exception x) { }
            }
        }
    }
}