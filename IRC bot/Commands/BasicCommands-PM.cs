using System;
using Meebey.SmartIrc4net;
using System.Net.Sockets;

namespace IRC_Bot
{
    class BasicCommands_PM : Bot
    {
        public static void irc_OnQueryMessage(object sender, IrcEventArgs e)
        {
            switch (e.Data.MessageArray[0])
            {
                case "!calc":
                    try
                    {
                        double num1, num2;
                        string strnum1, strnum2;
                        string oper;
                        double final;
                        strnum1 = e.Data.MessageArray[1];
                        oper = e.Data.MessageArray[2];
                        strnum2 = e.Data.MessageArray[3];

                        // irc.SendMessage(SendType.Message, e.Data.Nick, strnum1 + " " + oper + " " + strnum2 + "=" + final );
                        num1 = Convert.ToDouble(strnum1);
                        num2 = Convert.ToDouble(strnum2);
                        if (oper == "+")
                        {
                            final = num1 + num2;
                            irc.SendMessage(SendType.Message, e.Data.Nick,
                                            "10" + num1 + "" + "14" + oper + "" + "10" + num2 + "14" + "=" + "10" +
                                            final);
                        }
                        if (oper == "-")
                        {
                            final = num1 - num2;
                            irc.SendMessage(SendType.Message, e.Data.Nick,
                                            "10" + num1 + "" + "14" + oper + "" + "10" + num2 + "14" + "=" + "10" +
                                            final);
                        }


                        if (oper == "*")
                        {
                            final = num1*num2;
                            irc.SendMessage(SendType.Message, e.Data.Nick,
                                            "10" + num1 + "" + "14" + oper + "" + "10" + num2 + "14" + "=" + "10" +
                                            final);
                        }

                        if (oper == "/")
                        {
                            final = num1/num2;
                            irc.SendMessage(SendType.Message, e.Data.Nick,
                                            "10" + num1 + "" + "14" + oper + "" + "10" + num2 + "14" + "=" + "10" +
                                            final);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Error: incorrect syntax, command: !calc");
                    }
                    break;

                case "!realmlist":
                    irc.SendMessage(SendType.Message, e.Data.Nick, "customize command message");
                                   
                    break;

                case "!commands":

                    irc.SendMessage(SendType.Message, e.Data.Nick,
                                    "Current commands: !realmlist, !patches, !commands, !calc, !botinfo");
                    break;

                case "!patches":
                    irc.SendMessage(SendType.Message, e.Data.Nick,
                                    "Patches can be found at wowwiki.com/Patch_Mirrors. Be sure to download the right one for your client locale (ex. enUS, enGB, esES etc.)");

                    break;

                
                case "!botinfo":
                    irc.SendMessage(SendType.Notice, e.Data.Nick,
                                    "ircbot4wowps r" + Revision.bot_revision);
                    
                    break;
            }
        }
    }
}