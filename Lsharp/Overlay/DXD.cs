using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpDX;
using SharpDX.Direct3D9;
using Lsharp.Overlay.Drawing;
using SharpDX.Windows;
using Lsharp.Overlay;
using System.Runtime.InteropServices;
namespace ExSharpBase.Overlay
{

    public partial class DXD : Form
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        public const string WINDOW_NAME = "League of Legends (TM) Client";
        public static RECT window_size;
        NativeImport NativeImport = new NativeImport();
        public struct RECT
        {
            public int Left;        // x position of upper-left corner
            public int Top;         // y position of upper-left corner
            public int Right;       // x position of lower-right corner
            public int Bottom;      // y position of lower-right corner
        }
        public DXD()
        {
            InitializeComponent();
            GetWindowSize();
            this.DoubleBuffered = true;
        }
        private void GetWindowSize()
        {
            IntPtr handle = FindWindow(null, WINDOW_NAME);
            GetWindowRect(handle, out window_size);
        }
        private void DXD_Load(object sender, EventArgs e)
        {
            OnLoad();
        }

        private void DXD_Paint(object sender, PaintEventArgs e)
        {
            OnPaint();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                // turn on WS_EX_TOOLWINDOW style bit
                cp.ExStyle |= 0x80;
                return cp;
            }
        }

        internal void OnDraw()
        {
            RenderLoop.Run(this, () =>
            {


            });
        }

        public void DrawScene() {
            //if (!ExSharpBase.Events.Drawing.IsMenuBeingDrawn) NativeImport.BringWindowToTop(this.Handle);
            NativeImport.BringWindowToTop(this.Handle);//new
            DrawFactory.device.Clear(ClearFlags.Target, new SharpDX.Mathematics.Interop.RawColorBGRA(0, 0, 0, 0), 1.0f, 0);
            DrawFactory.device.SetRenderState(RenderState.ZEnable, false);
            DrawFactory.device.SetRenderState(RenderState.Lighting, false);
            DrawFactory.device.SetRenderState(RenderState.CullMode, Cull.None);
            DrawFactory.device.SetTransform(TransformState.Projection, Matrix.OrthoOffCenterLH(0, this.Width, this.Height, 0, 0, 1));

            DrawFactory.device.BeginScene();
            DrawManager.OnDrawMan();
            //ExSharpBase.Events.Drawing.OnDeviceDraw();
            //DrawFactory.DrawLine(0, 0, 1000, 1000, 5, new SharpDX.Color(255, 100, 0));
            DrawFactory.device.EndScene();
            DrawFactory.device.Present();
        }
        private static bool IsInitialised = false;
        internal void OnLoad()
        {
            NativeImport.SetWindowLong(this.Handle, DrawFactory.GWL_EXSTYLE,
            (IntPtr)(NativeImport.GetWindowLong(this.Handle, DrawFactory.GWL_EXSTYLE) ^ DrawFactory.WS_EX_LAYERED ^ DrawFactory.WS_EX_TRANSPARENT));

            NativeImport.SetLayeredWindowAttributes(this.Handle, 0, 255, DrawFactory.LWA_ALPHA);

            if (IsInitialised == false)
            {
                PresentParameters presentParameters = new PresentParameters();
                presentParameters.Windowed = true;
                presentParameters.SwapEffect = SwapEffect.Discard;
                presentParameters.BackBufferFormat = Format.A8R8G8B8;

                DrawFactory.device = new Device(DrawFactory.D3D, 0, DeviceType.Hardware, this.Handle, CreateFlags.HardwareVertexProcessing, presentParameters);

                DrawFactory.drawLine = new Line(DrawFactory.device);
                DrawFactory.drawBoxLine = new Line(DrawFactory.device);
                DrawFactory.drawCircleLine = new Line(DrawFactory.device);
                DrawFactory.drawFilledBoxLine = new Line(DrawFactory.device);
                DrawFactory.drawTriLine = new Line(DrawFactory.device);

                FontDescription fontDescription = new FontDescription()
                {
                    FaceName = "Fixedsys Regular",
                    CharacterSet = FontCharacterSet.Default,
                    Height = 20,
                    Weight = FontWeight.Bold,
                    MipLevels = 0,
                    OutputPrecision = FontPrecision.Default,
                    PitchAndFamily = FontPitchAndFamily.Default,
                    Quality = FontQuality.ClearType
                };

                DrawFactory.font = new SharpDX.Direct3D9.Font(DrawFactory.device, fontDescription);
                DrawFactory.InitialiseCircleDrawing(DrawFactory.device);

                IsInitialised = true;

                OnDraw();
            }
        }

        public void OnPaint()
        {
            DrawFactory.Marg.Left = 0;
            DrawFactory.Marg.Top = 0;
            DrawFactory.Marg.Right = this.Width;
            DrawFactory.Marg.Bottom = this.Height;

            NativeImport.DwmExtendFrameIntoClientArea(this.Handle, ref DrawFactory.Marg);
        }
    }
}
