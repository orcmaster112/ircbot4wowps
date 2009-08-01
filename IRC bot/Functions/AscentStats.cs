using System.Xml;

namespace IRC_Bot
{
    class AscentStats
    {
        
        public static XmlDocument doc = new XmlDocument();
        public static string _location;
        public AscentStats(string location)
        {
            doc.Load(location);
            _location = location;
        }
        
        public string GetValue(string xpath) 
        {
            /*
             * function to make grabbing info from the stats page easier.
             * Uses XPath to select nodes. Also reduces the amount of code required.
             */
            doc.Load(_location); // make sure stats page is up to date?
            XmlNode node = doc.SelectSingleNode(xpath);
            string val = node.InnerText;
            return val;
        }
        
        
        #region factions
        public string GetHorde()
        {
           return GetValue("/serverpage/status/horde");
           
        }

        public string GetAlliance()
        {
            return GetValue("/serverpage/status/alliance");
        }
        #endregion
        public string GetTotalPlayers()
        {
            return GetValue("/serverpage/status/oplayers");
        }

        #region server
        public string GetMemoryUsage()
        {
            return GetValue("/serverpage/status/ram");
        }

        public string GetCPU()
        {
            return GetValue("/serverpage/status/cpu") + "%";
        }


        public string GetLatency()
        {
            return GetValue("/serverpage/status/avglat") + "ms";
                
        }


        #endregion

        #region races
        public string GetHuman()
        {
            return GetValue("/serverpage/statsummary/human");
        }

        public string GetDwarf()
        {
            return GetValue("/serverpage/statsummary/dwarf");
        }

        public string GetNightElf()
        {
            return GetValue("/serverpage/statsummary/nightelf");
        }

        public string GetGnome()
        {
            return GetValue("/serverpage/statsummary/gnome");
        }
        
        public string GetOrc()
        {
            return GetValue("/serverpage/statsummary/orc");
        }

        public string GetUndead()
        {
            return GetValue("/serverpage/statsummary/undead");
        }

        public string GetTauren()
        {
            return GetValue("/serverpage/statsummary/tauren");
        }

        public string GetTroll()
        {
            return GetValue("/serverpage/statsummary/troll");
        }

        public string GetBloodElf()
        {
            return GetValue("/serverpage/statsummary/bloodelf");
        }

        public string GetDraenei()
        {
            return GetValue("/serverpage/statsummary/draenei");
        }

        #endregion

        #region classes
        public string GetWarrior()
        {
            return GetValue("/serverpage/statsummary/warrior");
        }

        public string GetHunter()
        {
            return GetValue("/serverpage/statsummary/hunter");
        }

        public string GetPriest()
        {
            return GetValue("/serverpage/statsummary/priest");
        }

        public string GetMage()
        {
            return GetValue("/serverpage/statsummary/mage");
        }

        public string GetWarlock()
        {
            return GetValue("/serverpage/statsummary/warlock");
        }

        public string GetPaladin()
        {
            return GetValue("/serverpage/statsummary/paladin");
        }

        public string GetRogue()
        {
            return GetValue("/serverpage/statsummary/rogue");
        }

        public string GetShaman()
        {
            return GetValue("/serverpage/statsummary/shaman");
        }

        public string GetDruid()
        {
            return GetValue("/serverpage/statsummary/druid");
        }

        public string GetDeathKnight()
        {
            return GetValue("/serverpage/statsummary/deathknight");
        }
        #endregion
        public string GetGMCount()
        {
            return GetValue("/serverpage/status/gmcount");
        }
        

    }
}