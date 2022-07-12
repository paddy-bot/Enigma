using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{
    internal class Reflector
    {
        public int ReflectorID;
        public string ReflectorIn = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public Dictionary<char, char> ReflectorConnectionsMap = new();


        public static void ReflectorMenu()
        {
            Console.WriteLine("Which reflector would you like?");
            Console.WriteLine("1. Spring 1942 Reflector Gamma, M4 R2");
            Console.WriteLine("2. 1940 Reflector B Thin, M4 R1 (M3 + Thin)");
            Console.WriteLine("3. Date unknown, Reflector A");
        }
        public int ChooseReflector()
        {
            try
            {
                ReflectorID = int.Parse(Console.ReadLine());
                if (ReflectorID < 1 || ReflectorID > 3)
                {
                    Console.WriteLine("Please enter a single integer 1-3.");
                    ChooseReflector();
                }
            }
            catch
            {
                Console.WriteLine("Please enter a single integer 1-3.");
                ChooseReflector();
            }
            return ReflectorID;
        }
        public Dictionary<char, char> SetReflectorConnections(string Values)
        {
            foreach (char key in ReflectorIn)
            {
                char val = Values.ToCharArray()[Values.IndexOf(key)];
                ReflectorConnectionsMap.Add(key, val);
            }
            return ReflectorConnectionsMap;
        }
        public char ReflectorEncrypt(char c)
        {
            c = ReflectorConnectionsMap.GetValueOrDefault(c);
            return c;
        }
    }
}
