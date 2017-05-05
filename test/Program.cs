using System;
using System.Reflection;
using NUnitLite;

namespace smartbox.SeaweedFs.Client.Test
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var typeInfo = typeof(Program).GetTypeInfo();
            var result = new AutoRun(typeInfo.Assembly).Execute(args);
            Console.ReadLine();
            return result;
        }

    }
}
