using System;
using Core;
using NHibernate.ByteCode.Castle;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;

namespace Nh3Hacking {
    internal static class Program {
        private static void Main() {
#if ENABLE_NHPROF
            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();
#endif

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
                    var animal = session.Get<Animal>(dogId);
                    Console.WriteLine(animal);
                    tx.Commit();
                }
            }

            Console.WriteLine("Press <ENTER> to exit...");
            Console.ReadLine();
        }
    }
}