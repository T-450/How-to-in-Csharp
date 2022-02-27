// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HowToReadAndWriteCSV.Models;

/// <summary>
/// Class to represent the CSV Data.
/// </summary>
public class Software
{
    public int SoftwareId { get; set; }

    public int OrganizationId { get; set; }

    public string? SoftwareName { get; set; }

    public string? SoftwareAlternateName { get; set; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{this.SoftwareId} {this.OrganizationId} {this.SoftwareName} {this.SoftwareAlternateName}";
    }
}
