// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Globalization;
using System.IO.Abstractions;
using CsvHelper;
using CsvHelper.Configuration;

namespace HowToReadAndWriteCSV.Helpers.CSV;

public class CsvFileHelper<T> : ICsvFileHelper<T>
{
    public CsvFileHelper(string inputFilePath, string outputFilePath, IFileSystem fileSystem)
    {
        this.InputFilePath = inputFilePath;
        this.OutputFilePath = outputFilePath;
        this.fileSystem = fileSystem;
    }

    public CsvFileHelper(string inputFilePath, string outputFilePath)
        : this(inputFilePath, outputFilePath, new FileSystem())
    { }

    /// <summary>
    /// Mock object for testing.
    /// </summary>
    private readonly IFileSystem fileSystem;

    /// <summary>
    /// Location of the specified file
    /// </summary>
    private string InputFilePath { get; set; }

    /// <summary>
    /// Where the file should be saved - manually in Resources/Output.
    /// </summary>
    private string OutputFilePath { get; set; }

    /// <summary>
    /// Read content from CSV and return the content as objects.
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public IEnumerable<T> Read(string filePath = null)
    {
        if (filePath is not null) this.InputFilePath = filePath;

        using StreamReader inputReader = this.fileSystem.File.OpenText(this.InputFilePath);

        var csvConfiguration = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            NewLine = Environment.NewLine,
            Comment = '@',
            AllowComments = true,
            HasHeaderRecord = true,
            // Remove the underscore and set variable names to PascalCase
            PrepareHeaderForMatch = args => args.Header.Replace("_", string.Empty).ToLowerInvariant(),
            TrimOptions = TrimOptions.Trim,
            IgnoreBlankLines = true, // default
            MissingFieldFound = null,
        };

        using var csvReader = new CsvReader(inputReader, csvConfiguration);
        //var software1 = csvReader.Context.RegisterClassMap<SoftwareMap>();
        var records = csvReader.GetRecords<T>().ToList();
        return records;
    }

    /// <summary>
    /// Write provided data to a file that will be store in destinationPath.
    /// </summary>
    /// <param name="data"></param>
    /// <param name="outputFilePath"></param>
    public void Write(IEnumerable<T> data, string outputFilePath = null)
    {
        if (outputFilePath is not null) this.OutputFilePath = outputFilePath;

        using StreamWriter outputWriter = this.fileSystem.File.CreateText(this.OutputFilePath);
        using var csvWriter = new CsvWriter(outputWriter, CultureInfo.InvariantCulture);

        csvWriter.WriteHeader<T>();
        csvWriter.NextRecord();
        csvWriter.WriteRecords(data);

        csvWriter.Flush();
    }
}
