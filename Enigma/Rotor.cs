//using Enigma.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{
    // Maybe nested For loops to simulate 3 rotors, for int i < 26, increment connection
    // Another Key/Value dictionary in order to map 1-26 to A-Z, this time we can increment 
    // by 1 each time, until == 26, then increment next rotor by 1 and set initial rotor = 0
    // Nested For loop actually seems like really good way to handle this only issue is saving 
    // i, j, k values because they continue for all messages until settings are reset the next day. 

    // Ok question for the day, does it matter that my dictionary is not ordered
    // this could cause a problem for indexing all kvp by 1 to perform shift. 
    // Answer: it doesn't if I just keep track of a list alongside the dictionary
    internal class Rotor
    {
        // Used to set the rotor order 1-3, user pick from 5
        public int RotorID;
        // Front Side Rotor Orientation
        public const string Keys = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        // Index of Rotor 1-3
        public int RotorIndex = 0;
        // Index keys for increment in RotateRotor
        public List<char> RotorKeysIndex = new();
        // Index values for increment in RotateRotor
        public List<char> RotorValuesIndex = new();
        // char Key/Value pairs to map input for encryption
        public Dictionary<char, char> RotorConnectionsMap = new();

        public static void RotorMenu()
        {
            Console.WriteLine("Which rotors would you like?");
            Console.WriteLine("1. 1924 Rotor II-C, Commercial Enigma");
            Console.WriteLine("2. February 1939 Rotor III-K, Swiss K");
            Console.WriteLine("3. February 7th, 1941 Rotor I, German Railway");
            Console.WriteLine("4. Spring 1942, Rotor Gamma, M4 R2");
            Console.WriteLine("5. December 1942 Rotor VII, M3 M4 Naval");
        }
        public int ChooseRotor()
        {
            try
            {
                RotorID = int.Parse(Console.ReadLine());
                if (RotorID < 1 || RotorID > 5)
                {
                    Console.WriteLine("Please enter an integer 1-5: ");
                    ChooseRotor();
                }
            }
            catch
            {
                Console.WriteLine("Please enter an integer 1-5: ");
                ChooseRotor();
            }
            return RotorID;
        }
        // Setting a rotor's connections based on instanced RotorConnectionsMap
        // Also setting RotorValueIndex and RotorKeyIndex with this function
        public Dictionary<char, char> SetRotorConnections(string Values)
        {
            // Adding Key/Value pairs to RotorConnectionsMap, also adding ASCII
            // value to RotorValueIndex for increment later
            foreach (char key in Keys)
            {
                char val = Values.ToCharArray()[Values.IndexOf(key)];

                RotorConnectionsMap.Add(key, val);
                RotorValuesIndex.Add(val);
                RotorKeysIndex.Add(key);
            }
            return RotorConnectionsMap;
        }
        // Get index values to set rotors at from user
        public void GetRotorIndex()
        {
            Console.WriteLine("Rotor Index: Enter integer 1-26");
            try
            {
                RotorIndex = int.Parse(Console.ReadLine());
                if (RotorIndex < 1 || RotorIndex > 26)
                {
                    Console.WriteLine("Please enter an integer value 1-26: ");
                    GetRotorIndex();
                }
            }
            catch
            {
                Console.WriteLine("Please enter an integer value 1-26: ");
                GetRotorIndex();
            }
        }
        public void SetRotorIndex()
        {
            for (int i = 0; i < RotorIndex; i++)
                RotateRotor();
            RotorIndex = 0;
        }
        // Function to rotate rotors 1 notch 
        // IMPORTANT, SetRotorConnections must be called first because that is where
        // the lists used here are assigned.
        public Dictionary<char, char> RotateRotor()
        {
            // EDIT: After watching Computerphile video, keys do not shift, if you think about it
            // what would be the point of the rotating the entire rotor. Instead, it increments the 
            // the values side, which means it is now offset from a home key/value. We can call this
            // Offset: +1 to +26 whatever the rotation is. 
            // Shifting keys over 1 and putting last item first
            /*char tempKey = RotorKeysIndex.Last();
            RotorKeysIndex.RemoveAt(RotorKeysIndex.Count - 1);
            RotorKeysIndex.Insert(0, tempKey);
            // Assign new incremented keys to the connection map
            int i = 0;
            foreach (char c in RotorConnectionsMap.Keys)
            {
                RotorConnectionsMap[c] = RotorKeysIndex[i];
                i++;
            }
            */
            // Shifting values over 1 and putting last item first
            char tempVal = RotorValuesIndex.Last();
            RotorValuesIndex.RemoveAt(RotorValuesIndex.Count - 1);
            RotorValuesIndex.Insert(0, tempVal);
            // Assign new incremented values to the connection map
            int j = 0;
            foreach (char c in RotorConnectionsMap.Values)
            {
                RotorConnectionsMap[c] = RotorValuesIndex[j];
                j++;
            }

            return RotorConnectionsMap;
        }
        // Pass through rotor for encryption
        public char RotorEncrypt(char c)
        {
            c = RotorConnectionsMap.GetValueOrDefault(c);
            return c;
        }
    }
}
