﻿using System;
using System.Collections.Concurrent;
using System.IO;
using System.Windows;
using ELM.MsgData;

namespace ELM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MsgHandler msgHandler;
        public JSONFormatter jSONFormatter;
        ConcurrentDictionary<string, int>  trendingList;
        ConcurrentDictionary<string, int> natureOfIncidentList;

        public MainWindow()
        {
            InitializeComponent();
            msgHandler = new MsgHandler();
            jSONFormatter = new JSONFormatter();
            trendingList = new ConcurrentDictionary<string, int>();
            natureOfIncidentList = new ConcurrentDictionary<string, int>();
        }

        private void BtnMsgInput(object sender, RoutedEventArgs e)
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
            else if (!int.TryParse(msgID.Substring(1, 9), out _))
            {
                MessageBox.Show("Message Header must have E, S or T followed by 9 numbers.");
            }
            else
            {
                try
                {
                    Msg msg = msgHandler.ProcessData(msgID, bodyMsg);
                    string jsonData = jSONFormatter.StoreJSON(msg);
                    msgOutput.Text = jsonData;
                    File.WriteAllText(msg.Type.ToString() + msg.MsgID + ".txt", msgOutput.Text);
                    if (msg.Type == MsgType.Email)
                    {
                        string[] noiList = bodyMsg.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                        if (noiList[1].StartsWith("SIR"))
                        {
                            string listNoI = noiList[2] + "-" + noiList[3];
                            natureOfIncidentList.AddOrUpdate(listNoI.Trim(), 1, (noi, count) => count + 1);
                        }
                        string noiTxt = "";
                        foreach ((string noi, int count) in natureOfIncidentList)
                        {
                            noiTxt += noi + " " + count + Environment.NewLine;
                        }
                        natureOfIncident.Text = noiTxt;

                    }
                    if (msg.Type == MsgType.Tweet)
                    {
                        string[] mntHT = bodyMsg.Split(new string[] { " ", Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                        for (int i = 1; i < mntHT.Length; i++)
                        {
                            if (mntHT[i].StartsWith("@"))
                            {
                                mentions.Text += mntHT[i].Trim() + "; ";
                            }
                            if (mntHT[i].StartsWith("#"))
                            {
                                trendingList.AddOrUpdate(mntHT[i].Trim(), 1, (hashtag, count) => count + 1);
                            }
                        }
                        string trendingTxt = "";
                        foreach ((string hash, int count) in trendingList)
                        {
                            trendingTxt += hash + " " + count + "; ";
                        }
                        trending.Text = trendingTxt;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BtnMsgSelect(object sender, RoutedEventArgs e)
        {
            msgBody.Clear();
            try
            {
                string msgID = msgHeader.Text;
                msgOutput.Text = jSONFormatter.DisplayJSON(msgID);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}