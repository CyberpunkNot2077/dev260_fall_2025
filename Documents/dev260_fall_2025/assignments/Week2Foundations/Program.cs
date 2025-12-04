using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using System.Xml;

namespace DataStructuresAndAll
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Welcome to the Data Strcutures and All!");
            SetTheArrays();
            SortTheLists();
            StackTheDeck();
            FormAQueue();
            LookInTheDictionary();
            AllHashedUp();
            LinkTheLIsts();
            GrowingTrees();
            TimeToGraph();
            smallBenchmark();

        }
        static void SetTheArrays()
        {
            int[] tens = new int[10];
            tens[0] = 2; tens[2] = 88; tens[7] = 900;
            Console.WriteLine(tens[2]);

            for (int a = 0; a < tens.Length; a++)
            {
                Console.WriteLine($"{tens[a]}");
            }

            int full = int.MinValue;
            foreach (var e in tens) full = Math.Max(full, e);
            Console.WriteLine($"Highest value: , {full},e");
        }
        static void SortTheLists()
        {
            Console.WriteLine("Here is a list!");
            var integers = new List<int> { 0, 1, 2 };
            integers.Add(3);
            integers.Insert(1, 99);
            Console.WriteLine(string.Join(", ", integers));
            integers.Remove(99);
            Console.WriteLine($"Count after removal: {integers.Count}\n");
        }
        static void StackTheDeck()
        {
            Console.WriteLine("Here we have stacks");
            var stack = new Stack<string>();
            stack.Push("https://www.fanfiction.net/" + "Lets get writing, or reading... whatever!");
            stack.Push("I have faith in the Heart of the Cards!" + "https://yugioh.fandom.com/wiki/Yu-Gi-Oh!");
            stack.Push("Deception Shatters Innocence" + "https://liesofp.wiki.fextralife.com/Lies+of+P+Wiki");
            Console.WriteLine($"Sneak Peek: {stack.Peek()}");
            while (stack.Count > 0)
            {
                Console.WriteLine($"Pop: {stack.Pop}");
            }
            Console.WriteLine();
        }
        static void FormAQueue()
        {
            Console.WriteLine("Please form a queue!");
            var u = new Queue<int>();
            u.Enqueue(40); u.Enqueue(25); u.Enqueue(00);
            Console.WriteLine($"Front: {u.Peek()}");
            while (u.Count > 0)
            {
                Console.WriteLine($"Dequeue: {u.Dequeue()}");
            }
            Console.WriteLine();
        }
        static void LookInTheDictionary()
        {
            Console.WriteLine("Wonder what you are looking for in this Dictionary?");
            var gear = new Dictionary<string, int>
            {
                ["SKU-124"] = 55,
                ["SKU-246"] = 33,
                ["SKU-765"] = 97
            };
            gear["SKU-124"] += 5;
            Console.WriteLine(gear.ContainsKey("SKU-124"));
            if (gear.TryGetValue("SKU-009", out var qty))
                Console.WriteLine(qty);
            else
                Console.WriteLine("SKU-009 is not there");
            Console.WriteLine();
        }
        static void AllHashedUp()
        {
            Console.WriteLine("Let us get hashing!");
            var hashingUp = new HashSet<int> { 1, 2, 3 };
            Console.WriteLine($"Add 2 once more? {hashingUp.Add(2)}");
            var alpha = new HashSet<int> { 1, 2, 3 };
            var beta = new HashSet<int> { 4, 5, 6 };
            Console.WriteLine($"The size of the union is: {alpha.Count}\n");
        }
        class CyberNode
        {
            public int Value;
            public CyberNode? Next;
            public CyberNode(int v) { Value = v; }
        }
        static void LinkTheLIsts()
        {
            Console.WriteLine("Here is a Linked List!");
            CyberNode head = new CyberNode(1);
            head.Next = new CyberNode(2);
            head.Next.Next = new CyberNode(3);

            var secondaryHead = new CyberNode(0) { Next = head };
            head = new thingHead;
            var current = head;
            while (current != null)
            {
                Console.Write($"{current.Value} ");
                current = current.Next;
            }
            Console.WriteLine();
            Console.WriteLine($"Now has 2? {Contains(head, 2)}\n");
        }
        static bool Contains(CyberNode head, int reticle)
        {
            var current = head;
            while (current != null)
            {
                if (current.Value == reticle) return true;
                current = current.Next;
            }
            return false;
        }
        class NodeOfTree
        {
            public int Value;
            public NodeOfTree? Left, Right;
            public NodeOfTree(int x) { Value = x; }
        }
        static void GrowingTrees()
        {
            Console.WriteLine("Shall we get as tall as a tree?");
            var root = new NodeOfTree(10)
            {
                Left = new NodeOfTree(5),
                Right = new NodeOfTree(15) { Left = new NodeOfTree(12) }
            };
            Console.WriteLine("InOrder: "); InOrder(root); Console.WriteLine();
            Console.WriteLine("Order Beforehand: "); orderBeforehand(root); Console.WriteLine();
            Console.WriteLine("PostOrder"); AfterOrder(root); Console.WriteLine("\n");
        }
        static void InOrder(NodeOfTree? e)
        {
            if (e == null) return;
            InOrder(e.Left);
            Console.Write($"{e.Value}");
            InOrder(e.Right);
        }
        static void orderBeforehand(NodeOfTree? e)
        {
            if (e == null) return;
            Console.Write($"{e.Value} ");
            orderBeforehand(e.Left);
            orderBeforehand(e.Right);
        }
        static void AfterOrder(NodeOfTree? e)
        {
            if (e == null) return;
            AfterOrder(e.Left);
            AfterOrder(e.Right);
            Console.Write($"{e.Value}");
        }
        static void TimeToGraph()
        {
            Console.WriteLine("Shall we begin the Graphing?");
            var graph = new Dictionary<string, List<string>>
            {
                ["SEA"] = new() { "SFO", "LAX" },
                ["SFO"] = new() { "SEA", "DEN" },
                ["LAX"] = new() { "SEA", "PHX" },
                ["DEN"] = new() { "SFO" },
                ["PHX"] = new() { "LAX" }
            };
            var order = Bfs(graph, "SEA");
            Console.WriteLine(string.Join(" -> ", order));
            Console.WriteLine();
        }
        static List<string> Bfs(Dictionary<string, List<string>> g, string start)
        {
            var order = new List<string>();
            var w = new Queue<string>();
            var beenToBefore = new HashSet<string>();

            w.Enqueue(start);
            beenToBefore.Add(start);

            while (w.Count > 0)
            {
                var now = w.Dequeue();
                order.Add(now);
                foreach (var nei in g[now])
                    if (beenToBefore.Add(nei)) w.Enqueue(nei);
            }
            return order;
        }
        static void smallBenchmark()
        {
            Console.WriteLine("Welcome to the Benchmark, ladies and gentlemen!");
            var list = new List<int>();
            var dict = new Dictionary<int, bool>();
            for (int c = 0; c < 200_000; c++)
            {
                list.Add(c); dict[c] = true;
            }
            var sw = Stopwatch.StartNew();
            bool listHas = list.Contains(199_999);
            sw.Stop();
            Console.WriteLine($"List.Contains: {sw.ElapsedMilliseconds} ms");
        }
    }
}