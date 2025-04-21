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
                // 1. Voice Greeting
                PlayVoiceGreeting();

                // 2. ASCII Logo
                DisplayAsciiLogo();

                // 3. Text Welcome Banner
                ShowTextWelcomeBanner();

                // 4. User Name Input and Greeting
                Console.Write("\nWhat is your name: ");
                string userName = Console.ReadLine();
                Console.WriteLine($"\nWelcome, {userName}! I'm your Cybersecurity Awareness Bot.\n");

                // 5. Menu
                ShowMenu(userName);



            }

            static void PlayVoiceGreeting()
            {
                try
                {
                    string fileName = "greeting.wav";
                    string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                    if (!File.Exists(fullPath))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"\n[!] Audio file not found at: {fullPath}");
                        Console.ResetColor();
                        return;
                    }

                    using (SoundPlayer player = new SoundPlayer(fullPath))
                    {
                        Console.WriteLine("[*] Playing voice greeting...");
                        player.PlaySync();
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n[!] Error playing audio: " + ex.Message);
                    Console.ResetColor();
                }
            }


            static void DisplayAsciiLogo()
            {
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
                        Console.WriteLine("Bot: I’m just code, but I’m functioning perfectly!");
                    }
                    else if (input.Contains("purpose"))
                    {
                        Console.WriteLine("Bot: I help you learn about cybersecurity and how to stay safe online.");
                    }
                    else if (input.Contains("password"))
                    {
                        Console.WriteLine("Bot: Always use a mix of letters, numbers, and symbols. Avoid using the same password across sites.");
                    }
                    else if (input.Contains("phishing"))
                    {
                        Console.WriteLine("Bot: Phishing is when scammers try to trick you into giving personal info. Don’t click on suspicious links!");
                    }
                    else if (input.Contains("safe browsing"))
                    {
                        Console.WriteLine("Bot: Make sure websites use HTTPS, avoid downloading unknown files, and keep your browser updated.");
                    }
                    else if (input.Contains("tip"))
                    {
                        Console.WriteLine("Bot: Enable two-factor authentication wherever possible for added security.");
                    }
                    else if (input == "exit")
                    {
                        Console.WriteLine("Bot: Goodbye! Stay safe online.");
                        return;
                    }
                    else
                    {
                        Console.WriteLine("Bot: Sorry, I didn’t understand that. Could you rephrase?");
                    }
                }
            }
        }
    }
}