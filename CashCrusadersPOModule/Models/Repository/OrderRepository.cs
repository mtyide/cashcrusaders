using CashCrusadersPOModule.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// This class defines the order repository
/// </summary>
public class OrderRepository : IRepository<Order>
{
    public static List<Order> GetOrders(int supplierId)
    {
        var orders = Orders.GetOrders(new Supplier { Id = supplierId });
        var list = new List<Order>();
        foreach (var item in orders)
        {
            list.Add(new Order
            {
                Created = item.Created,
                Total = item.Total,
                VAT = item.VAT,
                Id = item.Id
            });
        }
        return list;
    }

    public static List<Product> GetPurchaseOrder(int orderId)
    {
        var orders = Orders.GetPurchaseOrder(new Order { Id = orderId });
        var list = new List<Product>();
        foreach (var item in orders)
        {
            list.Add(new Product
            {
                Code = item.Code,
                Description = item.Description,
                Price = item.Price,
                Id = item.Id
            });
        }
        return list;
    }

    public int Add(Order order)
    {
        return Orders.CreateOrder(order);
    }
}