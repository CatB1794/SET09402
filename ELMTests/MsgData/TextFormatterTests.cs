using Microsoft.VisualStudio.TestTools.UnitTesting;
using ELM.MsgData;
using System;
using System.Collections.Generic;
using System.Text;

namespace ELM.MsgData.Tests
{
    [TestClass()]
    public class TextFormatterTests
    {
        TextFormatter textFormatter = new TextFormatter();
        [TestMethod()]
        public void FormatMsgTest()
        {
            string lol = "LOL";
            string expectedLol = "LOL < Laughing out loud > ";
            string actualLol = textFormatter.FormatMsg(lol);

            Assert.AreEqual(expectedLol, actualLol);
        }
    }
}