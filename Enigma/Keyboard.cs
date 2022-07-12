using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Enigma
{
    // This is simply going to Console.Read stream from keyboard and return that char to run 
    // through enigma machine. 
    // Should probably take input as char array in order to go one character at a time through
    // input that way each character can be converted and then placed back in same char array.
    internal class Keyboard
    {
        private static readonly Regex reg = new Regex("[^a-zA-Z]");
        public char[] GetMessageToDecode()
        {
            // I need to filter out everything that isn't alpha 
            Console.WriteLine("Enter the text to encrypt: ");
            string toEncrypt = Console.ReadLine().ToUpper();
            toEncrypt = reg.Replace(toEncrypt, String.Empty);
            return toEncrypt.ToArray();
        }
        public static void PrintEncryptedMessage(char[] message)
        {
            int count = 0;
            for (int i = 0; i < message.Length; i++)
            {
                Console.Write(message[i]);
                count++;
                if (count % 5 == 0)
                    Console.Write(" ");
                if (count % 15 == 0)
                    Console.Write('\n');
            }
            Console.Write('\n');
        }
    }
}
