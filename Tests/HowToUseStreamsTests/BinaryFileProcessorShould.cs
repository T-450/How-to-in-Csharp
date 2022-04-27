// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Linq;
using HowToUseStreams.Data;
using Xunit;

namespace HowToUseStreamsTests;

public class BinaryFileProcessorShould
{
    private static readonly string currentDir = Directory.GetCurrentDirectory()
        .Substring(0, Directory.GetCurrentDirectory().LastIndexOf("bin"));

    private static readonly string filePath = Path.Combine(currentDir, "Resources", "BinaryFile.dat");
    private static readonly string outputPath = Path.Combine(currentDir, "Resources", "BinDataOutput.dat");

    [Fact(DisplayName = "Read contents from a specified file")]
    public void ReadContentsFromASpecifiedFile()
    {
        // Create a mock input file
        var mockInputFileData = new MockFileData("ÿ^");

        var mockFileSystem = new MockFileSystem();
        mockFileSystem.AddFile(filePath, mockInputFileData);
        mockFileSystem.AddFile(outputPath, mockInputFileData);

        var sut = new BinaryFileProcessor(
            filePath,
            outputPath
        );
        var actual = sut.Read();
        var exptected = mockInputFileData.Contents;

        Assert.Equal(actual.Take(exptected.Length), exptected);
    }

    [Fact(DisplayName = "Write specified contenst to a file")]
    public void WriteContentsFromASpecifiedFile()
    {
        // Create a mock input file
        var mockInputFileData = new MockFileData("ÿ^");

        var mockFileSystem = new MockFileSystem();
        mockFileSystem.AddFile(filePath, mockInputFileData);
        mockFileSystem.AddFile(outputPath, mockInputFileData);

        var sut = new BinaryFileProcessor(
            filePath,
            outputPath
        );

        sut.Write(mockInputFileData.Contents);

        Assert.True(mockFileSystem.FileExists(outputPath));
        var processedFile = mockFileSystem.GetFile(outputPath);

        Assert.NotNull(processedFile);
        Assert.Equal(processedFile.Contents.Take(mockInputFileData.Contents.Length), mockInputFileData.Contents);
    }
}
