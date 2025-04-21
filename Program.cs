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
                    //Voice greeting code
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
                   