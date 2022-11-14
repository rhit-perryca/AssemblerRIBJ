using System.Collections.Generic;
using System.IO;

namespace AssemblerRIBJ
{
    internal class Assembler
    {
        public static string[] assembleCode(string[] code, bool showOriginal, bool showSeperators, bool lineNumbers,bool showHex)
        {
            List<Lable> lables = new List<Lable>();
            int lineCounterLable = 0;
            for (int i = 0; i < code.Length; i++)
            {

                string line = code[i].Replace("\t", "").Replace(" ", "");
                if (!line.Contains(":") && !line.Contains(","))
                {
                    code[i] = null;
                    continue;
                }

                if (line[line.Length - 1] == ':')
                {
                    lables.Add(new Lable(lineCounterLable + 1, line.Substring(0, line.Length - 1)));
                }
                else
                {
                    lineCounterLable++;
                }
            }
            List<string> codeOut = new List<string>();
            int lineCounter = 0;
            int visualLineCounter = 0;
            for (int i = 0; i < code.Length; i++)
            {
                if (code[i] == null)
                    continue;
                else if (code[i].Contains(":"))
                {
                    codeOut.Add(code[i].Replace("\t", " ").Replace(" ", ""));
                }
                else
                {
                    Instruction inst = Instruction.getInstruction(code[i].Replace(" ", ""), lineCounter + 1, lables);
                    codeOut.Add(((lineNumbers) ? $"{lineCounter + 1}: " : "") + inst.getMachineCode(showSeperators,showHex) + ((showOriginal) ? $"      #{code[i]}" : ""));
                    lineCounter++;
                }

            }
            return codeOut.ToArray();
        }
        public static void writeMachineCodeToFile(string[] code, bool showOriginal, bool seps, bool lineNumbers,bool showHex)
        {
            try
            {
                string[] machineCode = assembleCode(code, showOriginal, seps, lineNumbers,showHex);
                File.WriteAllLines("out.txt", machineCode);
            }
            catch (InstructionError e)
            {
                File.WriteAllText("out.txt", e.getMessage());
            }
        }
    }
}
