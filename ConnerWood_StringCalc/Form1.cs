using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ConnerWood_StringCalculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCalculateStringNumber_Click(object sender, EventArgs e)
        {
            int nResult = 0;
            StringCalculator sc = new StringCalculator();
            //Call method Add which takes a string and returns an int
            nResult = sc.Add(txtInput.Text);
            txtOutput.Text = nResult.ToString();
        }

    }
}
