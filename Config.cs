using System.Configuration;

namespace PingCondition
{
    internal static class Config
    {
        public static string Host
        {
            get
            {
                return ConfigurationManager.AppSettings["Host"];
            }
        }
        public static string Command
        {
            get
            {
                return ConfigurationManager.AppSettings["CmdCommand"];
            }
        }
        public static int Delay
        {
            get
            {
                if (int.TryParse(ConfigurationManager.AppSettings["DelayS"], out int _))
                {
                    return int.Parse(ConfigurationManager.AppSettings["DelayS"]); // all is ok (delay is number)
                }
                else
                {
                    System.Console.WriteLine("[ERROR] Can't parse the given delay to int");
                    return -1;
                }
            }
        }
    }
}
