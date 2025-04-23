using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberChatBotPOEPart1
{
    static class AsciiArt
    {
        //method to display the ascii art
        public static void DisplayRobot()
        {
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

        public static void DisplayLogo()
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
    }

    // Class for banner
    static class WelcomeBanner
    {
        public static void Show()
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
    }
}