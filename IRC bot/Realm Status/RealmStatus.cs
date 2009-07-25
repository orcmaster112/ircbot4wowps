﻿using System;
using System.Net.Sockets;
using System.Threading;
using System.Diagnostics;
using Meebey.SmartIrc4net;

namespace IRC_Bot
{
    public class RealmStatus : Bot
    {
        private string _servername;
        private string _serverIP;
        private int _port;
        public string old_RealmStatus = "";
        public bool First = true;
        public Timer timer;
        //Stopwatch sw = new Stopwatch();

        public RealmStatus(string servername, string serverIP, int port)
        {
            _servername = servername;
            _serverIP = serverIP;
            _port = port;
            StatusCheck();
        }


        public RealmStatus(string servername, string serverIP)
        {
            _servername = servername;
            _serverIP = serverIP;
            _port = 3724;
            StatusCheck();
        }

        private void StatusCheck()
        {
            TimerCallback cb = RealmCheckTimer;
            timer = new Timer(cb, null, 7000, 7000);
            Console.WriteLine("Checks starting for " + _servername + ":" + _port);
        }


        private void RealmCheckTimer(object obj)
        {
            string new_RealmStatus = IsOnline();
            if (new_RealmStatus != old_RealmStatus)
            {
                old_RealmStatus = new_RealmStatus;
                if (!First)
                {
                    Channel channel = irc.GetChannel(settings.channel);
                    foreach (ChannelUser cuser in channel.Users.Values)
                    {
                        
                        irc.SendMessage(SendType.Notice, cuser.Nick, _servername + " is now " + new_RealmStatus + "!");
                    }

                    Console.WriteLine(_servername + " is now " + new_RealmStatus + "!");
                    Thread.Sleep(500);
                }
                else
                {
                    First = false;
                }
            }
        }

        private string IsOnline()
        {
            TcpClient tcpChecker = new TcpClient();
            try
            {
                tcpChecker.Connect(_serverIP, _port);

                tcpChecker.Close();
                return "Online";
            }
            catch
            {
                tcpChecker.Close();
                return "Offline";
            }
        }
    }
}