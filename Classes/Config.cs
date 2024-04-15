using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamerCheck.Classes
{
    static public class Config
    {
        static public string[] configValues = File.ReadAllLines("./configuration.cfg");

        static public void CheckConfigFile()
        {

            if (!configValues.Any())
            {
                configValues = new string[2];

                System.Console.WriteLine("Provide client id: ");
                configValues[0] = Console.ReadLine() ?? "";

                System.Console.WriteLine("Provide client secret: ");
                configValues[1] = Console.ReadLine() ?? "";

                File.WriteAllLines("./configuration.cfg", configValues);
            }
        }

        static public void Yeet()
        {
            foreach (string file in Directory.GetFiles("./txt"))
            {
                //File.WriteAllText(Path.GetFullPath(file), "");
                File.WriteAllText(@$"./{file}", "");
            }
        }
    }
}