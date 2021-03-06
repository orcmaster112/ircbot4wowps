﻿using System;
using Meebey.SmartIrc4net;

namespace IRC_Bot
{
    class CtcpCommands : Bot
    {
        public static void irc_OnCtcpRequest(object sender, CtcpEventArgs e)
        {
            switch (e.CtcpCommand)
            {
                case "VERSION":
                    irc.SendMessage(SendType.CtcpReply, e.Data.Nick,
                                    "\rVERSION" + " Bot version: r" + Revision.bot_revision + "-trunk");
				                                                
                    break;

                case "TIME":
                    irc.SendMessage(SendType.CtcpReply, e.Data.Nick, "\rtime \r" + DateTime.Now);
                    break;
            }
        }
    }
}