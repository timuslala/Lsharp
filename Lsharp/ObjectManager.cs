using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace Lsharp
{


    public partial class Program
    {
        public static int ObjManagerAdress;
        public static int MaxObjects;
        public static int FirstObject;
        public static int ObjectArrayAdress;
        public static int[] ObjectArray = new int[10000];
        public static int[] ChampionsBlue = new int[20];
        public static int[] ChampionsRed = new int[20];
        public static int[] MissileArray = new int[10000];
        
        public static void InitObjManager()
        {
            ObjManagerAdress = GetObjInt32(BAdress + OBJECT_MANAGER);
            RefreashMaxObjects();
            ObjectArrayAdress = GetObjInt32(ObjManagerAdress + OBJ_MGR_OBJECT_ARRAY);
        }
        public static void RefreashMaxObjects()
        {
            MaxObjects = GetObjInt32(ObjManagerAdress + OBJ_MGR_MAX_OBJECTS);
            FirstObject = GetFirstObject(ObjManagerAdress);
        }
        public static void IterateObjects()
        {

            int i = 0;
            GetFirstObject(ObjManagerAdress);
            ObjectArray[i] = GetFirstObject(ObjManagerAdress);
            do
            {
                i++;
                ObjectArray[i] = GetNextObject(ObjManagerAdress, ObjectArray[i - 1]);


            } while (ObjectArray[i] !=ObjectArray[1]||i==1 );
            int a = i;
        }
        public static void FindMissiles()
        {
            Array.Clear(MissileArray, 0, MissileArray.Length);
            RefreashMaxObjects();
            int n = 0;
            for (int i = 0; i < MaxObjects; i++)
            {
                if (IsMissile(ObjectArray[i]))
                {
                    MissileArray[n] = ObjectArray[i];
                    n++;
                }
            }
        }
        public static void SearchForChampions()
        {
            int champnrblue = 0;
            int champnrred = 0;
            RefreashMaxObjects();
            for (int i = 0; i < MaxObjects; i++)
            {

                if (GetObjType(ObjectArray[i])==ObjType.Hero)
                {
                    Team TeamCurrObj = (Team)GetObjInt32(ObjectArray[i] + OBJ_TEAM);
                    if (TeamCurrObj == Team.Blue)
                    {
                        ChampionsBlue[champnrblue] = ObjectArray[i];
                        champnrblue++;
                    }
                    if (TeamCurrObj == Team.Red)
                    {
                        ChampionsRed[champnrred] = ObjectArray[i];
                        champnrred++;
                    }                  
                }
            }
        }
        public static string DebugPrintChamps()
        {
            string text = "";
            foreach (int champion in ChampionsBlue.Concat(ChampionsRed))
            {
                text = text + GetObjActorName(champion) + "/" + GetObjPlayerName(champion) + "/" + GetObjHpMax(champion) + "/" + GetObjManaMax(champion) + "/" + champion.ToString("X")+"/" + GetNavPtr(champion).ToString("X")+ Environment.NewLine;

            }
            return text;
        }
        public static int GetFirstObject(int ObjManager)
        {
            int v1; // eax
            int v2; // edx

            v1 = GetObjInt32(ObjManager+0x14);
            v2 = GetObjInt32(ObjManager + 0x18);

            if (v1 == v2)
                return 0;
            while(GetObjByte(v1)%2 == 1|| GetObjInt32(v1) == 0)
            {
                v1 += 4;
                if (v1 == v2)
                    return 0;
            }


                    return GetObjInt32(v1);


        }
        public static int GetNextObject(int ObjManager, int current)
        {
            int v2; // eax
            int v3; // edx
            int v4; // esi
            int v5; // eax

            v2 = GetObjInt32(ObjManager+0x14);
            v3 =( GetObjInt16(current + 0x20) + 1);
            v4 = ((GetObjInt32(ObjManager+0x18) - v2) >> 2);

            if (v3 >= v4)
            {
                return 1;
            }
            
            v5 =(int)( v2 + 4 * v3);
            while (GetObjByte(v5) % 2 == 1 || GetObjInt32(v5) == 0)
            {
                ++v3;
                v5 += 4;
                if (v3 >= v4)
                {
                    return 0;
                }
            }
            return GetObjInt32(v5);
        }
    }
}
