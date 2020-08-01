using System;
using System.Collections.Generic;
using System.Text;

namespace ELM.MsgData
{
    public static class MsgTypes
    {
        public static MsgType FromStr(string str)
        {
            if (str == "E")
            {
                return MsgType.Email;
            }
            else if (str ==  "S")
            {
                return MsgType.SMS;
            }
            else if (str == "T")
            {
                return MsgType.Tweet;
            }
            else
            {
                throw new Exception("Message must be either an email, sms or a tweet.\nPlease set to either E, S, or T.");
            }
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
            if (sir == "Bomb Threat")
            {
                return SIRTypes.BombThreat;
            }
            else if (sir == "Customer Attack")
            {
                return SIRTypes.CustomerAttack;
            }
            else if (sir == "Device Damage")
            {
                return SIRTypes.DeviceDamage;
            }
            else if (sir == "Personal Info Leak")
            {
                return SIRTypes.PersonalInfoLeak;
            }
            else if (sir == "Raid")
            {
                return SIRTypes.Raid;
            }
            else if (sir == "Sport Injury")
            {
                return SIRTypes.SportInjury;
            }
            else if (sir == "Staff Abuse")
            {
                return SIRTypes.StaffAbuse;
            }
            else if (sir == "Staff Attack")
            {
                return SIRTypes.StaffAttack;
            }
            else if (sir == "Suspicious Incident")
            {
                return SIRTypes.SuspiciousIncident;
            }
            else if (sir == "Terrorism")
            {
                return SIRTypes.Terrorism;
            }
            else if (sir == "Theft of Properties")
            {
                return SIRTypes.TheftofProperties;
            }
            else
            {
                throw new Exception("That is not a valid Significant Incident Report subject.");
            }
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