using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;

namespace ELM.MsgData
{
    public class JSONFormatter : MsgHandler
    {
        /// <summary>
        /// Takes in the processed message and will serialise the message type values into JSON format.
        /// Once done it will write this string to a JSON file.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public string StoreJSON(Msg msg)
        {
            string jsonOut = "";
            if (msg.Type == MsgType.Email)
            {
                Email email = (Email)msg;
                jsonOut = JsonSerializer.Serialize<Email>(email);
                File.WriteAllText(msg.Type.ToString() + msg.MsgID + ".json", jsonOut);
            }
            else if (msg.Type == MsgType.SMS)
            {
                SMS sms = (SMS)msg;
                jsonOut = JsonSerializer.Serialize<SMS>(sms);
                File.WriteAllText(msg.Type.ToString() + msg.MsgID + ".json", jsonOut);
            }
            else if (msg.Type == MsgType.Tweet)
            {
                Tweet tweet = (Tweet)msg;
                jsonOut = JsonSerializer.Serialize<Tweet>(tweet);
                File.WriteAllText(msg.Type.ToString() + msg.MsgID + ".json", jsonOut);
            }
            return jsonOut;
        }

        /// <summary>
        /// Reads the JSON file into a string using the id value passed.
        /// Once done, the string will be passed onto the json deserialise parser, which is then be used to reformat the JSON into the original message input.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string DisplayJSON(string id)
        {
            string jsonRead = File.ReadAllText(id + ".json");
            string nl = Environment.NewLine;
            string jsonIn = "";

            if (id.StartsWith("Email"))
            {
                Email jsonEmail = JsonSerializer.Deserialize<Email>(jsonRead);
                string emAddress = jsonEmail.Address;
                string emSbj = jsonEmail.SbjLine;
                string emBoby = jsonEmail.EmailBody;
                jsonIn = String.Format("Address: {1}{0}Subject: {2}{0}Content: {3}", nl, emAddress, emSbj, emBoby);
            }
            else if (id.StartsWith("SMS"))
            {
                SMS jsonSMS = JsonSerializer.Deserialize<SMS>(jsonRead);
                string intNum = jsonSMS.InternationalNumber;
                string smsTxt = jsonSMS.SMSBody;
                jsonIn = String.Format("Number: {1}{0}SMS: {2}", nl, intNum, smsTxt);
            }
            else if (id.StartsWith("Tweet"))
            {
                Tweet jsonTweet = JsonSerializer.Deserialize<Tweet>(jsonRead);
                string twtID = jsonTweet.TwitterID;
                string twtTxt = jsonTweet.TweetBody;
                jsonIn = String.Format("TwitterID: {1}{0}Tweet: {2}", nl, twtID, twtTxt);
            }
            return jsonIn;
        }
    }
}