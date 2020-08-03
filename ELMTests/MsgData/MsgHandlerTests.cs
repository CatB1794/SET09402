using Microsoft.VisualStudio.TestTools.UnitTesting;
using ELM.MsgData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace ELM.MsgData.Tests
{
    [TestClass()]
    public class MsgHandlerTests
    {
        MsgHandler msgHandler = new MsgHandler();
        [TestMethod()]
        [ExpectedException(typeof(Exception), "Email must have an email address.")]
        public void ProcessEmailValidAddressExceptionTest()
        {
            Email input = new Email();
            string msgBody = "no address here";
            var emailActual = msgHandler.ProcessEmail(input, msgBody);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), "Email must have a sender address, subject line and message separated by a new line.")]
        public void ProcessEmailValidNewLineExceptionTest()
        {
            Email input = new Email();
            string nl = Environment.NewLine;
            string address = "40338726@live.napier.ac.uk";
            string noBody = "no subject line or body";
            string msgBody = String.Format("{1}{0}{2}", nl, address, noBody);
            var emailActual = msgHandler.ProcessEmail(input, msgBody);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), "Incident reports need a centre code and a nature of incident report label.")]
        public void ProcessEmailValidSIRNewLineExceptionTest()
        {
            Email input = new Email();
            string nl = Environment.NewLine;
            string address = "EustonLeisure@SportCentres.org";
            string sbjLine = "SIR 01/08/2020";
            string msgBody = String.Format("{1}{0}{2}", nl, address, sbjLine);
            var emailActual = msgHandler.ProcessEmail(input, msgBody);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), "Subject line has a maximum of 20 characters.")]
        public void ProcessEmailValidSbjLineLngExceptionTest()
        {
            Email input = new Email();
            string nl = Environment.NewLine;
            string address = "test@test.org";
            string sbjLine = "123456789101112131415";
            string msgBody = String.Format("{1}{0}{2}{0}Testing sbj line", nl, address, sbjLine);
            var emailActual = msgHandler.ProcessEmail(input, msgBody);
        }

        [TestMethod()]
        public void ProcessStandardEmailValidTest()
        {
            string address = "40338726@live.napier.ac.uk";
            string sbjLine = "SET-09102";
            string emBody = "Software Engineering Coursework.";
            
            Email input = new Email();
            string nl = Environment.NewLine;
            string strMsg = String.Format("{1}{0}{2}{0}{3}", nl, address, sbjLine, emBody);
            var actualEmail = msgHandler.ProcessEmail(input, strMsg);

            Assert.AreEqual(address, actualEmail.Address);
            Assert.AreEqual(sbjLine, actualEmail.SbjLine);
            Assert.AreEqual(emBody + Environment.NewLine, actualEmail.EmailBody);
            Assert.AreEqual(EmailType.Standard, actualEmail.EmailType);
            Assert.AreEqual(null, actualEmail.NoI);
            Assert.AreEqual(0, actualEmail.URL.Count);
            Assert.AreEqual(0, actualEmail.MsgID);
            Assert.AreEqual(MsgType.Email, actualEmail.Type);
        }

        [TestMethod()]
        public void ProcessSIREmailValidTest()
        {
            string nl = Environment.NewLine;
            string address = "EustonLeisure@SportCentres.org";
            string sbjLine = "SIR 01/08/20";
            string centreCode = "31-728-90";
            string sir = "Suspicious Incident";
            string emBody = String.Format("{1}{0}{2}{0}Incident occurred out of hours.", nl, centreCode, sir);

            Email input = new Email();
            string strMsg = String.Format("{1}{0}{2}{0}{3}", nl, address, sbjLine, emBody);
            var actualEmail = msgHandler.ProcessEmail(input, strMsg);

            Assert.AreEqual(address, actualEmail.Address);
            Assert.AreEqual(sbjLine, actualEmail.SbjLine);
            Assert.AreEqual(emBody + Environment.NewLine, actualEmail.EmailBody);
            Assert.AreEqual(EmailType.SIR, actualEmail.EmailType);
            Assert.AreEqual(centreCode, actualEmail.NoI.CentreCode);
            Assert.AreEqual(NatureOfIncident.SuspiciousIncident, actualEmail.NoI.Type);
            Assert.AreEqual(0, actualEmail.URL.Count);
            Assert.AreEqual(0, actualEmail.MsgID);
            Assert.AreEqual(MsgType.Email, actualEmail.Type);
        }

        [TestMethod()]
        public void ProcessEmailURLTest()
        {
            string address = "40338726@live.napier.ac.uk";
            string sbjLine = "SET-09102";
            string emBody = "Software Engineering Coursework, https://moodle.napier.ac.uk/";
            string urlQuar = "Software Engineering Coursework, <URL Quarantined>";

            Email input = new Email();
            string nl = Environment.NewLine;
            string strMsg = String.Format("{1}{0}{2}{0}{3}", nl, address, sbjLine, emBody);
            var actualEmail = msgHandler.ProcessEmail(input, strMsg);

            Assert.AreEqual(address, actualEmail.Address);
            Assert.AreEqual(sbjLine, actualEmail.SbjLine);
            Assert.AreEqual(urlQuar + Environment.NewLine, actualEmail.EmailBody);
            Assert.AreEqual(EmailType.Standard, actualEmail.EmailType);
            Assert.AreEqual(null, actualEmail.NoI);
            Assert.AreEqual(1, actualEmail.URL.Count);
            Assert.AreEqual(0, actualEmail.MsgID);
            Assert.AreEqual(MsgType.Email, actualEmail.Type);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), "SMS must have a sender number and message separated by a new line.")]
        public void ProcessSMSValidNewLineExceptionTest()
        {
            SMS input = new SMS();
            string msgBody = "+09327849072 sup";
            var smsActual = msgHandler.ProcessSMS(input, msgBody);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), "SMS must have an international number beginning with +.")]
        public void ProcessSMSValidNumberExceptionTest()
        {
            SMS input = new SMS();
            string nl = Environment.NewLine;
            string msgBody = String.Format("09327849072{0}sup", nl);
            var smsActual = msgHandler.ProcessSMS(input, msgBody);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), "Not a valid international number.")]
        public void ProcessSMSValidInternationalNumberMinExceptionTest()
        {
            SMS input = new SMS();
            string nl = Environment.NewLine;
            string msgBody = String.Format("+093278{0}sup", nl);
            var smsActual = msgHandler.ProcessSMS(input, msgBody);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), "Not a valid international number.")]
        public void ProcessSMSValidInternationalNumberExceptionTest()
        {
            SMS input = new SMS();
            string nl = Environment.NewLine;
            string msgBody = String.Format("+0192xc8374653{0}sup", nl);
            var smsActual = msgHandler.ProcessSMS(input, msgBody);
        }

        [TestMethod()]
        public void ProcessSMSValidTest()
        {
            string intNum = "+0192837465";
            string setNum = "0192837465";
            string smsBody = "That was fun, but you have to ask yourself: WWJD";
            string formatSMS = "That was fun, but you have to ask yourself: WWJD < What would Jesus do? >  ";

            SMS input = new SMS();
            string nl = Environment.NewLine;
            string strMsg = String.Format("{1}{0}{2}", nl, intNum, smsBody);
            var actualSMS = msgHandler.ProcessSMS(input, strMsg);

            Assert.AreEqual(setNum, actualSMS.InternationalNumber);
            Assert.AreEqual(formatSMS, actualSMS.SMSBody);
            Assert.AreEqual(MsgType.SMS, actualSMS.Type);
            Assert.AreEqual(0, actualSMS.MsgID);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), "Tweet must have a Twitter ID and tweet separated by a new line.")]
        public void ProcessTweetValidNewLineExceptionTest()
        {
            Tweet input = new Tweet();
            string msgBody = "@test test";
            var twtActual = msgHandler.ProcessTweet(input, msgBody);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), "Tweet must have a Twitter ID beginning with @.")]
        public void ProcessTweetValidTwitterIDExceptionTest()
        {
            Tweet input = new Tweet();
            string nl = Environment.NewLine;
            string msgBody = String.Format("test{0}test", nl);
            var twtActual = msgHandler.ProcessTweet(input, msgBody);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception), "Not a valid Twitter ID.")]
        public void ProcessTweetValidTwitterIDLngExceptionTest()
        {
            Tweet input = new Tweet();
            string nl = Environment.NewLine;
            string msgBody = String.Format("@testtesttesttest{0}test", nl);
            var twtActual = msgHandler.ProcessTweet(input, msgBody);
        }

        [TestMethod()]
        public void ProcessTweetValidTest()
        {
            string twtID = "@NapierUni";
            string twtBody = "@NapierStudent welcome to Napier! #NewInTake CUA";
            string formatTwt = "@NapierStudent welcome to Napier! #NewInTake CUA < See you around >  ";

            Tweet input = new Tweet();
            string nl = Environment.NewLine;
            string strMsg = String.Format("{1}{0}{2}", nl, twtID, twtBody);
            var actualTweet = msgHandler.ProcessTweet(input, strMsg);

            Assert.AreEqual(twtID, actualTweet.TwitterID);
            Assert.AreEqual(formatTwt, actualTweet.TweetBody);
            Assert.AreEqual(1, actualTweet.Mentions.Count);
            Assert.AreEqual(1, actualTweet.Hashtags.Count);
            Assert.AreEqual(MsgType.Tweet, actualTweet.Type);
            Assert.AreEqual(0, actualTweet.MsgID);
        }
    }
}