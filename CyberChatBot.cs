using System;
using System.Collections.Generic;
using CyberChatBotPOEPart1;

namespace CyberChatBotPOEPart2
{
    class CyberChatbot : ChatBot
    {
        private delegate void ChatAction(string userInput);
        private Dictionary<string, ChatAction> keywordHandlers;
        private Dictionary<string, List<string>> randomResponses;
        private string rememberedInterest = null;
        private string lastTopic = null;

        public CyberChatbot()
        {
            keywordHandlers = new Dictionary<string, ChatAction>(StringComparer.OrdinalIgnoreCase)
            {
                { "password", HandlePassword },
                { "phishing", HandlePhishing },
                { "scam", HandleScam },
                { "tip", HandleTips },
                { "privacy", HandlePrivacy },
                { "malware", HandleMalware }
            };

            randomResponses = new Dictionary<string, List<string>>
            {
                ["phishing"] = new List<string>
                {
                    "Be cautious of emails asking for personal information.",
                    "Scammers often disguise themselves as trusted organisations.",
                    "Check the sender’s email address carefully before clicking links."
                },
                ["tip"] = new List<string>
                {
                    "Enable 2FA for extra account protection.",
                    "Avoid using public Wi-Fi without a VPN.",
                    "Update your software regularly to patch vulnerabilities."
                }
            };
        }

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

                Console.Write("\nHow are you feeling today? ");
                string feeling = Console.ReadLine();
                RespondToSentiment(feeling);

                Console.WriteLine("\nHow can I help you today? Feel free to ask anything about cybersecurity!");
                Console.WriteLine("Type 'exit' to quit.\n");

                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("You: ");
                    string input = Console.ReadLine();
                    Console.ResetColor();

                    if (string.IsNullOrWhiteSpace(input)) continue;
                    if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Bot: Goodbye! Stay alert and stay safe out there!");
                        Console.ResetColor();
                        break;
                    }

                    RespondToSentiment(input);
                    CheckForInterest(input);

                    bool matched = false;
                    foreach (var keyword in keywordHandlers.Keys)
                    {
                        if (input.ToLower().Contains(keyword))
                        {
                            keywordHandlers[keyword](input);
                            lastTopic = keyword;
                            matched = true;
                            break;
                        }
                    }

                    if (!matched)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Bot: I didn’t quite catch that. Try asking about passwords, phishing, scams, privacy, or tips.");
                        Console.ResetColor();
                    }

                    if (rememberedInterest != null && !input.ToLower().Contains(rememberedInterest.ToLower()))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Bot: By the way, earlier you mentioned you're interested in {rememberedInterest}. Would you like more info on that?");
                        Console.ResetColor();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[!] Unexpected error: {ex.Message}");
                Console.ResetColor();
            }
        }

        public override void GreetUser(string userName)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nWelcome, {userName}! I'm your Cybersecurity Awareness Bot.\n");
            Console.ResetColor();
        }

        private void RespondToSentiment(string input)
        {
            input = input.ToLower();

            if (input.Contains("worried") || input.Contains("scared") || input.Contains("anxious"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Bot: It's totally normal to feel that way. Cybersecurity can be overwhelming, but I'm here to help you feel safe.");
                Console.ResetColor();
            }
            else if (input.Contains("curious") || input.Contains("interested") || input.Contains("excited"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Bot: I love that curiosity! Let’s explore some cybersecurity topics together.");
                Console.ResetColor();
            }
            else if (input.Contains("frustrated") || input.Contains("confused"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Bot: Sorry you're feeling that way. Let me explain it again in a simpler way.");
                if (!string.IsNullOrEmpty(lastTopic) && keywordHandlers.ContainsKey(lastTopic))
                {
                    keywordHandlers[lastTopic](lastTopic);
                }
                Console.ResetColor();
            }
            else if (input.Contains("good") || input.Contains("great") || input.Contains("fine"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Bot: Glad to hear that! Let's make sure your online life stays just as good.");
                Console.ResetColor();
            }
            else if (input.Contains("nervous"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Bot: Being nervous is okay. Cyber threats are real, but awareness makes you powerful.");
                Console.ResetColor();
            }
        }

        private void CheckForInterest(string input)
        {
            if (input.Contains("interested in"))
            {
                var words = input.Split(' ');
                int index = Array.IndexOf(words, "in");
                if (index != -1 && index < words.Length - 1)
                {
                    rememberedInterest = words[index + 1];
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Bot: Great! I'll remember that you're interested in {rememberedInterest}.");
                    Console.ResetColor();
                }
            }
        }

        private void PromptFollowUp(string question, string answer)
        {
            Console.Write($"Bot: {question} (yes/no): ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string reply = Console.ReadLine()?.Trim().ToLower();
            Console.ResetColor();

            if (reply == "yes")
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Bot: {answer}");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Bot: Got it! Let me know if you want to learn more.");
                Console.ResetColor();
            }
        }

        private void HandlePassword(string input)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Bot: Use strong, unique passwords for each account. Avoid using personal details in your passwords.");
            PromptFollowUp("Would you like to hear about password managers?", "Password managers help you generate and store complex passwords securely.");
            Console.ResetColor();
        }

        private void HandlePhishing(string input)
        {
            Random rand = new Random();
            var responses = randomResponses["phishing"];
            string selected = responses[rand.Next(responses.Count)];

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Bot: {selected}");
            PromptFollowUp("Would you like help identifying phishing emails?", "Watch for urgency, suspicious links, and grammar errors.");
            Console.ResetColor();
        }

        private void HandleTips(string input)
        {
            Random rand = new Random();
            var tips = randomResponses["tip"];
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Bot: {tips[rand.Next(tips.Count)]}");
            PromptFollowUp("Want another tip?", tips[rand.Next(tips.Count)]);
            Console.ResetColor();
        }

        private void HandlePrivacy(string input)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Bot: Protect your privacy by limiting data shared online, adjusting social media settings, and using encrypted communication.");
            PromptFollowUp("Want to learn how to enhance your privacy settings?", "Start with reviewing app permissions and using private browsing.");
            Console.ResetColor();
        }

        private void HandleMalware(string input)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Bot: Malware is software designed to harm your device. Avoid downloading from unknown sources and keep your antivirus updated.");
            PromptFollowUp("Would you like to learn about antivirus tools?", "Antivirus software helps detect and remove threats before they cause harm.");
            Console.ResetColor();
        }

        private void HandleScam(string input)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Bot: Scams try to trick you into giving up sensitive info. Look out for suspicious messages or calls asking for personal details.");
            PromptFollowUp("Need help recognizing common scams?", "Never share OTPs or account numbers with unknown contacts.");
            Console.ResetColor();
        }
    }
}
