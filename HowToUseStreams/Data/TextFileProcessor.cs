// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.IO;
using System.IO.Abstractions;
using System.Threading.Tasks;

namespace HowToUseStreams.Data;

/// <summary>
/// This class Reads and Write to a Text files using Streams
/// </summary>
public class TextFileProcessor
{
    private string FilePath { get; set; }
    private string OutputFilePath { get; set; }

    private IFileSystem FileSystem { get; set; }

    public TextFileProcessor(string filePath, string outputFilePath, IFileSystem fileSystem)
    {
        FilePath = filePath;
        OutputFilePath = outputFilePath;
        this.FileSystem = fileSystem;
    }

    public TextFileProcessor(string filePath, string outputFilePath) : this(filePath, outputFilePath, new FileSystem())
    { }

    // <inheritdoc/>
    public virtual string Read()
    {
        // A string writer to store the results from the ReadStream
        using var stringWriter = new StringWriter();
        using var sr = new StreamReader(FilePath, System.Text.Encoding.UTF8);
        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine();
            bool isLastLine = sr.EndOfStream;
            if (isLastLine) stringWriter.Write(line);
            else stringWriter.WriteLine(line);

            stringWriter.Flush();
        }
        return stringWriter.ToString();
    }
    // <inheritdoc/>
    public async Task<string> ReadAsync()
    {
        // A string writer to store the results from the ReadStream
        using var sb = new StringWriter();

        using var sr = new StreamReader(FilePath, System.Text.Encoding.UTF8);
        while (!sr.EndOfStream)
        {
            var line = sr.ReadLine();
            bool isLastLine = sr.EndOfStream;
            // Removes new line from the end of a file
            if (isLastLine) await sb.WriteAsync(line).ConfigureAwait(false);
            else await sb.WriteLineAsync(line).ConfigureAwait(false);

            await sb.FlushAsync().ConfigureAwait(false);
        }

        return sb.ToString();
    }
    // <inheritdoc/>
    public virtual void Write(string data)
    {
        using var sw = new StreamWriter(OutputFilePath);
        // Set's the system enviroment NewLine (more portability)
        sw.NewLine = Environment.NewLine;
        sw.Write(data);
        sw.Flush();
    }
    // <inheritdoc/>
    public virtual async Task WriteAsync(string data)
    {
        using var sw = new StreamWriter(OutputFilePath);
        await sw.WriteAsync(data.ToString()).ConfigureAwait(false);
        // Set's the system enviroment NewLine (more portability)
        sw.NewLine = Environment.NewLine;
        await sw.FlushAsync().ConfigureAwait(false);
    }

}
