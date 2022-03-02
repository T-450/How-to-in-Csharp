// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.IO;
using HowToWorkWithXML.Services;

namespace HowToWorkWithXML;

public class Program
{
    private static readonly string currentDir = Directory.GetCurrentDirectory().Substring(0, Directory.GetCurrentDirectory().LastIndexOf("bin"));
    private static readonly string customerFilePath = Path.Combine(currentDir, "Resources", "Customers.xml");
    private static readonly string salesOrderFilePath = Path.Combine(currentDir, "Resources", "SalesOrderHeaders.xml");
    private static readonly string salesOrderDetailsFilePath = Path.Combine(currentDir, "Resources", "SalesOrderDetails.xml");

    static void Main(string[] args)
    {
        var customerXmlService = new CustomerXmlService(customerFilePath, salesOrderFilePath, salesOrderDetailsFilePath);
        var customers = customerXmlService.GetAllCustomers();
        var findCustomer = customerXmlService.Find(xElement => xElement.Element("CustomerID").Value == "1");
        var join = customerXmlService.Join();
        var count = customerXmlService.Count();
        var sum = customerXmlService.Sum();
        var min = customerXmlService.Minimum();
        var max = customerXmlService.Maximum();
        var avg = customerXmlService.Average();
    }
}
