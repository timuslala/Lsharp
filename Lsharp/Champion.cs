
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
namespace Lsharp
{
    public class Champion
    {
        public bool[] spellGotStacks;
        public string[] spellname;
        public int[] spell;
        public int spellbook;
        public Stopwatch recalltimer;
        public bool measuringRecall;
        public Single recallingtime;
        public Single attackrange;
        public System.Numerics.Vector3 position;
        public int AImanagerPtr;
        public Champion()
        {
            this.spellGotStacks = new bool[6];
            this.spellname = new string[6];
            this.spell = new int[6];
            this.spellbook = new int();
            this.recalltimer = new Stopwatch();
            this.measuringRecall = new bool();
            this.recallingtime = new float();
        }
    }
}
