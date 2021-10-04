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

        private Tab TriggerTab;
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

            Window = mw;
        }

        private void MWStart(object obj)
        {
            var mw = new MainWindow();
            ((object[])obj)[0] = mw;
            mw.ShowDialog();
        }

        private void SetupInterface()
        {
            TabManager = Window.GetTabManager();
            TriggerTab = TabManager.AddNewPanel("Trigger");
            MovementTab = TabManager.AddNewPanel("Movement");

            
        }

        public void Start()
        {
            StartMainWindow();
            SetupInterface();
        }
    }
}
