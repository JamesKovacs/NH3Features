using System;
using Core;
//using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace NH2Comparison {
    internal static class Program {
        private static void Main() {
//            NHibernateProfiler.Initialize();

            var cfg = new Configuration();
            cfg.Configure();
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