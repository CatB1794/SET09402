using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ELM.MsgData
{
    public class TextFormatter
    {
        public string textwordsPath = @"..\MsgData\textwords.csv";
        public Dictionary<string, string> txtMsg = new Dictionary<string, string>();

        public void ReadCSV()
        {
            string[] textwords = File.ReadAllLines(textwordsPath);

            foreach (string txtword in textwords)
            {
                string[] words = txtword.Split(",");
                txtMsg.Add(words[0].Trim(), words[1]);
            }
        }

        public string FormatTxt(string text)
        {
            string[] txt = text.Split(" ");
            for (int i = 0; i < txt.Length; i++)
            {
                if (txtMsg.ContainsKey(txt[i]))
                {
                    txt[i] += "< "+ txtMsg[txt[i]] +" > ";
                }
            }
            return string.Join(" ", txt);
        }
    }
}