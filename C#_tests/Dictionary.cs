using System;
using System.Collections.Generic;

public class HelloWorld
{
    public static void Main(string[] args)
    {
        List<string> names = new List<string>() { "Felix", "Fabia", "Simon", "Matthias" };
        Console.WriteLine("Original List:");
        Console.WriteLine(string.Join("\n", names));

        // Convert into a Dictionary using the length of names as keys
        Dictionary<int, string> namesDictionary = new Dictionary<int, string>();

        foreach (string name in names)
        {
            namesDictionary.Add(name.Length, name);
        }

        Console.WriteLine("\nDictionary:");
        foreach (var kvp in namesDictionary)
        {
            Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
        }
    }
}
