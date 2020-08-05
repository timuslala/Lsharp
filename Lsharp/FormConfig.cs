using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.CompilerServices;

namespace Lsharp
{
    
    public partial class form : Form
    {
        public static Thread UpdateVals;
        


        public form()
        {
            InitializeComponent();
            this.FormClosing += KillLoopThread;

    }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.labelPID.Text = "PID ligii to: "+Convert.ToString(Lsharp.Program.GetLeaguePID());
            if (this.labelPID.Text == "PID ligii to: 0")
            {
                this.labelPID.Text = "Odpal najpierw ligę debilu(PID = 0)";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonBaseAddress_Click(object sender, EventArgs e)
        {
            this.labelBaseAdress.Text = Lsharp.Program.GetBaseAddress("League of Legends.exe").ToString("X");
            if (this.labelBaseAdress.Text == "0")
            {
                this.labelBaseAdress.Text = "Odpal najpierw ligę debilu(BAdress = 0)";
            }
        }

        private void ReadMemory_Click(object sender, EventArgs e)
        {
            logs.Text = string.Concat(logs.Text, Environment.NewLine, Lsharp.Program.ReadMemoryL());
        }

        private void MasterSwitch_CheckedChanged(object sender, EventArgs e)
        {
            
            System.Windows.Forms.CheckBox chkbox = (System.Windows.Forms.CheckBox)sender;
            
            
            
            if (chkbox.Checked)
            {
                UpdateVals = new Thread(Program.MainDisplayLoop);
                UpdateVals.Start();
                
            }
            else
            {
                this.Close();
                
            }
        }
        private void KillLoopThread(object a,FormClosingEventArgs b)
        {
            
            try
            {
                UpdateVals.Abort();

            }
            catch
            {

            }
    }

        private void checkBoxShowAlly_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox senderr = (CheckBox)sender;
            if (senderr.Checked){
                Program.showallies = true;
            }
            else
            {
                Program.showallies = false;
            }
        }
    }
}
