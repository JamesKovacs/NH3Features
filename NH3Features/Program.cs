using System;
using System.Reflection;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate.ByteCode.Castle;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;

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
                .AddAssembly(Assembly.GetExecutingAssembly())
                .SessionFactory().GenerateStatistics();

            new SchemaExport(cfg).Execute(script: false, export: true, justDrop: false);

            var sessionFactory = cfg.BuildSessionFactory();

            using(var session = sessionFactory.OpenSession())
            using(var tx = session.BeginTransaction()) {
                // NHibernate code goes here
                tx.Commit();
            }

            Console.WriteLine("Press <ENTER> to exit...");
            Console.ReadLine();
        }
    }
}