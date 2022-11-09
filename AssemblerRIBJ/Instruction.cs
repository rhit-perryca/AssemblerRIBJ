

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AssemblerRIBJ
{
    /// <summary>
    /// Error indicating the instrution that is invalid and on what line 
    /// </summary>
    class InstructionError : Exception
    {
        int line;
        string message;
        public InstructionError(int line, string message)
        {
            this.line = line;
            this.message = message;
        }
        public string getMessage()
        {
            return $"Error on line {line}:{message}";
        }
    }
    /// <summary>
    /// Super class for all other instruction types
    /// </summary>
    internal abstract class Instruction
    {
        public static readonly string[] instructions;
        public static bool hasInst(string inst) { return false; }
        public abstract string getMachineCode(bool seperators);

        /// <summary>
        /// Gets the type code for instruction
        /// </summary>
        /// <returns>Instruction type code</returns>
        public string getTypeCode()
        {
            return toUBianary(opSelector, 4) + toUBianary(opType, 2);
        }
        public uint opType, opSelector;

        /// <summary>
        /// Converts unsinged int to bianary with a set ammount of leading 0s
        /// </summary>
        /// <param name="num">number to convert</param>
        /// <param name="leading0s">leading 0s</param>
        /// <returns></returns>
        protected string toUBianary(uint num, int leading0s)
        {
            string output = Convert.ToString(num, 2).PadLeft(leading0s, '0');
            return output.Substring(output.Length - leading0s);
        }

        /// <summary>
        /// Converts int to bianary with a set ammount of leading 0s
        /// </summary>
        /// <param name="num">number to convert</param>
        /// <param name="leading0s">leading 0s</param>
        /// <returns></returns>
        protected string toBianary(int num, int leading0s)
        {
            char leading = '0';
            if (num < 0)
                leading = '1';
            string output = Convert.ToString(num, 2).PadLeft(leading0s, leading);
            return output.Substring(output.Length - leading0s);
        }
        /// <summary>
        /// converts instruction as a string to a instruction object
        /// </summary>
        /// <param name="full">Full instruction string</param>
        /// <param name="line">line number</param>
        /// <param name="lables">list of lables in program</param>
        /// <returns></returns>
        /// <exception cref="InstructionError"></exception>
        public static Instruction getInstruction(string full, int line, List<Lable> lables)
        {
            try
            {

                //clean up instruction and split by comma
                if (full.Contains(' '))
                    full = full.Replace(" ", "");
                if (full.Contains('\t'))
                    full = full.Replace("\t", "");
                string[] parts = full.Split(',');

                //get instruction name
                String inst = parts[1];

                //initialize instruction based off of the name
                if (RInstruction.hasInst(inst))
                    return new RInstruction(inst, line, getReg(parts[0]), getReg(parts[2]), getReg(parts[3]));
                if (IInstruction.hasInst(inst))
                {
                    if (inst.Equals("sw") || inst.Equals("lw"))
                    {
                        int imm = Int16.Parse(Regex.Match(parts[2], @"\d+").Value);
                        string reg = parts[2].Split('[')[0];
                        return new IInstruction(inst, line, getReg(reg), getReg(parts[0]), imm);
                    }
                    if (inst.Equals("jalr"))
                    {
                        int imm = 0;
                        if (parts.Length == 3)
                            imm = Int16.Parse(parts[2]);
                        return new IInstruction(inst, line, getReg(parts[0]), 0, imm);
                    }
                    return new IInstruction(inst, line, getReg(parts[2]), getReg(parts[0]), Int16.Parse(parts[3]));
                }
                if (BInstruction.hasInst(inst))
                {

                    //get lable name and look for it in the list of know lables
                    string lable = parts[0];
                    foreach (Lable labelObj in lables)
                    {
                        if (labelObj.name.Equals(lable))
                        {
                            return new BInstruction(inst, line, getReg(parts[2]), getReg(parts[3]), labelObj);
                        }
                    }
                    throw new InstructionError(line, $"lable {lable} does not exist");
                }
                if (JInstruction.hasInst(inst))
                {
                    //get lable name and look for it in the list of know lables
                    string lable = parts[2];
                    foreach (Lable labelObj in lables)
                    {
                        if (labelObj.name.Equals(lable))
                        {
                            return new JInstruction(inst, line, getReg(parts[0]), line, labelObj);
                        }
                    }
                    throw new InstructionError(line, $"lable {lable} does not exist");
                }
                else
                {
                    throw new InstructionError(line, $"the instruction {inst} does not exist");
                }

            }
            catch (FormatException e)
            {
                throw new InstructionError(line, "register not defined correctly");
            }
            catch (Exception e)
            {
                throw new InstructionError(line, "The instruction is not formatted correctly. Your register number also might be out of the valid range(0-31).");
            }
        }
        /// <summary>
        /// gets a register nubmer based off of the name
        /// </summary>
        /// <param name="reg">register name</param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException">throws if the register name is not entered correctly</exception>
        public static uint getReg(string register)
        {
            string reg = register.ToLower();
            if (reg.ToLower().Equals("sp"))
                return 3;
            if (reg.ToLower().Equals("ra"))
                return 2;
            if (reg.ToLower().Equals("pc"))
                return 4;
            if (reg.ToLower().Equals("gp"))
                return 5;
            try
            {
                string type = reg.Split('.')[0].ToLower();
                uint number = UInt32.Parse(reg.Split('.')[1]);
                if (type.Equals("rv"))
                {
                    if (number > 3)
                        throw new FormatException();
                    return 6 + number;
                }
                else if (type.Equals("fa"))
                {
                    if (number > 5)
                        throw new FormatException();
                    return 10 + number;
                }
                else if (type.Equals("s"))
                {
                    if (number > 10)
                        throw new FormatException();
                    return 16 + number;
                }
                else if (type.Equals("t"))
                {
                    if (number > 6)
                        throw new FormatException();
                    return 26 + number;
                }
                else if (type.Equals("r"))
                {
                    if (number > 31)
                        throw new FormatException();
                    return number;
                }
                throw new FormatException();

            }
            catch (Exception e)
            {
                throw new FormatException();
            }
        }
    }
    /// <summary>
    /// R-type instruction
    /// </summary>
    internal class RInstruction : Instruction
    {
        uint rd, rs1, rs2;
        public static new readonly string[] instructions = { "add", "sub", "or", "and" };
        public RInstruction(string inst, int line, uint rd, uint rs1, uint rs2)
        {
            opType = 0b00;
            this.rd = rd;
            this.rs1 = rs1;
            this.rs2 = rs2;
            if (!hasInst(inst))
                throw new InstructionError(line, $"{inst} is not a valid instruction");
            for (int i = 0; i < instructions.Length; i++)
            {
                if (instructions[i].Equals(inst))
                {
                    opSelector = (uint)i;
                }
            }
        }

        /// <summary>
        /// Loops through all r type instructions and checks if inst is one of them
        /// </summary>
        /// <param name="inst">instruction name</param>
        /// <returns>true/false based on if this type has the given instruction</returns>
        public static new bool hasInst(string inst)
        {
            foreach (string i in instructions)
            {
                if (i.Equals(inst))
                    return true;
            }
            return false;
        }


        /// <summary>
        /// gets the machine code for the line
        /// </summary>
        /// <returns>machine code as a string</returns>
        public override string getMachineCode(bool seperators)
        {
            string rs2Bin = toUBianary(rs2, 5), rs1Bin = toUBianary(rs1, 5), rdBin = toUBianary(rd, 5);
            string code = getTypeCode();
            string sep = (seperators) ? "-" : "";
            return (rs2Bin + sep + rs1Bin + sep + rdBin + sep + code).PadLeft(32, '0');
        }
    }
    /// <summary>
    /// I-type instruction
    /// </summary>
    internal class IInstruction : Instruction
    {
        uint rs1, rd;
        int imm;
        public static new readonly string[] instructions = { "addi", "subi", "ori", "sli", "sri", "lw", "sw", "jalr" };
        public IInstruction(string inst, int line, uint rs1, uint rd, int imm)
        {
            opType = 1;
            this.rs1 = rs1;
            this.rd = rd;
            this.imm = imm;
            if (!hasInst(inst))
                throw new InstructionError(line, $"{inst} is not a valid instruction");
            for (int i = 0; i < instructions.Length; i++)
            {
                if (instructions[i].Equals(inst))
                {
                    opSelector = (uint)i;
                }
            }
        }
        public IInstruction(string inst, int line, uint rd, Lable lable)
        {
            opType = 1;
            rs1 = 0;
            this.rd = rd;
            imm = (lable.line - line) * 2;
            if (!hasInst(inst))
                throw new InstructionError(line, $"{inst} is not a valid instruction");
            for (int i = 0; i < instructions.Length; i++)
            {
                if (instructions[i].Equals(inst))
                {
                    opSelector = (uint)i;
                }
            }
        }
        /// <summary>
        /// Loops through all r type instructions and checks if inst is one of them
        /// </summary>
        /// <param name="inst">instruction name</param>
        /// <returns>true/false based on if this type has the given instruction</returns>
        public static new bool hasInst(string inst)
        {
            foreach (string i in instructions)
            {
                if (i.Equals(inst))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// gets the machine code for the line
        /// </summary>
        /// <returns>machine code as a string</returns>
        public override string getMachineCode(bool seperators)
        {
            string sep = (seperators) ? "-" : "";
            string inst = (toBianary(imm, 16) + sep + toUBianary(rs1, 5) + sep + toUBianary(rd, 5) + sep + getTypeCode());
            return inst.PadLeft(32, inst[0]);
        }
    }

    /// <summary>
    /// Class that keeps track of lables, their names  and postions
    /// </summary>
    internal class Lable
    {
        public int line;
        public string name;

        public Lable(int line, string name)
        {
            this.line = line;
            this.name = name;
        }
        public override string ToString()
        {
            return name;
        }
    }

    /// <summary>
    /// B-type instruction
    /// </summary>
    internal class BInstruction : Instruction
    {
        uint rs1, rs2;
        int lineNum = 0;
        Lable lable;
        public static new readonly string[] instructions = { "bgt", "blt", "bgte", "blte", "be", "bne" };

        public BInstruction(string inst, int line, uint rs1, uint rs2, Lable lable)
        {
            this.rs1 = rs1;
            this.rs2 = rs2;
            this.lable = lable;
            this.lineNum = line;
            if (!hasInst(inst))
                throw new InstructionError(line, $"{inst} is not a valid instruction");
            for (int i = 0; i < instructions.Length; i++)
            {
                if (instructions[i].Equals(inst))
                {
                    opSelector = (uint)i;
                }
            }
        }
        // <summary>
        /// Loops through all r type instructions and checks if inst is one of them
        /// </summary>
        /// <param name="inst">instruction name</param>
        /// <returns>true/false based on if this type has the given instruction</returns>
        public static new bool hasInst(string inst)
        {
            foreach (string i in instructions)
            {
                if (i.Equals(inst))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// gets the machine code for the line
        /// </summary>
        /// <returns>machine code as a string</returns>
        public override string getMachineCode(bool seperators)
        {
            string sep = (seperators) ? "-" : "";
            string inst = (toBianary((lable.line - lineNum) * 2, 16) + sep + toUBianary(rs2, 5) + sep + toUBianary(rs1, 5) + sep + getTypeCode());
            return inst.PadLeft(32, inst[0]);
        }
    }
    /// <summary>
    /// J-type instruction class
    /// </summary>
    internal class JInstruction : Instruction
    {
        uint rd;
        int lineNum;
        Lable lable;
        public static new readonly string[] instructions = { "jal" };
        public JInstruction(string inst, int line, uint rd, int lineNum, Lable lable)
        {
            this.rd = rd;
            this.lable = lable;
            this.lineNum = lineNum;
            if (!hasInst(inst))
                throw new InstructionError(line, $"{inst} is not a valid instruction");
            for (int i = 0; i < instructions.Length; i++)
            {
                if (instructions[i].Equals(inst))
                {
                    opSelector = (uint)i;
                }
            }
        }
        // <summary>
        /// Loops through all r type instructions and checks if inst is one of them
        /// </summary>
        /// <param name="inst">instruction name</param>
        /// <returns>true/false based on if this type has the given instruction</returns>
        public static new bool hasInst(string inst)
        {
            foreach (string i in instructions)
            {
                if (i.Equals(inst))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// gets the machine code for the line
        /// </summary>
        /// <returns>machine code as a string</returns>
        public override string getMachineCode(bool seperators)
        {
            string sep = (seperators) ? "-" : "";
            string inst = (toBianary((lable.line - lineNum) * 2, 21) + sep + toUBianary(rd, 5) + sep + getTypeCode());
            return inst.PadLeft(32, inst[0]);
        }
    }
}
