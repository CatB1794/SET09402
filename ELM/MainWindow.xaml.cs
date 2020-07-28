using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ELM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string msgID = msgHeader.Text;
            string msgType = msgID.Substring(0, 1);
            string bodyMsg = msgBody.Text;

            if (msgID.Length < 10 || msgID.Length > 10 || msgID == "")
            {
                MessageBox.Show("Message Header must be 10 character long and cannot be empty.");
            }
            else if (!msgType.Contains("E") && !msgType.Contains("e") && !msgType.Contains("S") && !msgType.Contains("s") && !msgType.Contains("T") && !msgType.Contains("t"))
            {
                MessageBox.Show("Message Header must begin with either E, S or T.");
            }
            else if (!int.TryParse(msgID.Substring(1, 9), out int result))
            {
                MessageBox.Show("Message Header must have E, S or T followed by 9 numbers.");
            }
            else
            {
                if (msgType == "E" || msgType == "e")
                {
                    int index = -1;
                    int nlCount = 0;
                    while ((index = bodyMsg.IndexOf(Environment.NewLine, index + 1)) != -1)
                    {
                        nlCount++;
                    }
                    if (nlCount < 2)
                    {
                        MessageBox.Show("Email must have a sender address, subject line and message separated by a new line.");
                        if (!bodyMsg.Contains("@"))
                        {
                            MessageBox.Show("Email must have an email address.");
                        }
                    }
                }
                if (msgType == "S" || msgType == "s")
                {
                    if(!bodyMsg.Contains("\n"))
                    {
                        MessageBox.Show("SMS must have a sender number and message separated by a new line.");
                        if (!bodyMsg.StartsWith("+"))
                        {
                            MessageBox.Show("SMS must have an international number beginning with +.");
                        }
                    }
                    /*string sms = MsgHandler.ProcessSMS(msgID, bodyMsg);
                    msgOutput.Text = sms;
                    JSONFormatter.StoreJSON(msgID, sms);*/
                }
                if (msgType == "T" || msgType == "t")
                {
                    if (!bodyMsg.Contains("\n"))
                    {
                        MessageBox.Show("Tweet must have a Twitter ID and tweet separated by a new line.");
                        if (!bodyMsg.Contains("@"))
                        {
                            MessageBox.Show("Tweet must have a Twitter IDbeginning with @.");
                        }
                    }
                }
            }
        }
    }
}