using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.ComponentModel;

namespace Lsharp
{
    public partial class Program
    {
        public static string ReadMemoryL()
        {

            processHandle = OpenProcess(PROCESS_WM_READ, false, LPID);
            var buffer = new byte[4];
            var buffer2 = new byte[4100];
            var buffer3 = new byte[40000];
            
            int bytesRead = 0;
            string Memoryread = "";
            ASCIIEncoding ascii = new ASCIIEncoding();
            Int32 baseAddress = BAdress + 0x34CB79C;
            ReadProcessMemory(processHandle,baseAddress, buffer, buffer.Length, ref bytesRead);


            bytesRead = 0;
            ReadProcessMemory(processHandle, BitConverter.ToInt32(buffer, 0), buffer2, buffer2.Length, ref bytesRead);
            for (int i = 0; i < 4000; i++)
            {
                bytesRead = 0;

                try{
                    if (m_form.CheckBoxIsString.Checked) {
                        Regex regex = new Regex(@"^[a-zA-Z0-9]*$");
                        Match match = regex.Match(System.Text.Encoding.ASCII.GetString(buffer2, i, 5));
                        if (match.Success) {
                            Memoryread = string.Concat(Memoryread, System.Text.Encoding.ASCII.GetString(buffer2, i, 5), " //", i.ToString("X"), " (String)", Environment.NewLine);
                        }
                    }
                    ReadProcessMemory(processHandle, BitConverter.ToInt32(buffer2, i), buffer3, buffer3.Length, ref bytesRead);
                    for (int x = 0; x < 3000; x++)
                    {
                        if (m_form.CheckBoxIsSingle.Checked)
                        {
                            if (Math.Round(BitConverter.ToSingle(buffer3, i)) < Convert.ToSingle(m_form.TextBoxValueToFind.Text) + 0.01 && Math.Round(BitConverter.ToSingle(buffer3, i)) > Convert.ToSingle(m_form.TextBoxValueToFind.Text) - 0.01)
                            {
                                Memoryread = string.Concat(Memoryread, BitConverter.ToSingle(buffer3, i), " //", i.ToString("X"), "(Single)", Environment.NewLine);
                            }
                        }
                    
                    /*
                        bytesRead = 0;
                        ReadProcessMemory(processHandle, BitConverter.ToInt32(buffer2,i),buffer3,buffer3.Length, ref bytesRead);
                        for (int x = 0; x < 39000; x++)
                        {
                            Regex regex = new Regex(@"^[a-zA-Z0-9]*$");
                            Match match = regex.Match(System.Text.Encoding.ASCII.GetString(buffer3, x, 5));
                            if (match.Success)
                            {
                                Memoryread = string.Concat(Memoryread, System.Text.Encoding.ASCII.GetString(buffer2, i, 5), " //", i.ToString("X"),"//",x.ToString("X"), " (String)", Environment.NewLine);
                            }
                        }*/

                    }
                    if (m_form.CheckBoxIsInt32.Checked) {
                        if (BitConverter.ToInt32(buffer2, i) == Convert.ToInt32(m_form.TextBoxValueToFind.Text))
                        {
                            Memoryread = string.Concat(Memoryread, BitConverter.ToInt32(buffer2, i), " //", i.ToString("X"), " (Int32)", Environment.NewLine);
                        } 
                    }
         
                    
                }
                catch(Exception e)
                {
                    //Memoryread = e.Message;
                }
            }
            //return (Marshal.GetLastWin32Error().ToString());
            //return (BitConverter.ToString(buffer3));
            return (Memoryread);
        }
    }
}
