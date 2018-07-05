﻿using System;
using System.Collections.Generic;
using System.Linq;
using Utility.CommandLine;

namespace Examples
{
    /// <summary>
    ///     Provides an eval/print loop for command line argument strings.
    /// </summary>
    internal class Program
    {
        #region Private Properties

        /// <summary>
        ///     Gets or sets a value indicating whether the Bool argument was supplied.
        /// </summary>
        [Argument('b', "boolean")]
        private static bool Bool { get; set; }

        /// <summary>
        ///     Gets or sets the value of the Double argument.
        /// </summary>
        [Argument('f', "float")]
        private static double Double { get; set; }

        /// <summary>
        ///     Gets or sets the value of the Int argument
        /// </summary>
        [Argument('i', "integer")]
        private static int Int { get; set; }

        /// <summary>
        ///     Gets or sets the value of the List argument.
        /// </summary>
        [Argument('l', "list")]
        private static List<int> List { get; set; }

        /// <summary>
        ///     Gets or sets the list of operands.
        /// </summary>
        [Operands]
        private static string[] Operands { get; set; }

        /// <summary>
        ///     Gets or sets the String argument.
        /// </summary>
        [Argument('s', "string")]
        private static string String { get; set; }

        #endregion Private Properties

        #region Private Methods

        /// <summary>
        ///     Application entry point
        /// </summary>
        /// <param name="args">Command line arguments</param>
        private static void Main(string[] args)
        {
            // enable ctrl+c
            Console.CancelKeyPress += (o, e) =>
            {
                Environment.Exit(1);
            };

            Console.WriteLine("At the prompt, enter text as if it were a string of command line arguments. Enter 'exit' to exit.");

            while (true)
            {
                Console.Write("> ");

                string input = Console.ReadLine();

                if (input == "exit")
                {
                    break;
                }

                Print(input);
            }
        }

        /// <summary>
        ///     Parses the specified command line string and displays the resulting dictionary, then populates the application's
        ///     properties with the values.
        /// </summary>
        /// <param name="commandLine">The command line string to parse.</param>
        private static void Print(string commandLine)
        {
            Reset();

            // populate properties
            Arguments.Populate(commandLine);

            Console.WriteLine("\r\nArgument\tValue");
            Console.WriteLine("-------\t\t-------");

            Dictionary<string, object> argumentDictionary = Arguments.Parse(commandLine).ArgumentDictionary;

            foreach (string key in argumentDictionary.Keys)
            {
                Console.WriteLine(key + "\t\t" + argumentDictionary[key]);
            }

            Console.WriteLine("\r\nProperty\tValue");
            Console.WriteLine("-------\t\t-------");

            Console.WriteLine("String\t\t" + String);
            Console.WriteLine("Bool\t\t" + Bool);
            Console.WriteLine("Int\t\t" + Int);
            Console.WriteLine("Double\t\t" + Double);

            if (List != null)
            {
                Console.WriteLine("List\t\t" + string.Join(",", List.Select(o => o.ToString())?.ToArray()));
            }

            Console.WriteLine("\r\nOperands\n-------");

            for (int i = 0; i < Operands.Length; i++)
            {
                Console.WriteLine(i + ".\t" + Operands[i]);
            }
        }

        /// <summary>
        ///     Resets properties to their default values.
        /// </summary>
        private static void Reset()
        {
            String = string.Empty;
            Bool = false;
            Int = 0;
            Double = 0;
            List = new List<int>();
        }

        #endregion Private Methods
    }
}