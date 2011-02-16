using System;
using System.Linq;
using Core;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;

namespace NH2Comparison {
    internal static class Program {
        private static void Main() {
            NHibernateProfiler.Initialize();

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
// NH2 Criteria works
//                    var animal = session.CreateCriteria<UnmappedAnimal>()
//                                        .Add(Restrictions.IdEq(dogId))
//                                        .UniqueResult<UnmappedAnimal>();
// NH2 LINQ works
//                    var query = from a in session.Linq<UnmappedAnimal>()
//                                where a.Id == dogId
//                                select a;
//                    var animal = query.Single();
// NH2 HQL fails
//                    var animal = session.CreateQuery("from a in UnmappedAnimal where a.id = :id")
//                                        .SetParameter("id", dogId)
//                                        .UniqueResult<UnmappedAnimal>();
// NH2 Get/Load fails
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