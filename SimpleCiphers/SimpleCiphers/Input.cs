using System;
using System.Text.RegularExpressions;

namespace SimpleCiphers
{
    public static class Input
    {
        private static bool ValidateBase64(string input)
        {
            return Regex.IsMatch(input, @"^(?=(.{4})*$)[a-zA-Z0-9+/]*={0,2}$");
        }

        public static string GetPlaintext()
        {
            Console.WriteLine("Please enter your plaintext:");
            return Console.ReadLine();
        }

        public static byte? GetShift()
        {
            Console.Write("Please enter the shift amount: ");

            int shift;
            while (true)
            {
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) return null;
                if (int.TryParse(input, out shift) && shift != 0) break;

                Console.Write("The shift should be a non-zero integer value ");
                Console.Write($" {-Cipher.Modulus} < n < {Cipher.Modulus}. ");
                Console.Write("Please try again or leave empty to cancel: ");
            }

            shift %= Cipher.Modulus;
            if (shift < 0) shift += Cipher.Modulus;

            return (byte)shift;
        }

        public static string GetKey()
        {
            Console.WriteLine("Please enter your key:");
            return Console.ReadLine();
        }

        public static string GetBase64Ciphertext()
        {
            Console.WriteLine("Please enter your base64 encoded ciphertext:");

            while (true)
            {
                string ciphertext = Console.ReadLine();
                if (ValidateBase64(ciphertext)) return ciphertext;
                Console.WriteLine(
                    "The entered string does not appear to be valid base64.");
                Console.WriteLine(
                    "Please try again or leave empty to cancel:");
            }
        }
    }
}