using System.Net.Sockets;

namespace IRC_Bot
{
    public class ServerStatus
    {
        public static string GetStatus(string ip, int port)
        {
            TcpClient tcpCheck = new TcpClient();
            string status;
            try
            {
                tcpCheck.Connect(ip, port);
                status = "online!";
                tcpCheck.Close();
            }
            catch
            {
                tcpCheck.Close();
                status = "offline!";
            }
            return status;
        }

    }
}
