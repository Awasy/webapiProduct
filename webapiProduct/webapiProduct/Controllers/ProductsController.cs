using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NHibernate;
using webapiProduct.Models;
using ISession = NHibernate.ISession;

namespace webapiProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ISession _session;

        public ProductsController(ISession session)
        {
            _session = session;
        }
        // GET: api/Products
        [HttpGet]
        public  IEnumerable<Product> Get()
        {
            
            IEnumerable<Product> products;
           
            products =  _session.Query<Product>().ToList();
            
            return products;
        }

        // GET: api/Products/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Product> Get(int id)
        {
            Product product;
          
            product = await _session.GetAsync<Product>(id);
            
            return product;
        }

        // POST: api/Products
        [HttpPost]
        public async void Post([FromBody]Product product )
        {
            System.IO.File.WriteAllText("D:\\txt.txt", product.Description + " " + product.Id);
                using (var tx = _session.BeginTransaction())
                {
                    await _session.SaveAsync(product);
                    await tx.CommitAsync();
                }

            
            
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async void Put(int id,[FromBody] Product product)
        {
           
                using (var tx = _session.BeginTransaction())
                {
                    await _session.UpdateAsync(product);
                    await tx.CommitAsync();
                }

            
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            
                using (var tx = _session.BeginTransaction())
                {
                    var product = await _session.GetAsync<Product>(id);
                    await _session.DeleteAsync(product);
                    await tx.CommitAsync();
                }

            
        }
    }
}
