using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lsharp
{
    public partial class Program
    {
        //inside game offsets
        const int LOCAL_PLAYER = 0x34FF634;
        const int OBJECT_MANAGER = 0x1C5CC30;//0x1C5C8B0; //outdated
        const int GAME_TIME = 0x34F7A7C;
        const int RENDERER = 0x35269A0;
        //inside renderer
        const int RENDERER_VIEWMATRIX = 0x6C;
        const int RENDERER_PROJECTIONMATRIX = 0xAC;
        //inside game functions
        const int FN_GETSPELLSTATE = 0x4F4A10;
        const int FN_ISHERO = 0x1EE810;
        //inside object manager
        const int OBJ_MGR_OBJECT_ARRAY = 0x14;
        const int OBJ_MGR_MAX_OBJECTS = 0x8;
        
        //inside nearly all objects offsets
        const int OBJ_HP_CURR = 0xF88;
        const int OBJ_HP_MAX = 0xF98;
        const int OBJ_MANA_CURR = 0x47C;
        const int OBJ_MANA_MAX = 0x48C;
        const int OBJ_POSITION = 0x1D8;
        const int OBJ_SUMMONER_NAME = 0x6C;
        const int OBJ_ACTOR_NAME = 0x35AC;//0x3290 ptr
        const int OBJ_TEAM = 0x4C;//100 - BLUE, 200 - RED
        const int OBJ_SPELLBOOK = 0x2F64;
        const int OBJ_ISTARGETABLE = 0xEF4;
        const int OBJ_RECALLSTATE = 0xF80;
        const int OBJ_ATTACKRANGE = 0x14B4;
        const int OBJ_VISIBILITY = 0x39C;
        //inside spellbook
        const int SPELLBOOK_FIRSTSPELL = 0x508;
        //inside spell
        const int SPELL_TIME_USED = 0x28;
        const int SPELL_LVL = 0x20;
        const int SPELL_STACK = 0x58;
        const int SPELL_RDY_NEXT_STACK = 0x64;
        const int SPELL_CD_TIME = 0x78;
        const int SPELL_SPELLINFO = 0x134;
        //inside spellinfo
        const int SPELLINFO_SPELLDATA = 0x38;
        const int SPELLINFO_SPELLNAME = 0x18;
        //inside spelldata
        const int SPELLDATA_SPELLNAME = 0x7C;
        //inside missile
        const int MISSILE_MSPELLINFO = 0x230;
        //inside missile spellinfo
        const int MSPELLINFO_STARTPOS = 0x78; //active spell 0x80
        const int MSPELLINFO_ENDPOS = 0x84; //active spell 0x8C
        const int MSPELLINFO_ISAUTOATTACK = 0xA8;
        const int MSPELLINFO_ISBASICATTACK = 0x4CC;
        const int MSPELLINFO_SPELLWIDTH = 0x44C;
        //2A8
        //2B4
        //wysokość = 1DC
        //activespell +0x8 -> 0x18
        
    }
}
/*MAKE_GET(FlagKey1, DWORD, 0x54); // inside gameobj
MAKE_GET(FlagKey2, DWORD, 0x64); // inside gameobj
*/