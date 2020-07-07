using Microsoft.VisualBasic;
using System;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace UltimateParseInt
{
    class Program
    {
        static void Main(string[] args)
        {
        

            Console.WriteLine("pick a number!");
            string numInWords = Console.ReadLine();


            Console.WriteLine(convertWordsToNums("one thousand and thirty"));

         


        }

        public static int convertWordsToNums(string numInWords)
        {
            int Sum = 0; //for summing the overall total;
            int SingleNum = 0; //for conversion of parts of a number, to chekc it works
            int TensNum = 0;
            int previousNum = 0;

            //the next bit every word is split by spaces, so we need to split the word first by spaces
            string[] BrokenDownNum = numInWords.Split(' ');
            foreach (string word in BrokenDownNum)
            {

                /************************DEAL with single digits and tens ******************************************/
                //words like twenty-four are split by hyphen from the example given
                string[] HyphenatedNum = word.Split('-');


                //this part is to deal with singles and tens e.g. twenty four
                foreach (string numpart in HyphenatedNum)
                {
                    // I still think this can be improved, and put into a function,
                    if (numpart.StartsWith("hun"))
                    {
                        Console.WriteLine("prevnum is" + previousNum);
                        //previous number times 100
                        Sum = Sum + (previousNum * 100) - previousNum;
                        previousNum = Sum;
                    }
                    else if (numpart.StartsWith("tho"))
                    {
                        Console.WriteLine("prevnum is" + previousNum);
                        //previous number times 100
                        Sum = Sum + (previousNum * 1000) - previousNum;
                        previousNum = 0;
                    }
                    else if (numpart.StartsWith("mil"))
                    {
                        Sum = Sum + (previousNum * 1000000) - previousNum;
                        previousNum = 0;
                    }
                    else
                    {

                        SingleNum = DealWithSingleNumbers(numpart);

                        TensNum = dealwithTens(numpart, SingleNum);


                    }


                    if (TensNum > 0)
                    {
                        Sum = Sum + TensNum;
                        previousNum = previousNum + TensNum;
                    }
                    else if (SingleNum > 0)
                    {
                        Sum = Sum + SingleNum;
                        previousNum = previousNum + SingleNum;
                        SingleNum = 0;
                    }



                }

                Console.WriteLine(Sum + "num is end ");
            }
            return Sum;
        }

        public static int DealWithSingleNumbers(string num)
        {
            /***************************************************************/
            /* this function deals with the initial prefix of numbers      */
            /* inputs: the number in words e.g. Two                        */
            /* outputs: either the number or 0                             */
            /***************************************************************/

            string[,] numberPrefixes = {{ "twel", "12" },{ "one", "1" }, { "tw", "2" }, { "th", "3" }, { "fo", "4" }, { "fi", "5" }, { "si", "6" }, { "se", "7" }, { "ei", "8" }, { "ni", "9" }, { "te", "10" }, { "el", "11" } };

            for(int count = 0; count < numberPrefixes.GetLength(0); count++)
            {
                if (num.Contains(numberPrefixes[count, 0]))
                {
                    return int.Parse(numberPrefixes[count, 1]);
                }
            }
            return 0;
        }

        public static int dealwithTens(string num, int currentNum)
        {
            /********************************************************8
            /* inputs: the word as a number, and the current number that 
            /*has been worked out e.g. if the number is Twenty, it will 
            /* send the word "twenty" and the number 2 as tw has the two prefix
            /* Outputs: a number with 10+ applied or 0
            /************************************************************/

            if (num.EndsWith("teen"))
            {
                return currentNum + 10;
            }
            else if (num.EndsWith("ty"))
            {
                return currentNum * 10;
            }
            else return 0;
        }
        //this needs testing and building into current solution
        public static int dealwithThousands(string num, int previousNum)
        {
            /**************************************************************/
            /* inputs: the word "hundred, thousand" and the previous number e.g. 24
            /* Output: the result of multiplying the hundred by the previous num e.g. 2400
            /***************************************************************/
            if (num.StartsWith("hun"))
            {
                //previous number times 100
                return previousNum * 100;
            }
            else if (num.StartsWith("tho"))
            {
                return previousNum * 1000;
            }
            else return previousNum;

        }

    }
}
