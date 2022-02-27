// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using CsvHelper.Configuration;
using HowToReadAndWriteCSV.Models;

namespace HowToReadAndWriteCSV.Configuration.Mappers;

/// <summary>
/// Class that maps it's properties to CSV header names.
/// </summary>
public class SoftwareMap : ClassMap<Software>
{
    public SoftwareMap()
    {
        this.AutoMap(System.Globalization.CultureInfo.InvariantCulture);

        // Map class property to csv header name
        this.Map(m => m.SoftwareId).Name("id");
        this.Map(m => m.OrganizationId).Name("organization_id");
        this.Map(m => m.SoftwareName).Name("name");
        this.Map(m => m.SoftwareAlternateName).Name("alternate_name");
    }
}
