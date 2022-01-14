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
            Console.WriteLine("[ERROR] Can't find configuration file");

            Console.WriteLine("Press any button...");
            Console.ReadKey();

            Environment.Exit(0);
        }
        Console.WriteLine($"[HOST] |{host}|: Ping...");

        while (true)
        {
            try
            {
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
            System.Threading.Thread.Sleep(delay * 1000);
        }
    }
    private static void ExecuteCMD(string command)
    {
        Process.Start("CMD.exe", command);
    }
}