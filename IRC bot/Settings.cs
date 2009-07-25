using Nini.Config;

namespace IRC_Bot
{
    public class Settings
    {
        private IConfigSource source = null;
        public static string dir = System.IO.Directory.GetCurrentDirectory();

        public Settings()
        {
            source = new IniConfigSource(dir + "//settings.ini");
        }

        #region IRC Settings

        public string user
        {
            get { return source.Configs["userinfo"].Get("nick", ""); }
        }

        public string realname
        {
            get { return source.Configs["userinfo"].Get("realname", ""); }
        }
        
        public string serverlist
        {
            get { return source.Configs["connection"].Get("server", ""); }
        }

        
        public string channel
        {
            get { return source.Configs["userinfo"].Get("channel", ""); }
        }


        public string ident
        {
            get { return source.Configs["userinfo"].Get("ident", "bot"); }
        }

        public int port
        {
            get
            {
                return source.Configs["connection"].GetInt("port", 6667);
            }
        }

        #endregion

        #region MySQL

        public int sqlenabled
        {
            get { return source.Configs["mysql"].GetInt("sqlenabled", 0); }
        }

        public string sqlserver
        {
            get { return source.Configs["mysql"].Get("server", "localhost"); }
        }

        public string sqluser
        {
            get { return source.Configs["mysql"].Get("user", "root"); }
        }

        public string sqlpass
        {
            get { return source.Configs["mysql"].Get("pass", ""); }
        }

        #endregion
    }
}