using System;
using System.Xml;
using System.Xml.XPath;
using System.Net;

namespace IRC_Bot
{
    class AscentStats : Bot
    {
        public static string _location;
        public AscentStats(string location)
        {
            _location = location;
        }
        
        
        
        
        //#region factions
        public string GetHorde()
        {
           WebClient wc = new WebClient();
            wc.DownloadFile(_location, "stats.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(Settings.dir + "//stats.xml");
            XmlNode node = doc.SelectSingleNode("/serverpage/status/horde");
            string horde = node.Value;
            return horde;

        }

        /*public int GetAlliance()
        {

        }
        #endregion
        public int GetTotalPlayers()
        {

        }

        #region server
        public string GetMemoryUsage()
        {

        }

        public string GetCPU()
        {

        }


        public string GetLatency()
        {

        }


        #endregion

        #region races
        public int GetHuman()
        {

        }

        public int GetDwarf()
        {

        }

        public int GetNightElf()
        {

        }

        public int GetGnome()
        {

        }

        public int GetOrc()
        {

        }

        public int GetUndead()
        {

        }

        public int GetTauren()
        {

        }

        public int GetTroll()
        {

        }

        public int GetBloodElf()
        {

        }

        public int GetDraenei()
        {

        }

        #endregion

        #region classes
        public int GetWarrior()
        {

        }

        public int GetHunter()
        {

        }

        public int GetPriest()
        {

        }

        public int GetMage()
        {

        }

        public int GetWarlock()
        {

        }

        public int GetPaladin()
        {

        }

        public int GetRogue()
        {

        }

        public int GetShaman()
        {

        }

        public int GetDruid()
        {

        }

        public int GetDeathKnight()
        {

        }
        #endregion
        public int GetGMCount()
        {

        }
        */

    }
}