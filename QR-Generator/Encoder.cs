using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR_Generator
{
    class Encoder
    {
        public int errorCorrection { get; set; }
        public int version { get; set; }
        public string encodingString { get; set; }
        public int encodingType { get; set; }

        //Finds the correction version and Error Correction needed for follow Standards.
        public int versionIdenifier(int encodingType, int lengthMessage)
        {

            /*Finds the lowest error correction for the length and type of input
             * max is: Numeric: 255, Alphanumeric: 224, and 8-byte: 271
             * 1 = Low, 0 = Medium, 3 = Quality, 2 = High*/
            switch (encodingType)
            {
                //Numeric
                case 1:
                    if (lengthMessage <= 41)
                    {
                        //Version One
                        if (lengthMessage <= 17)
                            errorCorrection = 2; //High!
                        else if (lengthMessage <= 27)
                            errorCorrection = 3; //Quality!
                        else if (lengthMessage <= 34)
                            errorCorrection = 0; //Medium
                        else
                            errorCorrection = 1; //Low
                        return 1;
                    }
                    else if (lengthMessage <= 77)
                    {
                        //Version Two
                        if (lengthMessage <= 48)
                            errorCorrection = 3; //Medium
                        else if (lengthMessage <= 63)
                            errorCorrection = 0; //Quality!
                        else
                            errorCorrection = 1; //High!
                        return 2;
                    }
                    else if (lengthMessage <= 127)
                    {
                        //Version Three
                        if (lengthMessage <= 101)
                            errorCorrection = 0; //Quality!
                        else
                            errorCorrection = 1; //High!
                        return 3;
                    }
                    else if (lengthMessage <= 187)
                    {
                        //Version Four
                        if (lengthMessage <= 149)
                            errorCorrection = 0; //Quality!
                        else
                            errorCorrection = 1; //High!
                        return 4;
                    }
                    else if (lengthMessage <= 255)
                    {
                        //Version Five
                        if (lengthMessage <= 202)
                            errorCorrection = 0; //Quality!
                        else
                            errorCorrection = 1;
                        return 5;
                    }
                    return 42; //Error, I might need this, but probably not.

                //AlphaNumeric
                case 10:
                    if (lengthMessage <= 25)
                    {
                        //Version One
                        if (lengthMessage <= 10)
                            errorCorrection = 2; //High!
                        else if (lengthMessage <= 16)
                            errorCorrection = 3; //Quality!
                        else if (lengthMessage <= 20)
                            errorCorrection = 0; //Medium
                        else
                            errorCorrection = 1; //Low
                        return 1;
                    }
                    else if (lengthMessage <= 47)
                    {
                        //Version Two
                        if (lengthMessage <= 29)
                            errorCorrection = 3; //Medium
                        else if (lengthMessage <= 38)
                            errorCorrection = 0; //Quality!
                        else
                            errorCorrection = 1; //High!
                        return 2;
                    }
                    else if (lengthMessage <= 77)
                    {
                        //Version Three
                        if (lengthMessage <= 61)
                            errorCorrection = 0; //Quality!
                        else
                            errorCorrection = 1; //High!
                        return 3;
                    }
                    else if (lengthMessage <= 114)
                    {
                        //Version Four
                        if (lengthMessage <= 96)
                            errorCorrection = 0; //Quality!
                        else
                            errorCorrection = 1; //High!
                        return 4;
                    }
                    else if (lengthMessage <= 154)
                    {
                        //Version Five
                        if (lengthMessage <= 122)
                            errorCorrection = 0; //Quality!
                        else
                            errorCorrection = 1; //High!
                        return 5;
                    }
                    else if (lengthMessage <= 195)
                    {
                        //Version Six
                        errorCorrection = 1; //High!
                        return 6;
                    }
                    else if (lengthMessage <= 224)
                    {
                        //Version Seven
                        errorCorrection = 1; //High!
                        return 7;
                    }
                    return 42; //Error D:

                //8-bit byte 
                case 100:
                    if (lengthMessage <= 17)
                    {
                        //Version One
                        if (lengthMessage <= 7)
                            errorCorrection = 2; //High!
                        else if (lengthMessage <= 11)
                            errorCorrection = 3; //Quality!
                        else if (lengthMessage <= 14)
                            errorCorrection = 0; //Medium
                        else
                            errorCorrection = 1; //Low
                        return 1;
                    }
                    else if (lengthMessage <= 32)
                    {
                        //Version Two
                        if (lengthMessage <= 20)
                            errorCorrection = 3; //Medium
                        else if (lengthMessage <= 26)
                            errorCorrection = 0; //Quality!
                        else
                            errorCorrection = 1; //High!
                        return 2;
                    }
                    else if (lengthMessage <= 53)
                    {
                        //Version Three
                        if (lengthMessage <= 42)
                            errorCorrection = 0; //Quality!
                        else
                            errorCorrection = 1; //High!
                        return 3;
                    }
                    else if (lengthMessage <= 78)
                    {
                        //Version Four
                        if (lengthMessage <= 62)
                            errorCorrection = 0; //Quality!
                        else
                            errorCorrection = 1; //High!
                        return 4;
                    }
                    else if (lengthMessage <= 106)
                    {
                        //Version Five
                        if (lengthMessage <= 84)
                            errorCorrection = 0; //Quality!
                        else
                            errorCorrection = 1; //High!
                        return 5;
                    }
                    else if (lengthMessage <= 134)
                    {
                        //Version Six
                        errorCorrection = 1; //High!
                        return 6;
                    }
                    else if (lengthMessage <= 154)
                    {
                        //Version Seven
                        errorCorrection = 1; //High!
                        return 7;
                    }
                    else if (lengthMessage <= 192)
                    {
                        //Version Eight
                        errorCorrection = 1; //High!
                        return 8;
                    }
                    else if (lengthMessage <= 230)
                    {
                        //Version Nine
                        errorCorrection = 1; //High!
                        return 9;
                    }
                    else if (lengthMessage <= 271)
                    {
                        //Version Ten
                        errorCorrection = 1; //High!
                        return 10;
                    }
                    return 42; //I got problems
            }
            return 50; //Another ERROR ?:(
        }

    }
}
