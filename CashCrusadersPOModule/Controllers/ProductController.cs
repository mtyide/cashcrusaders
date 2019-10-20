using CashCrusadersPOModule.Models.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

public class ProductController : ApiController
{
    [HttpGet]
    /// <summary>
    /// This method returns list of products from suppliers
    /// </summary>
    /// <returns></returns>
    public HttpResponseMessage GetProducts(int supplierId)
    {
        try
        {
            var products = ProductRepository.GetProducts(supplierId);
            var serialize = JsonConvert.SerializeObject(products);
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
    public HttpResponseMessage CreateProduct(Product product)
    {
        try
        {
            product.Created = DateTime.Now;
            IRepository<Product> repository = new ProductRepository();
            var id = repository.Add(product);
            return Request.CreateResponse<int>(HttpStatusCode.OK, id);
        }
        catch { return Request.CreateResponse<int>(HttpStatusCode.InternalServerError, 0); }
    }
}
