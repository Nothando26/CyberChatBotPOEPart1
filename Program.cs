using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.IO;

namespace CyberChatBotPOEPart1
{
    internal class Program
    {
        class CyberChatbot
        {
            static void Main()
            {
                // 1. Play welcome sound
                PlayVoiceGreeting();

                // 2. Show robot art
                DisplayAsciiRobot();

                // 3. Show title/logo
                DisplayAsciiLogo();

                // 4. Show welcome banner
                ShowTextWelcomeBanner();

                // 5. Ask for user name and greet them
                Console.Write("\nWhat is your name: ");
                string userName = Console.ReadLine();
                Console.WriteLine($"\nWelcome, {userName}! I'm your Cybersecurity Awareness Bot.\n");

                // 6. Start chatbot menu
                ShowMenu(userName);
            }

            static void PlayVoiceGreeting()
            {
                try
                {
                    // Set file name for sound
                    string fileName = "greeting.wav";
                    string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                    // Check if file exists
                    if (!File.Exists(fullPath))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\n[!] Audio file not found at: {fullPath}");
                        Console.ResetColor();
                        return;
                    }

                    // Play sound
                    using (SoundPlayer player = new SoundPlayer(fullPath))
                    {
                        Console.WriteLine("[*] Playing voice greeting...");
                        player.PlaySync();
                    }
                }
                catch (Exception ex)
                {
                    // Show error if sound doesn't work
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n[!] Error playing audio: " + ex.Message);
                    Console.ResetColor();
                }
            }

            static void DisplayAsciiRobot()
            {
                // Show robot picture made with text
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(@"
           ______
         /|_||_\`.__
        (   _    _ _\
        =`-(_)--(_)-'
          .-'````'-.   
         /  .--.  \ \ 
        |  |o_o | | |
        |  |:_/ | | |
       //\/    \/\ \\
      (|   |    |   |)
     /'\_/'\__/'\_/'\
     \___)|===(___/
        |_|   |_|
      _/((   ))\_
     \__/\___/\__/
");
                Console.ResetColor();
            }

            static void DisplayAsciiLogo()
            {
                // Show logo made with text
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(@"
   ______      __                                        _ __           ___                                                   ____        __ 
  / ____/_  __/ /_  ___  _____________  _______  _______(_) /___  __   /   |_      ______ _________  ____  ___  __________   / __ )____  / /_
 / /   / / / / __ \/ _ \/ ___/ ___/ _ \/ ___/ / / / ___/ / __/ / / /  / /| | | /| / / __ `/ ___/ _ \/ __ \/ _ \/ ___/ ___/  / __  / __ \/ __/
/ /___/ /_/ / /_/ /  __/ /  (__  )  __/ /__/ /_/ / /  / / /_/ /_/ /  / ___ | |/ |/ / /_/ / /  /  __/ / / /  __(__  |__  )  / /_/ / /_/ / /_  
\____/\__, /_.___/\___/_/  /____/\___/\___/\__,_/_/  /_/\__/\__, /  /_/  |_|__/|__/\__,_/_/   \___/_/ /_/\___/____/____/  /_____/\____/\__/  
     /____/                                                /____/                                                                            

                CYBERSECURITY AWARENESS BOT
                ");
                Console.ResetColor();
            }

            static void ShowTextWelcomeBanner()
            {
                // Show welcome banner
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(@"
************************************************************
*                                                          *
*       WELCOME TO THE CYBERSECURITY AWARENESS CHATBOT     *
*                                                          *
*       Learn. Understand. Stay Safe Online.               *
************************************************************");
                Console.ResetColor();
            }

            // This is the chatbot menu where user can ask questions
            static void ShowMenu(string userName)
            {
                Console.WriteLine($"\n{userName}, how can I help you today? Ask me anything about cybersecurity!");
                Console.WriteLine("Type 'exit' to quit.\n");

                while (true)
                {
                    Console.Write("You: ");
                    string input = Console.ReadLine().ToLower();

                    if (input.Contains("how are you"))
                    {
                        // Friendly response
                        Console.WriteLine("Bot: I’m great!, and how are you today?");
                    }
                    else if (input.Contains("purpose"))
                    {
                        // Explains bot's job
                        Console.WriteLine("Bot: My purpose is to educate and guide users about cybersecurity—covering topics like password safety, phishing awareness, safe browsing practices, and general tips to protect your digital life.");
                    }
                    else if (input.Contains("password"))
                    {
                        // Password advice
                        Console.WriteLine("Bot: A strong password includes a mix of uppercase and lowercase letters, numbers, and special characters. Avoid using easily guessed info like birthdays or names, and never reuse the same password across different websites. Consider using a password manager to keep track of them securely.");
                    }
                    else if (input.Contains("phishing"))
                    {
                        // Explains phishing
                        Console.WriteLine("Bot: Phishing is a type of online scam where attackers impersonate trustworthy entities—like banks or services—to trick you into giving up sensitive info. They often use fake emails, texts, or websites. Always double-check links and sender addresses, and never enter personal info on suspicious pages.");
                    }
                    else if (input.Contains("safe browsing"))
                    {
                        // Safe browsing tips
                        Console.WriteLine("Bot: Safe browsing means practicing good habits like only visiting trusted websites, checking for HTTPS in the URL, avoiding pop-up ads and strange downloads, and keeping your browser and antivirus software up to date. It’s your digital hygiene!");
                    }
                    else if (input.Contains("tip"))
                    {
                        // Bonus tip
                        Console.WriteLine("Bot: Here’s a tip: Always enable two-factor authentication (2FA) on your accounts. It adds an extra layer of protection by requiring a second verification step, such as a code sent to your phone or an app confirmation, even if someone steals your password.");
                    }
                    else if (input == "exit")
                    {
                        // Exit message
                        Console.WriteLine("Bot: Goodbye! Remember, cybersecurity is a shared responsibility. Stay alert and stay safe!");
                        return;
                    }
                    else
                    {
                        // If input is not recognized
                        Console.WriteLine("Bot: Hmm, I didn’t quite get that. Try asking about things like 'password safety', 'phishing', or 'safe browsing'.");
                    }
                }
            }
        }
    }
}
