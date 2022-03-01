// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.IO;
using System.IO.Abstractions;

namespace HowToUseStreams.Data;

/// <summary>
///  This class allow you to read and write discrete data types to an
///  underlying stream in a compact binary format.
/// </summary>
public class BinaryFileProcessor
{
    private string FilePath { get; set; }
    private string OutputFilePath { get; set; }
    private IFileSystem FileSystem { get; set; }

    public BinaryFileProcessor(string filePath, string outputFilePath, IFileSystem fileSystem)
    {
        FilePath = filePath;
        OutputFilePath = outputFilePath;
        this.FileSystem = fileSystem;
    }

    public BinaryFileProcessor(string filePath, string outputFilePath) : this(filePath, outputFilePath, new FileSystem())
    { }
    // <inheritdoc/>
    public void Write(byte[] data)
    {
        // Open a binary writer for a file.
        using FileStream fs = File.OpenWrite(OutputFilePath);
        using var bw = new BinaryWriter(fs);
        bw.Write(data);
        bw.Flush();
    }
    // <inheritdoc/>
    public byte[] Read()
    {
        // Open a binary reader for a file.
        using FileStream fs = File.OpenRead(FilePath);
        using var br = new BinaryReader(fs);
        var byteArray = new byte[1024];
        // Read the file and add values to the byteArray
        while (br.Read(byteArray, 0, byteArray.Length) > 0)
        {
        }
        return byteArray;
    }
}
