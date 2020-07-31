using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace ELM.MsgData
{
    public class TextFormatter
    {
        public string csvPath = @"..\textwords.csv";
        public Dictionary<string, string> txtMsg = new Dictionary<string, string>();

        public void ReadCSV()
        {
            string[] tws = File.ReadAllLines(csvPath);
            foreach (string tw in tws)
            {
                string[] words = tw.Split(",");
                txtMsg.Add(words[0], words[1]);
            }
        }

        public string FormatMsg(string text)
        {
            ReadCSV();
            string msg = "";
            string[] txt = text.Split(" ");
            for (int i = 0; i < txt.Length; i++)
            {
                //MessageBox.Show(txtMsg.ContainsKey(txt[i]).ToString());
                if (txtMsg.ContainsKey(txt[i]))
                {
                    msg += txt[i] + " < " + txtMsg[txt[i]] +" > ";
                }
                else
                {
                    msg += txt[i] + " ";
                }
            }
            return msg;
        }
    }
}