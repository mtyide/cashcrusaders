using CashCrusadersPOModule.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// This class defines the product repository
/// </summary>
public class ProductRepository : IRepository<Product>
{
    public static object GetProducts(int supplierId)
    {
        var products = Products.GetProducts(new Supplier { Id = supplierId });
        var list = new List<Product>();
        foreach (var item in products)
        {
            list.Add(new Product
            {
                Code = item.Code,
                Description = item.Description,
                Id = item.Id,
                Price = item.Price
            });
        }
        return list;
    }

    public int Add(Product product)
    {
        return Products.CreateProduct(product);
    }
}