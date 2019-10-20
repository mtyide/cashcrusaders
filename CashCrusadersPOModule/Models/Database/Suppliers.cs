using CashCrusadersPOModule.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// This class defines the order database repository
/// </summary>
public class Suppliers
{
    public static List<GetSuppliers_Result> GetSuppliers()
    {
        using (var ccEntities = new CashCrusadersPOModuleEntities())
        {
            return ccEntities.GetSuppliers().ToList();
        }
    }

    public static int CreateSupplier(Supplier supplier)
    {
        using (var ccEntities = new CashCrusadersPOModuleEntities())
        {
            ccEntities.Suppliers.Add(supplier);
            ccEntities.SaveChanges();
            return supplier.Id;
        }
    }
}