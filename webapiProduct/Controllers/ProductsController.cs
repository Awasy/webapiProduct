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

namespace webapiProduct.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
       private ISessionFactory SessionFactory { get; set; }

        public ProductsController(ISessionFactory sessionFactory)
        {
            SessionFactory = sessionFactory;
        }
        // GET: api/Products
        [HttpGet]
        public  IEnumerable<Product> Get()
        {
            
            IEnumerable<Product> products;
            using (var session = SessionFactory.OpenSession())
            {
                products = session.Query<Product>().ToList();

            }

            return products;
        }

        // GET: api/Products/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Product> Get(int id)
        {
            Product product;
            using (var session = SessionFactory.OpenSession())
            {
                product = await session.GetAsync<Product>(id);
            }
            
            return product;
        }

        // POST: api/Products
        [HttpPost]
        public async void Post([FromBody] Product product )
        {
            if (CheckProduct(product))
            {
                using (var session = SessionFactory.OpenSession())
                {
                    using (var tx = session.BeginTransaction())
                    {
                        await session.SaveAsync(product);
                        await tx.CommitAsync();
                    }
                }
            }
     
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async void Put(int id,[FromBody] Product product)
        {
            if (CheckProduct(product))
            {
                using (var session = SessionFactory.OpenSession())
                {
                    using (var tx = session.BeginTransaction())
                    {

                        await session.UpdateAsync(product);
                        await tx.CommitAsync();
                    }
                }
            }


        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async void Delete(int id)
        {
            using (var session = SessionFactory.OpenSession())
            {
                using (var tx = session.BeginTransaction())
                {
                    var product = await session.GetAsync<Product>(id);
                    if (product != null)
                    {
                        await session.DeleteAsync(product);
                        await tx.CommitAsync();
                    }
                 
                }
            } 
        }

        private bool CheckProduct(Product product)
        {
            if(product.Price > 0 && !string.IsNullOrEmpty(product.Name.Trim()) && !string.IsNullOrEmpty(product.Type.Trim()))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
