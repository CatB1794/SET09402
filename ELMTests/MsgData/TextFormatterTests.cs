using Microsoft.VisualStudio.TestTools.UnitTesting;

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