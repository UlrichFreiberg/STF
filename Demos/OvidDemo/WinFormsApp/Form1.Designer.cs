// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Form1.Designer.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WinFormsApp
{
    using System.ComponentModel;
    using System.Windows.Forms;

    /// <summary>
    /// The form 1.
    /// </summary>
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">
        /// true if managed resources should be disposed; otherwise, false.
        /// </param>
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
            this.BtnShowLogFile = new System.Windows.Forms.Button();
            this.BtnExit = new System.Windows.Forms.Button();
            this.BtnAssertStringEquals = new System.Windows.Forms.Button();
            this.BtnAssertStringMatches = new System.Windows.Forms.Button();
            this.BtnAssertStringContains = new System.Windows.Forms.Button();
            this.TxtStringAssertArg1 = new System.Windows.Forms.TextBox();
            this.TxtStringAssertArg2 = new System.Windows.Forms.TextBox();
            this.btnLogFail = new System.Windows.Forms.Button();
            this.btnLogPass = new System.Windows.Forms.Button();
            this.btnLogInfo = new System.Windows.Forms.Button();
            this.BtnCallStackDemo = new System.Windows.Forms.Button();
            this.btnLogTrace = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TxtMessage = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TxtAssertArg1 = new System.Windows.Forms.TextBox();
            this.BtnAssertEquals = new System.Windows.Forms.Button();
            this.TxtAssertArg2 = new System.Windows.Forms.TextBox();
            this.BtnAssertHasValue = new System.Windows.Forms.Button();
            this.BtnAssertNull = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();

            // BtnShowLogFile
            this.BtnShowLogFile.Location = new System.Drawing.Point(387, 301);
            this.BtnShowLogFile.Name = "BtnShowLogFile";
            this.BtnShowLogFile.Size = new System.Drawing.Size(75, 35);
            this.BtnShowLogFile.TabIndex = 2;
            this.BtnShowLogFile.Text = "Show LogFile";
            this.BtnShowLogFile.UseVisualStyleBackColor = true;
            this.BtnShowLogFile.Click += new System.EventHandler(this.BtnShowLogFile_Click);

            // BtnExit
            this.BtnExit.Location = new System.Drawing.Point(481, 301);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(75, 35);
            this.BtnExit.TabIndex = 3;
            this.BtnExit.Text = "Exit";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);

            // BtnAssertStringEquals
            this.BtnAssertStringEquals.Location = new System.Drawing.Point(6, 75);
            this.BtnAssertStringEquals.Name = "BtnAssertStringEquals";
            this.BtnAssertStringEquals.Size = new System.Drawing.Size(156, 23);
            this.BtnAssertStringEquals.TabIndex = 8;
            this.BtnAssertStringEquals.Text = "AssertStringEquals";
            this.BtnAssertStringEquals.UseVisualStyleBackColor = true;
            this.BtnAssertStringEquals.Click += new System.EventHandler(this.BtnAssertStringEquals_Click);

            // BtnAssertStringMatches
            this.BtnAssertStringMatches.Location = new System.Drawing.Point(7, 104);
            this.BtnAssertStringMatches.Name = "BtnAssertStringMatches";
            this.BtnAssertStringMatches.Size = new System.Drawing.Size(156, 23);
            this.BtnAssertStringMatches.TabIndex = 9;
            this.BtnAssertStringMatches.Text = "AssertStringMatches";
            this.BtnAssertStringMatches.UseVisualStyleBackColor = true;
            this.BtnAssertStringMatches.Click += new System.EventHandler(this.BtnAssertStringMatches_Click);

            // BtnAssertStringContains
            this.BtnAssertStringContains.Location = new System.Drawing.Point(7, 133);
            this.BtnAssertStringContains.Name = "BtnAssertStringContains";
            this.BtnAssertStringContains.Size = new System.Drawing.Size(156, 23);
            this.BtnAssertStringContains.TabIndex = 10;
            this.BtnAssertStringContains.Text = "AssertStringContains";
            this.BtnAssertStringContains.UseVisualStyleBackColor = true;
            this.BtnAssertStringContains.Click += new System.EventHandler(this.BtnAssertStringContains_Click);

            // TxtStringAssertArg1
            this.TxtStringAssertArg1.Location = new System.Drawing.Point(7, 19);
            this.TxtStringAssertArg1.Multiline = true;
            this.TxtStringAssertArg1.Name = "TxtStringAssertArg1";
            this.TxtStringAssertArg1.Size = new System.Drawing.Size(75, 21);
            this.TxtStringAssertArg1.TabIndex = 11;

            // TxtStringAssertArg2
            this.TxtStringAssertArg2.Location = new System.Drawing.Point(88, 19);
            this.TxtStringAssertArg2.Multiline = true;
            this.TxtStringAssertArg2.Name = "TxtStringAssertArg2";
            this.TxtStringAssertArg2.Size = new System.Drawing.Size(75, 21);
            this.TxtStringAssertArg2.TabIndex = 12;

            // btnLogFail
            this.btnLogFail.Location = new System.Drawing.Point(34, 162);
            this.btnLogFail.Name = "btnLogFail";
            this.btnLogFail.Size = new System.Drawing.Size(89, 23);
            this.btnLogFail.TabIndex = 7;
            this.btnLogFail.Text = "LogFail";
            this.btnLogFail.UseVisualStyleBackColor = true;
            this.btnLogFail.Click += new System.EventHandler(this.btnLogFail_Click);

            // btnLogPass
            this.btnLogPass.Location = new System.Drawing.Point(34, 133);
            this.btnLogPass.Name = "btnLogPass";
            this.btnLogPass.Size = new System.Drawing.Size(89, 23);
            this.btnLogPass.TabIndex = 6;
            this.btnLogPass.Text = "LogPass";
            this.btnLogPass.UseVisualStyleBackColor = true;
            this.btnLogPass.Click += new System.EventHandler(this.btnLogPass_Click);

            // btnLogInfo
            this.btnLogInfo.Location = new System.Drawing.Point(34, 75);
            this.btnLogInfo.Name = "btnLogInfo";
            this.btnLogInfo.Size = new System.Drawing.Size(91, 23);
            this.btnLogInfo.TabIndex = 4;
            this.btnLogInfo.Text = "LogInfo";
            this.btnLogInfo.UseVisualStyleBackColor = true;
            this.btnLogInfo.Click += new System.EventHandler(this.btnLogInfo_Click);

            // BtnCallStackDemo
            this.BtnCallStackDemo.Location = new System.Drawing.Point(34, 200);
            this.BtnCallStackDemo.Name = "BtnCallStackDemo";
            this.BtnCallStackDemo.Size = new System.Drawing.Size(89, 23);
            this.BtnCallStackDemo.TabIndex = 0;
            this.BtnCallStackDemo.Text = "CallStackDemo";
            this.BtnCallStackDemo.UseVisualStyleBackColor = true;
            this.BtnCallStackDemo.Click += new System.EventHandler(this.BtnCallStackDemo_Click);

            // btnLogTrace
            this.btnLogTrace.Location = new System.Drawing.Point(34, 104);
            this.btnLogTrace.Name = "btnLogTrace";
            this.btnLogTrace.Size = new System.Drawing.Size(89, 23);
            this.btnLogTrace.TabIndex = 5;
            this.btnLogTrace.Text = "LogTrace";
            this.btnLogTrace.UseVisualStyleBackColor = true;
            this.btnLogTrace.Click += new System.EventHandler(this.btnLogTrace_Click);

            // groupBox1
            this.groupBox1.Controls.Add(this.TxtMessage);
            this.groupBox1.Controls.Add(this.btnLogInfo);
            this.groupBox1.Controls.Add(this.btnLogTrace);
            this.groupBox1.Controls.Add(this.btnLogFail);
            this.groupBox1.Controls.Add(this.BtnCallStackDemo);
            this.groupBox1.Controls.Add(this.btnLogPass);
            this.groupBox1.Location = new System.Drawing.Point(12, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 256);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "LogFunctions";

            // TxtMessage
            this.TxtMessage.Location = new System.Drawing.Point(34, 20);
            this.TxtMessage.Name = "TxtMessage";
            this.TxtMessage.Size = new System.Drawing.Size(91, 20);
            this.TxtMessage.TabIndex = 8;

            // groupBox2
            this.groupBox2.Controls.Add(this.TxtStringAssertArg1);
            this.groupBox2.Controls.Add(this.BtnAssertStringEquals);
            this.groupBox2.Controls.Add(this.TxtStringAssertArg2);
            this.groupBox2.Controls.Add(this.BtnAssertStringMatches);
            this.groupBox2.Controls.Add(this.BtnAssertStringContains);
            this.groupBox2.Location = new System.Drawing.Point(188, 18);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(169, 259);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "StringAsserts";

            // groupBox3
            this.groupBox3.Controls.Add(this.TxtAssertArg1);
            this.groupBox3.Controls.Add(this.BtnAssertEquals);
            this.groupBox3.Controls.Add(this.TxtAssertArg2);
            this.groupBox3.Controls.Add(this.BtnAssertHasValue);
            this.groupBox3.Controls.Add(this.BtnAssertNull);
            this.groupBox3.Location = new System.Drawing.Point(387, 18);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(169, 259);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Asserts";

            // TxtAssertArg1
            this.TxtAssertArg1.Location = new System.Drawing.Point(7, 19);
            this.TxtAssertArg1.Multiline = true;
            this.TxtAssertArg1.Name = "TxtAssertArg1";
            this.TxtAssertArg1.Size = new System.Drawing.Size(75, 21);
            this.TxtAssertArg1.TabIndex = 11;

            // BtnAssertEquals
            this.BtnAssertEquals.Location = new System.Drawing.Point(6, 75);
            this.BtnAssertEquals.Name = "BtnAssertEquals";
            this.BtnAssertEquals.Size = new System.Drawing.Size(156, 23);
            this.BtnAssertEquals.TabIndex = 8;
            this.BtnAssertEquals.Text = "AssertEquals";
            this.BtnAssertEquals.UseVisualStyleBackColor = true;
            this.BtnAssertEquals.Click += new System.EventHandler(this.BtnAssertEquals_Click);

            // TxtAssertArg2
            this.TxtAssertArg2.Location = new System.Drawing.Point(88, 19);
            this.TxtAssertArg2.Multiline = true;
            this.TxtAssertArg2.Name = "TxtAssertArg2";
            this.TxtAssertArg2.Size = new System.Drawing.Size(75, 21);
            this.TxtAssertArg2.TabIndex = 12;

            // BtnAssertHasValue
            this.BtnAssertHasValue.Location = new System.Drawing.Point(7, 104);
            this.BtnAssertHasValue.Name = "BtnAssertHasValue";
            this.BtnAssertHasValue.Size = new System.Drawing.Size(156, 23);
            this.BtnAssertHasValue.TabIndex = 9;
            this.BtnAssertHasValue.Text = "AssertHasValue";
            this.BtnAssertHasValue.UseVisualStyleBackColor = true;
            this.BtnAssertHasValue.Click += new System.EventHandler(this.BtnAssertHasValue_Click);

            // BtnAssertNull
            this.BtnAssertNull.Location = new System.Drawing.Point(7, 133);
            this.BtnAssertNull.Name = "BtnAssertNull";
            this.BtnAssertNull.Size = new System.Drawing.Size(156, 23);
            this.BtnAssertNull.TabIndex = 10;
            this.BtnAssertNull.Text = "AssertNull";
            this.BtnAssertNull.UseVisualStyleBackColor = true;
            this.BtnAssertNull.Click += new System.EventHandler(this.BtnAssertNull_Click);

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(571, 348);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.BtnExit);
            this.Controls.Add(this.BtnShowLogFile);
            this.Name = "Form1";
            this.Text = "Ovid Demo App";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        /// <summary>
        /// The btn show log file.
        /// </summary>
        private Button BtnShowLogFile;

        /// <summary>
        /// The btn exit.
        /// </summary>
        private Button BtnExit;

        /// <summary>
        /// The btn assert string equals.
        /// </summary>
        private Button BtnAssertStringEquals;

        /// <summary>
        /// The btn assert string matches.
        /// </summary>
        private Button BtnAssertStringMatches;

        /// <summary>
        /// The btn assert string contains.
        /// </summary>
        private Button BtnAssertStringContains;

        /// <summary>
        /// The txt string assert arg 1.
        /// </summary>
        private TextBox TxtStringAssertArg1;

        /// <summary>
        /// The txt string assert arg 2.
        /// </summary>
        private TextBox TxtStringAssertArg2;

        /// <summary>
        /// The btn log fail.
        /// </summary>
        private Button btnLogFail;

        /// <summary>
        /// The btn log pass.
        /// </summary>
        private Button btnLogPass;

        /// <summary>
        /// The btn log info.
        /// </summary>
        private Button btnLogInfo;

        /// <summary>
        /// The btn call stack demo.
        /// </summary>
        private Button BtnCallStackDemo;

        /// <summary>
        /// The btn log trace.
        /// </summary>
        private Button btnLogTrace;

        /// <summary>
        /// The group box 1.
        /// </summary>
        private GroupBox groupBox1;

        /// <summary>
        /// The txt message.
        /// </summary>
        private TextBox TxtMessage;

        /// <summary>
        /// The group box 2.
        /// </summary>
        private GroupBox groupBox2;

        /// <summary>
        /// The group box 3.
        /// </summary>
        private GroupBox groupBox3;

        /// <summary>
        /// The txt assert arg 1.
        /// </summary>
        private TextBox TxtAssertArg1;

        /// <summary>
        /// The btn assert equals.
        /// </summary>
        private Button BtnAssertEquals;

        /// <summary>
        /// The txt assert arg 2.
        /// </summary>
        private TextBox TxtAssertArg2;

        /// <summary>
        /// The btn assert has value.
        /// </summary>
        private Button BtnAssertHasValue;

        /// <summary>
        /// The btn assert null.
        /// </summary>
        private Button BtnAssertNull;
    }
}

