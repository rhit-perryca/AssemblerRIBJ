using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssemblerRIBJ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            output.ReadOnly = true;
        }

        private void makeCode(object sender, EventArgs e)
        {
            string[] lines = input.Lines;
            try
            {
                string[] bin = Assembler.assembleCode(lines,showOriginal.Checked,seps.Checked,lineNumbers.Checked,showHex.Checked);
                for(int i = 0; i< bin.Length;i++)
                {
                    bin[i] = bin[i];
                }
                output.Lines = bin;
            }
            catch(InstructionError error)
            {
                

                MessageBox.Show(error.getMessage(), "Error",MessageBoxButtons.OK);
            }
        }

        private void copy_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetText(output.Text);
        }

        private void lineNumbers_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
