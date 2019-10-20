using CashCrusadersPOModule.Models.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

public class SupplierController : ApiController
{
    [HttpGet]
    /// <summary>
    /// This method returns list of suppliers
    /// </summary>
    /// <returns></returns>
    public HttpResponseMessage GetSuppliers()
    {
        try
        {
            var suppliers = SupplierRepository.GetSuppliers();
            var serialize = JsonConvert.SerializeObject(suppliers);
            var response = Request.CreateResponse<string>(HttpStatusCode.OK, serialize);

            return response;
        }
        catch (Exception x) { var response = Request.CreateResponse<string>(HttpStatusCode.InternalServerError, x.Message); return response; }
    }

    [HttpPost]
    /// <summary>
    /// This method creates a new supplier and returns new ID
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public HttpResponseMessage CreateSupplier([FromBody] SupplierQuery query)
    {
        try
        {
            var supplier = new Supplier { Name = query.Name, Created = DateTime.Now };
            IRepository<Supplier> repository = new SupplierRepository();
            var id = repository.Add(supplier);
            return Request.CreateResponse<int>(HttpStatusCode.OK, id);
        }
        catch { return Request.CreateResponse<int>(HttpStatusCode.InternalServerError, 0); }
    }

    #region Helper Class
    public class SupplierQuery
    {
        public SupplierQuery() { }
        public string Name { get; set; }
    }
    #endregion
}
