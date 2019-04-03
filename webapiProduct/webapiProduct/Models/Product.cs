using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace webapiProduct.Models
{
    public class Product
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual decimal Price { get; set; }
        public virtual string Type { get; set; }

        
    }

    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Id(p => p.Id);
            Map(p => p.Name);
            Map(p => p.Price);
            Map(p => p.Type);
        }
    }
}
