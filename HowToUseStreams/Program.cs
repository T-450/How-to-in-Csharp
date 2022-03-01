// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.IO;
using HowToUseStreams.Data;

namespace HowToUseStreams;

/// <summary>
/// This simple program demonstrate how to write and read files using .Net Streams
/// </summary>
internal class Program
{
    static void Main(string[] args)
    {
        // Process a text file
        new Program()
            .ProcessText();
        // Process a binary file
        new Program()
            .ProcessBinary();
    }

    public async void ProcessText()
    {
        var currentDir = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().LastIndexOf("bin"));
        var filePath = Path.Combine(currentDir, "Resources", "Far_Over_the_Misty_Mountains_Cold.txt");
        var outputPath = Path.Combine(currentDir, "Resources", "output.txt");

        var fileProcessor = new TextFileProcessor(filePath, outputPath);
        System.Console.WriteLine($"Reading a file with stream...");
        var result = await fileProcessor.ReadAsync().ConfigureAwait(false);
        System.Console.WriteLine($"Writing to a file with stream...");
        await fileProcessor.WriteAsync(result).ConfigureAwait(false);
    }

    public void ProcessBinary()
    {
        var currentDir = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().LastIndexOf("bin"));
        var filePath = Path.Combine(currentDir, "Resources", "BinaryFile.dat");
        var outputPath = Path.Combine(currentDir, "Resources", "BinDataOutput.dat");

        var fileProcessor = new BinaryFileProcessor(filePath, outputPath);
        System.Console.WriteLine($"Reading a binary file with stream...");
        var result = fileProcessor.Read();
        System.Console.WriteLine($"Writing to a binary file with stream...");
        fileProcessor.Write(result);
    }
}
