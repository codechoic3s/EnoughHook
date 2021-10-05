using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UnstableTrigger
{
    public class Unstable
    {
        [DllImport("user32.dll")]
        static extern short GetAsyncKeyState(int vKey);

        [DllImport("kernel32.dll")]
        static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, ref IntPtr lpNumberOfBytesRead);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, int Msg, int wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        public static int Readint(byte[] data)
        {
            return BitConverter.ToInt32(data, 0);
        }

        private const int MOUSEEVENTF_LEFTDOWN = 2; //FLag for pressed button
        private const int MOUSEEVENTF_LEFTUP = 4; //Flag for released button

        public static void Fire(bool state)
        {
            if (state)
                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            else
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }

        public int RPMInt(IntPtr ptr)
        {
            byte[] buf = new byte[4];
            IntPtr aha = IntPtr.Zero;
            ReadProcessMemory(Handle, ptr, buf, 4, ref aha);
            return Readint(buf);
        }

        public static bool GetKeyState(int key)
        {
            return (GetAsyncKeyState(key) & 0x8000) != 0;
        }

        public int LocalPlayerPTR;
        public int CrosshairIDPTR;
        public int TeamIDPTR;
        public int EntityListPTR;

        public IntPtr ClientModulePtr;
        public int ClientModuleSize;

        public IntPtr Handle;
        public IntPtr HWND;
        public Process CSGO;

        public bool AllocateProcess()
        {
            bool state = false;
            var procs = Process.GetProcessesByName("csgo");
            if (procs.Length > 0)
            {
                state = true;
                CSGO = procs[0];
                Handle = CSGO.Handle;
                HWND = CSGO.MainWindowHandle;
                var modules = CSGO.Modules;
                var mco = modules.Count;
                bool findedmodule = false;
                for (var i = 0; i < mco; i++)
                {
                    var m = modules[i];
                    if (m.ModuleName == "client.dll")
                    {
                        ClientModulePtr = m.BaseAddress;
                        ClientModuleSize = m.ModuleMemorySize;
                        findedmodule = true;
                    }
                }
                state = findedmodule;
            }
            return state;
        }

        public void LoadOffsets()
        {
            var file = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + @"/offsets.txt");
            LocalPlayerPTR = int.Parse(file[0]);
            CrosshairIDPTR = int.Parse(file[1]);
            TeamIDPTR = int.Parse(file[2]);
            EntityListPTR = int.Parse(file[3]);
        }
    }
}
