using PingCondition;
using System;
using System.Diagnostics;
using System.Net.NetworkInformation;

internal class Program
{
    private static readonly string host = Config.Host;
    private static readonly string command = Config.Command;
    private static readonly int delay = Config.Delay;
    private static void Main()
    {
        Ping ping = new Ping();
        PingReply reply = null;

        if (host == null)
        {
            Console.WriteLine("[" + DateTime.Now + "] [ERROR] Can't find configuration file");

            Console.WriteLine("Press any button...");
            Console.ReadKey();

            Environment.Exit(0);
        }

        while (true)
        {
            try
            {
                Console.WriteLine($"[{DateTime.Now}] [HOST] ({host}): Ping...");
                reply = ping.Send(host);
            }
            catch
            {
                ExecuteCMD(command);
                return;
            }
            finally
            {
                if (reply.Status != IPStatus.Success)
                {
                    ExecuteCMD(command);
                }
            }
            System.Threading.Thread.Sleep(delay * 1000); // s * 1000 = ms 
        }
    }
    private static void ExecuteCMD(string command)
    {
        Console.WriteLine($"[{DateTime.Now}] [ERROR] ({host}): Executing cmd command..");

        ProcessStartInfo cmd = new ProcessStartInfo("CMD.exe")
        {
            UseShellExecute = true,
            Verb = "runas",
            Arguments = command
        };
        Process.Start(cmd);
    }
}