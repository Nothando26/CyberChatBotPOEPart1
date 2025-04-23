using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CyberChatBotPOEPart1
{
    internal class ChatBot
    {
        public virtual void Run()
        {
            Console.WriteLine("Running chatbot...");
        }

        public virtual void GreetUser(string userName)
        {
            Console.WriteLine($"\nWelcome, {userName}! I'm your chatbot.");
        }
    }
}

