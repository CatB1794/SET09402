﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace ELM.MsgData
{
    public class MsgHandler : Msg
    {
        public Msg ProcessData(string msgID, string msgBody)
        {
            string msgType = msgID.Substring(0,1);
            int id = int.Parse(msgID.Substring(1,9));

            MsgType type = MsgTypes.FromStr(msgType);
            switch (type)
            {
                case MsgType.Email:
                    return ProcessEmail(new Email() { MsgID = id }, msgBody);
                case MsgType.SMS:
                    return ProcessSMS(new SMS() { MsgID = id }, msgBody);
                case MsgType.Tweet:
                    return ProcessTweet(new Tweet() { MsgID = id }, msgBody);
                default:
                    throw new Exception();
            }
        }

        public Email ProcessEmail(Email input, string content)
        {
            string[] msg = content.Split("\n");
            input.Address = msg[0];
            input.SbjLine = msg[1];
            if (msg[1].Length > 20)
            {
                MessageBox.Show("Subject line has a maximum of 20 characters.");
            }

            StringBuilder msgTxt = new StringBuilder();
            for(int i = 2; i < msg.Length; i++)
            {
                msgTxt.AppendLine(msg[i]);
            }
            if (msgTxt.Length > 1028)
            {
                MessageBox.Show("Email has a maximum of 1028 characters.");
            }
            else
            {
                input.EmailBody = msgTxt.ToString();
            }

            input.URL = new List<string>();
            for (int i = 2; i < msg.Length; i++)
            {
                if (msgTxt[i].ToString().Contains("www."))
                {
                    input.URL.Add(msgTxt[i].ToString());
                }
                msgTxt.Replace(input.URL.ToString(), "<URL Quarantined>");
            }
            TextWriter tw = new StreamWriter("QuarantinedList.txt");

            foreach (String str in input.URL)
            {
                tw.WriteLine(str);
            }
            tw.Close();

            if (!input.SbjLine.Contains("SIR"))
            {
                input.EmailType = EmailType.Standard;
                input.NoI = null;
                return input;
            }
            else
            {
                string centreCode = msg[2];
                string noi = msg[3];
                input.EmailType = EmailType.SIR;
                input.NoI = new SIRInfo();
                input.NoI.CentreCode = centreCode;
                input.NoI.Type = SIR.SetSIR(noi);
                return input;
            }
        }

        public SMS ProcessSMS(SMS input, string content)
        {
            string[] msg = content.Split("\n");
            string num = msg[0].Replace("+", "");
            if(num.Length > 15 || num.Length < 8)
            {
                MessageBox.Show("Not a valid international number.");
            } 
            else
            {
                input.InternationalNumber = num;
            }
            
            StringBuilder msgTxt = new StringBuilder();
            for (int i = 1; i < msg.Length; i++)
            {
                msgTxt.AppendLine(msg[i]);
            }
            if (msgTxt.Length > 140)
            {
                MessageBox.Show("SMS has a maximum of 140 characters.");
            }
            else
            {
                input.SMSBody = msgTxt.ToString();
            }
            
            return input;
        }

        public Tweet ProcessTweet(Tweet input, string content)
        {
            string[] msg = content.Split("\n");
            if (msg[0].Length > 15)
            {
                MessageBox.Show("Not a valid Twitter ID.");
            }
            else
            {
                input.TwitterID = msg[0];
            }

            StringBuilder msgTxt = new StringBuilder();
            for (int i = 1; i < msg.Length; i++)
            {
                msgTxt.AppendLine(msg[i]);
            }
            if (msgTxt.Length > 140)
            {
                MessageBox.Show("Tweets have a maximum of 140 characters.");
            }
            else
            {
                input.TweetBody = msgTxt.ToString();
            }

            input.Mentions = new List<string>();
            input.Hashtags = new List<string>();
            for (int i = 1; i < msgTxt.Length; i++)
            {
                if (msgTxt[i].ToString().Contains("@"))
                {
                    input.Mentions.Add(msgTxt[i].ToString());
                }
                if (msgTxt[i].ToString().Contains("#"))
                {
                    input.Hashtags.Add(msgTxt[i].ToString());
                }
            }

            return input;
        }
    }
}
