using System;
using System.Collections.Generic;
using System.Text;

namespace ELM
{
    public static class MsgTypes
    {
        public static MsgType FromChar(char c)
        {
            MsgType msgType = c switch
            {
                'E' => MsgType.Email,
                'S' => MsgType.Email,
                'T' => MsgType.Email,
                _ => throw new Exception("Message must be either an email, sms or a tweet.\nPlease set to either E, S, or T.")
            };
            return msgType;
        }

        public static string ShowType(this MsgType msgType)
        {
            return msgType switch
            {
                MsgType.Email => "Email",
                MsgType.SMS => "SMS",
                MsgType.Tweet => "Tweet",
                _ => throw new Exception()
            };
        }

        public static string SetType(this MsgType msgType)
        {
            return msgType switch
            {
                MsgType.Email => "E",
                MsgType.SMS => "S",
                MsgType.Tweet => "T",
                _ => throw new Exception()
            };
        }
    }

    public enum MsgType
    {
        Email, SMS, Tweet
    }

    public class Msg
    {
        public MsgType Type { get; set; }
        public int MsgID { get; set; }
    }

    public class Email : Msg
    {
        public Email()
        {
            this.Type = MsgType.Email;
        }

        public string Address { get; set; }
        public string SbjLine { get; set; }
        public string EmailBody { get; set; }
    }

    public class SMS : Msg
    {
        public SMS()
        {
            this.Type = MsgType.SMS;
        }

        public string InternationalNumber { get; set; }
        public string SMSBody { get; set; }
    }

    public class Tweet : Msg
    {
        public Tweet()
        {
            this.Type = MsgType.Tweet;
        }

        public string TwitterID { get; set; }
        public string TweetBody { get; set; }
    }
}
