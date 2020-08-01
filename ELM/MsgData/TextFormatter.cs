using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;

namespace ELM.MsgData
{
    public class TextFormatter
    {
        public Dictionary<string, string> txtMsg = new Dictionary<string, string>();

        public void ReadCSV()
        {
            string[] tws = File.ReadAllLines(@"..\textwords.csv");
            foreach (string tw in tws)
            {
                string[] words = tw.Split(",");
                txtMsg.Add(words[0].Trim(), words[1]);
            }
        }

        public string FormatMsg(string text)
        {
            ReadCSV();
            string msg = "";
            string[] txt = text.Split(new string[] { " ", "\r", "\n" }, StringSplitOptions.None);
            foreach (string input in txt)
            {
                if (txtMsg.ContainsKey(input))
                {
                    msg += input + " < " + txtMsg[input] +" > ";
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