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
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Reflection;
using ExSharpBase.Overlay;
using Lsharp.Overlay.Drawing;
using Lsharp.Overlay;

namespace Lsharp
{
    public partial class Program
    {
        public static Champion[] ChampionsBlueCache = new Champion[5];
        public static Champion[] ChampionsRedCache = new Champion[5];
        public static bool OverlayLoaded;
        public static Team localheroteam;
        public static SpellDB.SpellDB SpellDBLoaded;
        public static dynamic data;
        public static DXD DXDOverlay = new DXD();
        public static void Init()
        {
            using (StreamReader file = File.OpenText(@"C:\Users\Przemek\source\repos\Lsharp\Lsharp\SpellDB\newSpellDB.json"))
            {
                
                JsonSerializer serializer = new JsonSerializer();
                //data = JsonConvert.DeserializeObject(file.ReadToEnd());
                data = JObject.Parse(file.ReadToEnd());
                SpellDBLoaded = (SpellDB.SpellDB)serializer.Deserialize(file, typeof(SpellDB.SpellDB));
                
                
            }
            GetLeaguePID();
            GetBaseAddress("League of Legends.exe");
            InitObjManager();
            IterateObjects();
            GetRendererClass();
            SearchForChampions();
            //FormOverlay.Start();
            


        }
        public static void MainDisplayLoop(object state)
        {
            Init();
            
            m_form.labelHp.Invoke((MethodInvoker)delegate
            {

                m_form.logs.Text = DebugPrintChamps();
            });
            int LocalHeroPointer = GetObjInt32(BAdress + LOCAL_PLAYER);
            //while (!OverlayLoaded)
            //{

            //}
            InitCache();
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                DXDOverlay.Show();
            }).Start();
            
            while (true)
            {
                RefreashMaxObjects();
                IterateObjects();
                FindMissiles();
                Thread.Sleep(3);
                m_form.labelHp.Invoke((MethodInvoker)delegate
                {
                    System.Numerics.Vector2 ekran = new System.Numerics.Vector2();
                    WorldToScreen(GetObjPosition(LocalHeroPointer), out ekran);
                    m_form.labelHp.Text = "HP: " + GetObjHpCurr(ChampionsRed[1]).ToString() + "/" + GetObjHpMax(LocalHeroPointer).ToString()
                    + Environment.NewLine + "MANA: " + GetObjManaCurr(LocalHeroPointer).ToString() + "/" + GetObjManaMax(LocalHeroPointer).ToString();
                    m_form.labelLocalName.Text = String.Concat(GetObjActorName(LocalHeroPointer), " controlled by ", GetObjPlayerName(LocalHeroPointer), "Team: ", GetObjTeam(LocalHeroPointer), "//", GetObjType(LocalHeroPointer), Environment.NewLine, GetGameTime() +
                Environment.NewLine + MaxObjects + "//"  );
                });
                DXDOverlay.Invoke((MethodInvoker)delegate
                {
                    ListChamps();
                    DXDOverlay.DrawScene();
                });
            }
        }
        public static void AfterOverlayLoaded(object sender, EventArgs e)
        {
                OverlayLoaded = true;
        }
        public static void ListChamps()
        {
            int LocalHeroPointer = GetObjInt32(BAdress + LOCAL_PLAYER);
            localheroteam = GetObjTeam(LocalHeroPointer);
            if (localheroteam == Team.Blue)// || m_form.checkBoxShowAlly.Checked)
            {
                for (int i = 0; i < 5; i++)
                {
                    UpdateCacheChampion(ChampionsRed[i], ChampionsRedCache[i]);
                    System.Numerics.Vector2 ekran = new System.Numerics.Vector2();
                    
                    if (WorldToScreen(ChampionsRedCache[i].position, out ekran))
                    {
                        DrawManager.RedDrawPoint[i] = new System.Drawing.Point((int)ekran.X, (int)ekran.Y);
                        DrawManager.RedDrawText[i] = GetChampCD(ChampionsRed[i], ChampionsRedCache[i]);

                    }
                    else
                    {
                        DrawManager.RedDrawText[i] = "";
                    }
                }
            }
            if (localheroteam == Team.Red )//|| m_form.checkBoxShowAlly.Checked)
            {
                for (int i = 0; i < 5; i++)
                {
                    UpdateCacheChampion(ChampionsBlue[i], ChampionsBlueCache[i]);
                    System.Numerics.Vector2 ekran = new System.Numerics.Vector2();
                    if(WorldToScreen(ChampionsBlueCache[i].position, out ekran)) {
                        DrawManager.BlueDrawPoint[i] = new System.Drawing.Point((int)ekran.X, (int)ekran.Y);
                        DrawManager.BlueDrawText[i] = GetChampCD(ChampionsBlue[i], ChampionsBlueCache[i]);
                    }
                    else
                    {
                        DrawManager.BlueDrawText[i] = ""; 
                    }

                }
            }
            //o_form.Refresh();
        }
        public static string GetChampCD(int champ, Champion cache)
        {
            int spellbook = cache.spellbook;
            int spell1 = cache.spell[0];
            int spell2 = cache.spell[1];
            int spell3 = cache.spell[2];
            int spell4 = cache.spell[3];
            int spell5 = cache.spell[4];
            int spell6 = cache.spell[5];
            int spell1lvl = GetSpellLvl(spell1);
            int spell2lvl = GetSpellLvl(spell2);
            int spell3lvl = GetSpellLvl(spell3);
            int spell4lvl = GetSpellLvl(spell4);
            return (
             GetObjActorName(champ)+"  "+GetRecallTimeRemaining(champ,cache).ToString()
            + Environment.NewLine + "Q" + spell1lvl.ToString() + ": " + GetSpellCDLeft(spell1, cache.spellGotStacks[0]) + "s  "
            + Environment.NewLine + "W" + spell2lvl.ToString() + ": " + GetSpellCDLeft(spell2, cache.spellGotStacks[1]) + "s  "
            + Environment.NewLine + "E" + spell3lvl.ToString() + ": " + GetSpellCDLeft(spell3, cache.spellGotStacks[2]) + "s  "
            + Environment.NewLine + "R" + spell4lvl.ToString() + ": " + GetSpellCDLeft(spell4, cache.spellGotStacks[3]) + "s  "
            + Environment.NewLine  +cache.spellname[4]+ ": " + GetSpellCDLeft(spell5, cache.spellGotStacks[4]) + "s  "
            + Environment.NewLine  + cache.spellname[5]+ ": " + GetSpellCDLeft(spell6, cache.spellGotStacks[5]) + "s  "

            ); ;
        }
        public static void InitCache()
        {
            for (int i = 0; i < 5; i++)
            {
                ChampionsBlueCache[i] = new Champion();
                ChampionsRedCache[i] = new Champion();
                InitCacheChamption(ChampionsBlue[i], ChampionsBlueCache[i]);
                InitCacheChamption(ChampionsRed[i], ChampionsRedCache[i]);
            }
        }
        public static void InitCacheChamption(int champ, Champion cache)
        {
            cache.AImanagerPtr = GetNavPtr(champ);
            cache.spellbook = GetObjSpellBook(champ);
            for (int i = 0; i <= 5; i++) 
            {
                cache.spell[i] = GetSpellBookSpellById(cache.spellbook,i);
                cache.spellname[i] = GetSummonerSpellName(cache.spell[i]);
                cache.spellGotStacks[i] = GetObjSingle(cache.spell[i] + SPELL_RDY_NEXT_STACK) != 0;
                
            }
        }
        public static void UpdateCacheChampion(int champ, Champion cache)
        {
            cache.attackrange = GetObjAttackRange(champ);
            cache.position = GetObjPosition(champ);
        }
    }
}
