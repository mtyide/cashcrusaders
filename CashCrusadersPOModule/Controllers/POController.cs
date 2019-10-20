using CashCrusadersPOModule.Models.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

public class POController : ApiController
{
    [HttpGet]
    /// <summary>
    /// This method returns list of order for a specific supplier
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public HttpResponseMessage GetOrders(int id)
    {
        try
        {
            var orders = OrderRepository.GetOrders(id);
            var serialize = JsonConvert.SerializeObject(orders);
            var response = Request.CreateResponse<string>(HttpStatusCode.OK, serialize);

            return response;
        }
        catch (Exception x) { var response = Request.CreateResponse<string>(HttpStatusCode.InternalServerError, x.Message); return response; }
    }

    [HttpGet]
    /// <summary>
    /// This method returns list of items for a specific order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public HttpResponseMessage GetPurchaseOrder(int id)
    {
        try
        {
            var items = OrderRepository.GetPurchaseOrder(id);
            var serialize = JsonConvert.SerializeObject(items);
            var response = Request.CreateResponse<string>(HttpStatusCode.OK, serialize);

            return response;
        }
        catch (Exception x) { var response = Request.CreateResponse<string>(HttpStatusCode.InternalServerError, x.Message); return response; }
    }

    [HttpPost]
    /// <summary>
    /// This method creates a new order with items
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public HttpResponseMessage CreateOrder([FromBody] OrderQuery query)
    {
        try
        {
            var order = new Order { SupplierID = query.SupplierID, VAT = query.VAT, Total = query.Total, Created = DateTime.Now };
            IRepository<Order> repository = new OrderRepository();
            var id = repository.Add(order);
            Orders.CreatePurchaseOrder(query.Items, new Order { Id = id });
            return Request.CreateResponse<int>(HttpStatusCode.OK, id);
        }
        catch { return Request.CreateResponse<int>(HttpStatusCode.InternalServerError, 0); }
    }

    #region Helper Class
    public class OrderQuery
    {
        public OrderQuery() { }
        public decimal Total { get; set; }
        public decimal VAT { get; set; }
        public int SupplierID { get; set; }
        public List<int> Items { get; set; }
    }
    #endregion
}
