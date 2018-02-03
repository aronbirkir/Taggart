namespace Taggart
{
    partial class MainForm
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panelMikCuePoints = new System.Windows.Forms.Panel();
            this.panelLibraryCuePoints = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnReadMIK = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCopyAll = new System.Windows.Forms.Button();
            this.btnCopyMissing = new System.Windows.Forms.Button();
            this.cbAutoMik = new System.Windows.Forms.CheckBox();
            this.cuePointH = new Taggart.CuePointControl();
            this.cuepointA = new Taggart.CuePointControl();
            this.cuePointG = new Taggart.CuePointControl();
            this.cuePointB = new Taggart.CuePointControl();
            this.cuePointF = new Taggart.CuePointControl();
            this.cuePointC = new Taggart.CuePointControl();
            this.cuePointE = new Taggart.CuePointControl();
            this.cuePointD = new Taggart.CuePointControl();
            this.cuePointMik1 = new Taggart.CuePointControl();
            this.cuePointMik8 = new Taggart.CuePointControl();
            this.cuePointMik2 = new Taggart.CuePointControl();
            this.cuePointMik7 = new Taggart.CuePointControl();
            this.cuePointMik3 = new Taggart.CuePointControl();
            this.cuePointMik6 = new Taggart.CuePointControl();
            this.cuePointMik4 = new Taggart.CuePointControl();
            this.cuePointMik5 = new Taggart.CuePointControl();
            this.btnSaveLibrary = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelMikCuePoints.SuspendLayout();
            this.panelLibraryCuePoints.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 43);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(716, 27);
            this.textBox1.TabIndex = 0;
            this.textBox1.DoubleClick += new System.EventHandler(this.textBox1_DoubleClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 22);
            this.label1.TabIndex = 2;
            this.label1.Text = "Library Location";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(734, 41);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 33);
            this.button1.TabIndex = 3;
            this.button1.Text = "Scan";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 114);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1206, 974);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // panelMikCuePoints
            // 
            this.panelMikCuePoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelMikCuePoints.Controls.Add(this.cuePointMik1);
            this.panelMikCuePoints.Controls.Add(this.cuePointMik8);
            this.panelMikCuePoints.Controls.Add(this.cuePointMik2);
            this.panelMikCuePoints.Controls.Add(this.cuePointMik7);
            this.panelMikCuePoints.Controls.Add(this.cuePointMik3);
            this.panelMikCuePoints.Controls.Add(this.cuePointMik6);
            this.panelMikCuePoints.Controls.Add(this.cuePointMik4);
            this.panelMikCuePoints.Controls.Add(this.cuePointMik5);
            this.panelMikCuePoints.Location = new System.Drawing.Point(1228, 455);
            this.panelMikCuePoints.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelMikCuePoints.Name = "panelMikCuePoints";
            this.panelMikCuePoints.Size = new System.Drawing.Size(224, 248);
            this.panelMikCuePoints.TabIndex = 16;
            // 
            // panelLibraryCuePoints
            // 
            this.panelLibraryCuePoints.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panelLibraryCuePoints.Controls.Add(this.cuePointH);
            this.panelLibraryCuePoints.Controls.Add(this.cuepointA);
            this.panelLibraryCuePoints.Controls.Add(this.cuePointG);
            this.panelLibraryCuePoints.Controls.Add(this.cuePointB);
            this.panelLibraryCuePoints.Controls.Add(this.cuePointF);
            this.panelLibraryCuePoints.Controls.Add(this.cuePointC);
            this.panelLibraryCuePoints.Controls.Add(this.cuePointE);
            this.panelLibraryCuePoints.Controls.Add(this.cuePointD);
            this.panelLibraryCuePoints.Location = new System.Drawing.Point(1227, 114);
            this.panelLibraryCuePoints.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelLibraryCuePoints.Name = "panelLibraryCuePoints";
            this.panelLibraryCuePoints.Size = new System.Drawing.Size(228, 244);
            this.panelLibraryCuePoints.TabIndex = 17;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1225, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(166, 22);
            this.label2.TabIndex = 18;
            this.label2.Text = "Library Track Cue Points";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1229, 425);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(164, 22);
            this.label3.TabIndex = 19;
            this.label3.Text = "Mixed In Key Cue Points";
            // 
            // btnReadMIK
            // 
            this.btnReadMIK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReadMIK.Font = new System.Drawing.Font("Arial Narrow", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReadMIK.Location = new System.Drawing.Point(1397, 422);
            this.btnReadMIK.Margin = new System.Windows.Forms.Padding(1);
            this.btnReadMIK.Name = "btnReadMIK";
            this.btnReadMIK.Size = new System.Drawing.Size(59, 29);
            this.btnReadMIK.TabIndex = 20;
            this.btnReadMIK.Text = "Read";
            this.btnReadMIK.UseVisualStyleBackColor = true;
            this.btnReadMIK.Click += new System.EventHandler(this.btnReadMIK_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 22);
            this.label4.TabIndex = 21;
            this.label4.Text = "Library Tracks";
            // 
            // btnCopyAll
            // 
            this.btnCopyAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyAll.Font = new System.Drawing.Font("Arial Narrow", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyAll.Location = new System.Drawing.Point(1227, 378);
            this.btnCopyAll.Margin = new System.Windows.Forms.Padding(1);
            this.btnCopyAll.Name = "btnCopyAll";
            this.btnCopyAll.Size = new System.Drawing.Size(92, 29);
            this.btnCopyAll.TabIndex = 22;
            this.btnCopyAll.Text = "Copy All";
            this.btnCopyAll.UseVisualStyleBackColor = true;
            this.btnCopyAll.Click += new System.EventHandler(this.btnCopyAll_Click);
            // 
            // btnCopyMissing
            // 
            this.btnCopyMissing.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyMissing.Font = new System.Drawing.Font("Arial Narrow", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCopyMissing.Location = new System.Drawing.Point(1336, 378);
            this.btnCopyMissing.Margin = new System.Windows.Forms.Padding(1);
            this.btnCopyMissing.Name = "btnCopyMissing";
            this.btnCopyMissing.Size = new System.Drawing.Size(119, 29);
            this.btnCopyMissing.TabIndex = 23;
            this.btnCopyMissing.Text = "Copy Missing";
            this.btnCopyMissing.UseVisualStyleBackColor = true;
            this.btnCopyMissing.Click += new System.EventHandler(this.btnCopyMissing_Click);
            // 
            // cbAutoMik
            // 
            this.cbAutoMik.AutoSize = true;
            this.cbAutoMik.Location = new System.Drawing.Point(835, 45);
            this.cbAutoMik.Name = "cbAutoMik";
            this.cbAutoMik.Size = new System.Drawing.Size(89, 26);
            this.cbAutoMik.TabIndex = 24;
            this.cbAutoMik.Text = "Auto MIK";
            this.cbAutoMik.UseVisualStyleBackColor = true;
            // 
            // cuePointH
            // 
            this.cuePointH.Caption = "H";
            this.cuePointH.Location = new System.Drawing.Point(1, 210);
            this.cuePointH.Margin = new System.Windows.Forms.Padding(0);
            this.cuePointH.Name = "cuePointH";
            this.cuePointH.Size = new System.Drawing.Size(222, 30);
            this.cuePointH.TabIndex = 7;
            this.cuePointH.Time = null;
            // 
            // cuepointA
            // 
            this.cuepointA.Caption = "A";
            this.cuepointA.Location = new System.Drawing.Point(1, 1);
            this.cuepointA.Margin = new System.Windows.Forms.Padding(0);
            this.cuepointA.Name = "cuepointA";
            this.cuepointA.Size = new System.Drawing.Size(222, 27);
            this.cuepointA.TabIndex = 0;
            this.cuepointA.Time = null;
            // 
            // cuePointG
            // 
            this.cuePointG.Caption = "G";
            this.cuePointG.Location = new System.Drawing.Point(1, 180);
            this.cuePointG.Margin = new System.Windows.Forms.Padding(0);
            this.cuePointG.Name = "cuePointG";
            this.cuePointG.Size = new System.Drawing.Size(222, 29);
            this.cuePointG.TabIndex = 6;
            this.cuePointG.Time = null;
            // 
            // cuePointB
            // 
            this.cuePointB.Caption = "B";
            this.cuePointB.Location = new System.Drawing.Point(1, 30);
            this.cuePointB.Margin = new System.Windows.Forms.Padding(0);
            this.cuePointB.Name = "cuePointB";
            this.cuePointB.Size = new System.Drawing.Size(222, 26);
            this.cuePointB.TabIndex = 1;
            this.cuePointB.Time = null;
            // 
            // cuePointF
            // 
            this.cuePointF.Caption = "F";
            this.cuePointF.Location = new System.Drawing.Point(1, 150);
            this.cuePointF.Margin = new System.Windows.Forms.Padding(0);
            this.cuePointF.Name = "cuePointF";
            this.cuePointF.Size = new System.Drawing.Size(222, 30);
            this.cuePointF.TabIndex = 5;
            this.cuePointF.Time = null;
            // 
            // cuePointC
            // 
            this.cuePointC.Caption = "C";
            this.cuePointC.Location = new System.Drawing.Point(1, 60);
            this.cuePointC.Margin = new System.Windows.Forms.Padding(0);
            this.cuePointC.Name = "cuePointC";
            this.cuePointC.Size = new System.Drawing.Size(222, 26);
            this.cuePointC.TabIndex = 2;
            this.cuePointC.Time = null;
            // 
            // cuePointE
            // 
            this.cuePointE.Caption = "E";
            this.cuePointE.Location = new System.Drawing.Point(1, 120);
            this.cuePointE.Margin = new System.Windows.Forms.Padding(0);
            this.cuePointE.Name = "cuePointE";
            this.cuePointE.Size = new System.Drawing.Size(222, 29);
            this.cuePointE.TabIndex = 4;
            this.cuePointE.Time = null;
            // 
            // cuePointD
            // 
            this.cuePointD.Caption = "D";
            this.cuePointD.Location = new System.Drawing.Point(1, 90);
            this.cuePointD.Margin = new System.Windows.Forms.Padding(0);
            this.cuePointD.Name = "cuePointD";
            this.cuePointD.Size = new System.Drawing.Size(222, 30);
            this.cuePointD.TabIndex = 3;
            this.cuePointD.Time = null;
            // 
            // cuePointMik1
            // 
            this.cuePointMik1.Caption = "1";
            this.cuePointMik1.Location = new System.Drawing.Point(1, 1);
            this.cuePointMik1.Margin = new System.Windows.Forms.Padding(0);
            this.cuePointMik1.Name = "cuePointMik1";
            this.cuePointMik1.Size = new System.Drawing.Size(222, 27);
            this.cuePointMik1.TabIndex = 8;
            this.cuePointMik1.Time = null;
            // 
            // cuePointMik8
            // 
            this.cuePointMik8.Caption = "8";
            this.cuePointMik8.Location = new System.Drawing.Point(1, 210);
            this.cuePointMik8.Margin = new System.Windows.Forms.Padding(0);
            this.cuePointMik8.Name = "cuePointMik8";
            this.cuePointMik8.Size = new System.Drawing.Size(222, 27);
            this.cuePointMik8.TabIndex = 15;
            this.cuePointMik8.Time = null;
            // 
            // cuePointMik2
            // 
            this.cuePointMik2.Caption = "2";
            this.cuePointMik2.Location = new System.Drawing.Point(1, 30);
            this.cuePointMik2.Margin = new System.Windows.Forms.Padding(0);
            this.cuePointMik2.Name = "cuePointMik2";
            this.cuePointMik2.Size = new System.Drawing.Size(222, 27);
            this.cuePointMik2.TabIndex = 9;
            this.cuePointMik2.Time = null;
            // 
            // cuePointMik7
            // 
            this.cuePointMik7.Caption = "7";
            this.cuePointMik7.Location = new System.Drawing.Point(1, 180);
            this.cuePointMik7.Margin = new System.Windows.Forms.Padding(0);
            this.cuePointMik7.Name = "cuePointMik7";
            this.cuePointMik7.Size = new System.Drawing.Size(222, 27);
            this.cuePointMik7.TabIndex = 14;
            this.cuePointMik7.Time = null;
            // 
            // cuePointMik3
            // 
            this.cuePointMik3.Caption = "3";
            this.cuePointMik3.Location = new System.Drawing.Point(1, 60);
            this.cuePointMik3.Margin = new System.Windows.Forms.Padding(0);
            this.cuePointMik3.Name = "cuePointMik3";
            this.cuePointMik3.Size = new System.Drawing.Size(222, 27);
            this.cuePointMik3.TabIndex = 10;
            this.cuePointMik3.Time = null;
            // 
            // cuePointMik6
            // 
            this.cuePointMik6.Caption = "6";
            this.cuePointMik6.Location = new System.Drawing.Point(1, 150);
            this.cuePointMik6.Margin = new System.Windows.Forms.Padding(0);
            this.cuePointMik6.Name = "cuePointMik6";
            this.cuePointMik6.Size = new System.Drawing.Size(222, 27);
            this.cuePointMik6.TabIndex = 13;
            this.cuePointMik6.Time = null;
            // 
            // cuePointMik4
            // 
            this.cuePointMik4.Caption = "4";
            this.cuePointMik4.Location = new System.Drawing.Point(1, 90);
            this.cuePointMik4.Margin = new System.Windows.Forms.Padding(0);
            this.cuePointMik4.Name = "cuePointMik4";
            this.cuePointMik4.Size = new System.Drawing.Size(222, 27);
            this.cuePointMik4.TabIndex = 11;
            this.cuePointMik4.Time = null;
            // 
            // cuePointMik5
            // 
            this.cuePointMik5.Caption = "5";
            this.cuePointMik5.Location = new System.Drawing.Point(1, 120);
            this.cuePointMik5.Margin = new System.Windows.Forms.Padding(0);
            this.cuePointMik5.Name = "cuePointMik5";
            this.cuePointMik5.Size = new System.Drawing.Size(222, 27);
            this.cuePointMik5.TabIndex = 12;
            this.cuePointMik5.Time = null;
            // 
            // btnSaveLibrary
            // 
            this.btnSaveLibrary.Location = new System.Drawing.Point(940, 41);
            this.btnSaveLibrary.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSaveLibrary.Name = "btnSaveLibrary";
            this.btnSaveLibrary.Size = new System.Drawing.Size(151, 33);
            this.btnSaveLibrary.TabIndex = 25;
            this.btnSaveLibrary.Text = "Save Library";
            this.btnSaveLibrary.UseVisualStyleBackColor = true;
            this.btnSaveLibrary.Click += new System.EventHandler(this.btnSaveLibrary_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1458, 1055);
            this.Controls.Add(this.btnSaveLibrary);
            this.Controls.Add(this.cbAutoMik);
            this.Controls.Add(this.btnCopyMissing);
            this.Controls.Add(this.btnCopyAll);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnReadMIK);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panelLibraryCuePoints);
            this.Controls.Add(this.panelMikCuePoints);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Font = new System.Drawing.Font("Arial Narrow", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelMikCuePoints.ResumeLayout(false);
            this.panelLibraryCuePoints.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private CuePointControl cuepointA;
        private CuePointControl cuePointH;
        private CuePointControl cuePointG;
        private CuePointControl cuePointF;
        private CuePointControl cuePointE;
        private CuePointControl cuePointD;
        private CuePointControl cuePointC;
        private CuePointControl cuePointB;
        private CuePointControl cuePointMik8;
        private CuePointControl cuePointMik7;
        private CuePointControl cuePointMik6;
        private CuePointControl cuePointMik5;
        private CuePointControl cuePointMik4;
        private CuePointControl cuePointMik3;
        private CuePointControl cuePointMik2;
        private CuePointControl cuePointMik1;
        private System.Windows.Forms.Panel panelMikCuePoints;
        private System.Windows.Forms.Panel panelLibraryCuePoints;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnReadMIK;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnCopyAll;
        private System.Windows.Forms.Button btnCopyMissing;
        private System.Windows.Forms.CheckBox cbAutoMik;
        private System.Windows.Forms.Button btnSaveLibrary;
    }
}

