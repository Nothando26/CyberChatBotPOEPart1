using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;
using System.Media;


namespace CyberChatBotPOEPart1
{
    //inherits attributes from chatbot
    class CyberChatbot : ChatBot
    {
        public override void Run()
        {
            try
            {
                AudioManager.PlayGreeting();
                AsciiArt.DisplayRobot();
                AsciiArt.DisplayLogo();
                WelcomeBanner.Show();

                Console.Write("\nWhat is your name: ");
                string userName = Console.ReadLine();
                GreetUser(userName);
                ShowMenu(userName);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[!] Unexpected error: {ex.Message}");
                Console.ResetColor();
            }
        }
        //displays the personalised greeting
        public override void GreetUser(string userName)
        {
            Console.WriteLine($"\nWelcome, {userName}! I'm your Cybersecurity Awareness Bot.\n");
        }

        private void ShowMenu(string userName)
        {
            Console.WriteLine($"\n{userName}, how can I help you today? Ask me anything about cybersecurity!");
            Console.WriteLine("Type 'exit' to quit.\n");

            //the while loop keeps the chatbot running
            // Continuous chatbot loop until user types "exit"
            while (true)
            {
                Console.Write("You: ");
                string input = Console.ReadLine()?.ToLower();

                // Skip empty input
                if (string.IsNullOrWhiteSpace(input)) continue;

                // Friendly bot response
                if (input.Contains("how are you"))
                {
                    Console.WriteLine("Bot: I’m great! How may I assist you?");
                }
                // Explain chatbot's purpose
                else if (input.Contains("purpose"))
                {
                    Console.WriteLine("Bot: My purpose is to educate and guide users about cybersecurity—covering topics like password safety, phishing awareness, and safe browsing.");
                    Console.Write("Bot: Would you like tips on a specific topic? (password/phishing/browsing): ");
                    string topic = Console.ReadLine()?.ToLower();
                    // Offers tips based on follow-up
                    if (topic == "password") Console.WriteLine("Bot: Use a mix of letters, numbers, and special characters. Avoid reuse!");
                    else if (topic == "phishing") Console.WriteLine("Bot: Always double-check email links and sender addresses. Don’t click on suspicious stuff!");
                    else if (topic == "browsing") Console.WriteLine("Bot: Stick to HTTPS websites and keep your browser updated.");
                    else Console.WriteLine("Bot: No worries! Ask me about anything else.");
                }
                // Password safety info
                else if (input.Contains("password"))
                {
                    Console.WriteLine("Bot: A strong password includes upper/lowercase letters, numbers, and symbols. Avoid personal info!");
                    Console.Write("Bot: Want to know about password managers? (yes/no): ");
                    string followUp = Console.ReadLine()?.ToLower();
                    if (followUp == "yes")
                        Console.WriteLine("Bot: Password managers help you generate and store complex passwords securely. Try apps like Bitwarden or 1Password.");
                }
                // Phishing explanation
                else if (input.Contains("phishing"))
                {
                    Console.WriteLine("Bot: Phishing is a scam where attackers trick you into giving up personal info. Usually through fake emails or texts.");
                    Console.Write("Bot: Curious how to spot phishing emails? (yes/no): ");
                    string response = Console.ReadLine()?.ToLower();
                    if (response == "yes")
                        Console.WriteLine("Bot: Watch out for poor grammar, urgent requests, or mismatched email addresses!");
                }
                // Safe browsing guidance
                else if (input.Contains("safe browsing"))
                {
                    Console.WriteLine("Bot: Practice safe browsing by avoiding sketchy websites, using ad blockers, and checking for HTTPS.");
                    Console.Write("Bot: Want browser extension recommendations? (yes/no): ");
                    string response = Console.ReadLine()?.ToLower();
                    if (response == "yes")
                        Console.WriteLine("Bot: Try uBlock Origin for ads and HTTPS Everywhere for secure connections!");
                }
                // General cybersecurity tip
                else if (input.Contains("tip"))
                {
                    Console.WriteLine("Bot: Here’s a top tip—enable two-factor authentication (2FA) on all accounts. It adds an extra layer of security.");
                    Console.Write("Bot: Would you like to hear more safety tips? (yes/no): ");
                    string reply = Console.ReadLine()?.ToLower();
                    if (reply == "yes")
                    {
                        Console.WriteLine("Bot: Keep your software up to date, avoid public Wi-Fi without a VPN, and back up your data regularly!");
                    }
                }
                // Exit command
                else if (input == "exit")
                {
                    Console.WriteLine("Bot: Goodbye! Stay alert and stay safe out there!");
                    break;
                }
                // Unrecognized input
                else
                {
                    Console.WriteLine("Bot: I didn’t quite get that. You can ask about topics like passwords, phishing, or tips.");
                }
            }
        }
    }
}