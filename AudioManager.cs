using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace CyberChatBotPOEPart1
{
    static class AudioManager
    {
        public static void PlayGreeting()
        {
            try
            {
                string fileName = "greeting.wav";
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);

                //checks if file exists
                if (!File.Exists(path))
                    throw new FileNotFoundException("Greeting audio file not found.", path);

                //plays sound using playsync
                using (SoundPlayer player = new SoundPlayer(path))
                {
                    Console.WriteLine("[*] Playing voice greeting...");
                    player.PlaySync();
                }
            }
            //error handling when it has trouble playing audio
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[!] Error playing audio: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}