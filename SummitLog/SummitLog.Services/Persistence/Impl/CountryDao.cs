using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Neo4jClient;
using SummitLog.Services.Exceptions;
using SummitLog.Services.Model;
using SummitLog.Services.Persistence.Extensions;

namespace SummitLog.Services.Persistence.Impl
{
    /// <summary>
    ///     DAO für Länder
    /// </summary>
    public class CountryDao : BaseDao, ICountryDao
    {
        /// <summary>
        ///     Erstellt eine neue Instanz des DAOs
        /// </summary>
        /// <param name="graphClient"></param>
        public CountryDao(GraphClient graphClient) : base(graphClient)
        {
        }

        /// <summary>
        ///     Liefert alle Länder
        /// </summary>
        /// <returns></returns>
        public IList<Country> GetAll()
        {
            return
                GraphClient.Cypher.Match("".Country("c"))
                    .Return(c => c.As<Country>())
                    .Results.ToList();
        }

        /// <summary>
        ///     Erstellt ein neues Land
        /// </summary>
        /// <param name="country"></param>
        public Country Create(Country country)
        {
            return GraphClient.Cypher.Create("".CountryWithParam()).WithParam("country", country).Return(c => c.As<Country>()).Results.First();
        }

        /// <summary>
        ///     Liefert ob ein Land noch verwendet wird
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public bool IsInUse(Country country)
        {
            if (country == null) throw new ArgumentNullException(nameof(country));
            var counts = GraphClient.Cypher.Match("".Country("c"))
                .OptionalMatch("".Node("c").AnyOutboundRelationAs("usagesInArea").Area())
                .OptionalMatch("".Node("c").AnyOutboundRelationAs("usagesInRoute").Route())
                .Where((Country c) => c.Id == country.Id)
                .Return((usagesInArea, usagesInRoute) => new
                {
                    UsagesInAreaCount = usagesInArea.Count(),
                    UsagesInRoute = usagesInRoute.Count()
                }).Results.First();
            return counts.UsagesInAreaCount > 0 || counts.UsagesInRoute > 0;
        }

        /// <summary>
        ///     Löscht ein Land, wenn dies nicht mehr verwendet wird
        /// </summary>
        /// <param name="country"></param>
        public void Delete(Country country)
        {
            if (country == null) throw new ArgumentNullException(nameof(country));
            if (IsInUse(country))
            {
                throw new NodeInUseException();
            }

            GraphClient.Cypher.Match("".Country("c"))
                .Where((Country c)=>c.Id == country.Id)
                .Delete("c").ExecuteWithoutResults();
        }
    }
}