// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using HowToWorkWithXML.Entities;
using HowToWorkWithXML.Extensions;

namespace HowToWorkWithXML.Services;

public class CustomerXmlService
{
    public CustomerXmlService(string xmlFilePath, string salesDocFilePath, string salesOrderDetailsFile)
    {
        DocCust = XElement.Load(xmlFilePath);
        SalesOrderHeaders = XElement.Load(salesDocFilePath);
        SalesOrderDetailsFile = XElement.Load(salesOrderDetailsFile);
    }

    private XElement DocCust { get; }
    private XElement SalesOrderDetailsFile { get; }
    private XElement SalesOrderHeaders { get; }

    #region GetAll Method

    public IEnumerable<Customer> GetAllCustomers()
    {
        var customers = DocCust.Elements("Customer").Select(cust => MapXElementToCustomer(cust));

        return customers;
    }

    #endregion

    #region SingleNode Method

    public IEnumerable<Customer> Find(Func<XElement, bool> filter)
    {
        var customersFiltered = DocCust.Elements("Customer").Where(filter).Select(cust => MapXElementToCustomer(cust));
        if (customersFiltered is null && !customersFiltered.Any())
        {
            return Enumerable.Empty<Customer>();
        }

        return customersFiltered;
    }

    #endregion

    #region Join Method

    /// <summary>
    ///     Join customers and orders, and create a new XML document with a different shape.
    /// </summary>
    public string Join()
    {
        var content = DocCust.Elements("Customer").Join(
                // Inner
                SalesOrderHeaders.Elements("SalesOrderHeader"),
                customer => (string) customer.Element("CustomerID"),
                salesOrders => (string) salesOrders.Element("CustomerID"),
                (customer, sales) => new {Sales = sales, Customer = customer})
            .Where(saledAndCustomers => saledAndCustomers.Customer.Element("CustomerID").Value ==
                                        saledAndCustomers.Sales.Element("CustomerID").Value)
            .Select(saledAndCustomers =>
                {
                    return new XElement("Order",
                        new XElement("CustomerID", (string) saledAndCustomers.Customer.Element("CustomerID")),
                        new XElement("CompanyName", (string) saledAndCustomers.Customer.Element("CompanyName")),
                        new XElement("FirstName", (string) saledAndCustomers.Customer.Element("FirstName")),
                        new XElement("LastName", (string) saledAndCustomers.Customer.Element("LastName")),
                        new XElement("OrderDate", (DateTime) saledAndCustomers.Sales.Element("OrderDate")),
                        new XElement("SalesOrderNumber", (string) saledAndCustomers.Sales.Element("SalesOrderNumber"))
                    );
                }
            );
        var mergedDoc = new XElement("SalesOrderWithCustomerInfo", content);
        return mergedDoc.ToString();
    }

    #endregion

    #region Count Method

    public int Count()
    {
        return SalesOrderDetailsFile.Elements("OrderDetail").Count();
    }

    #endregion

    private static Customer MapXElementToCustomer(XElement customer)
    {
        return new Customer
        {
            CustomerID = customer.Element("CustomerID").GetAs<int>(),
            Title = customer.Element("Title").GetAs<string>(),
            FirstName = customer.Element("FirstName").GetAs<string>(),
            MiddleName = customer.Element("MiddleName").GetAs<string>(),
            LastName = customer.Element("LastName").GetAs<string>(),
            CompanyName = customer.Element("CompanyName").GetAs<string>(),
            SalesPerson = customer.Element("SalesPerson").GetAs<string>(),
            EmailAddress = customer.Element("EmailAddress").GetAs<string>(),
            Phone = customer.Element("Phone").GetAs<string>()
        };
    }

    #region Sum Method

    public decimal Sum()
    {
        return SalesOrderDetailsFile.Elements("OrderDetail").Select(x => x.Element("LineTotal").GetAs<decimal>()).Sum();
    }

    #endregion

    #region Minimum Method

    public decimal Minimum()
    {
        return SalesOrderDetailsFile.Elements("OrderDetail").Select(x => x.Element("LineTotal").GetAs<decimal>()).Min();
    }

    #endregion

    #region Maximum Method

    public decimal Maximum()
    {
        return SalesOrderDetailsFile.Elements("OrderDetail").Select(x => x.Element("LineTotal").GetAs<decimal>()).Max();
    }

    #endregion

    #region Average Method

    public decimal Average()
    {
        return SalesOrderDetailsFile.Elements("OrderDetail").Select(x => x.Element("LineTotal").GetAs<decimal>())
            .Average();
    }

    #endregion
}
