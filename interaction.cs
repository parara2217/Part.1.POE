using System;
using System.Collections;
using System.Configuration;
using System.IO;
using System.Drawing;
using System.Drawing.Text;

namespace New
{
    public class interaction
    {
        string questions;
        //THIS IS THIRD

        //Variables Declared
        private string name = string.Empty;
        private string question = string.Empty;
        //This is to store the replies and what to ignore
        ArrayList replies = new ArrayList();
        ArrayList ignore = new ArrayList();

        public interaction()
        {
            // Welcoming the user 
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("*********************************************");
            Console.WriteLine("Hello! Welcome to the Cybersecurity Awareness Bot. I'm here to help you stay safe online.");
            Console.WriteLine("*********************************************");
            Console.ResetColor();  // Reset the color after the welcome message

            try
            {
                // Capture and validate user's name (will keep asking until valid)
                CaptureAndValidateUsername();

                Console.WriteLine($"Hi {name}! It's great to meet you.");

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
                Console.ResetColor();
            }
            catch (Exception)
            {
                // Catch any unexpected errors
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Oops, something went wrong. Please try again.");
                Console.ResetColor();
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
                    Console.WriteLine("\nYou have pressed a key to exit the interaction.");
                    Console.ResetColor();
                    continueInteraction = false; // Stop the interaction if any key is pressed
                }

                // Simulate a prompt for the next part of the interaction
                Console.WriteLine("\nAsk me a question related to cybersecurity, or press any key to stop the conversation.");

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
                    Console.WriteLine("Here are the details I found for you:\n");
                    Console.WriteLine(message);
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("I couldn't find anything directly related to your question. Could you try asking about specific cybersecurity topics?");
                    Console.ResetColor();
                }

                // A brief pause before next loop iteration, making the bot wait a bit before continuing.
                System.Threading.Thread.Sleep(1000); // Adjust the delay as needed
            }
        }

        // Method to capture and validate the username
        private void CaptureAndValidateUsername()
        {
            while (true)
            {
                Console.WriteLine("First things first, can I have your name?");
                name = Console.ReadLine(); // Get the user's input

                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentException("Oops! It seems you skipped your name. Please enter a valid name.");
                }

                break; // Exit the loop if the name is valid
            }
        }

        // Method to capture and validate the question
        private void CaptureAndValidateQuestion()
        {
            while (true)
            {
                Console.WriteLine("How can I assist you with cybersecurity today?");
                question = Console.ReadLine(); // Get the user's input

                if (string.IsNullOrWhiteSpace(question))
                {
                    throw new ArgumentException("It seems you didn't ask anything. Please enter a question related to cybersecurity.");
                }

                // Check if the question is cybersecurity-related
                if (!IsCybersecurityRelated(question))
                {
                    throw new ArgumentException("It seems your question isn't related to cybersecurity. Please ask about cybersecurity topics.");
                }

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
                "data breach",
                "encryption" };

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
            replies.Add("A strong password should be at least 8 characters long, including a number, an uppercase letter, a lowercase letter, and a special character.");
            replies.Add("SQL injection is a serious security vulnerability that allows an attacker to interfere with the queries an application makes to its database.");
            replies.Add("Phishing attacks often target mobile devices, so it’s important to be cautious about suspicious links in text messages or emails.");
            replies.Add("Cybersecurity is all about protecting systems, networks, and data from cyber threats, ensuring privacy and integrity of information.");
            replies.Add("Encryption is a method of converting information or data into a secure format that prevents unauthorized access.");
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
