// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.IO;
using System.IO.Abstractions.TestingHelpers;
using HowToUseStreams.Data;
using Xunit;

namespace HowToUseStreamsTests
{

    public class TextFileProcessorShould
    {
        private static string currentDir = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().LastIndexOf("bin"));
        private static string filePath = Path.Combine(currentDir, "Resources", "Far_Over_the_Misty_Mountains_Cold.txt");
        private static string outputPath = Path.Combine(currentDir, "Resources", "output.txt");

        [Fact(DisplayName = "Read contents from a specified file")]
        public void ReadContentsFromASpecifiedFile()
        {
            // Create a mock input file
            var mockInputFileData = new MockFileData("Line 1\nLine 2");

            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(filePath, mockInputFileData);
            mockFileSystem.AddFile(outputPath, mockInputFileData);

            var sut = new TextFileProcessor(
                filePath,
                outputPath
                );

            var actual = sut.Read().Replace("\r", string.Empty);
            var exptected = mockInputFileData.TextContents;

            Assert.Equal(actual, exptected);
        }

        [Fact(DisplayName = "Write specified contenst to a file")]
        public void WriteContentsFromASpecifiedFile()
        {
            // Create a mock input file
            var mockInputFileData = new MockFileData("Line 1\nLine 2");

            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(filePath, mockInputFileData);
            mockFileSystem.AddFile(outputPath, mockInputFileData);

            var sut = new TextFileProcessor(
                filePath,
                outputPath
                );

            sut.Write(mockInputFileData.TextContents);

            Assert.True(mockFileSystem.FileExists(outputPath));
            MockFileData processedFile = mockFileSystem.GetFile(outputPath);

            Assert.NotNull(processedFile);

            Assert.Equal(processedFile.TextContents, mockInputFileData.TextContents);
        }
    }
}
