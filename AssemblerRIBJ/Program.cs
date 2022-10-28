using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssemblerRIBJ
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            string[] lines = System.IO.File.ReadLines(@"program.txt").ToArray();
            List<Lable> lables = new List<Lable>();
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (line[line.Length - 1] == ':')
                {
                    lables.Add(new Lable(i+1, line.Substring(0, line.Length - 1)));

                    lines[i] = null;
                }
            }
            List<string> code = new List<string>();
            int lineCounter = 0;
            for (int i= 0;i < lines.Length;i++)
            {

                if (lines[i]!=null)
                {
                    try
                    {
                        Instruction inst = Instruction.getInstruction(lines[i], (uint)lineCounter + 1, lables);
                        code.Add(inst.getMachineCode() + $"      #{lines[i]}");
                        lineCounter++;
                    }catch(InstructionError e)
                    {
                        Console.WriteLine(e.getMessage());
                        File.WriteAllText("out.txt",e.getMessage());
                        return;
                    }
                }

            }
            File.WriteAllLines("out.txt",code);
        }
    }
}
