// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Globalization;
using CsvHelper.Configuration;
using HowToReadAndWriteCSV.Models;

namespace HowToReadAndWriteCSV.Configuration.Mappers;

/// <summary>
///     Class that maps it's properties to CSV header names.
/// </summary>
public class SoftwareMap : ClassMap<Software>
{
    public SoftwareMap()
    {
        AutoMap(CultureInfo.InvariantCulture);

        // Map class property to csv header name
        Map(m => m.SoftwareId).Name("id");
        Map(m => m.OrganizationId).Name("organization_id");
        Map(m => m.SoftwareName).Name("name");
        Map(m => m.SoftwareAlternateName).Name("alternate_name");
    }
}
