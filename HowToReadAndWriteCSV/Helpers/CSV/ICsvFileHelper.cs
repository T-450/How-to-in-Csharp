// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HowToReadAndWriteCSV.Helpers;

public interface ICsvFileHelper<T>
{
    IEnumerable<T> Read(string filePath);

    void Write(IEnumerable<T> data, string outputFilePath);
}
