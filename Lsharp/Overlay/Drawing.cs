﻿using Lsharp.Overlay.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lsharp.Overlay
{
    public static class DrawManager
    {
        public static System.Drawing.Point[] BlueDrawPoint = new System.Drawing.Point[6];
        public static System.Drawing.Point[] RedDrawPoint = new System.Drawing.Point[6];
        public static string[] BlueDrawText = new string[6];
        public static string[] RedDrawText = new string[6];
        public static SharpDX.Color mycolor = new SharpDX.Color(255, 100, 0);
        public static void OnDrawMan()
        {
            
            if (Program.localheroteam == Team.Blue || Program.showallies)
            {
                int i = 0;
                while (Program.ChampionsRed[i] != 0)
                {
                    DrawPath(Program.ChampionsRedCache[i]);
                    DrawFactory.DrawFont(DrawManager.RedDrawText[i], 10, new SharpDX.Vector2(DrawManager.RedDrawPoint[i].X*2, DrawManager.RedDrawPoint[i].Y*2), mycolor);
                    DrawFactory.DrawCircleRange(new SharpDX.Vector3(Program.ChampionsRedCache[i].position.X,
                    Program.ChampionsRedCache[i].position.Y,
                    Program.ChampionsRedCache[i].position.Z
                    ), Program.ChampionsRedCache[i].attackrange + Program.ChampionsRedCache[i].BoundingRadius, mycolor, 5.0f, false);
                    i++;
                }
            }
            if (Program.localheroteam == Team.Red || Program.showallies)
            {
                int i = 0;
                while (Program.ChampionsBlue[i] != 0)
                {
                    DrawPath(Program.ChampionsBlueCache[i]);
                    DrawFactory.DrawFont(DrawManager.BlueDrawText[i],10, new SharpDX.Vector2(DrawManager.BlueDrawPoint[i].X*2, DrawManager.BlueDrawPoint[i].Y*2),mycolor);
                    DrawFactory.DrawCircleRange(new SharpDX.Vector3(Program.ChampionsBlueCache[i].position.X,
                    Program.ChampionsBlueCache[i].position.Y,
                   Program.ChampionsBlueCache[i].position.Z
                    ), Program.ChampionsBlueCache[i].attackrange+Program.ChampionsBlueCache[i].BoundingRadius, mycolor, 5.0f, false);
                    i++;
                }
            }
            
            int k = 0;
            while (Program.MissileArray[k] != 0)
            {
                DrawSpell(Program.MissileArray[k],false);
                //DebugDraw(Program.MissileArray[k], false);
                k++;
            }
            for (int i = 0; i < 9; i++)
            {
                int activespell;
                if (i > 4)
                {
                    activespell = Program.GetObjInt32(Program.ChampionsRedCache[i - 5].spellbook + 0x20);
                }
                else
                {
                    activespell = Program.GetObjInt32(Program.ChampionsBlueCache[i].spellbook + 0x20);
                }
                DrawSpell(activespell,true);
                //DebugDraw(activespell,true);
            }
            
        }
        public static void DrawPath(Champion cache)
        {
            System.Numerics.Vector2 ekran = new System.Numerics.Vector2();
            System.Numerics.Vector2 ekran2 = new System.Numerics.Vector2();
            System.Numerics.Vector3 path;

            path = Program.GetObjVector3(cache.AImanagerPtr + 0x10);
            if (!Program.WorldToScreen(path, out ekran))
            {
                return;
            }
            if (!Program.WorldToScreen(cache.position, out ekran2))
            {
                return;
            }
            DrawFactory.DrawLine(ekran.X,ekran.Y,ekran2.X,ekran2.Y,2,mycolor);
        }

        public static void DrawSpell(int SpellToDraw, bool fromspellbook)
        {

            string nazwa;
            string nazwa2;
            if (fromspellbook)
            {
                nazwa = Program.GetActiveMissileNameInside(SpellToDraw);
                nazwa2 = Program.GetActiveMissileName(SpellToDraw);
            }
            else
            {
                nazwa = Program.GetObjPlayerName(SpellToDraw);
                nazwa2 = Program.GetMissileName(SpellToDraw);
            }
            foreach (var data2 in Program.data.SelectTokens("*.*"))
            {
                if (data2.missileName == nazwa || data2.missileName == nazwa2)
                {
                    
                    if (data2.type == "linear")
                    {
                        //RYSOWANKO MISSILE
                        System.Numerics.Vector2 ekran3 = new System.Numerics.Vector2();
                        //System.Numerics.Vector2 start = new System.Numerics.Vector2();
                        //System.Numerics.Vector2 end = new System.Numerics.Vector2();
                        System.Numerics.Vector2 P1 = new System.Numerics.Vector2();
                        System.Numerics.Vector2 P2 = new System.Numerics.Vector2();
                        System.Numerics.Vector2 P3 = new System.Numerics.Vector2();
                        System.Numerics.Vector2 P4 = new System.Numerics.Vector2();
                        System.Numerics.Vector3 startpos;
                        System.Numerics.Vector3 currpos;
                        System.Numerics.Vector3 endpos;
                        if (fromspellbook)
                        {
                            startpos = Program.GetObjVector3(SpellToDraw + 0x80);
                            currpos = startpos;
                            endpos = Program.GetObjVector3(SpellToDraw + 0x8C);
                        }
                        else
                        {
                            startpos = Program.GetMissileStartPos(SpellToDraw);
                            currpos = Program.GetObjPosition(SpellToDraw);
                            endpos = Program.GetMissileEndPos(SpellToDraw);
                        }
                        float spellWidth = data2.radius;

                        double width = endpos.X - startpos.X;
                        double height = endpos.Z - startpos.Z;
                        float Length = (float)Math.Sqrt(width * width + height * height); //Thats length of perpendicular
                        float NX = (float)(spellWidth * height / Length);
                        float NY = (float)(spellWidth * width / Length);
                        float range = data2.range;
                        endpos.X = (float)(startpos.X + (width / Length * range));
                        endpos.Z = (float)(startpos.Z + (height / Length * range));
                        float R1X = currpos.X - NX;
                        float R1Y = currpos.Z + NY;
                        float R2X = currpos.X + NX;
                        float R2Y = currpos.Z - NY;
                        float R3X = endpos.X + NX;
                        float R3Y = endpos.Z - NY;
                        float R4X = endpos.X - NX;
                        float R4Y = endpos.Z + NY;
                        System.Numerics.Vector3 P1new = new System.Numerics.Vector3(R1X, startpos.Y, R1Y);
                        System.Numerics.Vector3 P2new = new System.Numerics.Vector3(R2X, startpos.Y, R2Y);
                        System.Numerics.Vector3 P3new = new System.Numerics.Vector3(R3X, endpos.Y, R3Y);
                        System.Numerics.Vector3 P4new = new System.Numerics.Vector3(R4X, endpos.Y, R4Y);
                        if (range > 3000)
                        {
                            System.Numerics.Vector3 P5new = new System.Numerics.Vector3((R1X+R4X)/2, (startpos.Y+endpos.Y)/2,(R1Y+R4Y)/2);
                            System.Numerics.Vector3 P6new = new System.Numerics.Vector3((R2X + R3X) / 2, (startpos.Y + endpos.Y) / 2, (R2Y + R3Y) / 2);
                            System.Numerics.Vector2 P5 = new System.Numerics.Vector2();
                            System.Numerics.Vector2 P6 = new System.Numerics.Vector2();
                            bool[] P = new bool[7];
                            P[1] = Program.WorldToScreen(P1new, out P1);
                            P[2] = Program.WorldToScreen(P2new, out P2);
                            P[3] = Program.WorldToScreen(P3new, out P3);
                            P[4] = Program.WorldToScreen(P4new, out P4);
                            P[5] = Program.WorldToScreen(P5new, out P5);
                            P[6] = Program.WorldToScreen(P6new, out P6);
                            P[0] = Program.WorldToScreen(Program.GetObjPosition(SpellToDraw), out ekran3);
                            if (P[1] && P[2])
                            {
                                DrawFactory.DrawLine(P1.X, P1.Y, P2.X, P2.Y, 2, mycolor);
                            }
                            if (P[2] && P[6])
                            {
                                DrawFactory.DrawLine(P2.X, P2.Y, P6.X, P6.Y, 2, mycolor);
                            }
                            if (P[6] && P[3])
                            {
                                DrawFactory.DrawLine(P6.X, P6.Y, P3.X, P3.Y, 2, mycolor);
                            }
                            if (P[3] && P[4])
                            {
                                DrawFactory.DrawLine(P3.X, P3.Y, P4.X, P4.Y, 2, mycolor);
                            }
                            if (P[4] && P[5])
                            {
                                DrawFactory.DrawLine(P4.X, P4.Y, P5.X, P5.Y, 2, mycolor);
                            }
                            if (P[5] && P[1])
                            {
                                DrawFactory.DrawLine(P5.X, P5.Y, P1.X, P1.Y, 2, mycolor);
                            }
                            return;
                        }
                        if (!Program.WorldToScreen(Program.GetObjPosition(SpellToDraw), out ekran3))
                        {
                            return;
                        }
                        if (!Program.WorldToScreen(P1new, out P1))
                        {
                            return;
                        }
                        if (!Program.WorldToScreen(P2new, out P2))
                        {
                            return;
                        }
                        if (!Program.WorldToScreen(P3new, out P3))
                        {
                            return;
                        }
                        if (!Program.WorldToScreen(P4new, out P4))
                        {
                            return;
                        }

                        DrawFactory.DrawLine(P1.X, P1.Y, P2.X, P2.Y, 2, mycolor);
                        DrawFactory.DrawLine(P2.X, P2.Y, P3.X, P3.Y, 2, mycolor);
                        DrawFactory.DrawLine(P3.X, P3.Y, P4.X, P4.Y, 2, mycolor);
                        DrawFactory.DrawLine(P4.X, P4.Y, P1.X, P1.Y, 2, mycolor);

                    }else if (data2.type == "circular")
                    {
                        System.Numerics.Vector3 endpos;
                        if (fromspellbook)
                        {
                            //startpos = Program.GetObjVector3(SpellToDraw + 0x80);
                            //currpos = startpos;
                            endpos = Program.GetObjVector3(SpellToDraw + 0x8C);
                        }
                        else
                        {
                            //startpos = Program.GetMissileStartPos(SpellToDraw);
                            //currpos = Program.GetObjPosition(SpellToDraw);
                            endpos = Program.GetMissileEndPos(SpellToDraw);
                        }
                        
                        SharpDX.Vector3 endpossharp = new SharpDX.Vector3(endpos.X, endpos.Y, endpos.Z);
                        DrawFactory.DrawCircleRange(endpossharp,(float)data2.radius,mycolor,2.0f,false);
                        
                    }
                }
            }
        }
        public static void DebugDraw(int activespell, bool fromspellbook)
        {
            System.Numerics.Vector2 ekran2 = new System.Numerics.Vector2();
            System.Numerics.Vector2 ekran3 = new System.Numerics.Vector2();
            string nazwa = Program.GetObjPlayerName(activespell);
            string nazwa2 = Program.GetActiveMissileName(activespell);
            
            if (!Program.WorldToScreen(Program.GetObjPosition(activespell), out ekran2))
            {
                return;
            }
            if (!Program.WorldToScreen(Program.GetObjVector3(activespell + 0x80), out ekran3))
            {
                //return;
            }
            DrawFactory.DrawFont(nazwa + "//" + nazwa2+"//"+activespell.ToString("X"), 10, new SharpDX.Vector2(ekran2.X*2,ekran2.Y*2), mycolor);
            //DrawFactory.DrawLine(ekran2.X,ekran2.Y,ekran3.X,ekran3.Y,3,mycolor);
        }
    }
}
