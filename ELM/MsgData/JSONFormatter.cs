using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;

namespace ELM.MsgData
{
    public class JSONFormatter : MsgHandler
    {
        public string setPath = @"..\MsgData\";

        public string StoreJSON(Msg msg)
        {
            Email email = new Email();
            SMS sms = new SMS();
            Tweet tweet = new Tweet();

            if (msg.Type == MsgType.Email)
            {
                string jsonEmail = JsonSerializer.Serialize<Email>(EmailToJSON(email), new JsonSerializerOptions() { WriteIndented = true });
                File.WriteAllText(setPath + msg.Type.ToString() + msg.MsgID + ".json", jsonEmail);
                return jsonEmail;
            }
            else if (msg.Type == MsgType.SMS)
            {
                string jsonSMS = JsonSerializer.Serialize<SMS>(SMSToJSON(sms), new JsonSerializerOptions() { WriteIndented = true });
                File.WriteAllText(setPath + msg.Type.ToString() + msg.MsgID + ".json", jsonSMS);
                return jsonSMS;
            }
            else
            {
                string jsonTweet = JsonSerializer.Serialize<Tweet>(TweetToJSON(tweet), new JsonSerializerOptions() { WriteIndented = true });
                File.WriteAllText(setPath + msg.Type.ToString() + msg.MsgID + ".json", jsonTweet);
                return jsonTweet;
            }
        }

        [JsonExtensionData]
        public Dictionary<string, object> ExtData { get; set; }

        public Email EmailToJSON(Email msg)
        {
            msg.MsgID = this.MsgID;
            msg.Address = ((JsonElement)ExtData["EmailAddress"]).GetString();
            msg.SbjLine = ((JsonElement)ExtData["SubjectLine"]).GetString();
            msg.EmailBody = ((JsonElement)ExtData["EmailBody"]).GetString();

            return msg;
        }

        public SMS SMSToJSON(SMS msg)
        {
            msg.MsgID = this.MsgID;
            msg.InternationalNumber = ((JsonElement)ExtData["InternationalNumber"]).GetString();
            msg.SMSBody = ((JsonElement)ExtData["SMSBody"]).GetString();
            
            return msg;
        }

        public Tweet TweetToJSON(Tweet msg)
        {
            msg.MsgID = this.MsgID;
            msg.TwitterID = ((JsonElement)ExtData["TwitterID"]).GetString();
            msg.TweetBody = ((JsonElement)ExtData["TweetBody"]).GetString();
            msg.Mentions = new List<string>();
            foreach (JsonElement json in ((JsonElement)ExtData["Mentions"]).EnumerateArray())
            {
                msg.Mentions.Add(json.GetString());
            }
            msg.Hashtags = new List<string>();
            foreach (JsonElement json in ((JsonElement)ExtData["Hashtags"]).EnumerateArray())
            {
                msg.Hashtags.Add(json.GetString());
            }
            
            return msg;
        }
    }
}