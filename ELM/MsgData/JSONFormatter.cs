using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ELM.MsgData
{
    public class JSONFormatter : Msg
    {
        public string setPath = "..\\MsgData\\";

        public string StoreJSON(Msg msg)
        {
            string jsonEmail = JsonSerializer.Serialize<Email>(EmailToJSON(), new JsonSerializerOptions() { WriteIndented = true });
            string jsonSMS = JsonSerializer.Serialize<SMS>(SMSToJSON(), new JsonSerializerOptions() { WriteIndented = true });
            string jsonTweet = JsonSerializer.Serialize<Tweet>(TweetToJSON(), new JsonSerializerOptions() { WriteIndented = true });

            if (this.Type == MsgType.Email)
            {
                File.WriteAllText(@setPath + msg.MsgID + ".json", jsonEmail);
                return jsonEmail;
            }
            else if (this.Type == MsgType.SMS)
            {
                File.WriteAllText(@setPath + msg.MsgID + ".json", jsonSMS);
                return jsonSMS;
            }
            else
            {
                File.WriteAllText(@setPath + msg.MsgID + ".json", jsonTweet);
                return jsonTweet;
            }
        }

        [JsonExtensionData]
        public Dictionary<string, object> ExtData { get; set; }

        public Msg GetMsg()
        {
            return this.Type switch
            {
                MsgType.Email => EmailToJSON(),
                MsgType.SMS => SMSToJSON(),
                MsgType.Tweet => TweetToJSON(),
                _ => throw new Exception()
            };
        }

        public Email EmailToJSON()
        {
            Email email = new Email();

            email.MsgID = this.MsgID;
            email.Address = ((JsonElement)ExtData["EmailAddress"]).GetString();
            email.SbjLine = ((JsonElement)ExtData["SubjectLine"]).GetString();
            email.EmailBody = ((JsonElement)ExtData["EmailMessage"]).GetString();

            return email;
        }

        public SMS SMSToJSON()
        {
            SMS sms = new SMS();
            
            sms.MsgID = this.MsgID;
            sms.InternationalNumber = ((JsonElement)ExtData["InternationalNumber"]).GetString();
            sms.SMSBody = ((JsonElement)ExtData["SMSMessage"]).GetString();
            
            return sms;
        }

        public Tweet TweetToJSON()
        {
            Tweet tweet = new Tweet();
            
            tweet.MsgID = this.MsgID;
            tweet.TwitterID = ((JsonElement)ExtData["TwitterID"]).GetString();
            tweet.TweetBody = ((JsonElement)ExtData["TwitterMessage"]).GetString();
            tweet.Mentions = new List<string>();
            foreach (JsonElement json in ((JsonElement)ExtData["Mentions"]).EnumerateArray())
            {
                tweet.Mentions.Add(json.GetString());
            }
            tweet.Hashtags = new List<string>();
            foreach (JsonElement json in ((JsonElement)ExtData["Hashtags"]).EnumerateArray())
            {
                tweet.Hashtags.Add(json.GetString());
            }
            
            return tweet;
        }
    }
}
