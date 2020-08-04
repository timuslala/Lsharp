using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lsharp
{
    public partial class Program
    {
        public static int GetObjHpCurr(int ObjPointer)
        {
            return(GetObjReal(ObjPointer + OBJ_HP_CURR));
        }
        public static int GetObjHpMax(int ObjPointer)
        {
            return (GetObjReal(ObjPointer + OBJ_HP_MAX));
        }
        public static int GetObjManaCurr(int ObjPointer)
        {
            return (GetObjReal(ObjPointer + OBJ_MANA_CURR));
        }
        public static int GetObjManaMax(int ObjPointer)
        {
            return (GetObjReal(ObjPointer + OBJ_MANA_MAX));
        }
        public static Team GetObjTeam(int ObjPointer)
        {
            return((Team)GetObjInt32(ObjPointer + OBJ_TEAM));
        }
        public static string GetObjPlayerName(int ObjPointer)
        {
            var buffer = new byte[30];
            int bytesRead = 0;
            ReadProcessMemory(processHandle, ObjPointer + OBJ_SUMMONER_NAME, buffer, buffer.Length, ref bytesRead);
            return (System.Text.Encoding.UTF8.GetString(buffer, 0, 30).Split((char)0)[0]);
        }
        public static string GetObjActorName(int ObjPointer)
        {
            var buffer = new byte[20];
            int bytesRead = 0;
            ReadProcessMemory(processHandle, ObjPointer + OBJ_ACTOR_NAME, buffer, buffer.Length, ref bytesRead);
            return (System.Text.Encoding.UTF8.GetString(buffer, 0, 20).Split((char)0)[0]);
        }
        public static bool GetObjIsTargetable(int ObjPointer)
        {
            return (GetObjInt32(ObjPointer + OBJ_ISTARGETABLE) == 1);
        }
        public static ObjType GetObjType(int ObjPointer)
        {
            if (GetObjByte(ObjPointer + 81) == 1)
            {
                int a = 4 * GetObjByte(ObjPointer + 88) + 92 + ObjPointer;
                int b = GetObjInt32(ObjPointer + 84);
                int result = GetObjInt32(a) ^ ~b;
                return ((ObjType)(result ^ 0x40401));
            }
            return 0;
        }
        public static bool IsMissile(int ObjPointer)
        {
            if (GetObjByte(ObjPointer + 81) == 1)
            {
                int a = 4 * GetObjByte(ObjPointer + 88) + 92 + ObjPointer;
                int b = GetObjInt32(ObjPointer + 84);
                int result = GetObjInt32(a) ^ ~b;
                return ((result & 0x8000)!=0);
            }
            return false;
        }
        public static bool IsRecalling(int ObjPointer, Champion cache)
        {
            return (GetObjByte(ObjPointer + 0xF68) != 0);
        }
        public static string GetRecallTimeRemaining(int ObjPointer, Champion cache)
        {
            if (!IsRecalling(ObjPointer, cache))
            {
                if (cache.measuringRecall&&cache.recallingtime>0.08)
                {
                    cache.measuringRecall = false;
                    cache.recalltimer.Reset();
                }
                if (cache.recalltimer.ElapsedMilliseconds < 12000&&cache.recalltimer.ElapsedMilliseconds>10)
                {
                    cache.measuringRecall = false;
                    return ("RECALLED");
                }
                if (cache.recallingtime <= 0.08 && cache.recalltimer.ElapsedMilliseconds >= 12000)
                {
                    cache.measuringRecall = false;
                    cache.recalltimer.Reset();
                }
                return "";
            }
            else
            {
                if (!cache.measuringRecall)
                {
                    cache.measuringRecall = true;
                    cache.recalltimer.Restart();
                }
                int recallStatus = GetObjByte(ObjPointer + OBJ_RECALLSTATE);
                if (recallStatus == 6)
                {
                    cache.recallingtime = (Single)Math.Round(Convert.ToSingle(8000-cache.recalltimer.ElapsedMilliseconds) / 1000, 1);
                    return (Math.Max(cache.recallingtime, 0).ToString());
                }
                if (recallStatus == 11)
                {
                    cache.recallingtime = (Single)Math.Round(Convert.ToSingle(4000-cache.recalltimer.ElapsedMilliseconds) / 1000, 1);
                    return (Math.Max(cache.recallingtime, 0).ToString());
                }
                if (recallStatus == 15)
                {
                    cache.recallingtime = (Single)Math.Round(Convert.ToSingle(4000-cache.recalltimer.ElapsedMilliseconds) / 1000, 1);
                    return (Math.Max(cache.recallingtime,0).ToString());
                }
                return "";

            }
        }
        public static int GetObjAttackRange(int ObjPointer)
        {
            return (GetObjReal(ObjPointer + OBJ_ATTACKRANGE));
        }
        public static System.Numerics.Vector3 GetObjPosition(int ObjPointer)
        {
            return GetObjVector3(ObjPointer + OBJ_POSITION);
        }
        public static int GetMissileSpellInfo(int ObjPointer)
        {
            return GetObjInt32(ObjPointer + MISSILE_MSPELLINFO);
        }
        public static System.Numerics.Vector3 GetMissileStartPos(int MissileInfo)
        {
            System.Numerics.Vector3 temp = GetObjVector3(MissileInfo + 0x2A8);
            //temp.Y = GetObjReal(MissileInfo+ 0x1DC);
            return temp;
        }
        public static System.Numerics.Vector3 GetMissileEndPos(int MissileInfo)
        {
            System.Numerics.Vector3 temp = GetObjVector3(MissileInfo + 0x2B4);
            //temp.Y = GetObjReal(MissileInfo + 0x1DC);
            return temp;
        }
        public static int GetMissileSpellWidth(int ObjPointer)
        {
            return GetObjReal(ObjPointer + MSPELLINFO_SPELLWIDTH);
        }
        public static string GetMissileName(int ObjPointer)
        {
            
            var buffer = new byte[30];
            int bytesRead = 0;
            ReadProcessMemory(processHandle, GetObjInt32(ObjPointer + 0x6C), buffer, buffer.Length, ref bytesRead);
            return (System.Text.Encoding.UTF8.GetString(buffer, 0, 30).Split((char)0)[0]);
        }

    }
}
