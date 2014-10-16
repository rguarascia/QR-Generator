using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR_Generator
{
    class Helpers
    {
        Encoder dataEncoder = new Encoder();
        Dictionary<String, short> AlphaNum = new Dictionary<String, short>();
        int[,] codewordIndex = {{ 9, 13, 16, 19 }, { 16, 22, 28, 34 }, { 26, 34, 44, 55 }, { 36, 48, 64, 80 }, 
                               { 46, 62, 86, 108 }, { 60, 76, 108, 136 }, { 66, 88, 124, 156 }, { 86, 110, 154, 194 },
                               { 100, 132, 182, 232 }, { 122, 154, 216, 274 }};
        //Preps that Dictionary for the AlphaNumeric encoding
        private void Alphanumeric()
        {
            AlphaNum.Add("0", 0);
            AlphaNum.Add("1", 1);
            AlphaNum.Add("2", 2);
            AlphaNum.Add("3", 3);
            AlphaNum.Add("4", 4);
            AlphaNum.Add("5", 5);
            AlphaNum.Add("6", 6);
            AlphaNum.Add("7", 7);
            AlphaNum.Add("8", 8);
            AlphaNum.Add("9", 9);
            AlphaNum.Add("A", 10);
            AlphaNum.Add("B", 11);
            AlphaNum.Add("C", 12);
            AlphaNum.Add("D", 13);
            AlphaNum.Add("E", 14);
            AlphaNum.Add("F", 15);
            AlphaNum.Add("G", 16);
            AlphaNum.Add("H", 17);
            AlphaNum.Add("I", 18);
            AlphaNum.Add("J", 19);
            AlphaNum.Add("K", 20);
            AlphaNum.Add("L", 21);
            AlphaNum.Add("M", 22);
            AlphaNum.Add("N", 23);
            AlphaNum.Add("O", 24);
            AlphaNum.Add("P", 25);
            AlphaNum.Add("Q", 26);
            AlphaNum.Add("R", 27);
            AlphaNum.Add("S", 28);
            AlphaNum.Add("T", 29);
            AlphaNum.Add("U", 30);
            AlphaNum.Add("V", 31);
            AlphaNum.Add("W", 32);
            AlphaNum.Add("X", 33);
            AlphaNum.Add("Y", 34);
            AlphaNum.Add("Z", 35);
            AlphaNum.Add(" ", 36);
            AlphaNum.Add("$", 37);
            AlphaNum.Add("%", 38);
            AlphaNum.Add("*", 39);
            AlphaNum.Add("+", 40);
            AlphaNum.Add("-", 41);
            AlphaNum.Add(".", 42);
            AlphaNum.Add("/", 43);
            AlphaNum.Add(":", 44);
            //That was pain staking. 
        }

        //Splits a string into sizes that are asked
        public List<string> dataChunker(string Sentence, int chunkSize)
        {
            //Cuts input into whatever size needed
            List<string> cutUp = new List<string>();
            for (int i = 0; i < Sentence.Length; i += chunkSize)
            {
                if (i + chunkSize > Sentence.Length) chunkSize = Sentence.Length - i;
                cutUp.Add((Sentence.Substring(i, chunkSize)));
            }
            return cutUp;
        }

        //Encoded the data into correct AlphaNumeric sequence
        private string AlphaNumericEncode(string theInput)
        {
            //Load all the chars into dictionary
            Alphanumeric();
            List<string> choppedData = dataChunker(theInput, 2);
            StringBuilder intValues = new StringBuilder("0010 " + versionTypeSizeCorrector(theInput.Length, 10, dataEncoder.version) + " ");

            for (int placement = 0; placement < choppedData.Count; placement++)
            {
                char[] cutUp = choppedData[placement].ToCharArray();
                int tempOne = 0;
                int tempTwo = 0;
                if (AlphaNum.ContainsKey(cutUp[0].ToString()))
                    tempOne = AlphaNum[cutUp[0].ToString()];
                if (cutUp.Length >= 2)
                {
                    if (AlphaNum.ContainsKey(cutUp[1].ToString()))
                        tempTwo = AlphaNum[cutUp[1].ToString()];
                    intValues.Append(Convert.ToString(Convert.ToInt16((tempOne * 45) + tempTwo), 2).PadLeft(11, '0'));
                }
                else
                    intValues.Append(Convert.ToString(tempOne, 2).PadLeft(6, '0'));

                intValues.Append(" ");
            }
            return intValues.ToString();
        }

        //Sizes the chunks of the string into proper sizes required by the standards of the version and type of encoding
        private string versionTypeSizeCorrector(int inputLength, int dataType, int versionNum)
        {
            if (versionNum <= 9)
            {
                switch (dataType)
                {
                    case 1:
                        return Convert.ToString(inputLength, 2).PadLeft(10, '0');
                    case 10:
                        return Convert.ToString(inputLength, 2).PadLeft(9, '0');
                    case 100:
                        return Convert.ToString(inputLength, 2).PadLeft(8, '0');
                }
            }
            else if (versionNum <= 26)
            {
                switch (dataType)
                {
                    case 1:
                        return Convert.ToString(inputLength, 2).PadLeft(12, '0');
                    case 10:
                        return Convert.ToString(inputLength, 2).PadLeft(11, '0');
                    case 100:
                        return Convert.ToString(inputLength, 2).PadLeft(16, '0');
                }
            }
            return "1"; //error return? I am guessing ?:o
        }

        //Processes a homemade, duct tape, spit and grit algorithm to check if it is a valid AlpaNumeric string
        private bool alphaCheck(string theInput)
        {
            //Gets string and checks if it meets the needs of alphanumeric
            //Probably more efficent ways
            bool checkThis = true;
            string alphaCheck = "ABCDEFGHIJKLMNOPQRSTUVWXYZ$%*+-./: ";
            foreach (char letters in theInput)
            {
                foreach (char lets in alphaCheck)
                {
                    if (letters.Equals(lets))
                    {
                        checkThis = true;
                        break;
                    }
                    else if (!letters.Equals(lets))
                        checkThis = false;
                }
            }
            return checkThis;
        }

        //Encodes the string properly to the Standards of Eightbit encoding
        private string EightBit(string Data)
        {
            //Concerts the string into hex, the binary then pads left if needed
            StringBuilder Pile = new StringBuilder("0100 " + versionTypeSizeCorrector(Data.Length, 100, dataEncoder.version) + " ");
            char[] values = Data.ToCharArray();
            foreach (char letter in values)
            {
                int value = Convert.ToInt32(letter);
                string hexOutput = String.Format("{0:X}", value);
                Pile.Append(Convert.ToString(Convert.ToInt32(hexOutput, 16), 2).PadLeft(8, '0'));
            }
            return Pile.ToString();
        }

        //Checks the type of encoding for the correction type for the entered string
        private int EncodeType(string data)
        {
            if (data.All(Char.IsDigit))
                return 1; //Numeric
            else if (alphaCheck(data))
                return 10; //AlphaNumeric
            else
                return 100; //8-bit byte type
        }

        //Does a lot of stuff
        // - Adds the termintor bytes
        // - Makes multiple of eight
        // - Adds sequence of bytes to fill void
        public string addPadding(string inMessage, int codewordNum)
        {
            string[] tempMessage = inMessage.Split(' ');
            codewordNum *= 8; //Gets the ammount of bits nessesary 
            int messageLength = inMessage.Replace(" ", null).Length;
            StringBuilder compiledData = new StringBuilder();
            //Adds Terminator Bytes
            int lastIndex = tempMessage.Length - 2;

            if (Math.Abs(messageLength - codewordNum) >= 4)
                tempMessage[lastIndex] = tempMessage[lastIndex].PadRight(tempMessage[lastIndex].Length + 4, '0');
            else
                tempMessage[lastIndex] = tempMessage[lastIndex].PadRight(Math.Abs(messageLength - codewordNum), '0');
            for (int x = 0; x < tempMessage.Length; x++)
                compiledData.Append(tempMessage[x]);
            string theData = compiledData.ToString();
            //Makes byes multiple of 8
            int leftOver = theData.Length % 8;
            leftOver = Math.Abs(8 - leftOver);
            if (leftOver != 0)
                theData = theData.PadRight(theData.Length + leftOver, '0');
            int remainding = Math.Abs(codewordNum - theData.Length) / 8;
            string[] padBytes = new string[] { "11101100", "00010001" };

            bool flipFlop = true;
            //Adds bytes if the message is still too short
            if (remainding != 0)
            {
                for (int x = 0; x < remainding; x++)
                {
                    if (flipFlop)
                        theData += padBytes[0];
                    else
                        theData += padBytes[1];

                    flipFlop = !flipFlop;
                }
            }
            return theData;
        }

        //Returns the number of codewords
        public int getCodewordNum(int versionNum, int correctionNum)
        {
            return codewordIndex[versionNum - 1, errorLevelFix(correctionNum)];
        }

        //Shifts the error corrects to match the array index
        private int errorLevelFix(int oldError)
        {
            switch (oldError)
            {
                case 1:
                    return 3;
                case 0:
                    return 2;
                case 3:
                    return 1;
                case 2:
                    return 0;
            }

            return 42; //Whatever. If this gets called then I did something seriously wrong
        }
    }
}
