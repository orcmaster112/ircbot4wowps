using Meebey.SmartIrc4net;

namespace IRC_Bot
{
    class Filter_NoSQL : Bot
    {
        

        public static void banned_word_check(object sender, IrcEventArgs e)
        {
            
            if (badwords.Contains(e.Data.Message))
            {
                int i = rng.Next(kickreasons.Length + 1);
                irc.RfcKick(e.Data.Channel, e.Data.Nick, kickreasons[i]);


            }


        }



    }
}