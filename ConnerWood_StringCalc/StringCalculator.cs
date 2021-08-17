using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnerWood_StringCalculator
{
    public class StringCalculator
    {
        public int Add(string sNumber)
        {
            int nResult = 0;
            if (string.IsNullOrEmpty(sNumber))
            {
                return nResult;
            }

            nResult = ProcessStringToNumber(sNumber);

            return nResult;
        }


        private int ProcessStringToNumber(string sNumber)
        {
            int nNumber = 0;
            string[] sNumArray = null;
            List<string> delimiterList = new List<string>{ "," };
            List<int> negativeNums = new List<int>();
            //Handle new lines as specified in 2.
            delimiterList.Add("\n");

            //Windows Textbox it will use \r\n so we need to replace that with just \n
            sNumber = sNumber.Replace(Environment.NewLine, "\n");

            //3.  Allow custom delimiter control code.
            if (sNumber.StartsWith("//",StringComparison.CurrentCultureIgnoreCase))
            {
                //We need to parse out and add our custom delimiter to our code
                //We are assuming no user error here
                //The terminator of the control code is the new line "\n" following it.
                int nCodeEnd = 0;
                nCodeEnd = sNumber.IndexOf("\n", 0);
                string customDelimiter = sNumber.Substring(2, nCodeEnd - 2);
                if(!string.IsNullOrEmpty(customDelimiter))
                {
                    //Bonus 3. Allow for multiple delimiters (each delimiter is separated by single comma)
                    if(customDelimiter.Contains(","))
                    {
                        //Assume we are dealing with multiple delimiters
                        string[] customDelims = customDelimiter.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        foreach(string sDelim in customDelims)
                        {
                            //Add custom delimiter to our delimiter list
                            delimiterList.Add(sDelim);
                        }
                    }
                    else
                    {
                        //Add the single custom delimiter to our delimiter list
                        delimiterList.Add(customDelimiter);
                    }
                    //Remove control code from sNumber as we no longer need it
                    sNumber = sNumber.Substring(nCodeEnd + 1);
                }
            }

            sNumArray = sNumber.Split(delimiterList.ToArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (string sNum in sNumArray)
            {
                if (!string.IsNullOrEmpty(sNum))
                {
                    //Cast string to int and add the numbers up
                    int i = Int32.Parse(sNum);
                    if(i >= 0)
                    {
                        //Bonus 1. ignore numbers larger than 1000
                        if(i <= 1000)
                        {
                            //Adding the numbers
                            nNumber = nNumber + i;
                        }
                    }
                    else
                    {
                        //Add to list of negative numbers to call an exception listing negative numbers
                        negativeNums.Add(i);
                    }
                }
            }

            //If we have negative numbers, throw exception
            if(negativeNums.Count > 0)
            {
                //Throw "Negatives not allowed" Exception
                //Exception needs to list the negative numbers
                string sDetails = string.Join(",", negativeNums);
                Exception ex = new Exception("Negatives not allowed: " + sDetails);
                throw ex;
            }

            //Return Number
            return nNumber;
        }
    }
}
