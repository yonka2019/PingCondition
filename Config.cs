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
                if (int.TryParse(ConfigurationManager.AppSettings["Delay"], out int _))
                {
                    return int.Parse(ConfigurationManager.AppSettings["Delay"]); // all is ok (delay is number)
                }
                else
                {
                    System.Console.WriteLine("[ERROR] Can't parse the given delay to int");
                    return -1;
                }
            }
        }
        public static int SleepCmdTime
        {
            get
            {
                if (int.TryParse(ConfigurationManager.AppSettings["SleepTimeCMD"], out int _))
                {
                    return int.Parse(ConfigurationManager.AppSettings["SleepTimeCMD"]); // all is ok (delay is number)
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
