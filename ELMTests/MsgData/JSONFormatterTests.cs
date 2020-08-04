using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Web;

namespace ELM.MsgData.Tests
{
    [TestClass()]
    public class JSONFormatterTests
    {
        public MsgHandler msgHandler = new MsgHandler();
        public JSONFormatter jSONFormatter = new JSONFormatter();
        [TestMethod()]
        public void StoreJSONEmailTest()
        {
            string nl = Environment.NewLine;
            string strMsg = String.Format("40338726@live.napier.ac.uk{0}SET-09102{0}Software Engineering Coursework.", nl);
            Msg msg = msgHandler.ProcessData("E019283746", strMsg);
            string jsonActual = jSONFormatter.StoreJSON(msg);
            string jsonExpected = String.Format("{{\"Address\":\"40338726@live.napier.ac.uk\",\"SbjLine\":\"SET-09102\",\"EmailType\":0,\"NoI\":null,\"EmailBody\":\"Software Engineering Coursework.{0}\",\"URL\":[],\"Type\":0,\"MsgID\":19283746}}", HttpUtility.JavaScriptStringEncode(nl));
            Assert.AreEqual(jsonExpected, jsonActual);
        }

        [TestMethod()]
        public void StoreJSONSMSTest()
        {
            string nl = Environment.NewLine;
            string strMsg = String.Format("+0192837465{0}SET09102 Software Engineering Coursework.", nl);
            Msg msg = msgHandler.ProcessData("S193283547", strMsg);
            string jsonActual = jSONFormatter.StoreJSON(msg);
            string jsonExpected = "{\"InternationalNumber\":\"0192837465\",\"SMSBody\":\"SET09102 Software Engineering Coursework.  \",\"Type\":1,\"MsgID\":193283547}";
            Assert.AreEqual(jsonExpected, jsonActual);
        }

        [TestMethod()]
        public void StoreJSONTweetTest()
        {
            string nl = Environment.NewLine;
            string strMsg = String.Format("@40338726{0}SET09102 Coursework @SoftwareEngineeringCourse #Y3.", nl);
            Msg msg = msgHandler.ProcessData("T829173645", strMsg);
            string jsonActual = jSONFormatter.StoreJSON(msg);
            string jsonExpected = "{\"TwitterID\":\"@40338726\",\"TweetBody\":\"SET09102 Coursework @SoftwareEngineeringCourse #Y3.  \",\"Mentions\":[\"@SoftwareEngineeringCourse\"],\"Hashtags\":[\"#Y3.\"],\"Type\":2,\"MsgID\":829173645}";
            Assert.AreEqual(jsonExpected, jsonActual);
        }
    }
}