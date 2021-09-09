using System;
using System.ComponentModel;

namespace SimpleCiphers
{
    internal static class Program
    {
        private const string Separator = "---";

        private static void Main()
        {
            Console.WriteLine("Simple encryption program");

            while (true)
            {
                Console.WriteLine("c) caesar");
                Console.WriteLine("v) vigenere");
                Console.WriteLine("x) quit");
                Console.WriteLine(Separator);
                Console.Write("Your choice: ");
                string userChoice = Console.ReadLine()?.ToLower();

                switch (userChoice?.ToLower())
                {
                    case "c":
                    case "v":
                        Vigenere(userChoice == "c");
                        break;
                    case "x":
                        Exit();
                        return;
                    default:
                        Console.WriteLine("Invalid input. Please try again.");
                        break;
                }
            }
        }

        // Trivial signifies a Caesar cipher, as it's a Vigenere cipher with
        // a constant key. For instance, a shift of 97 equates to the key "a",
        // since the ASCII decimal value for 'a' is 97, and the key is looped.
        // The program uses UTF-8, but the same principle applies.
        private static void Vigenere(bool trivial = false)
        {
            // Convert the shift into a character, if dealing with Caesar.
            string key = trivial ? $"{(char?)Input.GetShift()}" : Input.GetKey();
            if (string.IsNullOrEmpty(key)) return;

            while (true)
                switch (GetAction())
                {
                    case Actions.Encrypt:
                        string message = Input.GetPlaintext();
                        if (string.IsNullOrEmpty(message)) continue;
                        Cipher.Encipher(message, key);
                        break;
                    case Actions.Decrypt:
                        string ciphertext = Input.GetBase64Ciphertext();
                        if (string.IsNullOrEmpty(ciphertext)) continue;
                        Cipher.Decipher(ciphertext, key);
                        break;
                    case Actions.Return:
                        return;
                    case Actions.Exit:
                        Exit();
                        break;
                    default:
                        throw new InvalidEnumArgumentException("Action not handled");
                }
        }

        private static Actions GetAction()
        {
            Console.WriteLine("Please choose your action:");
            Console.WriteLine($"{(char)Actions.Encrypt}) encrypt");
            Console.WriteLine($"{(char)Actions.Decrypt}) decyrpt");
            Console.WriteLine($"{(char)Actions.Return}) back to previous");
            Console.WriteLine($"{(char)Actions.Exit}) quit");
            Console.WriteLine(Separator);

            while (true)
            {
                Console.Write("Your choice: ");
                char? action = Console.ReadLine()?.ToLower()[0];
                if (action == (char)Actions.Exit) Exit();

                foreach (Actions value in Enum.GetValues(typeof(Actions)))
                    if (action == (char)value)
                        return value;

                Console.WriteLine("Invalid input. Please try again.");
            }
        }

        private static void Exit()
        {
            Console.WriteLine("Exiting the program. Bye bye.");
            Environment.Exit(0);
        }

        private enum Actions
        {
            Encrypt = 'e',
            Decrypt = 'd',
            Return = 'b',
            Exit = 'x'
        }
    }
}