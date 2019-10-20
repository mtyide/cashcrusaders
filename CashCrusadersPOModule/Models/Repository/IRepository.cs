using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// This is a class definition for IRepository
/// </summary>
public interface IRepository<T>
{
    int Add(T type);
}