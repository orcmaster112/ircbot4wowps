﻿using System;
using System.Threading;
using Meebey.SmartIrc4net;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
/*
 * using IronPython;
using Microsoft.Scripting;
using Microsoft.Scripting.Hosting;
 */


namespace IRC_Bot
{
    public class Bot

    {
        public static Random rng = new Random(); // create instance of random number generator.
        public static Settings settings = new Settings();

        
        public static IrcClient irc = new IrcClient();
      

        private static string connstr = String.Format("server={0};user id ={1}; password={2}; database=bot_db",
                                                      settings.sqlserver, settings.sqluser, settings.sqlpass);

        public static MySqlConnection msqlconn = new MySqlConnection(connstr);
        public static List<string> badwords = new List<string>();
        
        public static string[] kickreasons;
        public static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main";
            Settings settings = new Settings();

            Console.WriteLine("ircbot4wowps r" + Revision.bot_revision);
            
            

            SQLCheck();
            LoadBadWords(); //load bad words from badwords.txt, each entry must be put on a separate line.
            LoadKickReasons(); //load kick reasons from kickreasons.txt

            // UTF-8 test
            irc.Encoding = System.Text.Encoding.UTF8;

            // wait time between messages, we can set this lower on own irc servers
            irc.SendDelay = 200;

            // we use channel sync, means we can use irc.GetChannel() and so on
            irc.ActiveChannelSyncing = true;
            irc.AutoNickHandling = true;
            irc.AutoJoinOnInvite = true;
            irc.AutoRejoinOnKick = true;
            irc.SupportNonRfc = true;
            


            // here we connect the events of the API to our written methods
            // most have own event handler types, because they ship different data
            irc.OnQueryMessage += new IrcEventHandler(Messages.OnQueryMessage);
            irc.OnError += new ErrorEventHandler(Messages.OnError);
            irc.OnChannelMessage += new IrcEventHandler(BasicCommands.OnChannelMsg);
            irc.OnRawMessage += new IrcEventHandler(Messages.OnRawMessage);
            if (settings.sqlenabled == 1)
            {
                irc.OnChannelMessage += new IrcEventHandler(Filter.banned_word_check); //use filter with SQL when MySQL is enabled in the configs. Has more features.
            }
            else
            {
                irc.OnChannelMessage += new IrcEventHandler(Filter_NoSQL.banned_word_check); //use filter without SQL, less functions.
                
            }
            irc.OnQueryMessage += new IrcEventHandler(BasicCommands_PM.irc_OnQueryMessage);
            irc.OnCtcpRequest += new CtcpEventHandler(CtcpCommands.irc_OnCtcpRequest);
            

            


            try
            {
                // here we try to connect to the server and exceptions get handled
                irc.Connect(settings.serverlist, settings.port);
            }
            catch (ConnectionException e)
            {
                // something went wrong, the reason will be shown
                Console.WriteLine("couldn't connect! Reason: " + e.Message);
                Exit();
            }


            try
            {
                // here we logon and register our nickname and so on 

                irc.Login(settings.user, settings.realname , 4, settings.ident);
               

                // join the channel
                irc.RfcJoin(settings.channel);


                /* Adding Realm Status Checking:  
                 RealmStatus test = new RealmStatus(" test ", "127.0.0.1", port);
                 *                           -OR-
                 * RealmStatus test = new RealmStatus(" test ", "127.0.0.1"); -- Default port is 3724
                 * Use two spaces in the server name like the example to prevent output from looking like this:
                 *Localhost - Testis now Offline!
                 *With spaces:
                 *Localhost - Test is now Offline!
                  */

               
                RealmStatus LocalhostChecker = new RealmStatus(" localhost-test ", "127.0.0.1");


                // spawn a new thread to read the stdin of the console, this we use
                // for reading IRC commands from the keyboard while the IRC connection
                // stays in its own thread
                new Thread(new ThreadStart(ReadCommands)).Start();

                // here we tell the IRC API to go into a receive mode, all events
                // will be triggered by _this_ thread (main thread in this case)
                // Listen() blocks by default, you can also use ListenOnce() if you
                // need that does one IRC operation and then returns, so you need then 
                // an own loop 
                irc.Listen();


                // when Listen() returns our IRC session is over, to be sure we call
                // disconnect manually
                irc.Disconnect();
            }
            catch (ConnectionException)
            {
                // this exception is handled because Disconnect() can throw a not
                // connected exception
                Exit();
            }
            catch (Exception e)
            {
                // this should not happen by just in case we handle it nicely
                Console.WriteLine("Error occurred! Message: " + e.Message);
                Console.WriteLine("Exception: " + e.StackTrace);
                //Exit();
            }
        }


        public static void ReadCommands()
        {
            // here we read the commands from the stdin and send it to the IRC API
            // WARNING, it uses WriteLine() means you need to enter RFC commands
            // like "JOIN #test" and then "PRIVMSG #test :hello to you"
            while (true)
            {
                irc.WriteLine(Console.ReadLine());
            }
        }

        public static void SQLCheck()
        {
            if (settings.sqlenabled == 1)
            {
                Console.WriteLine("MySQL enabled.");

                try
                {
                    msqlconn.Open();

                }

                catch(MySqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    Console.WriteLine("Exiting in 5s");
                    Thread.Sleep(5000);
                    Exit();
                }
                Console.WriteLine("Connected to MySQL.");
            }

            else // do nothing
            {
            }
        }

        public static void Exit()
        {
            // we are done, lets exit...
            Console.WriteLine("Exiting...");
            Environment.Exit(0);
        }

        public static void LoadBadWords()
        {
           string dir = System.IO.Directory.GetCurrentDirectory();
            string[] _badwords = System.IO.File.ReadAllLines(dir + "//badwords.txt");
           
           badwords.AddRange(_badwords);
            
            Console.WriteLine("Filter: Loaded " + badwords.Count + " bad word definitions");
            
            
        }

        public static void LoadKickReasons()
        {
            string dir = System.IO.Directory.GetCurrentDirectory();
            string[] _kickreasons = System.IO.File.ReadAllLines(dir + "//kickreasons.txt");
            kickreasons = _kickreasons;
            Console.WriteLine("Loaded " + kickreasons.Length + " kick reasons");
        }

    }
}