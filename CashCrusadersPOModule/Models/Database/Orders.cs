using CashCrusadersPOModule.Models.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// This class defines the order database repository
/// </summary>
public class Orders
{
    public static List<GetOrders_Result> GetOrders(Supplier supplier)
    {
        using (var ccEntities = new CashCrusadersPOModuleEntities())
        {
            return ccEntities.GetOrders(supplier.Id).ToList();
        }
    }

    public static List<GetPurchaseOrder_Result> GetPurchaseOrder(Order order)
    {
        using (var ccEntities = new CashCrusadersPOModuleEntities())
        {
            return ccEntities.GetPurchaseOrder(order.Id).ToList();
        }
    }

    public static int CreateOrder(Order order)
    {
        using (var ccEntities = new CashCrusadersPOModuleEntities())
        {
            ccEntities.Orders.Add(order);
            ccEntities.SaveChanges();
            return order.Id;
        }
    }

    public static void CreatePurchaseOrder(List<int> items, Order order)
    {
        using (var ccEntities = new CashCrusadersPOModuleEntities())
        {
            foreach (var item in items)
            {
                var purchaseOrder = new PurchaseOrder { OrderID = order.Id, ProductID = item, Created = DateTime.Now };
                ccEntities.PurchaseOrders.Add(purchaseOrder);
                ccEntities.SaveChanges();
            }
        }
    }
}