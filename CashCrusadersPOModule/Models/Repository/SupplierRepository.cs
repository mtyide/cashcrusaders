using CashCrusadersPOModule.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// This class defines the supplier repository
/// </summary>
public class SupplierRepository : IRepository<Supplier>
{
    public static List<Supplier> GetSuppliers()
    {
        var suppliers = Suppliers.GetSuppliers();
        var list = new List<Supplier>();
        foreach (var item in suppliers)
        {
            list.Add(new Supplier
            {
                Name = item.Name,
                Id = item.Id
            });
        }
        return list;
    }

    public int Add(Supplier supplier)
    {
        return Suppliers.CreateSupplier(supplier);
    }
}