using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;

namespace ELM.MsgData
{
    public class MsgHandler : Msg
    {
        public TextFormatter textFormatter;

        /// <summary>
        /// Takes a message id and message content, and passes it onto the corresponding message type processor.
        /// </summary>
        /// <param name="msgID"></param>
        /// <param name="msgBody"></param>
        /// <returns></returns>
        public Msg ProcessData(string msgID, string msgBody)
        {
            string msgType = msgID.Substring(0, 1);
            int id = int.Parse(msgID.Substring(1, 9));

            MsgType type = MsgTypes.FromStr(msgType);

            if (type == MsgType.Email)
            {
                return ProcessEmail(new Email() { MsgID = id }, msgBody);
            }
            else if (type == MsgType.SMS)
            {
                return ProcessSMS(new SMS() { MsgID = id }, msgBody);
            }
            else if (type == MsgType.Tweet)
            {
                return ProcessTweet(new Tweet() { MsgID = id }, msgBody);
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Takes in Email object and string values, using the Email input, it assigns values through the getters and setters.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public Email ProcessEmail(Email input, string content)
        {
            if (!content.Contains("@"))
            {
                throw new Exception("Email must have an email address.");
            }
            int index = -1;
            int nlCount = 0;
            while ((index = content.IndexOf(Environment.NewLine, index + 1)) != -1)
            {
                nlCount++;
            }
            if (nlCount < 2)
            {
                throw new Exception("Email must have a sender address, subject line and message separated by a new line.");
            }
            else if (content.Contains("SIR") && nlCount < 4)
            {
                throw new Exception("Incident reports need a centre code and a nature of incident report label.");
            }
            string[] msg = content.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            input.Address = msg[0];
            input.SbjLine = msg[1];
            if (msg[1].Length > 20)
            {
                throw new Exception("Subject line has a maximum of 20 characters.");
            }

            StringBuilder msgTxt = new StringBuilder();
            for (int i = 2; i < msg.Length; i++)
            {
                msgTxt.AppendLine(msg[i]);
            }

            input.URL = new List<string>();
            string[] urls = msgTxt.ToString().Split(new string[] { " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < urls.Length; i++)
            {
                if (urls[i].Trim().StartsWith("www.") || urls[i].Trim().StartsWith("http"))
                {
                    input.URL.Add(urls[i]);
                    msgTxt.Replace(urls[i], "<URL Quarantined>");
                    TextWriter tw = new StreamWriter("QuarantinedList" + input.MsgID + ".txt");
                    foreach (string str in urls)
                    {
                        if (str.StartsWith("www.") || str.StartsWith("http"))
                        {
                            tw.WriteLine(str);
                        }
                    }
                    tw.Close();
                }
            }
            if (msgTxt.Length > 1028)
            {
                throw new Exception("Email has a maximum of 1028 characters.");
            }
            else
            {
                input.EmailBody = msgTxt.ToString();
            }

            if (!input.SbjLine.Contains("SIR"))
            {
                input.EmailType = EmailType.Standard;
                input.NoI = null;
                return input;
            }
            else
            {
                string centreCode = msg[2].Trim();
                string noi = msg[3].Trim();
                input.EmailType = EmailType.SIR;
                input.NoI = new NoIInfo();
                input.NoI.CentreCode = centreCode;
                input.NoI.Type = NoI.SetNoI(noi);
                return input;
            }
        }

        /// <summary>
        /// Helper function for ProcessSMS method, checks if string num is a string of ints.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Takes in SMS object and string values, using the SMS input, it assigns values through the getters and setters.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public SMS ProcessSMS(SMS input, string content)
        {
            textFormatter = new TextFormatter();

            if (!content.Contains(Environment.NewLine))
            {
                throw new Exception("SMS must have a sender number and message separated by a new line.");
            }
            if (!content.StartsWith("+"))
            {
                throw new Exception("SMS must have a international number beginning with +.");
            }
            string[] msg = content.Split(new string[] { " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            string num = msg[0].Replace("+", "");
            if (num.Length > 15 || num.Length < 8 || !IsDigitsOnly(num))
            {
                throw new Exception("Not a valid international number.");
            }
            else
            {
                input.InternationalNumber = num;
            }

            StringBuilder msgTxt = new StringBuilder();
            for (int i = 1; i < msg.Length; i++)
            {
                msgTxt.AppendLine(msg[i].Trim());
            }
            if (msgTxt.Length > 140)
            {
                throw new Exception("SMS has a maximum of 140 characters.");
            }
            else
            {
                input.SMSBody = textFormatter.FormatMsg(msgTxt.ToString());
            }

            return input;
        }

        /// <summary>
        /// Takes in Tweet object and string values, using the Tweet input, it assigns values through the getters and setters.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public Tweet ProcessTweet(Tweet input, string content)
        {
            textFormatter = new TextFormatter();

            if (!content.Contains(Environment.NewLine))
            {
                throw new Exception("Tweet must have a Twitter ID and tweet separated by a new line.");
            }
            if (!content.StartsWith("@"))
            {
                throw new Exception("Tweet must have a Twitter ID beginning with @.");
            }
            string[] msg = content.Split(new string[] { " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            if (msg[0].Length > 15)
            {
                throw new Exception("Not a valid Twitter ID.");
            }
            else
            {
                input.TwitterID = msg[0];
            }

            StringBuilder msgTxt = new StringBuilder();
            for (int i = 1; i < msg.Length; i++)
            {
                msgTxt.AppendLine(msg[i].Trim());
            }
            if (msgTxt.Length > 140)
            {
                throw new Exception("Tweets have a maximum of 140 characters.");
            }
            else
            {
                input.TweetBody = textFormatter.FormatMsg(msgTxt.ToString());
            }

            input.Mentions = new List<string>();
            input.Hashtags = new List<string>();
            string[] mentionHastag = msgTxt.ToString().Split(new string[] { " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < mentionHastag.Length; i++)
            {
                if (mentionHastag[i].StartsWith("@"))
                {
                    input.Mentions.Add(mentionHastag[i].Trim());
                }
                if (mentionHastag[i].StartsWith("#"))
                {
                    input.Hashtags.Add(mentionHastag[i].Trim());
                }
            }

            return input;
        }
    }
}