using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace ELM.MsgData
{
    public class MsgHandler
    {
        public MsgHandler ProcessData(string msgID, string msgBody)
        {
            string msgType = msgID.Substring(0,1);
            int id = int.Parse(msgID.Substring(1,9));

            MsgType type = MsgTypes.FromStr(msgType);
            switch (type)
            {
                case MsgType.Email:
                    //return ProcessEmail(new Email() { MsgID = id }, msgBody);
                    throw new NotImplementedException();
                case MsgType.SMS:
                    throw new NotImplementedException();
                case MsgType.Tweet:
                    throw new NotImplementedException();
                default:
                    throw new Exception();
            }
        }

        public Email ProcessEmail(Email input, string content)
        {
            string[] msg = content.Split("\n");
            input.Address = msg[0];
            input.SbjLine = msg[1];
            if (msg[1].Length > 20)
            {
                MessageBox.Show("Subject line has a maximum of 20 characters.");
            }

            StringBuilder msgTxt = new StringBuilder();
            for(int i = 2; i < msg.Length; i++)
            {
                msgTxt.AppendLine(msg[i]);
            }
            input.EmailBody = msgTxt.ToString();
            if (msgTxt.Length > 1028)
            {
                MessageBox.Show("Email has a maximum of 1028 characters.");
            }

            if (!input.SbjLine.Contains("SIR"))
            {
                input.EmailType = EmailType.Standard;
                input.NoI = null;
                return input;
            }
            else
            {
                input.EmailType = EmailType.SIR;
                input.NoI = new SIR();
            }
            return input;
        }
    }
}
