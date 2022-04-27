// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HowToReadAndWriteCSV.Models;

/// <summary>
///     Class to represent the CSV Data.
/// </summary>
public class Contact
{
    public int Id { get; set; }

    public int? LocationId { get; set; }

    public int? OrganizationId { get; set; }

    public int? ServiceId { get; set; }

    public string? Name { get; set; }

    public string? Title { get; set; }

    public string? Email { get; set; }

    public string? Department { get; set; }

    public override string ToString()
    {
        return $"{Id} {LocationId} {OrganizationId} {ServiceId} {Name} {Title} {Email} {Department}";
    }
}
