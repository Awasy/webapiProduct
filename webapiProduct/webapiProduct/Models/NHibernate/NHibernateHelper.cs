using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace webapiProduct.Models.NHibernate
{
    public class NHibernateHelper
    {
        public static ISession OpenSession()
        {
            ISessionFactory sessionFactory = Fluently.Configure()
     //Настройки БД. Строка подключения к БД MS Sql Server 2008
     .Database(MsSqlConfiguration.MsSql7.ConnectionString(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=databaseStore;Integrated Security=True;Connect Timeout=30;")
            .ShowSql()
            )
            //Маппинг. Используя AddFromAssemblyOf NHibernate будет пытаться маппить КАЖДЫЙ класс в этой сборке (assembly). Можно выбрать любой класс. 
            .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Product>())
            //SchemeUpdate позволяет создавать/обновлять в БД таблицы и поля (2 поле == true) 
            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
            .BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}
