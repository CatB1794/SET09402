using System;
using System.Collections.Generic;
using System.Text;

namespace ELM.MsgData
{
    public static class MsgTypes
    {
        /// <summary>
        /// Takes a string value and if matched returns the corresponding enumerable
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
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

    /// <summary>
    /// Enumerables for the Msg types
    /// </summary>
    public enum MsgType
    {
        Email, SMS, Tweet
    }

    /// <summary>
    /// Gets and sets the Msg type and id
    /// </summary>
    public class Msg
    {
        public MsgType Type { get; set; }
        public int MsgID { get; set; }
    }

    /// <summary>
    /// Enumerables for the Email types
    /// </summary>
    public enum EmailType
    {
        Standard, SIR
    }

    /// <summary>
    /// Enumerables for the Nature of Incident types
    /// </summary>
    public enum NatureOfIncident
    {
        BombThreat, CustomerAttack, DeviceDamage, PersonalInfoLeak, Raid, SportInjury, StaffAbuse, StaffAttack, SuspiciousIncident, Terrorism, TheftofProperties
    }

    public class NoI
    {
        /// <summary>
        /// Takes a string value and if matched returns the corresponding enumerable
        /// </summary>
        /// <param name="sir"></param>
        /// <returns></returns>
        public static NatureOfIncident SetNoI(string sir)
        {
            if (sir == "Bomb Threat")
            {
                return NatureOfIncident.BombThreat;
            }
            else if (sir == "Customer Attack")
            {
                return NatureOfIncident.CustomerAttack;
            }
            else if (sir == "Device Damage")
            {
                return NatureOfIncident.DeviceDamage;
            }
            else if (sir == "Personal Info Leak")
            {
                return NatureOfIncident.PersonalInfoLeak;
            }
            else if (sir == "Raid")
            {
                return NatureOfIncident.Raid;
            }
            else if (sir == "Sport Injury")
            {
                return NatureOfIncident.SportInjury;
            }
            else if (sir == "Staff Abuse")
            {
                return NatureOfIncident.StaffAbuse;
            }
            else if (sir == "Staff Attack")
            {
                return NatureOfIncident.StaffAttack;
            }
            else if (sir == "Suspicious Incident")
            {
                return NatureOfIncident.SuspiciousIncident;
            }
            else if (sir == "Terrorism")
            {
                return NatureOfIncident.Terrorism;
            }
            else if (sir == "Theft of Properties")
            {
                return NatureOfIncident.TheftofProperties;
            }
            else
            {
                throw new Exception("That is not a valid Nature of Incident.");
            }
        }
    }

    /// <summary>
    /// Gets and sets the Nature of Incident type and centre code.
    /// </summary>
    public class NoIInfo
    {
        public string CentreCode { get; set; }
        public NatureOfIncident Type { get; set; }
    }

    /// <summary>
    /// Extends from Msg, gets and sets values for Email object
    /// </summary>
    public class Email : Msg
    {
        public Email()
        {
            this.Type = MsgType.Email;
        }

        public string Address { get; set; }
        public string SbjLine { get; set; }
        public EmailType EmailType { get; set; }
        public NoIInfo NoI { get; set; }
        public string EmailBody { get; set; }
        public List<string> URL { get; set; }
    }

    /// <summary>
    /// Extends from Msg, gets and sets values for SMS object
    /// </summary>
    public class SMS : Msg
    {
        public SMS()
        {
            this.Type = MsgType.SMS;
        }

        public string InternationalNumber { get; set; }
        public string SMSBody { get; set; }
    }

    /// <summary>
    /// Extends from Msg, gets and sets values for Tweet object
    /// </summary>
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