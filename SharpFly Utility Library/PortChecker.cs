using System.Net;
using System.Net.NetworkInformation;

namespace SharpFly_Utility_Library
{
    public static class PortChecker
    {
        public static bool IsPortAvailable(int port)
        {
            bool inUse = false;
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] tcpConnInfoArray = ipGlobalProperties.GetActiveTcpListeners();
            foreach (IPEndPoint endpoint in tcpConnInfoArray)
                if (endpoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            return inUse;
        }
    }
}
