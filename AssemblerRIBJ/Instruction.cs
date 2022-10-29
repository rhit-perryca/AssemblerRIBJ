﻿

using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace AssemblerRIBJ
{
    /// <summary>
    /// Error indicating the instrution that is invalid and on what line 
    /// </summary>
    class InstructionError : Exception
    {
        uint line;
        string message;
        public InstructionError(uint line, string message)
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
            return toUBianary(opSelector, 5) + toUBianary(opType, 3);
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
            string output = Convert.ToString(num, 2).PadLeft(leading0s, '0');
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
        public static Instruction getInstruction(string full, uint line, List<Lable> lables)
        {
            try
            {

                //clean up instruction and split by comma
                if (full.Contains(' '))
                    full = full.Replace(" ", "");
                string[] parts = full.Split(',');

                //get instruction name
                String inst = parts[1];

                //initialize instruction based off of the name
                if (RInstruction.hasInst(inst))
                    return new RInstruction(inst, line, getReg(parts[0]), getReg(parts[2]), getReg(parts[3]));
                if (IInstruction.hasInst(inst))
                    return new IInstruction(inst, line, getReg(parts[2]), getReg(parts[0]), Int32.Parse(parts[3]));
                if (BInstruction.hasInst(inst))
                {

                    //get lable name and look for it in the list of know lables
                    string lable = parts[0];
                    foreach (Lable labelObj in lables)
                    {
                        if (labelObj.name.Equals(lable))
                        {
                            return new BInstruction(inst, line, getReg(parts[2]), getReg(parts[3]), labelObj, line);
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
            catch(Exception e)
            {
                throw new InstructionError(line, "The instruction is not formatted correctly");
            }
        }
        /// <summary>
        /// gets a register nubmer based off of the name
        /// </summary>
        /// <param name="reg">register name</param>
        /// <returns></returns>
        /// <exception cref="IndexOutOfRangeException">throws if the register name is not entered correctly</exception>
        public static uint getReg(string reg)
        {
            if (reg[0] == 'r')
                return UInt32.Parse(reg.Substring(1));
            throw new FormatException();
        }
    }
    /// <summary>
    /// R-type instruction
    /// </summary>
    internal class RInstruction : Instruction
    {
        uint rd, rs1, rs2;
        public static new readonly string[] instructions = { "add", "sub", "or", "and" };
        public RInstruction(string inst, uint line, uint rd, uint rs1, uint rs2)
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
            return (rs2Bin +sep+ rs1Bin + sep + rdBin + sep + code).PadLeft(32, '0');
        }
    }
    /// <summary>
    /// I-type instruction
    /// </summary>
    internal class IInstruction : Instruction
    {
        uint rs1, rd;
        int imm;
        public static new readonly string[] instructions = { "addi", "subi", "ori", "andi", "sli", "sri", "lw", "sw", "jalr" };
        public IInstruction(string inst, uint line, uint rs1, uint rd, int imm)
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
            return (toBianary(imm, 14) +sep+ toUBianary(rs1, 5) + sep + toUBianary(rd, 5) + sep + getTypeCode()).PadLeft(32, '0');
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
    }

    /// <summary>
    /// B-type instruction
    /// </summary>
    internal class BInstruction : Instruction
    {
        uint rs1, rs2;
        uint lineNum = 0;
        Lable lable;
        public static new readonly string[] instructions = { "bgt", "blt", "bgte", "blte", "be", "bne" };

        public BInstruction(string inst, uint line, uint rs1, uint rs2, Lable lable, uint lineNum)
        {
            this.rs1 = rs1;
            this.rs2 = rs2;
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
            return (toBianary((lable.line - (int)lineNum) * 4, 14) +sep+ toUBianary(rs2, 5) +sep+ toUBianary(rs1, 5) +sep+ getTypeCode()).PadLeft(32, '0');
        }
    }
    /// <summary>
    /// J-type instruction class
    /// </summary>
    internal class JInstruction : Instruction
    {
        int imm;
        uint rd, lineNum;
        Lable lable;
        public static new readonly string[] instructions = { "jal" };
        public JInstruction(string inst, uint line, uint rd, uint lineNum, Lable lable)
        {
            this.imm = imm;
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
            return (toBianary((lable.line - (int)lineNum) * 4, 14) +sep+ toUBianary(rd, 5) +sep+ getTypeCode()).PadLeft(32, '0');
        }
    }
}
