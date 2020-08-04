using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Threading;

namespace Lsharp
{
    public partial class Program
    {

        public static form m_form = new form();
        public static int LPID;
        public static int BAdress;
        public static int Renderer_ptr;
        private const int PROCESS_WM_READ = 0x0010;
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess,int lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);
        public static IntPtr processHandle;
        public static FormOverlay o_form;
        public static Thread FormOverlay = new Thread(StartOverlay);



        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(m_form);
            
        }
        private static void StartOverlay()
        {
            o_form = new FormOverlay();
            Application.Run(o_form);


        }
    }
}
