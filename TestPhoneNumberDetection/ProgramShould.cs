using PhoneNumberDetection;
using System.Collections.Generic;
using Xunit;

namespace TestPhoneNumberDetection
{
    public class ProgramShould
    {
        [Fact]
        public void DetectPhoneNumbers_ShouldReturnCorrectPhoneNumbers()
        {
            string inputText = "10-digit numbers (e.g., 123-456-7890)";
            List<string> expectedPhoneNumbers = new List<string> { "123-456-7890" };
            List<string> actualPhoneNumbers = Program.DetectPhoneNumbers(inputText);
            Assert.Equal(expectedPhoneNumbers, actualPhoneNumbers);

            inputText = "Numbers with country codes (e.g., +1-123-456-7890, +91-1234567890)";
            expectedPhoneNumbers = new List<string> { "+1-123-456-7890", "+91-1234567890" };
            actualPhoneNumbers = Program.DetectPhoneNumbers(inputText);
            Assert.Equal(expectedPhoneNumbers, actualPhoneNumbers);

            inputText = "Numbers with parentheses for area codes (e.g., (91) 1234567890, (123) 456-7890)";
            expectedPhoneNumbers = new List<string> { "(91) 1234567890", "(123) 456-7890" };
            actualPhoneNumbers = Program.DetectPhoneNumbers(inputText);
            Assert.Equal(expectedPhoneNumbers, actualPhoneNumbers);

            inputText = "Numbers with spaces or dashes as separators (e.g., 123 456 7890, 123-456-7890, 91-123-456-7890, 91 123 456 7890, (91)1234567890, 01234567890 (without country code), 0123-4567890 (without country code), 1234567890";
            expectedPhoneNumbers = new List<string> { "123 456 7890", "123-456-7890", "91-123-456-7890", "91 123 456 7890", "(91)1234567890", "01234567890", "0123-4567890", "1234567890" };
            actualPhoneNumbers = Program.DetectPhoneNumbers(inputText);
            Assert.Equal(expectedPhoneNumbers, actualPhoneNumbers);

            inputText = "English: ONE TWO THREE FOUR FIVE SIX SEVEN EIGHT NINE ZERO";
            expectedPhoneNumbers = new List<string> { "1234567890" };
            actualPhoneNumbers = Program.DetectPhoneNumbers(inputText);
            Assert.Equal(expectedPhoneNumbers, actualPhoneNumbers);

            inputText = "Hindi: एक दो तीन चार पांच छह सात आठ नौ शूɊ";
            expectedPhoneNumbers = new List<string> { "1234567890" };
            actualPhoneNumbers = Program.DetectPhoneNumbers(inputText);
            Assert.Equal(expectedPhoneNumbers, actualPhoneNumbers);

            inputText = "Combination of English & Hindi: ONE दो तीन FOUR FIVE छह SEVEN EIGHT NINE शूɊ";
            expectedPhoneNumbers = new List<string> { "1234567890" };
            actualPhoneNumbers = Program.DetectPhoneNumbers(inputText);
            Assert.Equal(expectedPhoneNumbers, actualPhoneNumbers);
        }
    }
}
