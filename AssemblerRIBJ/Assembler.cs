using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblerRIBJ
{
    internal class Assembler
    {
        public static string[] assembleCode(string[] code,bool showOriginal,bool showSeperators)
        {
            List<Lable> lables = new List<Lable>();
            for (int i = 0; i < code.Length; i++)
            {
                string line = code[i];
                if (line[line.Length - 1] == ':')
                {
                    lables.Add(new Lable(i + 1, line.Substring(0, line.Length - 1)));

                    code[i] = null;
                }
            }
            List<string> codeOut = new List<string>();
            int lineCounter = 0;
            for (int i = 0; i < code.Length; i++)
            {

                if (code[i] != null)
                {
                        Instruction inst = Instruction.getInstruction(code[i], (uint)lineCounter + 1, lables);
                        codeOut.Add(inst.getMachineCode(showSeperators) + ((showOriginal)?$"      #{code[i]}":""));
                        lineCounter++;
                }

            }
            return codeOut.ToArray();
        }
        public static void writeMachineCodeToFile(string[] code,bool showOriginal,bool seps)
        {
            try
            {
                string[] machineCode = assembleCode(code,showOriginal,seps);
                File.WriteAllLines("out.txt", machineCode);
            }catch(InstructionError e)
            {
                File.WriteAllText("out.txt",e.getMessage());
            }
        }
    }
}
