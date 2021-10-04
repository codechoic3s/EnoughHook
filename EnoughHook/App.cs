using EnoughHook.CFG;
using EnoughHookMid.UI;
using EnoughHookUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnoughHook
{
    public class App
    {
        private Thread MainWindowThread;
        private IWindow Window;

        private TabManager TabManager;
        private ConfigManager ConfigManager;

        private Tab AutoFireTab;
        private Tab MovementTab;

        private void StartMainWindow()
        {
            MainWindowThread = new Thread(new ParameterizedThreadStart(MWStart));
            MainWindowThread.SetApartmentState(ApartmentState.STA);
            MainWindow mw = default;
            object[] ar = new object[] { mw };
            
            MainWindowThread.Start(ar);

            while (ar[0] is null)
            {
                // just wait
            }
            mw = (MainWindow)ar[0];

            Window = mw;
        }

        private void MWStart(object obj)
        {
            var mw = new MainWindow();
            ((object[])obj)[0] = mw;
            mw.ShowDialog();
        }

        private void SetupConfigManager()
        {
            ConfigManager = new ConfigManager(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void SetupUIInterface()
        {
            TabManager = Window.GetTabManager();

            AutoFireTab = TabManager.AddNewPanel("AutoFire");
            AutoFireTab.AddNewProp("Enable", ConfigManager.Current.AutoFire.Enabled);
            AutoFireTab.AddNewProp("PreDelay", ConfigManager.Current.AutoFire.PreDelay);
            AutoFireTab.AddNewProp("AfterDelay", ConfigManager.Current.AutoFire.AfterDelay);

            MovementTab = TabManager.AddNewPanel("Movement");
            MovementTab.AddNewProp("BunnyHop", ConfigManager.Current.Movement.BunnyHop);
            
        }

        public void Start()
        {
            StartMainWindow();
            SetupConfigManager();
            SetupUIInterface();
        }
    }
}
