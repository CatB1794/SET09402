using System;
using System.Collections.Generic;
using System.IO;

namespace ELM.MsgData
{
    public class TextFormatter
    {
        public Dictionary<string, string> txtMsg = new Dictionary<string, string>();

        /// <summary>
        /// Reads all of the values in the csv file provided, with the first string acting as the key for the dictionary txtMsg.
        /// </summary>
        public void ReadCSV()
        {
            string[] tws = File.ReadAllLines(@"..\textwords.csv");
            foreach (string tw in tws)
            {
                string[] words = tw.Split(",");
                txtMsg.Add(words[0].Trim(), words[1]);
            }
        }

        /// <summary>
        /// This method takes in a string and checks whether any words in the text match the key in dictionary txtMsg.
        /// If it does, the new msg value returned the key + its corresponding full length string.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string FormatMsg(string text)
        {
            ReadCSV();
            string msg = "";
            string[] txt = text.Split(new string[] { " ", Environment.NewLine }, StringSplitOptions.None);
            foreach (string input in txt)
            {
                if (txtMsg.ContainsKey(input))
                {
                    msg += input + " < " + txtMsg[input] + " > ";
                }
                else
                {
                    msg += input + " ";
                }
            }
            return msg;
        }
    }
}