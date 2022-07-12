

// Enigma Machine

// Keyboard - 26 keys A-Z

// Plug Board - 26 letters (Possible 13 swaps) with 10 swaps made

// Rotors - 3 Rotors, each with 26 connections A-Z. 
//      Rotors 1 rotates 1-26, when rotor 1 completes it's rotation it pushes 2nd rotor 1 notch. 
//      This continues down the line, 2nd completing a rotation pushing the 3rd 1 notch, "scrambling" a message"

// Plug Board Again - Second pass through plug board so letters are swapped once more. 
namespace Enigma
{
    class Program
    {
        // Did it. Solved. Devs around the globe tremble. Bow to me.
        // ToUpper()
        static void Main(string[] args)
        {
            // Total possible permutations for 26x26x26 rotors
            const int PossiblePermutations = 17576;
            // Keep running with same settings or start new settings
            bool NewSettingsProfile = false;
            // Real Enigma Rotor Configurations:
            List<string> RotorConfigsList = new List<string>()
            {
                // 1924 Rotor II-C, Commercial Enigma 
                "HQZGPJTMOBLNCIFDYAWVEUSRKX",
                // February 1939 Rotor III-K, Swiss K
                "EHRVXGAOBQUSIMZFLYNWKTPDJC",
                // February 7th, 1941 Rotor I, German Railway
                "JGDQOXUSCAMIFRVTPNEWKBLZYH",
                // Spring 1942, Rotor Gamma, M4 R2
                "FSOKANUERHMBTIYCWLQPZXVGJD",
                // December 1942 Rotor VII, M3 M4 Naval
                "NZJHGRCXMYSWBOUFAIVLPEKQDT"
            };
            // Real Enigma Reflector Configurations: 
            List<string> ReflectorOut = new()
            {
                // Spring 1942, Reflector Gamma M4 R2
                "FSOKANUERHMBTIYCWLQPZXVGJD",
                // 1940, Reflector B Thin, M4 R1 (M3 + Thin)
                "ENKQAUYWJICOPBLMDXZVFTHRGS",
                // Date unknown, Reflector A
                "EJMZALYXVBWFCRQUONTSPIKHGD"
            };

        // Settings

            //Plugboard Settings: 
            Plugboard plugSettings = new Plugboard();
            plugSettings.SetPlugSettings();

            //Rotor Settings: 
            // Create 3 rotors, user to choose from 5
            Rotor rotor1 = new Rotor();
            Rotor rotor2 = new Rotor();
            Rotor rotor3 = new Rotor();
            // Set rotorID for determining order 1-3
            Rotor.RotorMenu();
            rotor1.ChooseRotor();
            rotor2.ChooseRotor();
            rotor3.ChooseRotor();
            // Set rotor connections
            rotor1.SetRotorConnections(RotorConfigsList[rotor1.RotorID - 1]);
            rotor2.SetRotorConnections(RotorConfigsList[rotor2.RotorID - 1]);
            rotor3.SetRotorConnections(RotorConfigsList[rotor3.RotorID - 1]);
            // Get/Set Rotor Indices
            rotor1.GetRotorIndex();
            rotor1.SetRotorIndex();
            rotor2.GetRotorIndex();
            rotor2.SetRotorIndex();
            rotor3.GetRotorIndex();
            rotor3.SetRotorIndex();
            // Reflector settings
            // Create 1 reflector, choose from 3
            Reflector reflector = new Reflector();
            // Set reflectorID for determining which reflector to use
            Reflector.ReflectorMenu();
            reflector.ChooseReflector();
            // Set Connections for reflector
            reflector.SetReflectorConnections(ReflectorOut[reflector.ReflectorID - 1]);

        // Encryption

            // Get message to be encrypted via keyboard
            Keyboard messageToDecode = new Keyboard();
            char[] message = messageToDecode.GetMessageToDecode();
            // Plug encryption
            message = plugSettings.PlugEncrypt(message);
            // Scramble messages with rotors
            for (int i = 0; i < message.Length; i++)
            {
                message[i] = rotor1.RotorEncrypt(message[i]);
                rotor1.RotorIndex++;
                // Rotate every 1 key press
                if (rotor1.RotorIndex == PossiblePermutations / PossiblePermutations)
                {
                    rotor1.RotateRotor();
                    rotor1.RotorIndex = 0;
                }
            }
            for (int i = 0; i < message.Length; i++)
            {
                message[i] = rotor2.RotorEncrypt(message[i]);
                rotor2.RotorIndex++;
                // Rotate every time R1 rotates
                if (rotor2.RotorIndex == PossiblePermutations / 676)
                {
                    rotor2.RotateRotor();
                    rotor2.RotorIndex = 0;
                }
            }
            for (int i = 0; i < message.Length; i++)
            {
                message[i] = rotor3.RotorEncrypt(message[i]);
                rotor3.RotorIndex++;
                // Rotate everytime R2 rotates
                if (rotor3.RotorIndex == PossiblePermutations / 26)
                {
                    rotor3.RotateRotor();
                    rotor3.RotorIndex = 0;
                }
            }
            // Reflect i.e. swap key/value pairs and run again
            for (int i = 0; i < message.Length; i++)
            {
                message[i] = reflector.ReflectorEncrypt(message[i]);
            }
            // Scramble message with rotors again
            for (int i = 0; i < message.Length; i++)
            {
                message[i] = rotor3.RotorEncrypt(message[i]);
                rotor3.RotorIndex++;
                // Rotate everytime R2 rotates
                if (rotor3.RotorIndex == PossiblePermutations / 26)
                {
                    rotor3.RotateRotor();
                    rotor3.RotorIndex = 0;
                }
            }
            for (int i = 0; i < message.Length; i++)
            {
                message[i] = rotor2.RotorEncrypt(message[i]);
                rotor2.RotorIndex++;
                // Rotate every time R1 rotates
                if (rotor2.RotorIndex == PossiblePermutations / 676)
                {
                    rotor2.RotateRotor();
                    rotor2.RotorIndex = 0;
                }
            }
            for (int i = 0; i < message.Length; i++)
            {
                message[i] = rotor1.RotorEncrypt(message[i]);
                rotor1.RotorIndex++;
                // Rotate every 1 key press
                if (rotor1.RotorIndex == PossiblePermutations / PossiblePermutations)
                {
                    rotor1.RotateRotor();
                    rotor1.RotorIndex = 0;
                }
            }
            // Back through plugs
            message = plugSettings.PlugEncrypt(message);
            // Print Encryption
            Keyboard.PrintEncryptedMessage(message);
        }
    }
}