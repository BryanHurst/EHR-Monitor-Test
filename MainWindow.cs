// Default WinForms Imports
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace EHR_Monitor_Test
{
    // TODO: Log the name of the window that the user has in focus when a test runs.
    // TODO: If the focused window is the same as this window, don't allow the test to run.
    // TODO: Attempt to log any errors coming back from the user32.dll when reading or writing text to another process.

    public partial class MainWindow : Form
    {
        private const int ticks = 5;  // How many seconds do we want to allow a user to find the EHR Window before attempting to poll it
        private int currentTick = 0;

        private const int numberOfTests = 4;
        private bool[] results = new bool[numberOfTests  + 1];

        private int currentTestNumber = 0;  // What test are we currently on
        private delegate void TestDelegate();
        private TestDelegate currentTest = null;
        private bool warned = false;  // Have we warned the user to move their cursor focus to the EHR window already for this test?

        public MainWindow()
        {
            this.InitializeComponent();
            this.timer1.Stop();

            // Don't allow for maximizing the window as the layout is static...
            this.MaximizeBox = false;

            // Set any atributes on our own items that we may need
            this.ourText.ReadOnly = true;
            this.theirText.ReadOnly = true;

            // Just for easier reading of results...
            results[0] = true;

            // Go ahead and setup the first test in the view.
            this.SetupNextTest();

            MessageBox.Show("On all tests, after pushing the 'Run Test' button, you will have 5 seconds to bring your EHR Application to the foreground and click into the text box (make sure it has focus). \n\nFor these tests to work, the cursor must be in the main text box like you were actively working or typing in it.");
        }

        private void SetupTestOne()
        {
            this.testNumber.Text = "Test Number: 1";
            this.testDescription.Text = "First, we are going to test if we can successfully READ text from the text area inside the EHR Application. \r\nTo do this, YOU need to manually insert text into the EHR Application. The text on the left side below has been copied to your clipboard. \r\nPlease open your EHR Application and PASTE the text into the normal working area. \r\n\r\nThe test will attempt to read text from the EHR Application and place it on the right side. \r\nSuccess is when the right side contains the text from the left.";

            this.ourText.Text = "Test One \r\nSimple Text Test \r\n\r\nThis is a test of reading text from the EHR Application.";
            
            Clipboard.SetText(this.ourText.Text, TextDataFormat.Text);
        }

        private void RunTestOne()
        {
            if (!this.warned)
            {
                MessageBox.Show("Please make sure you have pasted the text from the left box into your EHR Application. \nOnce you push 'Ok', you will have 5 seconds to click into your EHR's main Text Box.");
                this.ShowTestCountdown();
            }
            else
            {
                // Read the text from the EHR Application
                string readText = EHRText.GetTextFromActiveWindowElement();
                // Set the right side text to what we read
                this.theirText.Text = readText;

                if (this.theirText.Text.Contains(this.ourText.Text))
                {
                    this.PrintPossibleSuccess();
                }
                else
                {
                    this.PrintPossibleFailure();
                }
            }
        }

        private void SetupTestTwo()
        {
            this.testNumber.Text = "Test Number: 2";
            this.testDescription.Text = "Next, we are going to test if we can successfully WRITE text to the text area inside the EHR Application. \r\nTo do this, WE will need to insert text into the EHR Application. \r\n\r\nThis test will attempt to write the text from the left side below into your EHR's Text Box. It will then read back your EHR's text and paste the read in value to the right side below. \r\nSuccess is when the right side matches the left side, AND the Text Box in the EHR Application contains the same text.";

            this.ourText.Text = "Test Two; Test of writing text. The left and right text boxes should match once the test has run.";
        }

        private void RunTestTwo()
        {
            if (!this.warned)
            {
                MessageBox.Show("Once you push 'Ok', you will have 5 seconds to click into your EHR's main Text Box.");
                this.ShowTestCountdown();
            }
            else
            {
                // Write our text to the EHR
                EHRText.SetTextOfActiveWindowElement(ourText.Text);

                // Read the text back from the EHR Application
                string readText = EHRText.GetTextFromActiveWindowElement();
                // Set the right side text to what we read
                this.theirText.Text = readText;

                if (this.theirText.Text.Equals(this.ourText.Text))
                {
                    this.PrintPossibleSuccess();
                }
                else
                {
                    this.PrintPossibleFailure();
                }
            }
        }

        private void SetupTestThree()
        {
            this.testNumber.Text = "Test Number: 3";
            this.testDescription.Text = "Now we need to test formatted text and see if we can successfully READ RTF from the Text Area inside the EHR Application. \r\nTo do this, YOU will need to manually insert text into the EHR Application. Please highlight and COPY the text on the left below and PASTE it into the normal working area of your EHR. It should have the same color and formatting as the text seen below. \r\n\r\nThe test will attempt to read text from the EHR Application and place it on the right side. \r\nSuccess is when the right side contains the text from the left with the same coloring and formatting.";

            this.ourText.Rtf = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Calibri;}{\\f1\\fnil\\fcharset2 Symbol;}}"
                                + "{\\colortbl ;\\red255\\green0\\blue0;\\red0\\green176\\blue80;}"
                                + "{\\*\\generator Msftedit 5.41.21.2510;}\\viewkind4\\uc1\\pard\\sa200\\sl276\\slmult1\\cf1\\lang9\\ul\\b\\f0\\fs22 Test Three\\cf0\\ulnone\\b0\\par"
                                + "\\cf2\\i Rich Text Test\\par"
                                + "\\cf0\\i0 This test tests the following:\\par"
                                + "\\pard{\\pntext\\f1\\'B7\\tab}{\\*\\pn\\pnlvlblt\\pnf1\\pnindent0{\\pntxtb\\'B7}}\\fi-360\\li720\\sa200\\sl276\\slmult1 Reading RTF Text from the EHR's Text Box\\par"
                                + "}";
 
        }

        private void RunTestThree()
        {
            if (!this.warned)
            {
                MessageBox.Show("Please make sure you have pasted the text from the left box into your EHR Application. \nOnce you push 'Ok', you will have 5 seconds to click into your EHR's main Text Box.");
                this.ShowTestCountdown();
            }
            else
            {
                string readRtf = EHRText.GetRTFFromActiveWindowElement();

                this.theirText.Rtf = readRtf;
            }
        }

        private void PrintPossibleSuccess()
        {
            MessageBox.Show("It looks like the test was a success! \nIf you agree, please push the 'Success' button and continue.");
        }

        private void PrintPossibleFailure()
        {
            MessageBox.Show("It looks like the test was a failure... \nIf you agree, please push the 'Faied' button and report the issue.");
        }

        private void ShowTestCountdown()
        {
            this.testCountdown.Text = "Test will begin in: " + ticks + " seconds";
            this.testCountdown.Enabled = true;
            this.testCountdown.Visible = true;

            this.warned = true;

            this.timer1.Start();
        }

        private void EnableRunButton()
        {
            this.runButton.Visible = true;
            this.runButton.Enabled = true;

            this.successButton.Enabled = false;
            this.successButton.Visible = false;
            this.failedButton.Enabled = false;
            this.failedButton.Visible = false;
        }

        private void EnableResultButtons()
        {
            this.runButton.Visible = false;
            this.runButton.Enabled = false;

            this.successButton.Enabled = true;
            this.successButton.Visible = true;
            this.failedButton.Enabled = true;
            this.failedButton.Visible = true;
        }

        private void runButton_Click(object sender, EventArgs e)
        {
            this.runButton.Enabled = false;
            this.runButton.Visible = false;

            switch (this.currentTestNumber)
            {
                case 1:
                    RunTestOne();
                    break;
                case 2:
                    RunTestTwo();
                    break;
                case 3:
                    RunTestThree();
                    break;
                case 4:
                    //RunTestFour();
                    break;
                default:
                    EnableRunButton();
                    break;
            }
        }

        private void ContinueCurrentTest()
        {
            this.EnableResultButtons();
            this.currentTest();
        }

        private void SetupNextTest()
        {
            this.warned = false;
            this.EnableRunButton();

            this.ourText.Text = "";
            this.theirText.Text = "";

            switch (this.currentTestNumber)
            {
                case 0:
                    this.currentTestNumber = 1;
                    this.currentTest = RunTestOne;

                    SetupTestOne();
                    break;
                case 1:
                    this.currentTestNumber = 2;
                    this.currentTest = RunTestTwo;

                    SetupTestTwo();
                    break;
                case 2:
                    this.currentTestNumber = 3;
                    this.currentTest = RunTestThree;

                    SetupTestThree();
                    break;
                case 3:
                    this.currentTestNumber = 4;
                    //this.currentTest = RunTestFour;

                    //SetupTestFour();
                    break;
                case numberOfTests:
                    ShowResults();
                    break;
                default:
                    break;
            }
        }

        private void ShowResults()
        {
            this.runButton.Enabled = false;
            this.runButton.Visible = false;
            this.successButton.Enabled = false;
            this.successButton.Visible = false;
            this.failedButton.Enabled = false;
            this.failedButton.Visible = false;

            this.testNumber.Text = "Test Number: Done";
            this.testDescription.Text = "Thank you for running all of our tests. \r\nPlease report back the below results.";

            this.ourText.Text = "Results: \r\n\r\n" + this.results.ToString();

            if (Array.TrueForAll(this.results, IsSuccess))
            {
                this.theirText.Text = "Good!";
            }
            else
            {
                this.theirText.Text = "Bad...";
            }
        }

        private bool IsSuccess(bool value)
        {
            return value;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.currentTick >= ticks)
            {
                this.timer1.Stop();
                this.currentTick = 0;
                this.testCountdown.Enabled = false;
                this.testCountdown.Visible = false;

                this.ContinueCurrentTest();
            }
            else
            {
                this.currentTick++;
                this.testCountdown.Text = "Test will begin in: " + (ticks - this.currentTick) + " seconds";
            }
        }

        private void successButton_Click(object sender, EventArgs e)
        {
            this.results[this.currentTestNumber] = true;
            this.SetupNextTest();
        }

        private void failedButton_Click(object sender, EventArgs e)
        {
            this.results[this.currentTestNumber] = false;
            this.SetupNextTest();
        }
    }
}
