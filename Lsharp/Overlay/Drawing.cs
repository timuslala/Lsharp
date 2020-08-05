using Lsharp.Overlay.Drawing;
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
            
            //DrawFactory.DrawLine(0, 0, 100, 100, 5, new SharpDX.Color(10, 10, 10));
            //g = e.Graphics;
            if (Program.localheroteam == Team.Blue || Program.m_form.checkBoxShowAlly.Checked)
            {
                for (int i = 0; i < 5; i++)
                {
                    try
                    {
                        DrawPath(Program.ChampionsRedCache[i]);
                        //g.DrawString(RedDrawText[i], drawFont, redBrush, Lsharp.FormOverlay.RedDrawPoint[i]);
                        DrawFactory.DrawFont(DrawManager.RedDrawText[i], 10, new SharpDX.Vector2(DrawManager.RedDrawPoint[i].X*2, DrawManager.RedDrawPoint[i].Y*2), mycolor);

                        System.Numerics.Vector2 ekran = new System.Numerics.Vector2();
                        System.Numerics.Vector2 ekran2 = new System.Numerics.Vector2();
                        System.Numerics.Vector3 range;
                        System.Numerics.Vector3 range2;

                        for (int j = 0; j < 63; j = j + 1)
                        {
                            range = Program.ChampionsRedCache[i].position;
                            range.X = (int)(Program.ChampionsRedCache[i].position.X + Program.ChampionsRedCache[i].attackrange * Math.Sin((((j + 1) * 0.1) + 2)) + Program.ChampionsRedCache[i].attackrange * Math.Sin(((j + 1) * 0.1)));
                            range.Z = (int)(Program.ChampionsRedCache[i].position.Z + Program.ChampionsRedCache[i].attackrange * Math.Cos((((j + 1) * 0.1) + 2)) + Program.ChampionsRedCache[i].attackrange * Math.Cos(((j + 1) * 0.1)));
                            if (!Program.WorldToScreen(range, out ekran))
                            {
                                break;
                            }
                            range2 = Program.ChampionsRedCache[i].position;
                            range2.X = (int)(Program.ChampionsRedCache[i].position.X + Program.ChampionsRedCache[i].attackrange * Math.Sin(((j * 0.1) + 2)) + Program.ChampionsRedCache[i].attackrange * Math.Sin(j * 0.1));
                            range2.Z = (int)(Program.ChampionsRedCache[i].position.Z + Program.ChampionsRedCache[i].attackrange * Math.Cos(((j * 0.1) + 2)) + Program.ChampionsRedCache[i].attackrange * Math.Cos(j * 0.1));

                            if (!Program.WorldToScreen(range2, out ekran2))
                            {
                                break;
                            }
                            DrawFactory.DrawLine(ekran.X, ekran.Y, ekran2.X, ekran2.Y, 2, mycolor);
                        }
                    }
                    catch { }
                }
            }
            if (Program.localheroteam == Team.Red || Program.m_form.checkBoxShowAlly.Checked)
            {
                for (int i = 0; i < 5; i++)
                {
                    DrawPath(Program.ChampionsBlueCache[i]);
                    //g.DrawString(Lsharp.FormOverlay.BlueDrawText[i], drawFont, redBrush, Lsharp.FormOverlay.BlueDrawPoint[i]);
                    DrawFactory.DrawFont(DrawManager.BlueDrawText[i],10, new SharpDX.Vector2(DrawManager.BlueDrawPoint[i].X*2, DrawManager.BlueDrawPoint[i].Y*2),mycolor);
                    try
                    {
                        System.Numerics.Vector2 ekran = new System.Numerics.Vector2();
                        System.Numerics.Vector2 ekran2 = new System.Numerics.Vector2();
                        System.Numerics.Vector3 range;
                        System.Numerics.Vector3 range2;

                        for (int j = 0; j < 63; j = j + 1)
                        {
                            range = Program.ChampionsBlueCache[i].position;
                            range.X = (int)(Program.ChampionsBlueCache[i].position.X + Program.ChampionsBlueCache[i].attackrange * Math.Sin((((j + 1) * 0.1) + 2)) + Program.ChampionsBlueCache[i].attackrange * Math.Sin(((j + 1) * 0.1)));
                            range.Z = (int)(Program.ChampionsBlueCache[i].position.Z + Program.ChampionsBlueCache[i].attackrange * Math.Cos((((j + 1) * 0.1) + 2)) + Program.ChampionsBlueCache[i].attackrange * Math.Cos(((j + 1) * 0.1)));
                            if (!Program.WorldToScreen(range, out ekran))
                            {
                                break;
                            }
                            range2 = Program.ChampionsBlueCache[i].position;
                            range2.X = (int)(Program.ChampionsBlueCache[i].position.X + Program.ChampionsBlueCache[i].attackrange * Math.Sin(((j * 0.1) + 2)) + Program.ChampionsBlueCache[i].attackrange * Math.Sin(j * 0.1));
                            range2.Z = (int)(Program.ChampionsBlueCache[i].position.Z + Program.ChampionsBlueCache[i].attackrange * Math.Cos(((j * 0.1) + 2)) + Program.ChampionsBlueCache[i].attackrange * Math.Cos(j * 0.1));

                            if (!Program.WorldToScreen(range2, out ekran2))
                            {
                                break;
                            }
                            DrawFactory.DrawLine(ekran.X, ekran.Y, ekran2.X, ekran2.Y, 2, mycolor);
                        }
                    }
                    catch { }
                }
            }

            int k = 0;
            while (Program.MissileArray[k] != 0)
            {
                DrawSpell(Program.MissileArray[k]);
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
                DrawSpell(activespell);
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

            //g.DrawLine(myPen, ekran.X, ekran.Y, ekran2.X, ekran2.Y);
        }

        public static void DrawSpell(int SpellToDraw)
        {

            string nazwa = Program.GetObjPlayerName(SpellToDraw);
            string nazwa2 = Program.GetMissileName(SpellToDraw);

            foreach (var data2 in Program.data.SelectTokens("*.*"))
            {
                if (data2.type == "linear")
                {
                    if (data2.missileName == nazwa || data2.missileName == nazwa2)
                    {
                        //RYSOWANKO MISSILE
                        System.Numerics.Vector2 ekran3 = new System.Numerics.Vector2();
                        //System.Numerics.Vector2 start = new System.Numerics.Vector2();
                        //System.Numerics.Vector2 end = new System.Numerics.Vector2();
                        System.Numerics.Vector2 P1 = new System.Numerics.Vector2();
                        System.Numerics.Vector2 P2 = new System.Numerics.Vector2();
                        System.Numerics.Vector2 P3 = new System.Numerics.Vector2();
                        System.Numerics.Vector2 P4 = new System.Numerics.Vector2();
                        System.Numerics.Vector3 startpos = Program.GetMissileStartPos(SpellToDraw);
                        System.Numerics.Vector3 endpos = Program.GetMissileEndPos(SpellToDraw);
                        float spellWidth = data2.radius;

                        double width = endpos.X - startpos.X;
                        double height = endpos.Z - startpos.Z;
                        float Length = (float)Math.Sqrt(width * width + height * height); //Thats length of perpendicular
                        float NX = (float)(spellWidth * height / Length);
                        float NY = (float)(spellWidth * width / Length);
                        float range = data2.range;
                        endpos.X = (float)(startpos.X + (width / Length * range));
                        endpos.Z = (float)(startpos.Z + (height / Length * range));
                        float R1X = startpos.X - NX;
                        float R1Y = startpos.Z + NY;
                        float R2X = startpos.X + NX;
                        float R2Y = startpos.Z - NY;
                        float R3X = endpos.X + NX;
                        float R3Y = endpos.Z - NY;
                        float R4X = endpos.X - NX;
                        float R4Y = endpos.Z + NY;
                        System.Numerics.Vector3 P1new = new System.Numerics.Vector3(R1X, startpos.Y, R1Y);
                        System.Numerics.Vector3 P2new = new System.Numerics.Vector3(R2X, startpos.Y, R2Y);
                        System.Numerics.Vector3 P3new = new System.Numerics.Vector3(R3X, endpos.Y, R3Y);
                        System.Numerics.Vector3 P4new = new System.Numerics.Vector3(R4X, endpos.Y, R4Y);
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

                    }
                }
            }
        }
    }
}
