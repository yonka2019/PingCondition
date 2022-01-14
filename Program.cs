using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.NetworkInformation;

internal class Program
{
    private static readonly string host = ConfigurationManager.AppSettings["Host"];
    private static void Main()
    {
        Ping ping = new Ping();
        PingReply reply;
        if (host == null)
        {
            Console.WriteLine("[ERROR] Can't find configuration file");

            Console.WriteLine("Press any button...");
            Console.ReadKey();

            Environment.Exit(0);
        }
        Console.WriteLine($"[HOST] |{host}|: Pinging...");

        while (true)
        {
            try
            {
                reply = ping.Send(host);
            }
            catch
            {
                Shutdown();
                return;
            }
            if (reply.Status != IPStatus.Success)
            {
                Shutdown();
            }
        }
    }
    private static void Shutdown()
    {
        Console.WriteLine($"[ERROR] |{host}|: Shutting down...");
        Process.Start("shutdown", "/s /t 0");  // Shutdowns PC
    }
}