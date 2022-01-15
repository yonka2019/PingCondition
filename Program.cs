using PingCondition;
using System;
using System.Diagnostics;
using System.Net.NetworkInformation;

internal class Program
{
    private static readonly string host = Config.Host;
    private static readonly string command = Config.Command;
    private static readonly int delay = Config.Delay;
    private static readonly int sleepTimeCmd = Config.SleepCmdTime;
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
                Console.Write($"[{DateTime.Now}] [HOST] ({host}): Ping...");
                reply = ping.Send(host);
            }
            catch
            {
                ExecuteCMD(command);
            }
            finally
            {
                if (reply.Status != IPStatus.Success)
                {
                    ExecuteCMD(command);
                }
                else if (reply.Status == IPStatus.Success)
                {
                    Console.WriteLine(" Success.");
                }
            }
            System.Threading.Thread.Sleep(delay * 1000); // s * 1000 = ms 
        }
    }
    private static void ExecuteCMD(string sCommands)
    {
        Console.WriteLine();
        string[] commands = sCommands.Split('|');
        foreach(string command in commands)
        {
            Console.WriteLine($"[{DateTime.Now}] [ERROR OCCURED] (CMD): {command}");

            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(command);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            Console.WriteLine(cmd.StandardOutput.ReadToEnd());
            cmd.Close();

            System.Threading.Thread.Sleep(sleepTimeCmd); // ms
        }
        Environment.Exit(1);
    }
}