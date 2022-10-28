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
    internal abstract class Instruction
    {
        public static readonly string[] instructions;
        public static bool hasInst(string inst) { return false; }
        public abstract string getMachineCode();
        public string getTypeCode()
        { 
            return toUBianary(opSelector, 5) + toUBianary(opType, 3);
        }
        public uint opType, opSelector;
        protected string toUBianary(uint num, int leading0s)
        {
            string output = Convert.ToString(num, 2).PadLeft(leading0s, '0');
            return output.Substring(output.Length-leading0s);
        }
        protected string toBianary(int num, int leading0s)
        {
            string output = Convert.ToString(num, 2).PadLeft(leading0s, '0');
            return output.Substring(output.Length - leading0s);
        }
        public static Instruction getInstruction(string full,uint line, List<Lable> lables)
        {
            try
            {
                if (full.Contains(' '))
                    full = full.Replace(" ", "");
                string[] parts = full.Split(',');
                String inst = parts[1];
                Instruction instObj;
                if (RInstruction.hasInst(inst))
                    return new RInstruction(inst,line, getReg(parts[0]), getReg(parts[2]), getReg(parts[3]));
                if (IInstruction.hasInst(inst))
                    return new IInstruction(inst,line, getReg(parts[2]), getReg(parts[0]), Int32.Parse(parts[3]));
                if (BInstruction.hasInst(inst)) {
                    string lable = parts[0];
                    foreach (Lable labelObj in lables) {
                        if (labelObj.name.Equals(lable)){
                            return new BInstruction(inst,line, getReg(parts[2]), getReg(parts[3]),labelObj,line);
                        }
                    }
                    throw new InstructionError(line, $"lable {lable} does not exist");
                }
                if (JInstruction.hasInst(inst))
                {
                    string lable = parts[2];
                    foreach (Lable labelObj in lables)
                    {
                        if (labelObj.name.Equals(lable))
                        {
                            return new JInstruction(inst,line, getReg(parts[0]), line, labelObj);
                        }
                    }
                    throw new InstructionError(line, $"lable {lable} does not exist");
                }
                else
                {
                    throw new InstructionError(line, $"the instruction {inst} does not exist");
                }

            }catch(IndexOutOfRangeException e)
            {
                throw new InstructionError(line, "Instruction format invalid");
            }catch(FormatException e)
            {
                throw new InstructionError(line, "register not defined correctly");
            }
        }

        public static uint getReg(string reg) 
        {
            if (reg[0]=='r')
                return UInt32.Parse(reg.Substring(1));
            throw new IndexOutOfRangeException();
        }
    }
    internal class RInstruction : Instruction
    {
        uint rd, rs1, rs2;
        public static new readonly string[] instructions = {"add","sub","or","and"};
        public RInstruction(string inst,uint line, uint rd, uint rs1, uint rs2) 
        {
            opType = 0b00;
            this.rd = rd;
            this.rs1 = rs1;
            this.rs2 = rs2;
            if(!hasInst(inst))
                throw new InstructionError(line, $"{inst} is not a valid instruction");
            for (int i = 0; i < instructions.Length; i++)
            {
                if (instructions[i].Equals(inst))
                {
                    opSelector = (uint)i;
                }
            }
        }
        public static new bool hasInst(string inst)
        {
            foreach (string i in instructions)
            {
                if (i.Equals(inst))
                    return true;
            }
            return false;
        }

        public override string getMachineCode()
        {
            string rs2Bin = toUBianary(rs2, 5), rs1Bin = toUBianary(rs1, 5), rdBin = toUBianary(rd, 5);
            string code = getTypeCode();
            return (rs2Bin + rs1Bin + rdBin + code).PadLeft(32,'0');
        }
    }
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
        public static new bool hasInst(string inst)
        {
            foreach (string i in instructions)
            {
                if (i.Equals(inst))
                    return true;
            }
            return false;
        }
        public override string getMachineCode()
        {
            return (toBianary(imm, 14) + toUBianary(rs1, 5) + toUBianary(rd, 5) + getTypeCode()).PadLeft(32, '0');
        }
    }
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
    internal class BInstruction : Instruction
    {
        uint rs1, rs2;
        uint lineNum = 0;
        Lable lable; 
        public static new readonly string[] instructions = { "bgt","blt","bgte","blte","be","bne"};
       
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
        public static new bool hasInst(string inst)
        {
            foreach (string i in instructions)
            {
                if (i.Equals(inst))
                    return true;
            }
            return false;
        }
        public override string getMachineCode()
        {
            return (toBianary((lable.line - (int)lineNum) * 4, 14) + toUBianary(rs1, 5) + toUBianary(rs1, 5)+ getTypeCode()).PadLeft(32, '0');
        }
    }
    internal class JInstruction : Instruction
    {
        int imm;
        uint rd, lineNum;
        Lable lable;
        public static new readonly string[] instructions = {"jal"};
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
        public static new bool hasInst(string inst)
        {
            foreach (string i in instructions)
            {
                if (i.Equals(inst))
                    return true;
            }
            return false;
        }

        public override string getMachineCode()
        {
            return (toBianary((lable.line - (int)lineNum) * 4, 14) + toUBianary(rd, 5) + getTypeCode()).PadLeft(32, '0');
        }
    }
}