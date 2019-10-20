using CashCrusadersPOModule.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// This class defines the products database repository
/// </summary>
public class Products
{
    public static List<GetProducts_Result> GetProducts(Supplier supplier)
    {
        using (var ccEntities = new CashCrusadersPOModuleEntities())
        {
            return ccEntities.GetProducts(supplier.Id).ToList();
        }
    }

    public static int CreateProduct(Product product)
    {
        using (var ccEntities = new CashCrusadersPOModuleEntities())
        {
            ccEntities.Products.Add(product);
            ccEntities.SaveChanges();
            return product.Id;
        }
    }
}