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
                string[] bin = Assembler.assembleCode(lines,showOriginal.Checked,seps.Checked);
                output.Lines = bin;
            }
            catch(InstructionError error)
            {
                

                MessageBox.Show(error.getMessage(), "Error",MessageBoxButtons.OK);
            }
        }

        private void seps_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void showOriginal_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
