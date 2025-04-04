using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Text.RegularExpressions;

namespace New
{
    public class interaction
    {
        //Variables Declared
        string questions;
        private string name = string.Empty;
        private string question = string.Empty;
        string ChatBot;
        //This is to store the replies and what to ignore
        ArrayList replies = new ArrayList();
        ArrayList ignore = new ArrayList();

        public interaction()
        {
            // Welcoming the user 
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("*********************************************");
            Console.WriteLine($"ChatBot ->\" Hello! Welcome to the Cybersecurity Awareness Bot. I'm here to help you stay safe online.");
            Console.WriteLine("*********************************************");


            try
            {
                // Capture and validate user's name (will keep asking until valid)
                Console.ForegroundColor = ConsoleColor.Red;
                CaptureAndValidateUsername();
                Console.WriteLine($"ChatBoy -> \"Hi {name}! It's great to meet you.\"");
                //  Console.WriteLine($"Hi {name}! It's great to meet you.");
                Console.ForegroundColor = ConsoleColor.Cyan;

                // Capture and validate the question (will keep asking until valid)
                CaptureAndValidateQuestion();

                // Call both methods to store data automatically
                KeepIgnore();
                KeepReplies();

                // Start the interactive loop
                StartInteractiveLoop();
            }
            catch (ArgumentException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);

            }
            catch (Exception)
            {
                // Catch any unexpected errors
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"ChatBot ->\"Oops, something went wrong. Please try again.");

            }
        }

        // Method to start the interactive loop
        private void StartInteractiveLoop()
        {
            bool continueInteraction = true;

            while (continueInteraction)
            {
                // Wait for a user input (key press) without blocking the rest of the interaction
                if (Console.KeyAvailable)
                {
                    Console.ReadKey(true); // Capture and ignore the key press to prevent it from displaying
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"ChatBot ->\"You have pressed a key to exit the interaction.");
                    Console.ResetColor();
                    continueInteraction = false; // Stop the interaction if any key is pressed
                }

                // Simulate a prompt for the next part of the interaction
                Console.WriteLine($"ChatBot ->\"Ask me a question related to cybersecurity, or press any key to stop the conversation.");

                // Capture and validate the question again (can keep prompting)
                CaptureAndValidateQuestion();

                // Process the question and provide relevant replies
                string[] storeWord = questions.Split(' ');
                ArrayList storeFwords = new ArrayList();

                // Check the words and store only the ones that aren't in the ignore list
                for (int count = 0; count < storeWord.Length; count++)
                {
                    if (!ignore.Contains(storeWord[count]))
                    {
                        storeFwords.Add(storeWord[count]);
                    }
                }

                bool found = false;
                string message = String.Empty;

                // Loop to find answers from the stored words
                foreach (var word in storeFwords)
                {
                    foreach (var reply in replies)
                    {
                        if (reply.ToString().ToLower().Contains(word.ToString().ToLower()))
                        {
                            message += reply + "\n";
                            found = true;
                        }
                    }
                }

                // Display the appropriate message
                if (found)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"ChatBot ->\"Here are the details I found for you:\n");
                    Console.WriteLine(message);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"ChatBot ->\"I couldn't find anything directly related to your question. Could you try asking about specific cybersecurity topics?");
                    Console.ResetColor();
                }

                // A brief pause before next loop iteration, making the bot wait a bit before continuing.
                System.Threading.Thread.Sleep(1000); // It is to pause every millisecond
            }
        }

        // Method to capture and validate the username
        private void CaptureAndValidateUsername()
        {
            do
            {
                Console.WriteLine($"ChatBot ->\"What is your name? (Type 'Exit' to exit the program)");
                name = Console.ReadLine(); // Get the user's input

                if (string.IsNullOrWhiteSpace(name) || name.ToLower() == "bye")
                {
                    if (name.ToLower() == "exit")
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"ChatBot ->\"Goodbye! Stay safe online!");
                        Environment.Exit(0);  // Exit the program if the user types "Exit"
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"ChatBot ->\"Oops! It seems you skipped your name. Please enter a valid name.");
                    }
                }

            } while (string.IsNullOrWhiteSpace(name)); // Keep asking until a valid name is entered
        }

        // Method to capture and validate the question

        // Method to capture and validate the question
        private void CaptureAndValidateQuestion()
        {
            while (true)
            {
                Console.WriteLine($"ChatBot ->\" How can I assist you with cybersecurity today?");
                question = Console.ReadLine(); // Get the user's input

                if (string.IsNullOrWhiteSpace(question) || question.ToLower() == "exit")
                {
                    if (question.ToLower() == "exit")
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.WriteLine($"ChatBot ->\"Goodbye! Stay safe online!");
                        Environment.Exit(0);  // Exit the program if the user types "Exit"
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"ChatBot ->\"It seems you didn't ask anything. Please enter a question related to cybersecurity.");
                    }
                }
                else if (!IsCybersecurityRelated(question))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"ChatBot ->\"It seems your question isn't related to cybersecurity. Please ask about cybersecurity topics.");
                }
                else
                {
                    questions = question; // Assign the valid question to the 'questions' variable
                    break; // Exit the loop if the question is valid
                }
            }
       

        // Method to check if the question is related to cybersecurity
        private bool IsCybersecurityRelated(string question)
        {
            string[] keywords = {"password","Passwords","passwords","PASSWORD","PASSWORDS" ,
                "phishing", "Phishing","PHISHING",
                "cybersecurity","CYBERSECURITY", "Cybersecurity","CyberSecurity",
                "hack", "hacks", "hacking", "Hack", "Hacks", "Hacking",
                "virus", "viruses", "Virus", "VIRUS", "VIRUSES",
                "encryption","encryptions", "ENCRYPTION","ENCRYPTIONS"};

            foreach (var keyword in keywords)
            {
                if (question.ToLower().Contains(keyword))
                {
                    return true; // Return true if a cybersecurity-related keyword is found
                }
            }

            return false; // Return false if no keyword is found
        }
      

        // Method for storing replies
        private void KeepReplies()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            replies.Add("*A strong password should be at least 8 characters long, including a number, an uppercase letter, a lowercase letter, and a special character.");
            replies.Add("*SQL injection is a serious security vulnerability that allows an attacker to interfere with the queries an application makes to its database.");
            replies.Add("*Cybersecurity is all about protecting systems, networks, and data from cyber threats, ensuring privacy and integrity of information.");
            replies.Add("*Encryption is a method of converting information or data into a secure format that prevents unauthorized access.");
            replies.Add("*Phishing is the act of sending phony emails that look like they are from reliable sources. This type of hack is the most prevalent and aims to steal sensitive information, including login credentials and credit card details. Using technology that filters phishing emails or educating yourself can help you protect yourself." + "*Phishing attacks often target mobile devices, so it’s important to be cautious about suspicious links in text messages or emails.");
        }

        // Method for storing ignored words
        private void KeepIgnore()
        {
            ignore.Add("tell");
            ignore.Add("me");
            ignore.Add("about");
            ignore.Add("what");
            ignore.Add("more");
        }
    }
}