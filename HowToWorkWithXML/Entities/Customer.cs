// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace HowToWorkWithXML.Entities;

public class Customer
{
    private int _CustomerID;
    private string _Title;
    private string _FirstName;
    private string _MiddleName;
    private string _LastName;
    private string _CompanyName;
    private string _SalesPerson;
    private string _EmailAddress;
    private string _Phone;

    /// <summary>
    /// Get/Set CustomerID
    /// </summary>
    public int CustomerID
    {
        get { return _CustomerID; }
        set
        {
            _CustomerID = value;
        }
    }

    /// <summary>
    /// Get/Set Title
    /// </summary>
    public string Title
    {
        get { return _Title; }
        set
        {
            _Title = value;
        }
    }

    /// <summary>
    /// Get/Set FirstName
    /// </summary>
    public string FirstName
    {
        get { return _FirstName; }
        set
        {
            _FirstName = value;
        }
    }

    /// <summary>
    /// Get/Set MiddleName
    /// </summary>
    public string MiddleName
    {
        get { return _MiddleName; }
        set
        {
            _MiddleName = value;
        }
    }

    /// <summary>
    /// Get/Set LastName
    /// </summary>
    public string LastName
    {
        get { return _LastName; }
        set
        {
            _LastName = value;
        }
    }

    /// <summary>
    /// Get/Set CompanyName
    /// </summary>
    public string CompanyName
    {
        get { return _CompanyName; }
        set
        {
            _CompanyName = value;
        }
    }

    /// <summary>
    /// Get/Set SalesPerson
    /// </summary>
    public string SalesPerson
    {
        get { return _SalesPerson; }
        set
        {
            _SalesPerson = value;
        }
    }

    /// <summary>
    /// Get/Set EmailAddress
    /// </summary>
    public string EmailAddress
    {
        get { return _EmailAddress; }
        set
        {
            _EmailAddress = value;
        }
    }

    /// <summary>
    /// Get/Set Phone
    /// </summary>
    public string Phone
    {
        get { return _Phone; }
        set
        {
            _Phone = value;
        }
    }
}
