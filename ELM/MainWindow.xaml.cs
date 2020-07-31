﻿using System;
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
using ELM.MsgData;

namespace ELM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MsgHandler MsgHandler;
        public JSONFormatter JSONFormatter;

        public MainWindow()
        {
            InitializeComponent();
            MsgHandler = new MsgHandler();
            JSONFormatter = new JSONFormatter();
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
            else if (!msgType.Contains("E") && !msgType.Contains("S") && !msgType.Contains("T"))
            {
                MessageBox.Show("Message Header must begin with either E, S or T.");
            }
            else if (!int.TryParse(msgID.Substring(1, 9), out int result))
            {
                MessageBox.Show("Message Header must have E, S or T followed by 9 numbers.");
            }
            else
            {
                try
                {
                    Msg msg = MsgHandler.ProcessData(msgID, bodyMsg);
                    string jsonData = JSONFormatter.StoreJSON(msg);
                    msgOutput.Text = jsonData;
                    if (msg.Type == MsgType.Tweet)
                    {
                        trendingMentions.Text = MsgHandler.TwitterList(jsonData);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}