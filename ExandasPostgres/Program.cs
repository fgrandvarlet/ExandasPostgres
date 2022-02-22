using System;
using System.Configuration;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

using ExandasPostgres.Forms;
using ExandasPostgres.Native;

namespace ExandasPostgres
{
    static class Program
    {
        static readonly Mutex mutex = new Mutex(true, "{857DBA7C-B9A4-45A1-924E-B17A345F3690}");
        public static SplashForm splashForm = null;

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                try
                {
                    var appSettings = ConfigurationManager.AppSettings;
                    var defaultCulture = appSettings["DefaultCulture"];
                    if (defaultCulture != null)
                    {
                        var culture = new CultureInfo(defaultCulture);
                        CultureInfo.DefaultThreadCurrentCulture = culture;
                        CultureInfo.DefaultThreadCurrentUICulture = culture;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Environment.Exit(0);
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                // show splash
                var splashThread = new Thread(new ThreadStart(
                    delegate
                    {
                        splashForm = new SplashForm();
                        Application.Run(splashForm);
                    }
                ));

                splashThread.SetApartmentState(ApartmentState.STA);
                splashThread.Start();

                // run form - time taking operation
                var mainForm = new MainForm();
                mainForm.Load += new EventHandler(MainForm_Load);
                Application.Run(mainForm);
                mutex.ReleaseMutex();
            }
            else
            {
                // send our Win32 message to make the currently running instance
                // jump on top of all the other windows
                NativeMethods.PostMessage(
                    (IntPtr)NativeMethods.HWND_BROADCAST,
                    NativeMethods.WM_SHOWME,
                    IntPtr.Zero,
                    IntPtr.Zero
                );
            }
        }

        static void MainForm_Load(object sender, EventArgs e)
        {
            // close splash
            if (splashForm == null)
            {
                return;
            }
            splashForm.Invoke(new Action(splashForm.Close));
            splashForm.Dispose();
            splashForm = null;
        }

    }
}
