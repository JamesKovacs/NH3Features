using System;
using System.Linq;
using Core;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate.ByteCode.Castle;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Criterion;
using NHibernate.Dialect;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Transform;

namespace Nh3Hacking {
    internal static class Program {
        private static void Main() {
            NHibernateProfiler.Initialize();

            var cfg = new Configuration();
            cfg.Proxy(p => p.ProxyFactoryFactory<ProxyFactoryFactory>())
                .DataBaseIntegration(db => {
                                         db.ConnectionStringName = "scratch";
                                         db.Dialect<MsSql2008Dialect>();
                                         db.BatchSize = 500;
                                     })
                .AddAssembly(typeof(Entity).Assembly)
                .SessionFactory().GenerateStatistics();

            new SchemaExport(cfg).Execute(script: false, export: true, justDrop: false);

            var sessionFactory = cfg.BuildSessionFactory();

            Guid dogId;
            using(var session = sessionFactory.OpenSession()) {
                using(var tx = session.BeginTransaction()) {
                    var cat = new Cat {Name = "Fluffy"};
                    session.Save(cat);
                    var dog = new Dog {Name = "Rex"};
                    session.Save(dog);
                    tx.Commit();
                    dogId = dog.Id;
                }
            }

            using(var session = sessionFactory.OpenSession()) {
                using(var tx = session.BeginTransaction()) {
// NH3 Criteria works
//                    var animal = session.CreateCriteria<UnmappedAnimal>()
//                                        .Add(Restrictions.IdEq(dogId))
//                                        .UniqueResult<UnmappedAnimal>());
// NH3 LINQ works
//                    var query = from a in session.Query<UnmappedAnimal>()
//                                where a.Id == dogId
//                                select a;
//                    var animal = query.Single();
// NH3 HQL fails
//                    var animal = session.CreateQuery("from a in UnmappedAnimal where a.id = :id")
//                                        .SetParameter("id", dogId)
//                                        .UniqueResult<UnmappedAnimal>();
// NH3 Get/Load works
                    var animal = session.Get<UnmappedAnimal>(dogId);
                    Console.WriteLine(animal);
                    tx.Commit();
                }
            }

            Console.WriteLine("Press <ENTER> to exit...");
            Console.ReadLine();
        }
    }
}