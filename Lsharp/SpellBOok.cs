using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lsharp
{
    public partial class Program
    {
        public static int GetSpellBookSpellById(int SpellBook, int SpellId)
        {
            return (GetObjInt32(SpellBook + SPELLBOOK_FIRSTSPELL + SpellId * 0x4));
        }
        public static int GetSpellLvl(int Spell)
        {
            return (GetObjInt32(Spell + SPELL_LVL));
        }
        public static Single GetSpellCD(int Spell)
        {
            return (GetObjSingle(Spell + SPELL_CD_TIME));
        }
        public static float GetSpellTimeUsed(int Spell)
        {
            return (GetObjSingle(Spell + SPELL_TIME_USED));
        }
        public static float GetGameTime()
        {
            return (GetObjSingle(BAdress + GAME_TIME));
        }
        public static float GetSpellCDLeft(int Spell, bool SpellGotStacks)
        {
            if (!SpellGotStacks)
            {
                float cd = GetSpellTimeUsed(Spell) - GetGameTime();
                if (cd <= 0)
                {
                    return 0;
                }
                return (Convert.ToSingle(Math.Round(cd, 1)));
            }
            else
            {
                float cd = GetSpellTimeUsed(Spell) - GetGameTime();
                int stacks = GetSpellStacks(Spell);
                if (stacks > 0)
                {
                    if (cd <= 0)
                    {
                        return 0;
                    }
                    return (Convert.ToSingle(Math.Round(cd, 1)));
                }
                else
                {
                    float nextstacktime = GetObjSingle(Spell + SPELL_RDY_NEXT_STACK) - GetGameTime();
                    return (Convert.ToSingle(Math.Round(Math.Max(nextstacktime, cd), 1)));
                }

            }
        }
        public static int GetSpellStacks(int spell)
        {
            return (GetObjInt32(spell + SPELL_STACK));
        }
        public static int GetObjSpellBook(int ObjPointer)
        {
            return (GetObjInt32(ObjPointer + OBJ_SPELLBOOK));
        }
        public static int GetSpellInfo(int Spell)
        {
            return (GetObjInt32(Spell + SPELL_SPELLINFO));
        }
        public static int GetSpellData(int SpellInfoPtr)
        {
            return (GetObjInt32(SpellInfoPtr + SPELLINFO_SPELLDATA));
        }
        public static string GetSpellName(int ObjPointer)
        {
            int spellinfo = GetSpellInfo(ObjPointer);
            int spelldata = GetObjInt32(spellinfo + 0xC);
            int spellnameptr = spelldata + 0x8;
            var buffer = new byte[20];
            int bytesRead = 0;
            ReadProcessMemory(processHandle, spellnameptr
            , buffer, buffer.Length, ref bytesRead);
            return (System.Text.Encoding.UTF8.GetString(buffer, 0, 20).Split((char)0)[0]);
        }
        public static string GetSummonerSpellName(int ObjPointer)
        {
            string spellname = GetSpellName(ObjPointer);
            if (spellname == "SummonerBoost")
            {
                return "Cleanse";
            }
            if (spellname == "SummonerExhaust")
            {
                return "Exhaust";
            }
            if (spellname == "SummonerTeleport")
            {
                return "Teleport";
            }
            if (spellname == "SummonerHaste")
            {
                return "Ghost";
            }
            if (spellname == "SummonerFlash")
            {
                return "Flash";
            }
            if (spellname == "SummonerHeal")
            {
                return "Heal";
            }
            if (spellname == "SummonerBarrier")
            {
                return "Barrier";
            }
            if (spellname == "SummonerDot")
            {
                return "Ignite";
            }
            if (spellname == "SummonerSmite" || spellname == "S5_SummonerSmitePlay"|| spellname == "S5_SummonerSmiteDuel")
            {
                return "Smite";
            }
            if(spellname == "SummonerSnowball")
            {
                return "Snowball";
            }
            return spellname;
        }
    }
}
