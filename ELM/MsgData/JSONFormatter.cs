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
        public MsgHandler MsgHandler;
        public string StoreJSON(Msg msg)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            if (msg.Type == MsgType.Email)
            {
                Email email = (Email)msg;
                string jsonEmail = JsonSerializer.Serialize<Email>(email, options);
                File.WriteAllText(msg.Type.ToString() + msg.MsgID + ".json", jsonEmail);
                return jsonEmail;
            }
            else if (msg.Type == MsgType.SMS)
            {
                SMS sms = (SMS)msg;
                string jsonEmail = JsonSerializer.Serialize<SMS>(sms, options);
                File.WriteAllText(msg.Type.ToString() + msg.MsgID + ".json", jsonEmail);
                return jsonEmail;
            }
            else
            {
                Tweet tweet = (Tweet)msg;
                string jsonEmail = JsonSerializer.Serialize<Tweet>(tweet, options);
                File.WriteAllText(msg.Type.ToString() + msg.MsgID + ".json", jsonEmail);
                return jsonEmail;
            }
        }

        public string DisplayJSON(string id)
        {
            MsgHandler msg = new MsgHandler();
            if (msg.Type == MsgType.Email)
            {
                return File.ReadAllText(id + ".json");
            }
            else if (msg.Type == MsgType.SMS)
            {
                return File.ReadAllText(id + ".json");
            }
            else
            {
                return File.ReadAllText(id + ".json");
            }
        }
    }
}