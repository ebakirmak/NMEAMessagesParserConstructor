﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMEA_MessageParserConstructor.BL.Dictionarys
{
    public class Ascii6
    {
        private string ValidCharacter { get; set; }
        private string BinaryField { get; set; }

        public Ascii6()
        {
            this.ValidCharacter = "";
            this.BinaryField = "";
        }

        public string getStringBinarySix(string binary)
        {
            this.ValidCharacter = "";
            //string currentBits = "";
            for (int i = 0; i < binary.Length; i+=6)
            {

                
                if (binary.Count() == 1)
                    BinaryField = binary.PadLeft(6, '0');
                else if (binary.Count() == 2)
                    BinaryField = binary.PadLeft(6, '0');
                else if (binary.Count() == 3)
                    BinaryField = binary.PadLeft(6, '0');
                else if (binary.Count() == 4)
                    BinaryField = binary.PadLeft(6, '0');
                else if (binary.Count() == 5)
                    BinaryField = binary.PadLeft(6, '0');
                else
                    BinaryField = binary;


                if (BinaryField == "000000")
                    this.ValidCharacter += "@";
                else if (BinaryField == "000001")
                    this.ValidCharacter += "A";
                else if (BinaryField == "000010")
                    this.ValidCharacter += "B";
                else if (BinaryField == "000011")
                    this.ValidCharacter += "C";
                else if (BinaryField == "000100")
                    this.ValidCharacter += "D";
                else if (BinaryField == "000101")
                    this.ValidCharacter += "E";
                else if (BinaryField == "000110")
                    this.ValidCharacter += "F";
                else if (BinaryField == "000111")
                    this.ValidCharacter += "G";
                else if (BinaryField == "001000")
                    this.ValidCharacter += "H";
                else if (BinaryField == "001001")
                    this.ValidCharacter += "I";
                else if (BinaryField == "001010")
                    this.ValidCharacter += "J";
                else if (BinaryField == "001011")
                    this.ValidCharacter += "K";
                else if (BinaryField == "001100")
                    this.ValidCharacter += "L";
                else if (BinaryField == "001101")
                    this.ValidCharacter += "M";
                else if (BinaryField == "001110")
                    this.ValidCharacter += "N";
                else if (BinaryField == "001111")
                    this.ValidCharacter += "O";
                else if (BinaryField == "010000")
                    this.ValidCharacter += "P";
                else if (BinaryField == "010001")
                    this.ValidCharacter += "Q";
                else if (BinaryField == "010010")
                    this.ValidCharacter += "R";
                else if (BinaryField == "010011")
                    this.ValidCharacter += "S";
                else if (BinaryField == "010100")
                    this.ValidCharacter += "T";
                else if (BinaryField == "010101")
                    this.ValidCharacter += "U";
                else if (BinaryField == "010110")
                    this.ValidCharacter += "V";
                else if (BinaryField == "010111")
                    this.ValidCharacter += "W";
                else if (BinaryField == "011000")
                    this.ValidCharacter += "X";
                else if (BinaryField == "011001")
                    this.ValidCharacter += "Y";
                else if (BinaryField == "011010")
                    this.ValidCharacter += "Z";
                else if (BinaryField == "011011")
                    this.ValidCharacter += "[";
                else if (BinaryField == "011100")
                    this.ValidCharacter += "\\";
                else if (BinaryField == "011101")
                    this.ValidCharacter += "]";
                else if (BinaryField == "011110")
                    this.ValidCharacter += "^";
                else if (BinaryField == "011111")
                    this.ValidCharacter += "-";
                else if (BinaryField == "100000")
                    this.ValidCharacter += " ";
                else if (BinaryField == "100001")
                    this.ValidCharacter += "!";
                else if (BinaryField == "100010")
                    this.ValidCharacter += "\"";
                else if (BinaryField == "100011")
                    this.ValidCharacter += "#";
                else if (BinaryField == "100100")
                    this.ValidCharacter += "$";
                else if (BinaryField == "100101")
                    this.ValidCharacter += "%";
                else if (BinaryField == "100110")
                    this.ValidCharacter += "&";
                else if (BinaryField == "100111")
                    this.ValidCharacter += "'";
                else if (BinaryField == "101000")
                    this.ValidCharacter += "(";
                else if (BinaryField == "101001")
                    this.ValidCharacter += ")";
                else if (BinaryField == "101010")
                    this.ValidCharacter += "*";
                else if (BinaryField == "101011")
                    this.ValidCharacter += "+";
                else if (BinaryField == "101100")
                    this.ValidCharacter += ",";
                else if (BinaryField == "101101")
                    this.ValidCharacter += "-";
                else if (BinaryField == "101110")
                    this.ValidCharacter += ".";
                else if (BinaryField == "101111")
                    this.ValidCharacter += "/";
                else if (BinaryField == "110000")
                    this.ValidCharacter += "0";
                else if (BinaryField == "110001")
                    this.ValidCharacter += "1";
                else if (BinaryField == "110010")
                    this.ValidCharacter += "2";
                else if (BinaryField == "110011")
                    this.ValidCharacter += "3";
                else if (BinaryField == "110100")
                    this.ValidCharacter += "4";
                else if (BinaryField == "110101")
                    this.ValidCharacter += "5";
                else if (BinaryField == "110110")
                    this.ValidCharacter += "6";
                else if (BinaryField == "110111")
                    this.ValidCharacter += "7";
                else if (BinaryField == "111000")
                    this.ValidCharacter += "8";
                else if (BinaryField == "111001")
                    this.ValidCharacter += "9";
                else if (BinaryField == "111010")
                    this.ValidCharacter += ":";
                else if (BinaryField == "111011")
                    this.ValidCharacter += ";";
                else if (BinaryField == "111100")
                    this.ValidCharacter += "<";
                else if (BinaryField == "111101")
                    this.ValidCharacter += "=";
                else if (BinaryField == "111110")
                    this.ValidCharacter += ">";
                else if (BinaryField == "111111")
                    this.ValidCharacter += "?";
            }

            return this.ValidCharacter;
        }
    }
}