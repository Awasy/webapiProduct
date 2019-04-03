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
using webapiProduct.Models;
using webapiProduct.Models.NHibernate;

namespace webapiProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        // GET: api/Products
        [HttpGet]
        public  IEnumerable<Product> Get()
        {
            IEnumerable<Product> products;
            using (NHibernate.ISession session = NHibernateHelper.OpenSession())
            {
                 products =  session.Query<Product>().ToList();
            }
            return products;
        }

        // GET: api/Products/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Product> Get(int id)
        {
            Product product;
            using (NHibernate.ISession session = NHibernateHelper.OpenSession())
            {
                product = await session.GetAsync<Product>(id);
            }
            return product;
        }

        // POST: api/Products
        [HttpPost]
        public async void Post([FromBody]Product product )
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    await session.SaveAsync(product);
                    await tx.CommitAsync();
                }

            }
            
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async void Put(int id,[FromBody] Product product)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    await session.UpdateAsync(product);
                    await tx.CommitAsync();
                }

            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    var product = await session.GetAsync<Product>(id);
                    await session.DeleteAsync(product);
                    await tx.CommitAsync();
                }

            }
        }
    }
}
