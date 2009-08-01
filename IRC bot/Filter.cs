using System;
using Meebey.SmartIrc4net;
using MySql.Data.MySqlClient;



namespace IRC_Bot
{
     class Filter : Bot
    {
        public static int kicks;
       
        public static int GetKicks(string nick, string channel, string server)
        {
            try
            {
                
                string query =
                    string.Format("SELECT kicks from filter WHERE nick = '{0}' AND channel = '{1}' AND server = '{2}'",
                                  nick, channel, server);
               
                MySqlCommand cmd =
                    new MySqlCommand(query, msqlconn);
                        
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    kicks = reader.GetInt32("kicks");
                }
                reader.Close();
            }

            catch (MySqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return kicks;
        }


        public static void banned_word_check(object sender, IrcEventArgs e)
        {
         
            if (badwords.Contains(e.Data.Message)) 
            {
                if (InDB(e.Data.Nick, settings.serverlist, e.Data.Channel) == false)
                {
                   int i = rng.Next(kickreasons.Length + 1);
                    InsertEntry(e.Data.Nick, settings.serverlist, e.Data.Channel);
                    irc.RfcKick(e.Data.Channel, e.Data.Nick, kickreasons[i]);
                }

                else
                {
                    if (GetKicks(e.Data.Nick, e.Data.Channel, settings.serverlist) >= 5) //ban nick if kicked 5 times. 
                    {
                       
                        irc.Ban(e.Data.Channel, e.Data.Host);
                        irc.RfcKick(e.Data.Channel, e.Data.Nick, "banned");
                    }
                    else
                    {
                        int i = rng.Next(kickreasons.Length + 1);
                       irc.RfcKick(e.Data.Channel, e.Data.Nick, kickreasons[i]);
                        UpdateKicks(e.Data.Nick, settings.serverlist, e.Data.Channel);
                    }

                }
            }

          }


        public static bool InDB(string nick, string server, string channel) //checks if the nick exists for a specific channel and server.
        {
            bool result = false;
            string query = string.Format("SELECT * FROM filter WHERE nick='{0}' AND server='{1}' AND channel='{2}' LIMIT 1", nick, server, channel);
            MySqlCommand cmd = new MySqlCommand(query, msqlconn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (!string.IsNullOrEmpty(reader.GetString("nick")))
                {
                    result = true;
                }
            }
            reader.Dispose();
            return result;
        }

         public static void InsertEntry(string nick, string server, string channel) //adds a DB entry for a nick on a specific channel and server if not found.
         {
             string values = string.Format("('{0}', '{1}', '{2}', 1)", server, channel, nick);
             MySqlCommand cmd = new MySqlCommand("INSERT INTO filter (server, channel, nick, kicks) VALUES " + values, msqlconn );
             cmd.ExecuteNonQuery();
             
         }

         public static void UpdateKicks(string nick, string server, string channel) //use whenever a nick is kicked, adds 1 kick to the "kicks" field in the table.
         {
             int newkicks = GetKicks(nick, channel, server) + 1;
             string query =
                 string.Format(
                     "UPDATE filter SET kicks= " + newkicks + " WHERE server = '{0}' AND nick = '{1}' AND channel = '{2}' ",
                     server, nick, channel);
             MySqlCommand cmd = new MySqlCommand(query, msqlconn);
             cmd.ExecuteNonQuery();
             
         }
    }
}