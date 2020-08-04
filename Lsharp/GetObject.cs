
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lsharp
{
    public partial class Program
    {
        public static int GetObjInt32(int ObjPointer)
        {
            int bytesRead = 0;
            var buffer = new byte[4];
            ReadProcessMemory(processHandle, ObjPointer, buffer, buffer.Length, ref bytesRead);
            return (BitConverter.ToInt32(buffer, 0));
        }
        public static Int16 GetObjInt16(int ObjPointer)
        {
            int bytesRead = 0;
            var buffer = new byte[2];
            ReadProcessMemory(processHandle, ObjPointer, buffer, buffer.Length, ref bytesRead);
            return (BitConverter.ToInt16(buffer, 0));
        }
        public static uint GetObjUInt32(int ObjPointer)
        {
            int bytesRead = 0;
            var buffer = new byte[4];
            ReadProcessMemory(processHandle, ObjPointer, buffer, buffer.Length, ref bytesRead);
            return (BitConverter.ToUInt32(buffer, 0));
        }
        public static byte GetObjByte(int ObjPointer)
        {
            int bytesRead = 0;
            var buffer = new byte[1];
            ReadProcessMemory(processHandle, ObjPointer, buffer, buffer.Length, ref bytesRead);
            return (buffer[0]);
        }
        public static int GetObjReal(int ObjPointer)
        {
            var buffer = new byte[4];
            int bytesRead = 0;
            ReadProcessMemory(processHandle, ObjPointer, buffer, buffer.Length, ref bytesRead);
            return ((int)Math.Round(BitConverter.ToSingle(buffer, 0)));
        }
        public static Single GetObjSingle(int ObjPointer)
        {
            var buffer = new byte[4];
            int bytesRead = 0;
            ReadProcessMemory(processHandle, ObjPointer, buffer, buffer.Length, ref bytesRead);
            return (BitConverter.ToSingle(buffer, 0));
        }
        public static System.Numerics.Vector3 GetObjVector3(int ObjPointer)
        {
            var buffer = new byte[12];
            int bytesRead = 0;
            ReadProcessMemory(processHandle, ObjPointer, buffer, buffer.Length, ref bytesRead);
            return (new System.Numerics.Vector3(BitConverter.ToSingle(buffer, 0),
                BitConverter.ToSingle(buffer, 4),
                BitConverter.ToSingle(buffer, 8)
                ));

        }
        public static string GetObjString(int ObjPointer)
        {

            var buffer = new byte[30];
            int bytesRead = 0;
            ReadProcessMemory(processHandle, ObjPointer, buffer, buffer.Length, ref bytesRead);
            return (System.Text.Encoding.UTF8.GetString(buffer, 0, 30).Split((char)0)[0]);
        }

    }
}
