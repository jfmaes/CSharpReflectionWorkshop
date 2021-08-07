using System;
using System.Reflection;


namespace Raditz
{
    class Program
    {
        static void Reflect(string FilePath)
        {
            Assembly dotNetProgram = Assembly.LoadFile(FilePath);
            Object[] parameters = new String[] { null };
            dotNetProgram.EntryPoint.Invoke(null, parameters);
        }
        static void Main(string[] args)
        {
            Reflect(@"C:\Users\jarvis\source\repos\HelloReflectionWorld\bin\Release\HelloReflectionWorld.exe");
            Console.ReadKey();
        }
    }
}
