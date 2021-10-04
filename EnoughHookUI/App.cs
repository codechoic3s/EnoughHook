using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EnoughHookUI
{
    class App
    {
        [STAThread]
        static void Main(string[] args)
        {
            MainWindow mw = new MainWindow();
            mw.ShowDialog();
            //MessageBox.Show("Non executable", "Runtime", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
