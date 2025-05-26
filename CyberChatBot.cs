using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CyberChatBotPOEPart1;

namespace CyberChatBotPOEPart2
{
    // CyberChatbot inherits from base ChatBot class
    class CyberChatbot : ChatBot
    {
        // Delegate defining the signature for chat response handlers
        private delegate void ChatAction(string userInput);

        // Dictionary mapping keywords to corresponding handler methods
        private Dictionary<string, ChatAction> keywordHandlers;

        // Dictionary holding possible random responses for each topic
        private Dictionary<string, List<string>> randomResponses;

        // Stores a user’s remembered interest to revisit later
        private string rememberedInterest = null;

        // Keeps track of the last topic discussed
        private string lastTopic = null;

        // Flag to track if the bot has prompted about the remembered interest
        private bool interestPrompted = false;

        // Stores the current user’s name (default "User")
        private string userName = "User";

        // Single Random instance for consistent randomization
        private readonly Random rand = new Random();

        // Constructor initializes keyword handlers and random responses
        public CyberChatbot()
        {
            // Map keywords to their respective handler methods (case-insensitive)
            keywordHandlers = new Dictionary<string, ChatAction>(StringComparer.OrdinalIgnoreCase)
            {
                { "password", HandlePassword },
                { "phishing", HandlePhishing },
                { "scam", HandleScam },
                { "tip", HandleTips },
                { "privacy", HandlePrivacy },
                { "malware", HandleMalware }
            };

            // Define lists of varied responses for each keyword/topic
            randomResponses = new Dictionary<string, List<string>>
            {
                ["password"] = new List<string>
                {
                    "Use a combination of letters, numbers, and symbols in your passwords.",
                    "Avoid reusing the same password across sites.",
                    "Change your passwords regularly for sensitive accounts."
                },
                ["phishing"] = new List<string>
                {
                    "Be cautious of emails asking for personal information.",
                    "Scammers often disguise themselves as trusted organisations.",
                    "Check the sender’s email address carefully before clicking links."
                },
                ["scam"] = new List<string>
                {
                    "Never share OTPs or account numbers with unknown contacts.",
                    "If it sounds too good to be true, it probably is.",
                    "Legitimate companies won't ask for passwords via email or phone."
                },
                ["tip"] = new List<string>
                {
                    "Enable 2FA for extra account protection.",
                    "Avoid using public Wi-Fi without a VPN.",
                    "Update your software regularly to patch vulnerabilities."
                },
                ["privacy"] = new List<string>
                {
                    "Limit how much personal data you post online.",
                    "Review app permissions and disable what’s unnecessary.",
                    "Use encrypted messaging apps for sensitive conversations."
                },
                ["malware"] = new List<string>
                {
                    "Avoid downloading attachments from unknown emails.",
                    "Keep your antivirus software updated.",
                    "Run regular scans to detect hidden threats."
                }
            };
        }

        // Main loop for chatbot interaction
        public override void Run()
        {
            try
            {
                // Play greeting audio and display ASCII art/logo banners
                AudioManager.PlayGreeting();
                AsciiArt.DisplayRobot();
                AsciiArt.DisplayLogo();
                WelcomeBanner.Show();

                // Ask for user’s name and greet them
                Console.Write("\nWhat is your name? ");
                userName = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(userName))
                {
                    userName = "User";
                }
                GreetUser(userName);

                // Ask how the user is feeling and respond empathetically
                Console.Write($"\nHow are you feeling today, {userName}? ");
                string feeling = Console.ReadLine()?.Trim();
                if (!string.IsNullOrEmpty(feeling))
                {
                    RespondToSentiment(feeling);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("Bot: No worries if you don't want to share. I'm here to help whenever you're ready.");
                    Console.ResetColor();
                }

                Console.WriteLine("\nAsk me anything about cybersecurity! (Type 'exit' to quit)\n");

                // Start main input loop
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write($"{userName}: ");
                    string input = Console.ReadLine()?.Trim();
                    Console.ResetColor();

                    if (string.IsNullOrWhiteSpace(input)) continue;

                    // Exit command
                    if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"Bot: Goodbye, {userName}! Stay alert and stay safe out there!");
                        Console.ResetColor();
                        break;
                    }

                    // First, respond to any detected sentiment in input
                    RespondToSentiment(input);

                    // Check if user expresses interest to remember
                    CheckForInterest(input);

                    // Attempt to match input with any topic keywords
                    bool matched = false;
                    foreach (var keyword in keywordHandlers.Keys)
                    {
                        if (input.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            keywordHandlers[keyword](input);  // Call topic handler
                            lastTopic = keyword;  // Remember last topic
                            matched = true;
                            break;
                        }
                    }

                    // If no keywords matched, prompt the user with options
                    if (!matched)
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine("Bot: I’m not sure I understand. You can ask me about passwords, phishing, scams, privacy, malware, or general tips.");
                        Console.ResetColor();
                    }

                    // If the bot remembers an interest and hasn't prompted about it yet, ask user
                    if (rememberedInterest != null && !interestPrompted)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Bot: Earlier, you mentioned you're interested in '{rememberedInterest}'. Would you like to know more about that? (yes/no)");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write($"{userName}: ");
                        string interestReply = Console.ReadLine()?.Trim().ToLower();
                        Console.ResetColor();

                        if (interestReply == "yes")
                        {
                            string matchedKeyword = FindMatchingKeyword(rememberedInterest);
                            if (matchedKeyword != null)
                            {
                                // Provide information on remembered interest topic
                                keywordHandlers[matchedKeyword](matchedKeyword);
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine($"Bot: Sorry, I don't have detailed info on '{rememberedInterest}', but feel free to ask me about other cybersecurity topics!");
                                Console.ResetColor();
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Bot: No problem! Just let me know whenever you're curious.");
                            Console.ResetColor();
                        }

                        // Mark that user has been prompted to avoid repeated prompts
                        interestPrompted = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Catch unexpected errors and display in red
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n[!] Unexpected error: {ex.Message}");
                Console.ResetColor();
            }
        }

        // Welcomes the user at the start
        public override void GreetUser(string userName)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nWelcome, {userName}! I'm your Cybersecurity Awareness Bot, here to keep you informed and safe.\n");
            Console.ResetColor();
        }

        // Responds to detected sentiment keywords with empathetic messages
        private void RespondToSentiment(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return;

            string lowerInput = input.ToLower();

            // Check for negative or positive sentiment keywords and respond accordingly
            if (ContainsAny(lowerInput, "worried", "scared", "anxious", "afraid"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Bot: It's completely normal to feel worried about cybersecurity. I'm here to help you feel more confident and safe.");
            }
            else if (ContainsAny(lowerInput, "curious", "interested", "excited", "keen"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Bot: I love your curiosity! Let’s dive into some cybersecurity topics together.");
            }
            else if (ContainsAny(lowerInput, "frustrated", "confused", "lost", "stuck"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Bot: Sorry you're feeling that way. Let me explain it more clearly.");
                if (!string.IsNullOrEmpty(lastTopic) && keywordHandlers.ContainsKey(lastTopic))
                {
                    // Repeat explanation on last topic to help user understand better
                    keywordHandlers[lastTopic](lastTopic);
                }
                else
                {
                    Console.WriteLine("Bot: You can ask me about passwords, phishing, scams, privacy, malware, or tips.");
                }
            }
            else if (ContainsAny(lowerInput, "good", "great", "fine", "well", "okay", "happy"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Bot: That's wonderful to hear! Let's keep your online safety just as great.");
            }
            else if (ContainsAny(lowerInput, "nervous", "uneasy", "apprehensive"))
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Bot: Feeling nervous is okay. Awareness and knowledge will make you stronger against cyber threats.");
            }
            // If no sentiment keyword detected, no specific response given

            Console.ResetColor();
        }

        // Helper method to check if input contains any of the given keywords
        private bool ContainsAny(string input, params string[] keywords)
        {
            foreach (var kw in keywords)
            {
                if (input.Contains(kw)) return true;
            }
            return false;
        }

        // Detects if user expresses interest to remember for later prompting
        private void CheckForInterest(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return;

            // Regex pattern looks for phrase "interested in <topic>"
            var match = Regex.Match(input, @"interested in (.+)", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                string interest = match.Groups[1].Value.Trim();
                if (!string.IsNullOrEmpty(interest))
                {
                    rememberedInterest = interest;
                    interestPrompted = false; // reset to prompt user later
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"Bot: Got it! I'll remember that you're interested in '{rememberedInterest}'.");
                    Console.ResetColor();
                }
            }
        }

        // Attempts to find a matching topic keyword inside the remembered interest string
        private string FindMatchingKeyword(string interest)
        {
            if (string.IsNullOrEmpty(interest)) return null;

            foreach (var key in keywordHandlers.Keys)
            {
                if (interest.IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return key;
                }
            }
            return null;
        }

        // Prompts user with a follow-up yes/no question and responds accordingly
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
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Bot: Got it! Let me know if you want to learn more anytime.");
            }
            Console.ResetColor();
        }

        // ======= TOPIC HANDLERS WITH RANDOM RESPONSES =======

        // Provides random advice on passwords and prompts follow-up on password managers
        private void HandlePassword(string input)
        {
            DisplayRandomResponse("password");
            PromptFollowUp("Would you like to learn about password managers?",
                "Password managers help you generate and store complex passwords securely.");
        }

        // Provides random advice on phishing and prompts follow-up on identifying phishing emails
        private void HandlePhishing(string input)
        {
            DisplayRandomResponse("phishing");
            PromptFollowUp("Would you like help identifying phishing emails?",
                "Watch for urgent language, suspicious links, and spelling mistakes.");
        }

        // Provides random advice on scams and prompts follow-up on recognizing scams
        private void HandleScam(string input)
        {
            DisplayRandomResponse("scam");
            PromptFollowUp("Need help recognizing common scams?",
                "Never share OTPs or account details with unknown contacts.");
        }

        // Provides random cybersecurity tips and prompts follow-up for more tips
        private void HandleTips(string input)
        {
            DisplayRandomResponse("tip");
            PromptFollowUp("Want another cybersecurity tip?", GetRandomFrom("tip"));
        }

        // Provides random advice on privacy and prompts follow-up on privacy settings
        private void HandlePrivacy(string input)
        {
            DisplayRandomResponse("privacy");
            PromptFollowUp("Want to learn how to enhance your privacy settings?",
                "Start by reviewing app permissions and using private browsing modes.");
        }

        // Provides random advice on malware and prompts follow-up on antivirus tools
        private void HandleMalware(string input)
        {
            DisplayRandomResponse("malware");
            PromptFollowUp("Would you like to learn about antivirus tools?",
                "Antivirus software helps detect and remove threats before they harm your system.");
        }

        // Prints a random response from the list for a given topic
        private void DisplayRandomResponse(string topic)
        {
            if (randomResponses.TryGetValue(topic, out var list) && list.Count > 0)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Bot: {list[rand.Next(list.Count)]}");
                Console.ResetColor();
            }
        }

        // Returns a random string from the list of responses for a given topic
        private string GetRandomFrom(string topic)
        {
            if (randomResponses.TryGetValue(topic, out var list) && list.Count > 0)
            {
                return list[rand.Next(list.Count)];
            }
            return "Here's a general tip: Stay cautious and keep learning!";
        }
    }
}

