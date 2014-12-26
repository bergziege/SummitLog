using System.Reflection;

using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Conventions.Inspections;

using NHibernate.Cfg;

using Spring.Data.NHibernate;

namespace De.BerndNet2000.SummitLog.Factories {
    /// <summary>
    ///     Session Factory für zur Nutzung der Fluent und/oder HBM Mappings
    /// </summary>
    public class FluentSessionFactory : LocalSessionFactoryObject {
        private string[] _fluentMappingAssemblies;
        /// <summary>
        ///     Auflistung der Assemblies, in denen Mapping Fluent/HBM Mapping Dateien vorkommen.
        /// </summary>
        public string[] FluentMappingAssemblies {
            get { return _fluentMappingAssemblies; }
            set { _fluentMappingAssemblies = value; }
        }

        protected override void PostProcessConfiguration(Configuration config) {
            base.PostProcessConfiguration(config);

            Fluently.Configure(config)
                    .Mappings(m => {
                        foreach (string assemblyName in _fluentMappingAssemblies) {
                            /* HBM Mappings der Konfiguration hinzufügen. */
                            m.HbmMappings
                                    .AddFromAssembly(Assembly.Load(assemblyName));

                            /* Fluent Mappings der Konfiguration hinzufügen. */
                            m.FluentMappings
                                    .AddFromAssembly(Assembly.Load(assemblyName));
                            m.FluentMappings.Conventions.Add(DefaultAccess.CamelCaseField(CamelCasePrefix.Underscore));
                            m.FluentMappings.Conventions.Add(DefaultLazy.Never());
                            m.FluentMappings.Conventions.Add(ForeignKey.EndsWith("_Id"));
                            m.FluentMappings.Conventions.Add(Table.Is(x => "tbl" + x.EntityType.Name));
                        }
                    }).BuildConfiguration();
        }
    }
}