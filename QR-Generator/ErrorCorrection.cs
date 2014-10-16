using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QR_Generator
{
    class ErrorCorrection
    {
        Encoder dataEncoder = new Encoder();

        //These will store important information about the martix of the code
        public int blocks;
        public int numberBlocks;
        public int blocks2;
        public int numberBlocks2;

        //Gets the number of blocks and codeword index
        //This was pain staking to type. Thank heavens for ctrl-c and ctrl-v
        public int codewordBlock(int version, int correctionLevel)
        {
            switch (version)
            {
                case 1:
                    switch (correctionLevel)
                    {
                        case 1: //Low
                            return 19;
                        case 2: //High
                            return 9;
                        case 3: //Quality
                            return 13;
                        case 0: //Medium
                            return 16;
                    } break;
                case 2:
                    switch (correctionLevel)
                    {
                        case 2: //High
                            return 16;
                        case 3: //Quality
                            return 22;
                        case 0: //Medium
                            return 28;
                    } break;
                case 3:
                    switch (correctionLevel)
                    {
                        case 2: //High
                            blocks = 2;
                            numberBlocks = 22;
                            return 26;
                        case 3: //Quality
                            blocks = 2;
                            numberBlocks = 17;
                            return 34;
                    } break;
                case 4:
                    switch (correctionLevel)
                    {
                        case 2: //High
                            blocks = 4;
                            numberBlocks = 9;
                            return 36;
                        case 3: //Quality
                            blocks = 2;
                            numberBlocks = 24;
                            return 48;
                    } break;
                case 5:
                    switch (correctionLevel)
                    {
                        case 2: //High
                            blocks = 2;
                            numberBlocks = 11;
                            blocks2 = 2;
                            numberBlocks2 = 12;
                            return 46;
                        case 3: //Quality
                            blocks = 2;
                            numberBlocks = 15;
                            blocks2 = 2;
                            numberBlocks2 = 16;
                            return 62;
                    } break;
                //Because my version and error correction go to 10 and the last four are 
                //only high, I can simply just return the ER codeword number, block number and block index.
                case 6:
                    blocks = 4;
                    numberBlocks = 15;
                    return 60;
                case 7:
                    blocks = 4;
                    numberBlocks = 13;
                    blocks2 = 1;
                    numberBlocks2 = 14;
                    return 66;
                case 8:
                    blocks = 4;
                    numberBlocks = 14;
                    blocks2 = 2;
                    numberBlocks2 = 15;
                    return 86;
                case 9:
                    blocks = 4;
                    numberBlocks2 = 12;
                    blocks2 = 4;
                    numberBlocks2 = 13;
                    return 100;
                case 10:
                    blocks = 6;
                    numberBlocks = 15;
                    blocks2 = 2;
                    numberBlocks2 = 16;
                    return 122;
            }
            return 1; //Arrgggorrrrr
        }
    }
}
