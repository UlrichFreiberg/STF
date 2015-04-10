// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Form1.cs" company="Foobar">
//   2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace WinFormsApp
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;

    using Stf.Utilities;

    /// <summary>
    /// The form 1.
    /// </summary>
    public partial class Form1 : Form
    {
        /// <summary>
        /// The logfile name.
        /// </summary>
        private const string LogfileName = @"c:\temp\OvidDemo.html";

        /// <summary>
        /// Initializes a new instance of the <see cref="Form1"/> class.
        /// </summary>
        public Form1()
        {
            this.InitializeComponent();

            this.Mylogger = new StfLogger(LogfileName);
            this.MyAssert = new StfAssert(this.Mylogger) { EnableNegativeTesting = true };
            this.Mylogger.LogLevel = LogLevel.Trace;
 
            this.Mylogger.LogInfo("logFile opened from DemoApp constructor");
            this.TxtMessage.Text = @"Some test message";
        }

        /// <summary>
        /// Gets and sets The i.
        /// </summary>
        public int SomeCounter { get; private set; }

        /// <summary>
        /// Gets The mylogger.
        /// </summary>
        public StfLogger Mylogger { get; private set; }

        /// <summary>
        /// Gets The my assert.
        /// </summary>
        public StfAssert MyAssert { get; private set; }

        /// <summary>
        /// The button 1_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnCallStackDemo_Click(object sender, EventArgs e)
        {
            this.Mylogger.LogFunctionEnter(LogLevel.Info, "Void", "BtnCallStackDemo_Click");
            this.Mylogger.LogInfo("Someone pressed the CallStackDemo button");
            callStackDemo("Demo");
            this.Mylogger.LogFunctionExit(LogLevel.Info, "BtnCallStackDemo_Click");
        }

        /// <summary>
        /// The button 2_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnShowLogFile_Click(object sender, EventArgs e)
        {
            Process.Start(LogfileName);
        }

        /// <summary>
        /// The btn exit_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// The text box 1_ text changed.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// The btn log info_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void btnLogInfo_Click(object sender, EventArgs e)
        {
            this.Mylogger.LogInfo("Info: " + this.TxtMessage.Text);
        }

        /// <summary>
        /// The btn log trace_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void btnLogTrace_Click(object sender, EventArgs e)
        {
            this.Mylogger.LogTrace("Info: " + this.TxtMessage.Text);
        }

        /// <summary>
        /// The btn log pass_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void btnLogPass_Click(object sender, EventArgs e)
        {
            this.Mylogger.LogPass("OvidDemo", this.TxtMessage.Text);
        }

        /// <summary>
        /// The btn log fail_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void btnLogFail_Click(object sender, EventArgs e)
        {
            this.Mylogger.LogFail("OvidDemo", this.TxtMessage.Text);
        }

        /// <summary>
        /// The call stack demo.
        /// </summary>
        /// <param name="argument">
        /// The argument.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int callStackDemo(string argument)
        {
            var retVal = 40;

            this.Mylogger.LogFunctionEnter(LogLevel.Debug, "Void", "callStackDemo");
            this.Mylogger.LogInfo(string.Format("SomeCounter is [{0}]", this.SomeCounter++));
            this.callStackDemoL1("From Demo");
            this.callStackDemoL1("From Demo");

            this.Mylogger.LogFunctionExit(LogLevel.Debug, "callStackDemo");                        
            return retVal;
        }

        /// <summary>
        /// The call stack demo l 1.
        /// </summary>
        /// <param name="argument">
        /// The argument.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int callStackDemoL1(string argument)
        {
            var retVal = 41;

            this.Mylogger.LogFunctionEnter(LogLevel.Debug, "Void", "callStackDemoL1");
            this.Mylogger.LogInfo(string.Format("SomeCounter is [{0}]", this.SomeCounter++));
            this.callStackDemoL2("From Demo1");
            this.callStackDemoL2("From Demo1");
            this.Mylogger.LogFunctionExit(LogLevel.Debug, "callStackDemoL1");
            return retVal;
        }

        /// <summary>
        /// The call stack demo l 2.
        /// </summary>
        /// <param name="argument">
        /// The argument.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        private int callStackDemoL2(string argument)
        {
            var retVal = 42;

            this.Mylogger.LogFunctionEnter(LogLevel.Debug, "Void", "callStackDemoL2");
            this.Mylogger.LogInfo(string.Format("SomeCounter is [{0}]", this.SomeCounter++));
            this.Mylogger.LogFunctionExit(LogLevel.Debug, "callStackDemoL2");
            return retVal;
        }

        /// <summary>
        /// The btn assert string equals_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnAssertStringEquals_Click(object sender, EventArgs e)
        {
            this.MyAssert.StringEquals("DemoApp", this.TxtStringAssertArg1.Text, this.TxtStringAssertArg2.Text);
        }

        /// <summary>
        /// The btn assert string matches_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnAssertStringMatches_Click(object sender, EventArgs e)
        {
            this.MyAssert.StringMatches("DemoApp", this.TxtStringAssertArg1.Text, this.TxtStringAssertArg2.Text);
        }

        /// <summary>
        /// The btn assert string contains_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnAssertStringContains_Click(object sender, EventArgs e)
        {
            this.MyAssert.StringContains("DemoApp", this.TxtStringAssertArg1.Text, this.TxtStringAssertArg2.Text);
        }

        /// <summary>
        /// The btn assert equals_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnAssertEquals_Click(object sender, EventArgs e)
        {
            this.MyAssert.AssertEquals("DemoApp", this.TxtAssertArg1.Text, this.TxtAssertArg2.Text);
        }

        /// <summary>
        /// The btn assert has value_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnAssertHasValue_Click(object sender, EventArgs e)
        {
            this.MyAssert.AssertHasValue("DemoApp", this.TxtAssertArg1.Text);
        }

        /// <summary>
        /// The btn assert null_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void BtnAssertNull_Click(object sender, EventArgs e)
        {
            this.MyAssert.AssertNull("DemoApp", this.TxtAssertArg1.Text);
        }

        /// <summary>
        /// The form 1_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
