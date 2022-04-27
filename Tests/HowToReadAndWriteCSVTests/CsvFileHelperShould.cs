// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using HowToReadAndWriteCSV.Helpers.CSV;
using HowToReadAndWriteCSV.Models;
using Xunit;

namespace HowToReadAndWriteCSVTests;

public class CsvFileHelperShould
{
    [Fact(DisplayName = "Should Output a file with the records from previously read file.")]
    public void OutputFileFromCsvData()
    {
        const string inputDir = @"c:\root\in";
        const string inputFileName = "myfile.csv";
        var inputFilePath = Path.Combine(inputDir, inputFileName);

        const string outputDir =
            @"C:\root\source\projects\How-to-in-Csharp\HowToInCSharp\HowToReadAndWriteCSV\Resources\";
        const string outputFileName = "myfileout.csv";
        var outputFilePath = Path.Combine(outputDir, outputFileName);


        var csvLines = new StringBuilder();
        csvLines.AppendLine("id,location_id,organization_id,service_id,name,title,email,department");
        csvLines.AppendLine("1,1,,,Susan Houston,Director of Older Adult Services,,");
        csvLines.AppendLine("2,1,,,Christina Gonzalez,Center Director,,");
        csvLines.AppendLine("3,2,,,Brenda Brown,Director, Second Career Services,,");
        csvLines.AppendLine(Environment.NewLine);

        var mockInputFile = new MockFileData(csvLines.ToString());


        var mockFileSystem = new MockFileSystem();
        mockFileSystem.AddFile(inputFilePath, mockInputFile);
        mockFileSystem.AddDirectory(outputDir);


        var sut = new CsvFileHelper<Contact>(inputFilePath, outputFilePath, mockFileSystem);
        var data = sut.Read();
        sut.Write(data);

        Assert.True(mockFileSystem.FileExists(outputFilePath));

        var processedFile = mockFileSystem.GetFile(outputFilePath);
        Assert.NotNull(processedFile);
        var lines = processedFile.TextContents.Split(Environment.NewLine);

        Assert.Equal("Id,LocationId,OrganizationId,ServiceId,Name,Title,Email,Department", lines[0]);
        Assert.Equal("1,1,,,Susan Houston,Director of Older Adult Services,,", lines[1]);
        Assert.Equal("2,1,,,Christina Gonzalez,Center Director,,", lines[2]);
        Assert.Equal("3,2,,,Brenda Brown,Director,Second Career Services,", lines[3]);
        Assert.Equal(string.Empty, lines[4]);
    }
}
