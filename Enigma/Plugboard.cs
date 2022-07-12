using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{
    // I think implimenting a dictionary here to map key/value pairs will accomplish this effect. 
    // Keys are integers, and values are chars. 
    // In order to simulate the "swapping" of plugs we can change the value to match the swap so
    // for example, if we want to swap A and C the new value for Key A would be 3 and the new value
    // for Key C would be 1. All this accomplishes is changing the mapping for letters. Still have to figure
    // out how we want to take plugin positions.
    internal class Plugboard
    {
        Dictionary<char, char> PlugSettings = new();
        public Dictionary<char, char> SetPlugSettings()
        {
            Console.WriteLine("Enter the plugboard settings: ");
            // Iterate 20 times for 10 plugs, 6 that do not have plug and should be linked to themselves
            for (char c = 'A'; c <= 'T'; c++)
            {
                if (!PlugSettings.ContainsKey(c) && PlugSettings.Count < 26)
                {
                    char temp = GetPlugConnection(c);
                    PlugSettings.Add(c, temp);
                    PlugSettings.Add(temp, c);
                }
            }
            for (char c = 'A'; c <= 'Z'; c++)
            {
                if (!PlugSettings.ContainsKey(c))
                    PlugSettings.Add(c, c);
            }
            return PlugSettings;
        }
        public char GetPlugConnection(char Key)
        {
            Console.Write(Key + "= ");
            char _value;
            try
            {
                _value = char.Parse(Console.ReadLine().ToUpper());
                if (PlugSettings.ContainsKey(_value))
                {
                    Console.WriteLine("That value already has a plug. Try a different one: ");
                    _value = GetPlugConnection(Key);
                }
                else if ((int)_value < 65 || (int)_value > 90)
                {
                    Console.WriteLine("That is not a letter. Try entering a letter in the alphabet: ");
                    _value = GetPlugConnection(Key);
                }
                else if (_value == Key)
                {
                    Console.WriteLine("Cannot plug into itself! Try entering a different letter: ");
                    _value = GetPlugConnection(Key);
                }
                return _value;
            }
            catch
            {
                Console.WriteLine("Invalid input. Try again: ");
                _value = GetPlugConnection(Key);
            }
            return _value;
        }

        public char[] PlugEncrypt(char[] MessageToEncrypt)
        {
            for (int i = 0; i < MessageToEncrypt.Length; i++)
            {
                MessageToEncrypt[i] = PlugSettings.GetValueOrDefault(MessageToEncrypt[i]);
            }
            return MessageToEncrypt;
        }
    }
}
