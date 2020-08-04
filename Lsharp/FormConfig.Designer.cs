using System.Reflection.Emit;

namespace Lsharp
{
    partial class form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form));
            this.buttonPID = new System.Windows.Forms.Button();
            this.labelPID = new System.Windows.Forms.Label();
            this.buttonBaseAddress = new System.Windows.Forms.Button();
            this.labelBaseAdress = new System.Windows.Forms.Label();
            this.logs = new System.Windows.Forms.TextBox();
            this.ButtonReadMemory = new System.Windows.Forms.Button();
            this.ListAttachedDlls = new System.Windows.Forms.CheckBox();
            this.MasterSwitch = new System.Windows.Forms.CheckBox();
            this.labelHp = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageDisplay = new System.Windows.Forms.TabPage();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.labelChamps = new System.Windows.Forms.Label();
            this.labelLocalName = new System.Windows.Forms.Label();
            this.tabPageDebug = new System.Windows.Forms.TabPage();
            this.TextBoxValueToFind = new System.Windows.Forms.TextBox();
            this.CheckBoxIsString = new System.Windows.Forms.CheckBox();
            this.CheckBoxIsSingle = new System.Windows.Forms.CheckBox();
            this.CheckBoxIsInt32 = new System.Windows.Forms.CheckBox();
            this.Warning = new System.Windows.Forms.Label();
            this.checkBoxShowAlly = new System.Windows.Forms.CheckBox();
            this.tabControl1.SuspendLayout();
            this.tabPageDisplay.SuspendLayout();
            this.tabPageDebug.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonPID
            // 
            this.buttonPID.Location = new System.Drawing.Point(11, 124);
            this.buttonPID.Name = "buttonPID";
            this.buttonPID.Size = new System.Drawing.Size(112, 23);
            this.buttonPID.TabIndex = 0;
            this.buttonPID.Text = "Grabnij PID\'a";
            this.buttonPID.UseVisualStyleBackColor = true;
            this.buttonPID.Click += new System.EventHandler(this.button1_Click);
            // 
            // labelPID
            // 
            this.labelPID.AutoSize = true;
            this.labelPID.Location = new System.Drawing.Point(8, 108);
            this.labelPID.Name = "labelPID";
            this.labelPID.Size = new System.Drawing.Size(101, 13);
            this.labelPID.TabIndex = 1;
            this.labelPID.Text = "Tu będzie PID ligii :)";
            this.labelPID.Click += new System.EventHandler(this.label1_Click);
            // 
            // buttonBaseAddress
            // 
            this.buttonBaseAddress.Location = new System.Drawing.Point(8, 82);
            this.buttonBaseAddress.Name = "buttonBaseAddress";
            this.buttonBaseAddress.Size = new System.Drawing.Size(112, 23);
            this.buttonBaseAddress.TabIndex = 2;
            this.buttonBaseAddress.Text = "Grabnij BaseAdress";
            this.buttonBaseAddress.UseVisualStyleBackColor = true;
            this.buttonBaseAddress.Click += new System.EventHandler(this.buttonBaseAddress_Click);
            // 
            // labelBaseAdress
            // 
            this.labelBaseAdress.AutoSize = true;
            this.labelBaseAdress.Location = new System.Drawing.Point(8, 66);
            this.labelBaseAdress.Name = "labelBaseAdress";
            this.labelBaseAdress.Size = new System.Drawing.Size(120, 13);
            this.labelBaseAdress.TabIndex = 3;
            this.labelBaseAdress.Text = "Tu będzie base address";
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(23, 153);
            this.logs.Multiline = true;
            this.logs.Name = "logs";
            this.logs.Size = new System.Drawing.Size(200, 200);
            this.logs.TabIndex = 4;
            // 
            // ButtonReadMemory
            // 
            this.ButtonReadMemory.Location = new System.Drawing.Point(275, 289);
            this.ButtonReadMemory.Name = "ButtonReadMemory";
            this.ButtonReadMemory.Size = new System.Drawing.Size(90, 23);
            this.ButtonReadMemory.TabIndex = 5;
            this.ButtonReadMemory.Text = "ReadMemory";
            this.ButtonReadMemory.UseVisualStyleBackColor = true;
            this.ButtonReadMemory.Click += new System.EventHandler(this.ReadMemory_Click);
            // 
            // ListAttachedDlls
            // 
            this.ListAttachedDlls.AutoSize = true;
            this.ListAttachedDlls.Location = new System.Drawing.Point(199, 124);
            this.ListAttachedDlls.Name = "ListAttachedDlls";
            this.ListAttachedDlls.Size = new System.Drawing.Size(231, 17);
            this.ListAttachedDlls.TabIndex = 6;
            this.ListAttachedDlls.Text = "Logować listę dołączonych dll do procesu?";
            this.ListAttachedDlls.UseVisualStyleBackColor = true;
            // 
            // MasterSwitch
            // 
            this.MasterSwitch.AutoSize = true;
            this.MasterSwitch.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.MasterSwitch.Location = new System.Drawing.Point(426, 433);
            this.MasterSwitch.Name = "MasterSwitch";
            this.MasterSwitch.Size = new System.Drawing.Size(115, 17);
            this.MasterSwitch.TabIndex = 7;
            this.MasterSwitch.Text = "MASTER ON/OFF";
            this.MasterSwitch.UseVisualStyleBackColor = true;
            this.MasterSwitch.CheckedChanged += new System.EventHandler(this.MasterSwitch_CheckedChanged);
            // 
            // labelHp
            // 
            this.labelHp.AutoSize = true;
            this.labelHp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.4F);
            this.labelHp.Location = new System.Drawing.Point(8, 100);
            this.labelHp.Name = "labelHp";
            this.labelHp.Size = new System.Drawing.Size(0, 25);
            this.labelHp.TabIndex = 8;
            this.labelHp.Tag = "Program.HP";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageDisplay);
            this.tabControl1.Controls.Add(this.tabPageDebug);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(555, 482);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPageDisplay
            // 
            this.tabPageDisplay.Controls.Add(this.checkBoxShowAlly);
            this.tabPageDisplay.Controls.Add(this.treeView1);
            this.tabPageDisplay.Controls.Add(this.labelChamps);
            this.tabPageDisplay.Controls.Add(this.labelLocalName);
            this.tabPageDisplay.Controls.Add(this.labelHp);
            this.tabPageDisplay.Controls.Add(this.MasterSwitch);
            this.tabPageDisplay.Location = new System.Drawing.Point(4, 22);
            this.tabPageDisplay.Name = "tabPageDisplay";
            this.tabPageDisplay.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDisplay.Size = new System.Drawing.Size(547, 456);
            this.tabPageDisplay.TabIndex = 1;
            this.tabPageDisplay.Text = "Display";
            this.tabPageDisplay.UseVisualStyleBackColor = true;
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(309, 112);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(232, 253);
            this.treeView1.TabIndex = 11;
            // 
            // labelChamps
            // 
            this.labelChamps.AutoSize = true;
            this.labelChamps.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.4F);
            this.labelChamps.Location = new System.Drawing.Point(8, 268);
            this.labelChamps.Name = "labelChamps";
            this.labelChamps.Size = new System.Drawing.Size(86, 25);
            this.labelChamps.TabIndex = 10;
            this.labelChamps.Tag = "Program.HP";
            this.labelChamps.Text = "Champs";
            // 
            // labelLocalName
            // 
            this.labelLocalName.AutoSize = true;
            this.labelLocalName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.4F);
            this.labelLocalName.Location = new System.Drawing.Point(8, 12);
            this.labelLocalName.Name = "labelLocalName";
            this.labelLocalName.Size = new System.Drawing.Size(0, 25);
            this.labelLocalName.TabIndex = 9;
            this.labelLocalName.Tag = "Program.HP";
            // 
            // tabPageDebug
            // 
            this.tabPageDebug.Controls.Add(this.TextBoxValueToFind);
            this.tabPageDebug.Controls.Add(this.CheckBoxIsString);
            this.tabPageDebug.Controls.Add(this.CheckBoxIsSingle);
            this.tabPageDebug.Controls.Add(this.CheckBoxIsInt32);
            this.tabPageDebug.Controls.Add(this.Warning);
            this.tabPageDebug.Controls.Add(this.logs);
            this.tabPageDebug.Controls.Add(this.ButtonReadMemory);
            this.tabPageDebug.Controls.Add(this.ListAttachedDlls);
            this.tabPageDebug.Controls.Add(this.labelBaseAdress);
            this.tabPageDebug.Controls.Add(this.buttonBaseAddress);
            this.tabPageDebug.Controls.Add(this.labelPID);
            this.tabPageDebug.Controls.Add(this.buttonPID);
            this.tabPageDebug.Location = new System.Drawing.Point(4, 22);
            this.tabPageDebug.Name = "tabPageDebug";
            this.tabPageDebug.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageDebug.Size = new System.Drawing.Size(547, 456);
            this.tabPageDebug.TabIndex = 0;
            this.tabPageDebug.Text = "Debug, search for offsets";
            this.tabPageDebug.UseVisualStyleBackColor = true;
            // 
            // TextBoxValueToFind
            // 
            this.TextBoxValueToFind.Location = new System.Drawing.Point(275, 192);
            this.TextBoxValueToFind.Name = "TextBoxValueToFind";
            this.TextBoxValueToFind.Size = new System.Drawing.Size(100, 20);
            this.TextBoxValueToFind.TabIndex = 14;
            // 
            // CheckBoxIsString
            // 
            this.CheckBoxIsString.AutoSize = true;
            this.CheckBoxIsString.Location = new System.Drawing.Point(275, 266);
            this.CheckBoxIsString.Name = "CheckBoxIsString";
            this.CheckBoxIsString.Size = new System.Drawing.Size(101, 17);
            this.CheckBoxIsString.TabIndex = 13;
            this.CheckBoxIsString.Text = "Might be string?";
            this.CheckBoxIsString.UseVisualStyleBackColor = true;
            // 
            // CheckBoxIsSingle
            // 
            this.CheckBoxIsSingle.AutoSize = true;
            this.CheckBoxIsSingle.Location = new System.Drawing.Point(275, 242);
            this.CheckBoxIsSingle.Name = "CheckBoxIsSingle";
            this.CheckBoxIsSingle.Size = new System.Drawing.Size(103, 17);
            this.CheckBoxIsSingle.TabIndex = 12;
            this.CheckBoxIsSingle.Text = "Might be single?";
            this.CheckBoxIsSingle.UseVisualStyleBackColor = false;
            // 
            // CheckBoxIsInt32
            // 
            this.CheckBoxIsInt32.AutoSize = true;
            this.CheckBoxIsInt32.Location = new System.Drawing.Point(275, 218);
            this.CheckBoxIsInt32.Name = "CheckBoxIsInt32";
            this.CheckBoxIsInt32.Size = new System.Drawing.Size(100, 17);
            this.CheckBoxIsInt32.TabIndex = 11;
            this.CheckBoxIsInt32.Text = "Might be Int32?";
            this.CheckBoxIsInt32.UseVisualStyleBackColor = true;
            // 
            // Warning
            // 
            this.Warning.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.Warning.ForeColor = System.Drawing.Color.Red;
            this.Warning.Location = new System.Drawing.Point(20, 18);
            this.Warning.Name = "Warning";
            this.Warning.Size = new System.Drawing.Size(374, 35);
            this.Warning.TabIndex = 10;
            this.Warning.Text = "THIS PAGE IS NOT IN ENGLISH AND YOU SHOULD NOT USE IT IF YOU ARE NOT MESSING WITH" +
    " SOURCE";
            // 
            // checkBoxShowAlly
            // 
            this.checkBoxShowAlly.AutoSize = true;
            this.checkBoxShowAlly.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.checkBoxShowAlly.Location = new System.Drawing.Point(426, 410);
            this.checkBoxShowAlly.Name = "checkBoxShowAlly";
            this.checkBoxShowAlly.Size = new System.Drawing.Size(89, 17);
            this.checkBoxShowAlly.TabIndex = 12;
            this.checkBoxShowAlly.Text = "SHOW ALLY";
            this.checkBoxShowAlly.UseVisualStyleBackColor = true;
            this.checkBoxShowAlly.CheckedChanged += new System.EventHandler(this.checkBoxShowAlly_CheckedChanged);
            // 
            // form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(771, 523);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "form";
            this.Text = "Aplikacja Boga programistyki :)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPageDisplay.ResumeLayout(false);
            this.tabPageDisplay.PerformLayout();
            this.tabPageDebug.ResumeLayout(false);
            this.tabPageDebug.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonPID;
        private System.Windows.Forms.Label labelPID;
        private System.Windows.Forms.Button buttonBaseAddress;
        private System.Windows.Forms.Label labelBaseAdress;
        public System.Windows.Forms.TextBox logs;
        public System.Windows.Forms.CheckBox ListAttachedDlls;
        private System.Windows.Forms.CheckBox MasterSwitch;
        public System.Windows.Forms.Label labelHp;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPageDisplay;
        private System.Windows.Forms.Label Warning;
        public System.Windows.Forms.TextBox TextBoxValueToFind;
        public System.Windows.Forms.CheckBox CheckBoxIsString;
        public System.Windows.Forms.CheckBox CheckBoxIsSingle;
        public System.Windows.Forms.CheckBox CheckBoxIsInt32;
        public System.Windows.Forms.Button ButtonReadMemory;
        public System.Windows.Forms.TabPage tabPageDebug;
        public System.Windows.Forms.Label labelLocalName;
        public System.Windows.Forms.Label labelChamps;
        private System.Windows.Forms.TreeView treeView1;
        public System.Windows.Forms.CheckBox checkBoxShowAlly;
    }
}

