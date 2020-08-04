using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Threading;
using System.Reflection.Emit;
using Lsharp.Overlay.Drawing;

namespace Lsharp
{
    public partial class FormOverlay : Form
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "SetWindowLongA")]
        private static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        static extern IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex);
        public const string WINDOW_NAME = "League of Legends (TM) Client";
        public System.Windows.Forms.Label[] BlueTeam = new System.Windows.Forms.Label[5];
        public System.Windows.Forms.Label[] RedTeam = new System.Windows.Forms.Label[5];
        public static RECT window_size;
        public Graphics g;
        public Pen myPen = new Pen(Color.Red,2.0f);
        public Pen myPen2 = new Pen(Color.DarkRed, 20f);
        public SolidBrush redBrush = new SolidBrush(Color.Red);
        public Font drawFont = new Font("Halvetica", 10);
        public static System.Drawing.Point[] BlueDrawPoint = new System.Drawing.Point[6];
        public static System.Drawing.Point[] RedDrawPoint = new System.Drawing.Point[6];
        public static string[] BlueDrawText = new string[6];
        public static string[] RedDrawText = new string[6];
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }
        public FormOverlay()
        {
            GetWindowSize();
            InitializeComponent();
            this.Shown += new System.EventHandler(Program.AfterOverlayLoaded);
            Overlay();
        }

        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }

        private const int WS_EX_TOPMOST = 0x00000008;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= WS_EX_TOPMOST;
                createParams.ExStyle |= 0x80;
                createParams.ExStyle |= 0x02000000;
                return createParams;
            }
        }
        private void GetWindowSize()
        {
            IntPtr handle = FindWindow(null, WINDOW_NAME);
            GetWindowRect(handle, out window_size);
        }
        private void Overlay()
        {
            this.ShowInTaskbar = false;
            this.BackColor = Color.Black;
            this.TransparencyKey = Color.Black;
            this.FormBorderStyle = FormBorderStyle.None;

            UserControl uc = new UserControl();
            IntPtr handle = FindWindow(null, WINDOW_NAME);
            IntPtr style =GetWindowLongPtr(this.Handle, -20);
            HandleRef handleref = new HandleRef(this, this.Handle);
            int test = (int)style | 0x80000 | 0x20;
            SetWindowLongPtr64(handleref,-20, new IntPtr(test));
            GetWindowRect(handle, out window_size);
            this.Size = new Size(window_size.Right - window_size.Left, window_size.Bottom - window_size.Top);
            this.Top = window_size.Top;
            this.Left = window_size.Left;
            this.DoubleBuffered = true;
            
        }

        
        

        private void InitializeComponent()
        {
            /*for (int i = 0; i < 5; i++)
            {
                this.RedTeam[i] = new System.Windows.Forms.Label();
                this.BlueTeam[i] = new System.Windows.Forms.Label();
                this.SuspendLayout();

                // 
                // RedTeam
                // 
                this.RedTeam[i].AutoSize = true;
                this.RedTeam[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 10.4F);
                this.RedTeam[i].Location = new System.Drawing.Point(10, 160);
                this.RedTeam[i].Name = "RedTeam";
                this.RedTeam[i].Size = new System.Drawing.Size(86, 25);
                this.RedTeam[i].TabIndex = 10;
                this.RedTeam[i].Text = "";
                this.RedTeam[i].ForeColor = System.Drawing.Color.Red;
                this.RedTeam[i].BackColor = System.Drawing.Color.Transparent;
                // 
                // BlueTeam
                // 
                this.BlueTeam[i].AutoSize = true;
                this.BlueTeam[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 10.4F);
                this.BlueTeam[i].Location = new System.Drawing.Point(window_size.Right - 200, 100);
                this.BlueTeam[i].Name = "BlueTeam";
                this.BlueTeam[i].Size = new System.Drawing.Size(86, 25);
                this.BlueTeam[i].TabIndex = 11;
                this.BlueTeam[i].Text = "";
                this.BlueTeam[i].ForeColor = System.Drawing.Color.Red;
                this.BlueTeam[i].BackColor = System.Drawing.Color.Transparent;
                // 
                // FormOverlay
                // 
                this.ClientSize = new System.Drawing.Size(284, 261);
                this.Controls.Add(this.BlueTeam[i]);
                this.Controls.Add(this.RedTeam[i]);
            }*/
            this.Name = "FormOverlay";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FormOverlay_Paint_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void FormOverlay_Paint_1(object sender, PaintEventArgs e)
        {
            
            //DrawFactory.DrawLine(0, 0, 100, 100, 5, new SharpDX.Color(10, 10, 10));
            g = e.Graphics;
            if (Program.localheroteam == Team.Blue || Program.m_form.checkBoxShowAlly.Checked)
            {
                for (int i = 0; i < 5; i++)
                {
                    try { 
                        DrawPath(Program.ChampionsRedCache[i]);
                        g.DrawString(RedDrawText[i], drawFont, redBrush, RedDrawPoint[i]);
                    
                        System.Numerics.Vector2 ekran = new System.Numerics.Vector2();
                        System.Numerics.Vector2 ekran2 = new System.Numerics.Vector2();
                        System.Numerics.Vector3 range;
                        System.Numerics.Vector3 range2;

                        for (int j = 0; j < 63; j = j + 1)
                        {
                            range = Program.ChampionsRedCache[i].position;
                            range.X = (int)(Program.ChampionsRedCache[i].position.X + Program.ChampionsRedCache[i].attackrange * Math.Sin((((j + 1) * 0.1) + 2)) + Program.ChampionsRedCache[i].attackrange * Math.Sin(((j + 1) * 0.1)));
                            range.Z = (int)(Program.ChampionsRedCache[i].position.Z + Program.ChampionsRedCache[i].attackrange * Math.Cos((((j + 1) * 0.1) + 2)) + Program.ChampionsRedCache[i].attackrange * Math.Cos(((j + 1) * 0.1)));
                            if(!Program.WorldToScreen(range, out ekran))
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
                            g.DrawLine(myPen, ekran.X, ekran.Y, ekran2.X, ekran2.Y);
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
                    g.DrawString(BlueDrawText[i], drawFont, redBrush, BlueDrawPoint[i]);
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
                            if(!Program.WorldToScreen(range, out ekran))
                            {
                                break;
                            }
                            range2 = Program.ChampionsBlueCache[i].position;
                            range2.X = (int)(Program.ChampionsBlueCache[i].position.X + Program.ChampionsBlueCache[i].attackrange * Math.Sin(((j * 0.1) + 2)) + Program.ChampionsBlueCache[i].attackrange * Math.Sin(j * 0.1));
                            range2.Z = (int)(Program.ChampionsBlueCache[i].position.Z + Program.ChampionsBlueCache[i].attackrange * Math.Cos(((j * 0.1) + 2)) + Program.ChampionsBlueCache[i].attackrange * Math.Cos(j * 0.1));
                            
                            if(!Program.WorldToScreen(range2, out ekran2))
                            {
                                break;
                            }
                            g.DrawLine(myPen, ekran.X, ekran.Y, ekran2.X, ekran2.Y);
                        }
                    }
                    catch { }
                }
            }
        
            int k = 0;
            while (Program.MissileArray[k] != 0)
            {
                //g.DrawString(Program.GetObjPlayerName(Program.MissileArray[k]), drawFont, redBrush, new Point(0+10*k, 0+100*k));
                string nazwa = Program.GetObjPlayerName(Program.MissileArray[k]);
                string nazwa2 = Program.GetMissileName(Program.MissileArray[k]);

                foreach (var data2 in Program.data.SelectTokens("*.*"))
                {
                    if (data2.type == "linear")
                    {
                        if (data2.missileName == nazwa|| data2.missileName == nazwa2)
                        {
                            //RYSOWANKO MISSILE
                            System.Numerics.Vector2 ekran3 = new System.Numerics.Vector2();
                            //System.Numerics.Vector2 start = new System.Numerics.Vector2();
                            //System.Numerics.Vector2 end = new System.Numerics.Vector2();
                            System.Numerics.Vector2 P1 = new System.Numerics.Vector2();
                            System.Numerics.Vector2 P2 = new System.Numerics.Vector2();
                            System.Numerics.Vector2 P3 = new System.Numerics.Vector2();
                            System.Numerics.Vector2 P4 = new System.Numerics.Vector2();
                            System.Numerics.Vector3 startpos = Program.GetMissileStartPos(Program.MissileArray[k]);
                            System.Numerics.Vector3 endpos = Program.GetMissileEndPos(Program.MissileArray[k]);
                            float spellWidth = data2.radius;

                            double width = endpos.X - startpos.X;
                            double height = endpos.Z - startpos.Z;
                            float Length = (float)Math.Sqrt(width * width + height * height); //Thats length of perpendicular
                            float NX = (float)(spellWidth * height / Length) ;
                            float NY = (float)(spellWidth * width / Length) ;
                            float range = data2.range;
                            endpos.X =(float)(startpos.X+ ( width / Length * range));
                            endpos.Z = (float)(startpos.Z+ (height / Length * range));
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
                            if (!Program.WorldToScreen(Program.GetObjPosition(Program.MissileArray[k]), out ekran3))
                            {
                                k++;
                                return;
                            }
                            if (!Program.WorldToScreen(P1new, out P1))
                            {
                                k++;
                                return;
                            }
                            if (!Program.WorldToScreen(P2new, out P2))
                            {
                                k++;
                                return;
                            }
                            if (!Program.WorldToScreen(P3new, out P3))
                            {
                                k++;
                                return;
                            }
                            if (!Program.WorldToScreen(P4new, out P4))
                            {
                                k++;
                                return;
                            }
                            int test = (int)P1.Y;
                            /*g.DrawString(Program.MissileArray[0].ToString(), drawFont, redBrush, new Point((int)ekran3.X, (int)ekran3.Y));
                            g.DrawString(P1.X.ToString() + "//" + P1.Y.ToString() + Environment.NewLine +
                                P2.X.ToString() + "//" + P2.Y.ToString() + Environment.NewLine +
                                P3.X.ToString() + "//" + P3.Y.ToString() + Environment.NewLine +
                                P4.X.ToString() + "//" + P4.Y.ToString() + Environment.NewLine +
                                Environment.NewLine +
                                P1new + "//" + P2new, drawFont, redBrush, new Point(0, 0));*/
                            g.DrawLine(myPen, P1.X, P1.Y, P2.X, P2.Y);
                            g.DrawLine(myPen, P2.X, P2.Y, P3.X, P3.Y);
                            g.DrawLine(myPen, P3.X, P3.Y, P4.X, P4.Y);
                            g.DrawLine(myPen, P4.X, P4.Y, P1.X, P1.Y);

                            /*
                            if (!Program.WorldToScreen(startpos,out start))
                            {
                                return;
                            }
                            if (!Program.WorldToScreen(endpos, out end))
                            {
                                return;
                            }
                            g.DrawLine(myPen, new Point((int)start.X,(int)start.Y), new Point((int)end.X, (int)end.Y));
                            g.DrawLine(myPen2, new Point((int)ekran3.X, (int)ekran3.Y), new Point((int)end.X, (int)end.Y));
                            */
                            //KONIEC RYSOWANKA MISSILE
                        }
                    }
                    continue;
                }
                k++;
                
            }

            for (int i = 0; i < 9; i++)
            {
                int activespell;
                if (i > 4)
                {
                    activespell = Program.GetObjInt32(Program.ChampionsRedCache[i-5].spellbook + 0x20);
                }
                else
                {
                    activespell = Program.GetObjInt32(Program.ChampionsBlueCache[i].spellbook + 0x20);
                }
                string nazwa = Program.GetObjString(Program.GetObjInt32(activespell+0x8)+0x18);
 

                foreach (var data2 in Program.data.SelectTokens("*.*"))
                {
                    if (data2.type == "linear")
                    {
                        if (data2.missileName == nazwa)
                        {
                            //RYSOWANKO MISSILE
                            System.Numerics.Vector2 ekran3 = new System.Numerics.Vector2();
                            //System.Numerics.Vector2 start = new System.Numerics.Vector2();
                            //System.Numerics.Vector2 end = new System.Numerics.Vector2();
                            System.Numerics.Vector2 P1 = new System.Numerics.Vector2();
                            System.Numerics.Vector2 P2 = new System.Numerics.Vector2();
                            System.Numerics.Vector2 P3 = new System.Numerics.Vector2();
                            System.Numerics.Vector2 P4 = new System.Numerics.Vector2();
                            System.Numerics.Vector3 startpos = Program.GetObjVector3(activespell+0x80);
                            System.Numerics.Vector3 endpos = Program.GetObjVector3(activespell + 0x8C);
                            float spellWidth = data2.radius;

                            double width = endpos.X - startpos.X;
                            double height = endpos.Z - startpos.Z;
                            float Length = (float)Math.Sqrt(width * width + height * height); //Thats length of perpendicular
                            float NX = (float)(spellWidth * height / Length) ;
                            float NY = (float)(spellWidth * width / Length) ;
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
                            if (!Program.WorldToScreen(Program.GetObjPosition(Program.MissileArray[k]), out ekran3))
                            {
                                k++;
                                continue;
                            }
                            if (!Program.WorldToScreen(P1new, out P1))
                            {
                                k++;
                                continue;
                            }
                            if (!Program.WorldToScreen(P2new, out P2))
                            {
                                k++;
                                continue;
                            }
                            if (!Program.WorldToScreen(P3new, out P3))
                            {
                                k++;
                                continue;
                            }
                            if (!Program.WorldToScreen(P4new, out P4))
                            {
                                k++;
                                return;
                            }


                            g.DrawLine(myPen, P1.X, P1.Y, P2.X, P2.Y);
                            g.DrawLine(myPen, P2.X, P2.Y, P3.X, P3.Y);
                            g.DrawLine(myPen, P3.X, P3.Y, P4.X, P4.Y);
                            g.DrawLine(myPen, P4.X, P4.Y, P1.X, P1.Y);

                        }
                    }
                    continue;
                }
            }

            }
        private void DrawPath(Champion cache)
        {
            System.Numerics.Vector2 ekran = new System.Numerics.Vector2();
            System.Numerics.Vector2 ekran2 = new System.Numerics.Vector2();
            System.Numerics.Vector3 path;
            
            path = Program.GetObjVector3(cache.AImanagerPtr + 0x10);
            if(!Program.WorldToScreen(path, out ekran))
            {
                return;
            }
            if(!Program.WorldToScreen(cache.position, out ekran2))
            {
                return;
            }
            g.DrawLine(myPen, ekran.X, ekran.Y, ekran2.X, ekran2.Y);
        }



    }

    
}
    

