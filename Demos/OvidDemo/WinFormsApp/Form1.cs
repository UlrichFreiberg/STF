// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Form1.cs" company="Mir Software">
//   Copyright governed by Artistic license as described here:
//          http://www.perlfoundation.org/artistic_license_2_0
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Mir.Stf.Utilities;

namespace WinFormsApp
{
    using System;
    using System.Diagnostics;
    using System.Windows.Forms;

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
            InitializeComponent();

            Mylogger = new StfLogger(LogfileName);
            MyAssert = new StfAssert(Mylogger) { EnableNegativeTesting = true };
            Mylogger.LogLevel = StfLogLevel.Trace;
 
            Mylogger.LogInfo("logFile opened from DemoApp constructor");
            TxtMessage.Text = @"Some test message";
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
            Mylogger.LogFunctionEnter(StfLogLevel.Info, "Void", "BtnCallStackDemo_Click");
            Mylogger.LogInfo("Someone pressed the CallStackDemo button");
            callStackDemo("Demo");
            Mylogger.LogFunctionExit(StfLogLevel.Info, "BtnCallStackDemo_Click");
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
            Mylogger.LogInfo("Info: " + TxtMessage.Text);
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
            Mylogger.LogTrace("Info: " + TxtMessage.Text);
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
            Mylogger.LogPass("OvidDemo", TxtMessage.Text);
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
            Mylogger.LogFail("OvidDemo", TxtMessage.Text);
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

            Mylogger.LogFunctionEnter(StfLogLevel.Debug, "Void", "callStackDemo");
            Mylogger.LogInfo(string.Format("SomeCounter is [{0}]", SomeCounter++));
            callStackDemoL1("From Demo");
            callStackDemoL1("From Demo");

            Mylogger.LogFunctionExit(StfLogLevel.Debug, "callStackDemo");                        
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

            Mylogger.LogFunctionEnter(StfLogLevel.Debug, "Void", "callStackDemoL1");
            Mylogger.LogInfo(string.Format("SomeCounter is [{0}]", SomeCounter++));
            callStackDemoL2("From Demo1");
            callStackDemoL2("From Demo1");
            Mylogger.LogFunctionExit(StfLogLevel.Debug, "callStackDemoL1");
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

            Mylogger.LogFunctionEnter(StfLogLevel.Debug, "Void", "callStackDemoL2");
            Mylogger.LogInfo(string.Format("SomeCounter is [{0}]", SomeCounter++));
            Mylogger.LogFunctionExit(StfLogLevel.Debug, "callStackDemoL2");
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
            MyAssert.StringEquals("DemoApp", TxtStringAssertArg1.Text, TxtStringAssertArg2.Text);
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
            MyAssert.StringMatches("DemoApp", TxtStringAssertArg1.Text, TxtStringAssertArg2.Text);
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
            MyAssert.StringContains("DemoApp", TxtStringAssertArg1.Text, TxtStringAssertArg2.Text);
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
            MyAssert.AreEqual("DemoApp", TxtAssertArg1.Text, TxtAssertArg2.Text);
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
            MyAssert.HasValue("DemoApp", TxtAssertArg1.Text);
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
            MyAssert.IsNull("DemoApp", TxtAssertArg1.Text);
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
