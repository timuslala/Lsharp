using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace Lsharp
{
    public partial class Program
    {
        public static int GetLeaguePID()
        {
            Process[] Liga = Process.GetProcessesByName("League of Legends");
            if (Liga.Length == 0)
            {
                return (0);

            }
            LPID = Liga[0].Id;
            return (LPID);

        }
        public static int GetBaseAddress(string ModuleName)
        {
            try
            {
                processHandle = OpenProcess(PROCESS_WM_READ, false, LPID);
                Process[] processes = Process.GetProcessesByName("League of Legends");
                ProcessModuleCollection modules = processes[0].Modules;
                
                ProcessModule DLLBaseAddress = null;

                foreach (ProcessModule i in modules)
                {
                    if (m_form.ListAttachedDlls.Checked)
                    {
                        m_form.logs.Text = String.Concat(m_form.logs.Text, Environment.NewLine);
                        m_form.logs.Text = String.Concat(m_form.logs.Text, i.ModuleName);
                    }
                    if (i.ModuleName == ModuleName)
                    {
                        DLLBaseAddress = i;
                    }
                }
                BAdress = DLLBaseAddress.BaseAddress.ToInt32();
                return BAdress;

            }
            catch
            {

                return 0;
            }
        }
        public static void GetRendererClass()
        {
            int bytesRead = 0;
            var buffer = new byte[4];
            ReadProcessMemory(processHandle, BAdress+RENDERER, buffer, buffer.Length, ref bytesRead);
            Renderer_ptr = BitConverter.ToInt32(buffer, 0);
        }
    }
}
