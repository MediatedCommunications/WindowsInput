namespace Demo
{
    partial class Main
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
            this.checkBoxSuppressMouse = new System.Windows.Forms.CheckBox();
            this.panelSeparator = new System.Windows.Forms.Panel();
            this.labelWheel = new System.Windows.Forms.Label();
            this.labelMousePosition = new System.Windows.Forms.Label();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rbMouseNone = new System.Windows.Forms.RadioButton();
            this.rbMouseGlobal = new System.Windows.Forms.RadioButton();
            this.rbMouseApp = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbKeyNone = new System.Windows.Forms.RadioButton();
            this.rbKeyGlobal = new System.Windows.Forms.RadioButton();
            this.rbKeyApp = new System.Windows.Forms.RadioButton();
            this.clearLogButton = new System.Windows.Forms.Button();
            this.checkBoxSupressMouseWheel = new System.Windows.Forms.CheckBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // checkBoxSuppressMouse
            // 
            this.checkBoxSuppressMouse.AutoSize = true;
            this.checkBoxSuppressMouse.Location = new System.Drawing.Point(211, 128);
            this.checkBoxSuppressMouse.Name = "checkBoxSuppressMouse";
            this.checkBoxSuppressMouse.Size = new System.Drawing.Size(159, 17);
            this.checkBoxSuppressMouse.TabIndex = 13;
            this.checkBoxSuppressMouse.Text = "Suppress Right Mouse Click";
            this.checkBoxSuppressMouse.UseVisualStyleBackColor = true;
            // 
            // panelSeparator
            // 
            this.panelSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSeparator.BackColor = System.Drawing.Color.White;
            this.panelSeparator.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSeparator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelSeparator.Location = new System.Drawing.Point(6, 116);
            this.panelSeparator.Name = "panelSeparator";
            this.panelSeparator.Size = new System.Drawing.Size(584, 1);
            this.panelSeparator.TabIndex = 11;
            // 
            // labelWheel
            // 
            this.labelWheel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelWheel.AutoSize = true;
            this.labelWheel.BackColor = System.Drawing.Color.White;
            this.labelWheel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWheel.Location = new System.Drawing.Point(9, 155);
            this.labelWheel.Name = "labelWheel";
            this.labelWheel.Size = new System.Drawing.Size(89, 13);
            this.labelWheel.TabIndex = 6;
            this.labelWheel.Text = "Wheel={0:####}";
            // 
            // labelMousePosition
            // 
            this.labelMousePosition.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelMousePosition.AutoSize = true;
            this.labelMousePosition.BackColor = System.Drawing.Color.White;
            this.labelMousePosition.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelMousePosition.Location = new System.Drawing.Point(9, 129);
            this.labelMousePosition.Name = "labelMousePosition";
            this.labelMousePosition.Size = new System.Drawing.Size(125, 13);
            this.labelMousePosition.TabIndex = 2;
            this.labelMousePosition.Text = "x={0:####}; y={1:####}";
            // 
            // textBoxLog
            // 
            this.textBoxLog.AcceptsReturn = true;
            this.textBoxLog.BackColor = System.Drawing.Color.White;
            this.textBoxLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxLog.Location = new System.Drawing.Point(0, 204);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ReadOnly = true;
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(602, 230);
            this.textBoxLog.TabIndex = 8;
            this.textBoxLog.WordWrap = false;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBox1);
            this.groupBox2.Controls.Add(this.clearLogButton);
            this.groupBox2.Controls.Add(this.checkBoxSupressMouseWheel);
            this.groupBox2.Controls.Add(this.checkBoxSuppressMouse);
            this.groupBox2.Controls.Add(this.panelSeparator);
            this.groupBox2.Controls.Add(this.labelWheel);
            this.groupBox2.Controls.Add(this.labelMousePosition);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(602, 204);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rbMouseNone);
            this.groupBox3.Controls.Add(this.rbMouseGlobal);
            this.groupBox3.Controls.Add(this.rbMouseApp);
            this.groupBox3.Location = new System.Drawing.Point(304, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(276, 49);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mouse";
            // 
            // rbMouseNone
            // 
            this.rbMouseNone.AutoSize = true;
            this.rbMouseNone.BackColor = System.Drawing.Color.White;
            this.rbMouseNone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMouseNone.Location = new System.Drawing.Point(214, 19);
            this.rbMouseNone.Name = "rbMouseNone";
            this.rbMouseNone.Size = new System.Drawing.Size(51, 17);
            this.rbMouseNone.TabIndex = 24;
            this.rbMouseNone.Text = "None";
            this.rbMouseNone.UseVisualStyleBackColor = false;
            this.rbMouseNone.CheckedChanged += new System.EventHandler(this.Mouse_Changed);
            // 
            // rbMouseGlobal
            // 
            this.rbMouseGlobal.AutoSize = true;
            this.rbMouseGlobal.BackColor = System.Drawing.Color.White;
            this.rbMouseGlobal.Checked = true;
            this.rbMouseGlobal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMouseGlobal.Location = new System.Drawing.Point(121, 19);
            this.rbMouseGlobal.Name = "rbMouseGlobal";
            this.rbMouseGlobal.Size = new System.Drawing.Size(87, 17);
            this.rbMouseGlobal.TabIndex = 23;
            this.rbMouseGlobal.TabStop = true;
            this.rbMouseGlobal.Text = "Global hooks";
            this.rbMouseGlobal.UseVisualStyleBackColor = false;
            this.rbMouseGlobal.CheckedChanged += new System.EventHandler(this.Mouse_Changed);
            // 
            // rbMouseApp
            // 
            this.rbMouseApp.AutoSize = true;
            this.rbMouseApp.BackColor = System.Drawing.Color.White;
            this.rbMouseApp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMouseApp.Location = new System.Drawing.Point(6, 19);
            this.rbMouseApp.Name = "rbMouseApp";
            this.rbMouseApp.Size = new System.Drawing.Size(109, 17);
            this.rbMouseApp.TabIndex = 22;
            this.rbMouseApp.Text = "Application hooks";
            this.rbMouseApp.UseVisualStyleBackColor = false;
            this.rbMouseApp.CheckedChanged += new System.EventHandler(this.Mouse_Changed);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbKeyNone);
            this.groupBox1.Controls.Add(this.rbKeyGlobal);
            this.groupBox1.Controls.Add(this.rbKeyApp);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(286, 49);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Keyboard";
            // 
            // rbKeyNone
            // 
            this.rbKeyNone.AutoSize = true;
            this.rbKeyNone.BackColor = System.Drawing.Color.White;
            this.rbKeyNone.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbKeyNone.Location = new System.Drawing.Point(217, 19);
            this.rbKeyNone.Name = "rbKeyNone";
            this.rbKeyNone.Size = new System.Drawing.Size(51, 17);
            this.rbKeyNone.TabIndex = 17;
            this.rbKeyNone.Text = "None";
            this.rbKeyNone.UseVisualStyleBackColor = false;
            this.rbKeyNone.CheckedChanged += new System.EventHandler(this.Keyboard_Changed);
            // 
            // rbKeyGlobal
            // 
            this.rbKeyGlobal.AutoSize = true;
            this.rbKeyGlobal.BackColor = System.Drawing.Color.White;
            this.rbKeyGlobal.Checked = true;
            this.rbKeyGlobal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbKeyGlobal.Location = new System.Drawing.Point(124, 19);
            this.rbKeyGlobal.Name = "rbKeyGlobal";
            this.rbKeyGlobal.Size = new System.Drawing.Size(87, 17);
            this.rbKeyGlobal.TabIndex = 16;
            this.rbKeyGlobal.TabStop = true;
            this.rbKeyGlobal.Text = "Global hooks";
            this.rbKeyGlobal.UseVisualStyleBackColor = false;
            this.rbKeyGlobal.CheckedChanged += new System.EventHandler(this.Keyboard_Changed);
            // 
            // rbKeyApp
            // 
            this.rbKeyApp.AutoSize = true;
            this.rbKeyApp.BackColor = System.Drawing.Color.White;
            this.rbKeyApp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbKeyApp.Location = new System.Drawing.Point(9, 19);
            this.rbKeyApp.Name = "rbKeyApp";
            this.rbKeyApp.Size = new System.Drawing.Size(109, 17);
            this.rbKeyApp.TabIndex = 15;
            this.rbKeyApp.Text = "Application hooks";
            this.rbKeyApp.UseVisualStyleBackColor = false;
            this.rbKeyApp.CheckedChanged += new System.EventHandler(this.Keyboard_Changed);
            // 
            // clearLogButton
            // 
            this.clearLogButton.Location = new System.Drawing.Point(515, 151);
            this.clearLogButton.Name = "clearLogButton";
            this.clearLogButton.Size = new System.Drawing.Size(75, 23);
            this.clearLogButton.TabIndex = 16;
            this.clearLogButton.Text = "Clear Log";
            this.clearLogButton.UseVisualStyleBackColor = true;
            this.clearLogButton.Click += new System.EventHandler(this.clearLog_Click);
            // 
            // checkBoxSupressMouseWheel
            // 
            this.checkBoxSupressMouseWheel.AutoSize = true;
            this.checkBoxSupressMouseWheel.Location = new System.Drawing.Point(211, 151);
            this.checkBoxSupressMouseWheel.Name = "checkBoxSupressMouseWheel";
            this.checkBoxSupressMouseWheel.Size = new System.Drawing.Size(139, 17);
            this.checkBoxSupressMouseWheel.TabIndex = 15;
            this.checkBoxSupressMouseWheel.Text = "Suppress Mouse Wheel";
            this.checkBoxSupressMouseWheel.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(602, 434);
            this.Controls.Add(this.textBoxLog);
            this.Controls.Add(this.groupBox2);
            this.Name = "Main";
            this.Text = "Mouse and Keyboard Hooks Demo";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxSuppressMouse;
        private System.Windows.Forms.Panel panelSeparator;
        private System.Windows.Forms.Label labelWheel;
        private System.Windows.Forms.Label labelMousePosition;
        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox checkBoxSupressMouseWheel;
        private System.Windows.Forms.Button clearLogButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rbMouseNone;
        private System.Windows.Forms.RadioButton rbMouseGlobal;
        private System.Windows.Forms.RadioButton rbMouseApp;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbKeyNone;
        private System.Windows.Forms.RadioButton rbKeyGlobal;
        private System.Windows.Forms.RadioButton rbKeyApp;
    }
}

