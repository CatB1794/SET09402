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
    public enum SIRTypes
    {
        BombThreat, CustomerAttack, DeviceDamage, PersonalInfoLeak, Raid, SportInjury, StaffAbuse, StaffAttack, SuspiciousIncident, Terrorism, TheftofProperties
    }

    public class SIR
    {
        public static SIRTypes SetSIR(string sir)
        {
            return sir switch
            {
                "Bomb Threat" => SIRTypes.BombThreat,
                "Customer Attack" => SIRTypes.CustomerAttack,
                "Device Damage" => SIRTypes.DeviceDamage,
                "Personal Info Leak" => SIRTypes.PersonalInfoLeak,
                "Raid" => SIRTypes.Raid,
                "Sport Injury" => SIRTypes.SportInjury,
                "Staff Abuse" => SIRTypes.StaffAbuse,
                "Staff Attack" => SIRTypes.StaffAttack,
                "Suspicious Incident" => SIRTypes.SuspiciousIncident,
                "Terrorism" => SIRTypes.Terrorism,
                "Theft of Properties" => SIRTypes.TheftofProperties,
                _ => throw new Exception("That is not a valid Significant Incident Report subject.")
            };
        }
    }

    public class SIRInfo
    {
        public string CentreCode { get; set; }
        public SIRTypes Type { get; set; }
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
        public SIRInfo NoI { get; set; }
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
        public List<string> Mentions { get; set; }
        public List<string> Hashtags { get; set; }
    }
}
