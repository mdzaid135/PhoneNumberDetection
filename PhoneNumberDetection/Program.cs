using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace PhoneNumberDetection
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Allow the user to choose the input source
            Console.WriteLine("Choose input source:");
            Console.WriteLine("1. Enter text manually");
            Console.WriteLine("2. Read from a file");
            Console.Write("Enter your choice: ");

            string inputText;
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter the text:");
                        inputText = Console.ReadLine();
                        break;
                    case 2:
                        Console.Write("Enter the file path: ");
                        string filePath = Console.ReadLine();
                        try
                        {
                            inputText = File.ReadAllText(filePath);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error reading file: {ex.Message}");
                            return;
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        return;
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
                return;
            }

            List<string> phoneNumbers = DetectPhoneNumbers(inputText);

            if (phoneNumbers.Count > 0)
            {
                Console.WriteLine("Detected phone numbers:");
                foreach (string phoneNumber in phoneNumbers)
                {
                    Console.WriteLine($"Phone number: {phoneNumber}");
                }
            }
            else
            {
                Console.WriteLine("No phone numbers detected.");
            }
        }

        public static List<string> DetectPhoneNumbers(string inputText)
        {
            List<string> phoneNumbers = new List<string>();

            // Regular expression pattern to match phone numbers
            string pattern = @"(?:(?:\+\d{1,3}[-\s]?)?(?:\(\d{1,4}\)\s?|\d{1,4}[-\s]?)?\d{2,3}[-\s]?\d{3,4}[-\s]?\d{4}|\((?:\d{1,4})\)\s?\d{3,4}[-\s]?\d{4})";


            // Match phone numbers in the input text
            MatchCollection matches = Regex.Matches(inputText, pattern);

            foreach (Match match in matches)
            {
                phoneNumbers.Add(match.Value);
            }

            // Regular expression pattern to match phone numbers in english or hindi alphabets
            pattern = @"\b(?:ONE|एक)\s(?:TWO|दो)\s(?:THREE|तीन)\s(?:FOUR|चार)\s(?:FIVE|पांच)\s(?:SIX|छह)\s(?:SEVEN|सात)\s(?:EIGHT|आठ)\s(?:NINE|नौ)\s(?:ZERO|शूɊ)\b";

            matches = Regex.Matches(inputText, pattern, RegexOptions.IgnoreCase);

            foreach (Match match in matches)
            {
                phoneNumbers.Add(ConvertToDigits(match.Value));
            }


            return phoneNumbers;
        }

        static string ConvertToDigits(string phoneNumber)
        {
            // Mapping of English and Hindi representations to digits
            Dictionary<string, string> digitMap = new Dictionary<string, string>
        {
            {"ONE", "1"}, {"एक", "1"},
            {"TWO", "2"}, {"दो", "2"},
            {"THREE", "3"}, {"तीन", "3"},
            {"FOUR", "4"}, {"चार", "4"},
            {"FIVE", "5"}, {"पांच", "5"},
            {"SIX", "6"}, {"छह", "6"},
            {"SEVEN", "7"}, {"सात", "7"},
            {"EIGHT", "8"}, {"आठ", "8"},
            {"NINE", "9"}, {"नौ", "9"},
            {"ZERO", "0"}, {"शूɊ", "0"} // Assuming शूɊ represents ZERO
        };

            // Replace English and Hindi representations with digits
            foreach (var entry in digitMap)
            {
                phoneNumber = phoneNumber.Replace(entry.Key, entry.Value);
            }

            return phoneNumber.Replace(" ", string.Empty).Trim();
        }
    }
}

