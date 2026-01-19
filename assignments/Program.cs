using System;
using Week4DoublyLinkedLists.Core;
using Week4DoublyLinkedLists.Applications;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.ComponentModel;

namespace Week4DoublyLinkedLists
{
    /// <summary>
    /// Main program for Assignment 3: Doubly Linked Lists
    /// Demonstrates practical applications of doubly linked list data structure
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ASSIGNMENT 3: DOUBLY LINKED LISTS ===");
            Console.WriteLine("Data Structures & Algorithms - Step-by-Step Implementation");
            Console.WriteLine();

            bool keepRunning = true;
            
            while (keepRunning)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine() ?? "";
                
                switch (choice)
                {
                    case "1":
                        RunCoreListDemo();
                        break;
                    case "2":
                        RunMusicPlaylistDemo();
                        break;
                    case "3":
                        RunPerformanceComparison();
                        break;
                    case "4":
                        keepRunning = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
                
                if (keepRunning)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        /// <summary>
        /// Display the main menu options
        /// </summary>
        static void DisplayMainMenu()
        {
            Console.WriteLine("=== DOUBLY LINKED LIST DEMO ===");
            Console.WriteLine();
            Console.WriteLine("Choose an option:");
            Console.WriteLine();
            Console.WriteLine("1. Core List Operations Demo (Part A)");
            Console.WriteLine("   → Test step-by-step doubly linked list functionality");
            Console.WriteLine();
            Console.WriteLine("2. Music Playlist Manager (Part B)");
            Console.WriteLine("   → Manage songs and playlists with advanced controls");
            Console.WriteLine();
            Console.WriteLine("3. Performance Comparison (Bonus)");
            Console.WriteLine("   → Compare doubly linked lists with other data structures");
            Console.WriteLine();
            Console.WriteLine("4. Exit Program");
            Console.WriteLine();
            Console.Write("Enter your choice (1-4): ");
        }
        
        /// <summary>
        /// Demonstrate core doubly linked list operations step by step
        /// Uses the CoreListDemo class from the Core directory
        /// </summary>
        static void RunCoreListDemo()
        {
            Console.Clear();
            var coreDemo = new CoreListDemo();
            coreDemo.RunInteractiveDemo();
            
            TwoLinkedList<string> list = new TwoLinkedList<string>();
            list.AddFirst("Brave");
            list.AddLast("Way Away");
            list.AddFirst("Schools Out");
            list.AddNode("Thunderstruck");
            list.AddFirst("Go");
            list.AddLast("Super");
            list.AddLast("Your Move");
            list.AddFirst("Get Your Game On");
            list.AddLast("No Sleep Till Brooklyn");
            Console.WriteLine("The songs are as follows: ");
            foreach (var i in list)
            {
                Console.WriteLine(i);
            }
            list.RunCoreListDemo();
            
        }
        
        /// <summary>
        /// Run the music playlist manager application
        /// Uses the MusicPlaylistManager class from the Applications directory
        /// </summary>
        static void RunMusicPlaylistDemo()
        {
            LinkedList<string> musicalList = new LinkedList<string>();
            musicalList.AddFirst("Can You Feel The Power?");
            musicalList.AddFirst("Simple and Clean");
            musicalList.AddLast("Should I Stay Or Should I Go?");
            musicalList.AddBefore("Prelude: Final Fantasy");
            musicalList.AddFirst("Aria Of The Soul");
            foreach(var melody in musicalList)
            {
                Console.WriteLine(melody);
            }
            Console.Clear();
            var playlistManager = new MusicPlaylistManager();
            playlistManager.RunPlaylistDemo();
            MusicLinkedList list = new Music();
        }
        
        /// <summary>
        /// Compare performance of different data structures
        /// </summary>
        static void RunPerformanceComparison()
        {
            Console.Clear();
            Console.WriteLine("=== PERFORMANCE ANALYSIS ===");
            Console.WriteLine();
            
            Console.WriteLine("Comparing data structures...");
            Console.WriteLine("(This is a bonus feature - implement for extra credit!)");
            
            Console.WriteLine("Performance analysis would go here...");
            Console.WriteLine("Compare: DoublyLinkedList vs Array vs List<T>");
            Console.WriteLine("Operations: Insert, Remove, Search");
            Console.WriteLine("Data sizes: 100, 1000, 10000 elements");
            
            Console.WriteLine("\nPress any key to return to main menu...");
            Console.ReadKey();
        }
    }
}