// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using HowToReadAndWriteCSV.Helpers.CSV;
using HowToReadAndWriteCSV.Models;

namespace HowToReadAndWriteCsv;

/// <summary>
/// Simple program that reads and writes CSV using CSVHelper library.
/// </summary>
public class Program
{
    private static readonly string currentDir = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().LastIndexOf("bin"));
    private static readonly string fileOutputPath = Path.Combine(currentDir, "Resources", "Output", "Output.csv");
    private static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.WriteLine($"Error: Filepath should not be empty!");
            return;
        }

        var filePath = args[0];

        if (!File.Exists(Path.GetFullPath(filePath)))
        {
            Console.WriteLine($"Error: File {filePath} not found");
            return;
        }

        var fileProcessor = new CsvFileHelper<Contact>(filePath, fileOutputPath);
        var data = fileProcessor.Read();
        fileProcessor.Write(data);
    }
}
