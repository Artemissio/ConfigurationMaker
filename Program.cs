using System;
using System.IO;

namespace ConfigurationMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            string root = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            string path = root + @"\Data\TestConfig.config";

            //var n = new ConfigModel("New Url", "New Point", "New Key");

            //ObjectToConfig<ConfigModel>.WriteToFile(path, n);

            //var test = ObjectToConfig<ConfigModel>.ReadFromFile(path);       

            Console.ReadKey();
        }
    }
}