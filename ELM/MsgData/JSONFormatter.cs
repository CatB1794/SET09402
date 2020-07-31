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
        public string StoreJSON(Msg msg)
        {
            if (msg.Type == MsgType.Email)
            {
                Email email = (Email)msg;
                string jsonEmail = JsonSerializer.Serialize<Email>(email);
                File.WriteAllText(msg.Type.ToString() + msg.MsgID + ".json", jsonEmail);
                return jsonEmail;
            }
            else if (msg.Type == MsgType.SMS)
            {
                SMS sms = (SMS)msg;
                string jsonEmail = JsonSerializer.Serialize<SMS>(sms);
                File.WriteAllText(msg.Type.ToString() + msg.MsgID + ".json", jsonEmail);
                return jsonEmail;
            }
            else
            {
                Tweet tweet = (Tweet)msg;
                string jsonEmail = JsonSerializer.Serialize<Tweet>(tweet);
                File.WriteAllText(msg.Type.ToString() + msg.MsgID + ".json", jsonEmail);
                return jsonEmail;
            }
        }
    }
}