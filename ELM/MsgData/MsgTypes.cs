using System;
using System.Collections.Generic;
using System.Text;

namespace ELM.MsgData
{
    public static class MsgTypes
    {
        public static MsgType FromStr(string str)
        {
            MsgType msgType = str switch
            {
                "E" => MsgType.Email,
                "S" => MsgType.SMS,
                "T" => MsgType.Tweet,
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

    public enum EmailType
    {
        Standard, SIR
    }
    public enum SIRType
    {
        BombThreat, CustomerAttack, DeviceDamage, PersonalInfoLeak, Raid, SportInjury, StaffAbuse, StaffAttack, SuspiciousIncident, Terrorism, TheftofProperties
    }

    public class SIR
    {
        public static SIRType SetSIR(string sir)
        {
            return sir switch
            {
                "Bomb Threat" => SIRType.BombThreat,
                "Customer Attack" => SIRType.CustomerAttack,
                "Device Damage" => SIRType.DeviceDamage,
                "Personal Info Leak" => SIRType.PersonalInfoLeak,
                "Raid" => SIRType.Raid,
                "Sport Injury" => SIRType.SportInjury,
                "Staff Abuse" => SIRType.StaffAbuse,
                "Staff Attack" => SIRType.StaffAttack,
                "Suspicious Incident" => SIRType.SuspiciousIncident,
                "Terrorism" => SIRType.Terrorism,
                "Theft of Properties" => SIRType.TheftofProperties,
                _ => throw new Exception("That is not a valid Significant Incident Report subject.")
            };
        }

        public string CentreCode { get; set; }
        public SIRType Type { get; set; }
    }

    public class Email : Msg
    {
        public Email()
        {
            this.Type = MsgType.Email;
        }

        public string Address { get; set; }
        public string SbjLine { get; set; }
        public EmailType EmailType { get; set; }
        public SIRType Incident { get; set; }
        public SIR NoI { get; set; }
        public string EmailBody { get; set; }
        public List<string> URL { get; set; }
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
        public string Mentions { get; set; }
        public List<string> Hashtag { get; set; }
    }
}
