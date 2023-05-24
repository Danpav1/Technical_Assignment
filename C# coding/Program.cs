using System;
using System.IO;
using System.Collections.Generic;

/*
* @author Daniel Pavenko
* Program that finds, parses and validates contents of a csv file
*/

namespace TechnicalAssignmentNameSpace
{
    class TechnicalAssignment
    {
        //function that asks the user for the filename and stores the users input
        static void prompt()
        {
            string? directory = "", fileName = "";

            Console.WriteLine("Enter the filename or -1 to exit.");
            fileName = Console.ReadLine();

            //checks if exit condition was met
            if (fileName == "-1")
            {
                Console.WriteLine("Exitting.");
                Environment.Exit(1);
            }

            Console.WriteLine("Enter the directory to be searched or -1 to exit.");
            directory = Console.ReadLine();

            //checks if exit condition was met
            if (directory == "-1")
            {
                Console.WriteLine("Exitting.");
                Environment.Exit(1);
            }

            //checks if both the filename and directory were succesfully validated
            if (fileNameCheck(fileName) && directoryCheck(directory))
            {
                Console.WriteLine("Filename and Directory were validated succesfully");
                parseCSV(directory, fileName);
            }
            else
            {
                Console.WriteLine(" ");
                prompt();
            }
        }

        //function that checks if the directory is valid (exists)
        static Boolean directoryCheck(string directory)
        {
            if (Directory.Exists(directory))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Error: Directory does not exist");
                return false;
            }
        }

        //function that checks if the filename is valid (exists)
        static Boolean fileNameCheck(string fileName)
        {
            if (File.Exists(fileName))
            {
                return true;
            }
            else
            {
                Console.WriteLine("Error: File does not exist");
                return false;
            }
        }

        //function that parses the csv file and sorts the emails into valid and unvalid linkedlists
        static void parseCSV(string directory, string fileName)
        {
            string completeDirectory = directory + "/" + fileName;

            string[] lines = File.ReadAllLines(completeDirectory);

            LinkedList<String> validEmails = new LinkedList<string>();
            LinkedList<String> invalidEmails = new LinkedList<string>();

            int atCount = 0, periodCount = 0, prefixLength = 0, numOfCharAfterAt = 0, numOfCharAfterPeriod = 0;

            //iterates through each email and each char in said email
            foreach (string email in lines)
            {
                atCount = 0;
                periodCount = 0;
                prefixLength = 0;
                numOfCharAfterAt = 0;
                numOfCharAfterPeriod = 0;

                foreach (char ch in email)
                {
                    if (atCount == 0)
                    {
                        if (ch == '@')
                        {
                            atCount++;
                        }
                        else
                        {
                            prefixLength++;
                        }
                    }

                    if ((atCount == 1) && (periodCount == 0))
                    {
                        if (ch == '@')
                        {
                            continue;
                        }
                        else if (ch == '.')
                        {
                            periodCount++;
                        }
                        else
                        {
                            numOfCharAfterAt++;
                        }
                    }

                    if ((atCount == 1) && (periodCount == 1))
                    {
                        if (ch == '.')
                        {
                            continue;
                        }
                        else
                        {
                            numOfCharAfterPeriod++;
                        }
                    }
                }

                if (atCount == 1 && periodCount == 1 && prefixLength > 0 && numOfCharAfterAt > 0 && numOfCharAfterPeriod > 0)
                {
                    validEmails.AddLast(email);
                }
                else
                {
                    invalidEmails.AddLast(email);
                }
            }

            Console.WriteLine("Valid Emails:");
            Console.WriteLine(String.Join("\t\n", validEmails));

            Console.WriteLine("");

            Console.WriteLine("Invalid Emails:");
            Console.WriteLine(String.Join("\t\n", invalidEmails));
        }

        static void Main(string[] args)
        {
            prompt();
        }
    }
}